using System.Globalization;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TM_Impronet_Interface.Classes;
using TM_Impronet_Interface.Properties;

namespace TM_Impronet_Interface
{
    public partial class Form1 : Form
    {
        bool _bRunning, _bAutomated;
        int _seconds = 30;

        public Form1()
        {
            InitializeComponent();
            txtOutputPath.Text = Settings.Default.SaveFile;
            txtFbDatabasePath.Text = Settings.Default.FirebirdDatabasePath;
            txtSqlConnectionString.Text = Settings.Default.SQLConnectionString;
            txtTmpDatabasePath.Text = Settings.Default.TMPDatabaseLocation;
            txtRunner.Text = Settings.Default.Runner;
            chkRunnerEnabled.Checked = Settings.Default.RunnerEnabled;
            chkEnableTimer.Checked = Settings.Default.TimerEnabled;
            txtInterval.Text = Settings.Default.Interval.ToString(CultureInfo.InvariantCulture);
            chkSynEmployees.Checked = Settings.Default.SyncEnabled;

            if (Settings.Default.UseSql)
                radSql.Checked = true;
            else
            {
                radFirebird.Checked = true;
            }

            if (!chkEnableTimer.Checked) return;
            _seconds = txtInterval.Text == "" ? 300 : Convert.ToInt32(txtInterval.Text);
            tmrSeconds.Start();            
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            TestConnection(GetConnectionObject());
        }

        private DbConnection GetConnectionObject()
        {
            if (radSql.Checked)
                return new SqlConnection(txtSqlConnectionString.Text);
            var connectionString =
                "User=SYSDBA;" +
                "Password=masterkey;" +
                "Database=" + txtFbDatabasePath.Text + ";" +
                "DataSource=localhost;" +
                "Port=3050;" +
                "Dialect=3;" +
                "Charset=NONE;" +
                "Role=;" +
                "Connection lifetime=15;" +
                "Pooling=true;" +
                "Packet Size=8192;" +
                "ServerType=0";
            return new FbConnection(connectionString);
        }

        private DbConnection GetTmpConnectionObject()
        {            
            var connectionString =
                "User=SYSDBA;" +
                "Password=masterkey;" +
                "Database=" + txtTmpDatabasePath.Text + ";" +
                "DataSource=localhost;" +
                "Port=3050;" +
                "Dialect=3;" +
                "Charset=NONE;" +
                "Role=;" +
                "Connection lifetime=15;" +
                "Pooling=true;" +
                "Packet Size=8192;" +
                "ServerType=0";
            return new FbConnection(connectionString);
        }

        private DbCommand GetCommandObject()
        {
            if (radSql.Checked)
                return new SqlCommand();
            return new FbCommand();               
        }

        private static void TestConnection(IDbConnection connection)
        {
            try
            {
                // Set the ServerType to 1 for connect to the embedded server                
                connection.Open();
                MessageBox.Show(@"Connection Successful");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Process(dtFromDate.Value, dtToDate.Value, GetConnectionObject(), GetCommandObject());
        }

        private bool CheckAllFieldsValid()
        {
            if (radSql.Checked)
            {
                if (txtSqlConnectionString.Text == "")
                    return false;
            }

            if (radFirebird.Checked)
            {
                if (txtFbDatabasePath.Text == "")
                    return false;
            }

            if (chkSynEmployees.Checked)
            {
                if (txtTmpDatabasePath.Text == "")
                    return false;
            }

            return true;
        }


        private void Process(DateTime startDate, DateTime endDate, IDbConnection connection, IDbCommand command)
        {
            //Check
            if (!CheckAllFieldsValid())
            {
                MessageBox.Show("Database settings not valid.");
                return;
            }

            //Complete employee migration first
            if (chkSynEmployees.Checked)
                SyncEmployees();

            var unProcessed = 0;
            _bRunning = true;

            //Prepare .dat writer
            //FileStream fs = new FileStream(@txtOutputPath.Text, FileMode.OpenOrCreate);
            //StreamWriter binaryfile = new StreamWriter(fs);
            var iCounter = 0;

            try
            {
                command.Connection = connection;

                connection.Open();
                var transaction = connection.BeginTransaction();
                command.Transaction = transaction; 

                toolStripProgressBar1.Value = 0;
                if (_bAutomated)
                {
                    command.CommandText =
                    "SELECT COUNT(*) FROM TRANSACK " +
                    "WHERE TR_PROCESSED = 0 AND TR_MSTSQ <> 0";
                }
                else
                {
                    if (radIXP400.Checked)
                    {
                        command.CommandText =
                            "SELECT COUNT(*) FROM TRANSACK " +
                            $"WHERE TR_DATE >= @START_DATE AND TR_DATE <= @END_DATE AND TR_TERMSLA LIKE '{txtReaderAddress.Text}%' AND TR_MSTSQ <> 0";
                    }
                    else
                    {
                        command.CommandText =
                            "SELECT COUNT(*) FROM TRANSACK " +
                            $"WHERE TR_DATE >= @START_DATE AND TR_DATE <= @END_DATE AND TR_TERM_SLA LIKE '{txtReaderAddress.Text}%' AND TR_MSTSQ <> 0";
                    }                    
                }

                if (command is FbCommand)
                {
                    ((FbCommand)command).Parameters.Add("@START_DATE", FbDbType.Integer).Value = startDate.ToString("yyyMMdd");
                    ((FbCommand)command).Parameters.Add("@END_DATE", FbDbType.Integer).Value = endDate.ToString("yyyMMdd");
                }
                else if (command is SqlCommand)
                {
                    ((SqlCommand)command).Parameters.Add("@START_DATE", SqlDbType.Int).Value = startDate.ToString("yyyMMdd");
                    ((SqlCommand)command).Parameters.Add("@END_DATE", SqlDbType.Int).Value = endDate.ToString("yyyMMdd");
                }

                var iCount = command.ExecuteScalar();
                toolStripProgressBar1.Maximum = Convert.ToInt32(iCount);
                toolStripStatusLabel1.Text = iCount + @" record(s) found";

                if (_bAutomated)
                {
                    command.CommandText =
                    "SELECT * FROM TRANSACK a JOIN EMPLOYEE b ON a.TR_MSTSQ = b.MST_SQ  " +
                    "WHERE TR_PROCESSED = 0";
                }
                else if (cmbDirection.SelectedIndex != 0 && cmbDirection.SelectedIndex != 1)
                {
                    if (radIXP400.Checked)
                    {
                        command.CommandText = "SELECT * FROM TRANSACK a JOIN EMPLOYEE b ON a.TR_MSTSQ = b.MST_SQ " +
                                                $"WHERE TR_DATE >= @START_DATE AND TR_DATE <= @END_DATE AND TR_TERMSLA LIKE '{txtReaderAddress.Text}%'";
                    }
                    else
                    {
                        command.CommandText = "SELECT * FROM TRANSACK a JOIN MASTER b ON a.TR_MSTSQ = b.MST_SQ " +
                                                $"WHERE TR_DATE >= @START_DATE AND TR_DATE <= @END_DATE AND TR_TERM_SLA LIKE '{txtReaderAddress.Text}%'";
                    }
                }
                else
                {
                    if (radIXP400.Checked)
                    {
                        command.CommandText = "SELECT * FROM TRANSACK a JOIN EMPLOYEE b ON a.TR_MSTSQ = b.MST_SQ " +
                                                $"WHERE TR_DATE >= @START_DATE AND TR_DATE <= @END_DATE AND TR_TERMSLA LIKE '{txtReaderAddress.Text}%' AND TR_DIRECTION LIKE {cmbDirection.SelectedIndex}";
                    }
                    else
                    {
                        command.CommandText = "SELECT * FROM TRANSACK a JOIN MASTER b ON a.TR_MSTSQ = b.MST_SQ " +
                                                $"WHERE TR_DATE >= @START_DATE AND TR_DATE <= @END_DATE AND TR_TERM_SLA LIKE '{txtReaderAddress.Text}%' AND TR_DIRECTION LIKE {cmbDirection.SelectedIndex}";
                    }
                }

                var myReader = command.ExecuteReader();

                //Read Current Mappings
                var full = Settings.Default.Mappings;
                var singleMappings = full.Split(',');

                while (myReader.Read())
                {
                    iCounter++;
                    //increment toolbar
                    toolStripProgressBar1.Increment(1);

                    //standard data                    
                    string employeeNo;
                    if (radIXP400.Checked)
                    {
                        employeeNo = radMstsq.Checked
                            ? myReader["TR_MSTSQ"].ToString().PadRight(8, ' ')
                            : myReader["EMP_EMPLOYEENO"].ToString().PadRight(8, ' ');
                    }
                    else
                    {
                        employeeNo = radMstsq.Checked
                            ? myReader["TR_MSTSQ"].ToString().PadRight(8, ' ')
                            : myReader["MST_EMP"].ToString().PadRight(8, ' ');
                    }
                    var tDate = myReader["TR_DATE"].ToString();
                    //Build Date
                    var date = $"{tDate[6]}{tDate[7]}/{tDate[4]}{tDate[5]}/{tDate[0]}{tDate[1]}{tDate[2]}{tDate[3]}";

                    var tTime = myReader["TR_TIME"].ToString();
                    //Build Time
                    var time = "";
                    if (tTime.Length == 3)
                        time = $"00:0{tTime[0]}";
                    switch (tTime.Length)
                    {
                        case 4:
                            time = $"00:{tTime[0]}{tTime[1]}";
                            break;
                        case 5:
                            time = $"0{tTime[0]}:{tTime[1]}{tTime[2]}";
                            break;
                        case 6:
                            time = $"{tTime[0]}{tTime[1]}:{tTime[2]}{tTime[3]}";
                            break;
                    }

                    var direction = myReader["TR_DIRECTION"].ToString();
                    switch (direction)
                    {
                        case "0":
                            direction = "O";
                            break;
                        case "1":
                            direction = "I";
                            break;
                        default:
                            direction = "I";
                            break;
                    }


                    //Compare mappings
                    var sFound = radIXP400.Checked
                        ? singleMappings.FirstOrDefault(i => i.Contains(myReader["TR_TERMSLA"].ToString()))
                        : singleMappings.FirstOrDefault(i => i.Contains(myReader["TR_TERM_SLA"].ToString()));
                    if (sFound == null)
                    {
                        unProcessed++;
                        Application.DoEvents();
                        continue;
                    }
                    if (sFound != "")
                    {
                        var readerAddress = sFound.Split(';')[1];
                        var line = $"{employeeNo} {date} {time} {direction} {readerAddress}{Environment.NewLine}";
                        using (var sw = new StreamWriter(txtOutputPath.Text, true))
                        {
                            sw.Write(line);
                        }
                    }

                    Application.DoEvents();
                }

                connection.Close();

                if (_bAutomated)
                {
                    UpdateProcessed(connection, command);
                }

                toolStripStatusLabel1.Text =
                    $"{iCount} record(s) processed and {unProcessed} record(s) skipped due to no mappings";

                command.Dispose();
                connection.Close();

                if (!_bAutomated)
                    MessageBox.Show(@"Operation completed successfully");

                RunExe();

                _bRunning = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + @" Row: " + iCounter);
            }
        }

        private static List<Department> GetDepartments(IDbConnection connection, IDbCommand command)
        {
            try
            {
                command.Connection = connection;

                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText = "SELECT * FROM DEPARTMENT";
                var reader = command.ExecuteReader();

                var departments = new List<Department>();
                while (reader.Read())
                {
                    var department = new Department
                    {
                        Id = Convert.ToInt32(reader["DEPT_No"]),
                        Name = reader["DEPT_Name"].ToString(),
                        SiteSla = reader["SITE_SLA"].ToString()
                    };
                    departments.Add(department);
                }
                return departments;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }            
        }
        private static List<Department> GetDepartments(IDbConnection connection, IDbCommand command, string departmentList)
        {
            try
            {
                command.Connection = connection;

                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText = $"SELECT * FROM DEPARTMENT WHERE DEPT_No IN ({departmentList})";
                var reader = command.ExecuteReader();

                var departments = new List<Department>();
                while (reader.Read())
                {
                    var department = new Department
                    {
                        Id = Convert.ToInt32(reader["DEPT_No"]),
                        Name = reader["DEPT_Name"].ToString(),
                        SiteSla = reader["SITE_SLA"].ToString()
                    };
                    departments.Add(department);
                }
                return departments;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        private static List<Company> GetImproCompanies(IDbConnection connection, IDbCommand command)
        {
            try
            {
                command.Connection = connection;

                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText = $"SELECT DISTINCT EMP_Employer FROM EMPLOYEE WHERE EMP_Employer <> ''";
                var reader = command.ExecuteReader();

                var companies = new List<Company>();
                while (reader.Read())
                {
                    var company = new Company
                    {
                        Code = reader["EMP_Employer"].ToString()
                    };
                    companies.Add(company);
                }
                return companies;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private static IEnumerable<Employee> GetImproEmployees(IDbConnection connection, IDbCommand command, string departments)
        {
            try
            {                        
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;                

                command.CommandText = "SELECT * FROM MASTER a JOIN EMPLOYEE b ON a.MST_SQ = b.MST_SQ " +
                                      "WHERE b.DEPT_No IN (" + departments + ")";
                var reader = command.ExecuteReader();

                var employees = new List<Employee>();
                while (reader.Read())
                {
                    var employee = new Employee
                    {
                        CardNumber = reader["MST_SQ"].ToString(),
                        Name = reader["MST_FirstName"].ToString(),
                        LastName = reader["MST_LastName"].ToString(),
                        Employer = reader["EMP_Employer"].ToString(),
                        DepartmentNo = Convert.ToInt32(reader["DEPT_No"]),
                        EmployeeeNo = reader["EMP_EmployeeNo"].ToString(),
                        IdNumber = reader["MST_ID"].ToString()
                    };
                    if (employee.EmployeeeNo.Length > 10)
                        employee.EmployeeeNo = employee.EmployeeeNo.Substring(0, 10);
                    employees.Add(employee);
                }
                return employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private static IEnumerable<Employee> GetTmpEmployees(IDbConnection connection, IDbCommand command)
        {
            try
            {                
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText = "SELECT a.EMP_NO, b.BUTTON_NUMBER " +
                                      "FROM EMP a " +
                                      "LEFT OUTER JOIN BUTTON b ON a.EMP_NO = b.BUTTON_HLDR";                                      
                var reader = command.ExecuteReader();

                var employees = new List<Employee>();
                while (reader.Read())
                {
                    var employee = new Employee
                    {                        
                        EmployeeeNo = reader["EMP_NO"].ToString(),
                        CardNumber = reader["BUTTON_NUMBER"].ToString()
                    };
                    employees.Add(employee);
                }
                return employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private static IEnumerable<Department> GetTmpDepartments(IDbConnection connection, IDbCommand command)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText = "SELECT a.CODE, a.DESCRIPTION FROM DEPARTMENTS a ";
                var reader = command.ExecuteReader();

                var departments = new List<Department>();
                while (reader.Read())
                {
                    var department = new Department
                    {
                        Id = Convert.ToInt32(reader["CODE"]),
                        Name = reader["DESCRIPTION"].ToString(),
                    };
                    departments.Add(department);
                }
                return departments;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private static IEnumerable<Company> GetTmpCompanies(IDbConnection connection, IDbCommand command)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText = "SELECT a.CODE FROM COMPANIES a ";
                var reader = command.ExecuteReader();

                var companies = new List<Company>();
                while (reader.Read())
                {
                    var company = new Company
                    {
                        Code = reader["CODE"].ToString()
                    };
                    companies.Add(company);
                }
                return companies;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private static void CreateTmpDepartment(IDbConnection connection, IDbCommand command, Department department)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText = "INSERT INTO DEPARTMENTS (CODE, DESCRIPTION) VALUES (@CODE, @DESCRIPTION)";
                ((FbCommand)command).Parameters.Add("@CODE", FbDbType.VarChar).Value = department.Id.ToString();
                ((FbCommand) command).Parameters.Add("@DESCRIPTION", FbDbType.VarChar).Value = department.Name.Length > 20
                    ? department.Name.Substring(0, 20)
                    : department.Name;
                command.ExecuteNonQuery();                              
                transaction.Commit();

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }

        private static void CreateTmpCompany(IDbConnection connection, IDbCommand command, Company company)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText = "INSERT INTO COMPANIES (CODE, DESCRIPTION) VALUES (@CODE, @DESCRIPTION)";
                ((FbCommand)command).Parameters.Add("@CODE", FbDbType.VarChar).Value = company.Code;
                ((FbCommand) command).Parameters.Add("@DESCRIPTION", FbDbType.VarChar).Value = company.Description;
                command.ExecuteNonQuery();
                transaction.Commit();

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private static void CreateTmpEmployee(IDbConnection connection, IDbCommand command, Employee employee)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText = "INSERT INTO EMP " +
                                      "(EMP_NO, ID_NUMBER, NAME, SURNAME, OCCUPATION, COST_CENTRE, GRADE, WORKPATTERN, " +
                                      "DISCHARGED, EMPLOYMENT_DATE, DISCHARGE_DATE, HOME_ADD, POST_ADD, TEL_NO, NOK, " +
                                      "EMERGENCY_CONTACT, POSITION_APPLIED_FOR, BIRTHDATE, MARITAL_STATUS, SEX, LANGUAGES, " +
                                      "NATIONALITY, SA_RESIDENT, PREV_EMPLOYED, PREV_APPLICATIONS, BLACKLISTED, HOURLYRATE, " +
                                      "EARNINGS_BONUS, EARNINGS_ALLOWANCE, EARNINGS_IOD, EARNINGS_OTHER1, EARNINGS_OTHER2, " +
                                      "EARNINGS_OTHER3, EARNINGS_OTHER4, DEDUCT_UIF, DEDUCT_TAX, DEDUCT_IC, DEDUCT_UNION, " +
                                      "DEDUCT_LOAN, DEDUCT_OTHER1, DEDUCT_OTHER2, DEDUCT_OTHER3, DEDUCT_OTHER4, ABSENTFROM, " +
                                      "ABSENTTO, HOLIDAYCALENDAR, ISACTIVE, DEPARTMENT, COMPANY, INVOICERATENT, INVOICERATEOT1, " +
                                      "INVOICERATEOT2, INVOICERATEOT3, INVOICERATEOT4, ROSTER, ROSTERED, HRINFO, " +
                                      "SCHEDULEDWORKPATTERNS, REPORTFILTER) " +
                                      "" +
                                      "VALUES " +
                                      "(@EMPNO, @IDNUMBER, @NAME, @SURNAME, '01', '01', '01', '7-17', " +
                                      "'N', @EMPLOYMENTDATE, @DISCHARGEDATE, '','','',''," +
                                      "'','', @BIRTHDATE,'','','', " +
                                      "'','','','','', 0.000000, " +
                                      "0.000000, 0.000000, 0.000000, 0.000000, 0.000000, " +
                                      "0.000000, 0.000000, 0.000000, 0.000000, 0.000000, 0.000000, " +
                                      "0.000000, 0.000000, 0.000000, 0.000000, 0.000000, @ABSENTFROM, " +
                                      "@ABSENTTO, '01', '', @DEPARTMENT, @COMPANY, 0.000000, 0.000000, " +
                                      "0.000000, 0.000000, 0.000000, '', 'F', '-1,-1,-1,-1,-1,-1,', " +
                                      "'F', 'Y')";
                ((FbCommand) command).Parameters.Add("@EMPNO", FbDbType.VarChar).Value = employee.EmployeeeNo.Length > 10
                    ? employee.EmployeeeNo.Substring(0, 10)
                    : employee.EmployeeeNo;
                ((FbCommand)command).Parameters.Add("@IDNUMBER", FbDbType.VarChar).Value = employee.IdNumber.Length > 13
                    ? employee.IdNumber.Substring(0, 13)
                    : employee.IdNumber;
                ((FbCommand)command).Parameters.Add("@NAME", FbDbType.VarChar).Value = employee.Name.Length > 30
                    ? employee.Name.Substring(0, 30)
                    : employee.Name;
                ((FbCommand)command).Parameters.Add("@SURNAME", FbDbType.VarChar).Value = employee.LastName.Length > 30
                    ? employee.LastName.Substring(0, 30)
                    : employee.LastName;
                ((FbCommand)command).Parameters.Add("@EMPLOYMENTDATE", FbDbType.TimeStamp).Value = DateTime.Now;
                ((FbCommand)command).Parameters.Add("@DISCHARGEDATE", FbDbType.TimeStamp).Value = DateTime.Now;
                ((FbCommand)command).Parameters.Add("@BIRTHDATE", FbDbType.TimeStamp).Value = DateTime.Now;
                ((FbCommand)command).Parameters.Add("@BIRTHDATE", FbDbType.TimeStamp).Value = DateTime.Now;
                ((FbCommand)command).Parameters.Add("@ABSENTFROM", FbDbType.TimeStamp).Value = DateTime.MinValue;
                ((FbCommand)command).Parameters.Add("@ABSENTTO", FbDbType.TimeStamp).Value = DateTime.MinValue;
                ((FbCommand)command).Parameters.Add("@DEPARTMENT", FbDbType.VarChar).Value = employee.DepartmentNo;
                ((FbCommand)command).Parameters.Add("@COMPANY", FbDbType.VarChar).Value = employee.Employer;
                command.ExecuteNonQuery();
                transaction.Commit();

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private static void CreateTmpCardNumber(IDbConnection connection, IDbCommand command, Employee employee)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText = "INSERT INTO BUTTON " +
                                      "(BUTTON_NUMBER, BUTTON_HLDR, EXPIRY_DATE) " +
                                      "" +
                                      "VALUES " +
                                      "(@BUTTONNO, @BUTTONHLDR, @EXPIRY)";
                ((FbCommand)command).Parameters.Add("@BUTTONNO", FbDbType.VarChar).Value = employee.CardNumber.Length > 10
                    ? employee.CardNumber.Substring(0, 10)
                    : employee.CardNumber;
                ((FbCommand)command).Parameters.Add("@BUTTONHLDR", FbDbType.VarChar).Value = employee.EmployeeeNo.Length > 10
                    ? employee.EmployeeeNo.Substring(0, 10)
                    : employee.EmployeeeNo;
                ((FbCommand) command).Parameters.Add("@EXPIRY", FbDbType.Date).Value = DateTime.MaxValue;
                
                command.ExecuteNonQuery();
                transaction.Commit();

                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void SyncEmployees()
        {
            if (!chkSynEmployees.Checked)
                return;

            //Save and get departments
            SaveDepartments();
            var deptObj = Settings.Default.SelectedDepartments;
            if (deptObj == null)            
                return;
            
            var departments = deptObj.Cast<string>().Aggregate("", (current, dept) => current + $"'{dept}',");
            departments = departments.TrimEnd(',');

            var improDepartments = GetDepartments(GetConnectionObject(), GetCommandObject(), departments);
            if (improDepartments==null)
                return;
            var tmpDepartments = GetTmpDepartments(GetTmpConnectionObject(), new FbCommand());
            if (tmpDepartments==null)
                return;
            var improCompanies = GetImproCompanies(GetConnectionObject(), GetCommandObject());
            var tmpCompanies = GetTmpCompanies(GetTmpConnectionObject(), new FbCommand());

            //Determine if Companies already exist
            foreach (var comp in improCompanies.Where(comp => tmpCompanies.All(i => i.Code != comp.Code)))
            {
                //Create new employee in TMP
                CreateTmpCompany(GetTmpConnectionObject(), new FbCommand(), new Company { Code = comp.Code, Description = "DEFAULT" });
            }

            //Determine if Departments already exist
            foreach (var dept in improDepartments.Where(dept => tmpDepartments.All(i => i.Id != dept.Id)))
            {
                //Create new employee in TMP
                CreateTmpDepartment(GetTmpConnectionObject(), new FbCommand(), new Department {Id = dept.Id, Name = dept.Name});
            }

            var improEmployees = GetImproEmployees(GetConnectionObject(), GetCommandObject(), departments);
            if (improEmployees== null)
                return;
            var tmpEmployees = GetTmpEmployees(GetTmpConnectionObject(), new FbCommand());
            if (tmpEmployees == null)
                return;

            
            //Determine if Employees already exist
            var enumerable = improEmployees as Employee[] ?? improEmployees.ToArray();
            foreach (var empl in enumerable.Where(empl => tmpEmployees.All(i => i.EmployeeeNo != empl.EmployeeeNo)))
            {
                //Create new employee in TMP
                CreateTmpEmployee(GetTmpConnectionObject(), new FbCommand(), empl);
            }

            //Determine if Employee card number already exist
            foreach (var empl in enumerable.Where(empl => tmpEmployees.All(i => i.CardNumber != empl.CardNumber)))
            {
                //Create new employee in TMP
                CreateTmpCardNumber(GetTmpConnectionObject(), new FbCommand(), empl);
            }
        }

        private void UpdateProcessed(IDbConnection connection, IDbCommand command)
        {
            connection.Open();

            var myTransaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = myTransaction;
            command.CommandText = "UPDATE TRANSACK SET TR_PROCESSED = 1 WHERE TR_PROCESSED = 0";
            command.ExecuteNonQuery();
            myTransaction.Commit();
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadRefreshMappings(GetConnectionObject(), GetCommandObject());
        }

        private void LoadRefreshMappings(IDbConnection connection, IDbCommand command)
        {
            try
            {
                //Read Current Mappings
                var full = Settings.Default.Mappings;
                var singleMappings = full.Split(',');

                var dsTerminalMapping = new DataSet();
                dsTerminalMapping.Tables.Add();
                dsTerminalMapping.Tables[0].Columns.Add("TERM_SLA");
                dsTerminalMapping.Tables[0].Columns.Add("TERM_NAME");
                dsTerminalMapping.Tables[0].Columns.Add("MAPPING");

                connection.Open();

                var myTransaction = connection.BeginTransaction();

                command.Connection = connection;
                command.Transaction = myTransaction;

                command.CommandText = radIXP400.Checked
                    ? "SELECT TERM_SLA, TERM_NAME FROM TERMINAL"
                    : "SELECT T_ADDR, T_NAME FROM TERMINAL";

                var myReader = command.ExecuteReader();
                while (myReader.Read())
                {
                    var sFound = radIXP400.Checked
                        ? singleMappings.FirstOrDefault(i => i.Contains(myReader["TERM_SLA"].ToString()))
                        : singleMappings.FirstOrDefault(i => i.Contains(myReader["T_ADDR"].ToString()));
                    if (sFound == null)
                    {
                        if (radIXP400.Checked)
                        {
                            dsTerminalMapping.Tables[0].Rows.Add(myReader["TERM_SLA"], myReader["TERM_NAME"], "");
                        }
                        else
                        {
                            dsTerminalMapping.Tables[0].Rows.Add(myReader["T_ADDR"], myReader["T_NAME"], "");
                        }
                        continue;
                    }
                    if (sFound != "")
                    {
                        if (radIXP400.Checked)
                        {
                            dsTerminalMapping.Tables[0].Rows.Add(myReader["TERM_SLA"], myReader["TERM_NAME"],
                                sFound.Split(';')[1]);
                        }
                        else
                        {
                            dsTerminalMapping.Tables[0].Rows.Add(myReader["T_ADDR"], myReader["T_NAME"],
                                sFound.Split(';')[1]);
                        }
                    }

                    Application.DoEvents();
                }

                gridMappings.DataSource = dsTerminalMapping.Tables[0];

                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var toSave = new DataSet();
            toSave.Tables.Add(((DataTable)gridMappings.DataSource).Copy());

            var lstMappings = (from DataRow dr in toSave.Tables[0].Rows
                where dr["MAPPING"].ToString() != ""
                select $"{dr["TERM_SLA"]};{dr["MAPPING"]},").ToList();

            var sToSave = lstMappings.Aggregate("", (current, s) => current + s);

            Settings.Default.Mappings = sToSave;
            Settings.Default.Save();

            MessageBox.Show(@"Saved!");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRefreshMappings(GetConnectionObject(), GetCommandObject());
        }

        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            var openDlg = new OpenFileDialog
            {
                FileName = "IMPRONET.fdb",
                Filter = @"dat Files (*.fdb)|*.fdb|All files (*.*)|*.*",
                AddExtension = true
            };
            if (openDlg.ShowDialog() != DialogResult.OK)
                return;

            txtFbDatabasePath.Text = openDlg.FileName;
            Settings.Default.FirebirdDatabasePath = txtFbDatabasePath.Text;
            Settings.Default.Save();
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            var saveDlg = new SaveFileDialog
            {
                FileName = "Clocks.dat",
                Filter = @"dat Files (*.dat)|*.dat|All files (*.*)|*.*",
                AddExtension = true
            };
            if (saveDlg.ShowDialog() != DialogResult.OK)
                return;

            txtOutputPath.Text = saveDlg.FileName;
        }

        private void txtOutputPath_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.SaveFile = txtOutputPath.Text;
            Settings.Default.Save();
        }

        private void txtDatabasePath_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.FirebirdDatabasePath = txtFbDatabasePath.Text;
            Settings.Default.Save();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var openDlg = new OpenFileDialog
            {
                Filter = @"exe Files (*.exe)|*.exe|All files (*.*)|*.*",
                AddExtension = true
            };
            if (openDlg.ShowDialog() != DialogResult.OK)
                return;

            txtRunner.Text = openDlg.FileName;
        }

        private void txtRunner_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.Runner = txtRunner.Text;
            Settings.Default.Save();
        }

        private void RunExe()
        {
            if (!chkRunnerEnabled.Checked) return;
            if (txtRunner.Text != "")
            {                    
                System.Diagnostics.Process.Start(txtRunner.Text);
            }
        }
       
        private void chkRunnerEnabled_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.RunnerEnabled = chkRunnerEnabled.Checked;
            Settings.Default.Save();
        }

        private void chkEnableTimer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnableTimer.Checked)
            {
                _seconds = Convert.ToInt32(txtInterval.Text);
                tmrSeconds.Start();
            }
            else
                tmrSeconds.Stop();

            Settings.Default.TimerEnabled = chkEnableTimer.Checked;
            Settings.Default.Save();
        }

        private void txtReaderAddress_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtInterval_TextChanged(object sender, EventArgs e)
        {
            if (txtInterval.Text == "") return;
            Settings.Default.Interval = Convert.ToInt32(txtInterval.Text);
            Settings.Default.Save();
        }

        private void tmrMain_Tick(object sender, EventArgs e)
        {

        }

        private void tmrSeconds_Tick(object sender, EventArgs e)
        {
            lblCountdown.Text = $"{_seconds} second(s)";
            if (_seconds == 0 && !_bRunning)
            {
                tmrSeconds.Stop();
                _bAutomated = true;
                Process(GetLowestDT(DateTime.Now), GetHighestDT(DateTime.Now), GetConnectionObject(), GetCommandObject());
                _seconds = Convert.ToInt32(txtInterval.Text);
                _bAutomated = false;
                tmrSeconds.Start();
            }
            else if (_seconds < 0)
                _seconds = Convert.ToInt32(txtInterval.Text);
            else
                _seconds--;
        }

        private DateTime GetLowestDT(DateTime dateTimeCurrent)
        {
            return new DateTime(
                dateTimeCurrent.Year,
                dateTimeCurrent.Month,
                dateTimeCurrent.Day,
                0,
                0,
                0);
        }

        private void radFirebird_CheckedChanged(object sender, EventArgs e)
        {
            if (radFirebird.Checked)
            {
                txtFbDatabasePath.Enabled = true;
                btnBrowseFbDatabase.Enabled = true;
                Settings.Default.UseSql = false;
                Settings.Default.Save();
            }
            else
            {
                txtFbDatabasePath.Enabled = false;
                btnBrowseFbDatabase.Enabled = false;
            }
        }

        private void radSql_CheckedChanged(object sender, EventArgs e)
        {
            txtSqlConnectionString.Enabled = radSql.Checked;

            if (!radSql.Checked) return;

            Settings.Default.UseSql = true;
            Settings.Default.Save();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var openDlg = new OpenFileDialog
            {
                FileName = "TIMEMANAGERPLATINUM.GDB",
                Filter = @"dat Files (*.gdb)|*.gdb|All files (*.*)|*.*",
                AddExtension = true
            };
            if (openDlg.ShowDialog() != DialogResult.OK)
                return;

            txtTmpDatabasePath.Text = openDlg.FileName;
            Settings.Default.TMPDatabaseLocation = txtTmpDatabasePath.Text;
            Settings.Default.Save();
        }

        private void btnRefreshDepartments_Click(object sender, EventArgs e)
        {
            var departments = GetDepartments(GetConnectionObject(), GetCommandObject());

            if (departments == null)
                return;

            var selectedItems = Settings.Default.SelectedDepartments ?? new StringCollection();
            foreach (var department in departments)
            {
                chkDepartments.Items.Add(department,
                    selectedItems.Contains(department.Id.ToString()) ? CheckState.Checked : CheckState.Unchecked);
            }
        }

        private void SaveDepartments()
        {
            var items = chkDepartments.CheckedItems;
            if (items.Count == 0)
                return;

            var collection = new StringCollection();
            foreach (var dept in from object item in items select ((Department)item).Id.ToString())
            {
                collection.Add(dept);
            }
            Settings.Default.SelectedDepartments = collection;
            Settings.Default.Save();
        }

        private void chkSynEmployees_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.SyncEnabled = chkSynEmployees.Checked;
            Settings.Default.Save();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TestConnection(GetTmpConnectionObject());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtSqlConnectionString_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.SQLConnectionString = txtSqlConnectionString.Text;
            Settings.Default.Save();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < chkDepartments.Items.Count; i++)
            {
                chkDepartments.SetItemChecked(i, true);
            }
        }

        private void btnDeselctAll_Click(object sender, EventArgs e)
        {
            chkDepartments.ClearSelected();
        }

        private DateTime GetHighestDT(DateTime dateTimeCurrent)
        {
            return new DateTime(
                dateTimeCurrent.Year,
                dateTimeCurrent.Month,
                dateTimeCurrent.Day,
                23,
                59,
                59);
        }
    }
}
