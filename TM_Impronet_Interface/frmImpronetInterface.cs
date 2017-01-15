using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using FirebirdSql.Data.FirebirdClient;
using TM_Impronet_Interface.Classes;
using TM_Impronet_Interface.Properties;

namespace TM_Impronet_Interface
{
    public partial class frmImpronetInterface : Form
    {
        private bool _bRunning, _bSyncRunning, _bAutomated;
        private int _seconds = -1;
        private int _syncSeconds = -1;

        public frmImpronetInterface()
        {
            InitializeComponent();

            tabctrlMappings.Appearance = TabAppearance.FlatButtons;
            tabctrlMappings.ItemSize = new Size(0, 1);
            tabctrlMappings.SizeMode = TabSizeMode.Fixed;
            treeDepMappings.CheckBoxes = true;

            txtOutputPath.Text = Settings.Default.SaveFile;
            txtFbDatabasePath.Text = Settings.Default.FirebirdDatabasePath;
            txtSqlConnectionString.Text = Settings.Default.SQLConnectionString;
            txtTmpDatabasePath.Text = Settings.Default.TMPDatabaseLocation;
            txtRunner.Text = Settings.Default.Runner;
            chkRunnerEnabled.Checked = Settings.Default.RunnerEnabled;
            chkEnableTimer.Checked = Settings.Default.TimerEnabled;
            chkSyncTimerEnabled.Checked = Settings.Default.SyncTimerEnabled;
            txtInterval.Text = Settings.Default.Interval.ToString();
            chkSynEmployees.Checked = Settings.Default.SyncEnabled;
            txtDepMappingAccessControlCode.Text = Settings.Default.DepAccessControlCode;
            txtDepMappingTimeAndAttendanceCode.Text = Settings.Default.DepTimeAndAttendanceCode;
            chkSyncAccessControlDevices.Checked = Settings.Default.SyncAccessControl;
            txtSyncInterval.Text = Settings.Default.SyncInterval.ToString();
            chkEnableEmail.Checked = Settings.Default.EnableEmail;
            txtSmtpHost.Text = Settings.Default.SmtpHost;
            txtUsername.Text = Settings.Default.Username;
            txtEmailAddress.Text = Settings.Default.EmailAddress;
            txtSmtpPort.Text = Settings.Default.SmtpPort.ToString();
            txtPassword.Text = Settings.Default.Password;
            chkSSL.Checked = Settings.Default.SSL;
            txtToEmailAddress.Text = Settings.Default.ToEmailAddress;
            chkAlwaysSync.Checked = Settings.Default.SyncAllDepartments;
            chkSyncMstsq.Checked = Settings.Default.UseMSTSQ;
            chkChangeEmployeeNumber.Checked = Settings.Default.UpdateEmployeeNumber;

            if (Settings.Default.MappingConfig == 0)
                radUseStandardMapping.Checked = true;
            else
                radUseDepartmentMapping.Checked = true;

            if (Settings.Default.UseSql)
                radSql.Checked = true;
            else
            {
                radFirebird.Checked = true;
            }

            if (chkEnableTimer.Checked)
            {
                _seconds = txtInterval.Text == "" ? 300 : Convert.ToInt32(txtInterval.Text);
                tmrSeconds.Start();
            }

            if (chkSyncTimerEnabled.Checked)
            {
                _syncSeconds = txtSyncInterval.Text == "" ? 400 : Convert.ToInt32(txtSyncInterval.Text);
                tmrMain.Start();
            }

            radDuplicateMonths.Checked = Settings.Default.DuplicateMonths;
            radDuplicateWeeks.Checked = !Settings.Default.DuplicateMonths;
        }

        private void Send_Email(string to, string subject, string html)
        {
            var mailMessage = new MailMessage(txtEmailAddress.Text, to) {Subject = subject, Body = html};

            var mailSender = new SmtpClient(txtSmtpHost.Text, Convert.ToInt32(txtSmtpPort.Text))
            {
                EnableSsl = chkSSL.Checked,
                Credentials = new NetworkCredential(txtUsername.Text, txtPassword.Text)
            };
            //specify your login/password to log on to the SMTP server, if required
            mailSender.Send(mailMessage);
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
                "Database=" + Settings.Default.FirebirdDatabasePath + ";" +
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
                "Database=" + Settings.Default.TMPDatabaseLocation + ";" +
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

        private DbCommand GetTmpCommandObject()
        {
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
            if (!_bRunning)
                Process(dtFromDate.Value, dtToDate.Value, GetConnectionObject(), GetCommandObject());
            else
            {
                MessageBox.Show(@"Export Already Running");
            }
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
                MessageBox.Show(@"Database settings not valid.");
                return;
            }

            ////Complete employee migration first
            //if (chkSynEmployees.Checked)
            //    SyncEmployees();

            var unProcessed = 0;
            _bRunning = true;

            //Prepare .dat writer
            //FileStream fs = new FileStream(@txtOutputPath.Text, FileMode.OpenOrCreate);
            //StreamWriter binaryfile = new StreamWriter(fs);
            var iCounter = 0;
            //Create a list for all of the rows for in memory processing
            var fileOutput = new List<string>();
            var iCount = 0;

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

                var fbCommand = command as FbCommand;
                if (fbCommand != null)
                {
                    fbCommand.Parameters.Add("@START_DATE", FbDbType.Integer).Value = startDate.ToString("yyyMMdd");
                    fbCommand.Parameters.Add("@END_DATE", FbDbType.Integer).Value = endDate.ToString("yyyMMdd");
                }
                else if (command is SqlCommand)
                {
                    ((SqlCommand) command).Parameters.Add("@START_DATE", SqlDbType.Int).Value =
                        startDate.ToString("yyyMMdd");
                    ((SqlCommand) command).Parameters.Add("@END_DATE", SqlDbType.Int).Value = endDate.ToString("yyyMMdd");
                }

                iCount = Convert.ToInt32(command.ExecuteScalar());
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

                command.CommandTimeout = 0;
                var myReader = command.ExecuteReader();


                //Read Current Mappings
                var full = Settings.Default.Mappings;
                var singleMappings = full.Split(',');
                List<DepartmentMapping> departmentMappings = new List<DepartmentMapping>();
                if (Settings.Default.MappingConfig == 1)
                {
                    departmentMappings =
                        DeSerializeObject<DepartmentMappings>("DepartmentMappings.mxl").DepartmentMappingses;
                }



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
                    var departmentId = myReader["DEPT_No"].ToString();
                    string sFound;
                    if (Settings.Default.MappingConfig == 0)
                    {
                        sFound = radIXP400.Checked
                            ? singleMappings.FirstOrDefault(i => i.Contains(myReader["TR_TERMSLA"].ToString()))
                            : singleMappings.FirstOrDefault(i => i.Contains(myReader["TR_TERM_SLA"].ToString()));
                        if (sFound == null)
                        {
                            unProcessed++;
                            Application.DoEvents();
                            continue;
                        }
                        sFound = sFound.Split(';')[1];
                    }
                    else
                    {
                        var departmentMapping = departmentMappings.FindAll(i => i.Department.Id == departmentId);
                        if (departmentMapping.Count > 0)
                        {
                            var terminals = radIXP400.Checked
                                ? departmentMapping[0].Terminals.FindAll(
                                    i => i.Id.Contains(myReader["TR_TERMSLA"].ToString()))
                                : departmentMapping[0].Terminals.FindAll(
                                    i => i.Id.Contains(myReader["TR_TERM_SLA"].ToString()));
                            sFound = terminals.Count > 0
                                ? Settings.Default.DepTimeAndAttendanceCode
                                : Settings.Default.DepAccessControlCode;
                        }
                        else
                        {
                            sFound = Settings.Default.DepAccessControlCode;
                        }
                    }

                    if (sFound != "")
                    {
                        if (!Settings.Default.SyncAccessControl)
                            if (sFound == Settings.Default.DepAccessControlCode)
                            {
                                Application.DoEvents();
                                continue;
                            }
                        var line = $"{employeeNo} {date} {time} {direction} {sFound}{Environment.NewLine}";
                        fileOutput.Add(line);
                        //using (var sw = new StreamWriter(txtOutputPath.Text, true))
                        //{
                        //    sw.Write(line);
                        //}
                    }

                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + @" Row: " + iCounter + "\n\n" + ex);
            }
            finally
            {
                connection.Close();
                _bRunning = false;
            }

            File.AppendAllLines(txtOutputPath.Text, fileOutput);

            if (_bAutomated)
            {
                UpdateProcessed(connection, command);
            }

            toolStripStatusLabel1.Text =
                $"{iCount} record(s) processed and {unProcessed} record(s) skipped due to no mappings";

            command.Dispose();

            if (!_bAutomated)
                MessageBox.Show(@"Operation completed successfully");

            RunExe();

            
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
                        Id = reader["DEPT_No"].ToString(),
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
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static List<Department> GetDepartments(IDbConnection connection, IDbCommand command,
            string departmentList)
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
                        Id = reader["DEPT_No"].ToString(),
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
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static IEnumerable<Company> GetImproCompanies(IDbConnection connection, IDbCommand command)
        {
            try
            {
                command.Connection = connection;

                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText = "SELECT DISTINCT EMP_Employer FROM EMPLOYEE WHERE EMP_Employer <> \'\'";
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
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static IEnumerable<Employee> GetImproEmployees(IDbConnection connection, IDbCommand command,
            string departments)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText =
                    "SELECT DISTINCT EMPLOYEE.MST_SQ, MASTER.MST_FirstName, MASTER.MST_LastName, EMPLOYEE.EMP_Employer, EMPLOYEE.DEPT_No, EMPLOYEE.EMP_EmployeeNo, MASTER.MST_ID " +
                    "FROM EMPLOYEE INNER JOIN MASTER ON EMPLOYEE.MST_SQ = MASTER.MST_SQ  " +
                    "WHERE EMPLOYEE.DEPT_No IN (" + departments + ") AND MST_Current = '1' ";
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
                        DepartmentNo = reader["DEPT_No"].ToString(),
                        EmployeeeNo = reader["EMP_EmployeeNo"].ToString(),
                        IdNumber = reader["MST_ID"].ToString() //,
                        //Suspended = reader["TAG_Suspend"].ToString() != "0"
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
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private IEnumerable<Terminal> GetImproTerminals(IDbConnection connection, IDbCommand command)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText = radIXP400.Checked
                    ? "SELECT TERM_SLA, TERM_NAME FROM TERMINAL"
                    : "SELECT T_ADDR, T_NAME FROM TERMINAL";
                var reader = command.ExecuteReader();

                var terminals = new List<Terminal>();
                while (reader.Read())
                {

                    var terminal = new Terminal
                    {
                        Id = radIXP400.Checked ? reader["TERM_SLA"].ToString() : reader["T_ADDR"].ToString(),
                        Name = radIXP400.Checked ? reader["TERM_NAME"].ToString() : reader["T_NAME"].ToString()
                    };
                    terminals.Add(terminal);
                }
                return terminals;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static Employee GetTmpEmployee(IDbConnection connection, IDbCommand command, string employeeNumber)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText = "SELECT a.EMP_NO, a.DEPARTMENT, a.DISCHARGED, a.ID_NUMBER, b.BUTTON_NUMBER " +
                                      "FROM EMP a " +
                                      "LEFT OUTER JOIN BUTTON b ON a.EMP_NO = b.BUTTON_HLDR " +
                                      "WHERE a.EMP_NO = @EMPNO";
                ((FbCommand)command).Parameters.Add("@EMPNO", FbDbType.VarChar).Value = employeeNumber;
                var reader = command.ExecuteReader();

                Employee employee = null;
                while (reader.Read())
                {
                    employee = new Employee
                    {
                        EmployeeeNo = reader["EMP_NO"].ToString(),
                        CardNumber = reader["BUTTON_NUMBER"].ToString(),
                        DepartmentNo = reader["DEPARTMENT"].ToString(),
                        Suspended = reader["DISCHARGED"].ToString() == "Y",
                        IdNumber = reader["ID_NUMBER"].ToString()
                    };
                }
                return employee;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
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

                command.CommandText = "SELECT a.EMP_NO, a.DEPARTMENT, a.DISCHARGED, a.ID_NUMBER, b.BUTTON_NUMBER " +
                                      "FROM EMP a " +
                                      "LEFT OUTER JOIN BUTTON b ON a.EMP_NO = b.BUTTON_HLDR";
                var reader = command.ExecuteReader();

                var employees = new List<Employee>();
                while (reader.Read())
                {
                    var employee = new Employee
                    {
                        EmployeeeNo = reader["EMP_NO"].ToString(),
                        CardNumber = reader["BUTTON_NUMBER"].ToString(),
                        DepartmentNo = reader["DEPARTMENT"].ToString(),
                        Suspended = reader["DISCHARGED"].ToString() == "Y",
                        IdNumber = reader["ID_NUMBER"].ToString()
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
            finally
            {
                connection.Close();
                connection.Dispose();
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
                        Id = reader["CODE"].ToString(),
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
            finally
            {
                connection.Close();
                connection.Dispose();
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
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static IEnumerable<Roster> GetTmpRosters(IDbConnection connection, IDbCommand command)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText = "SELECT a.CODE, a.DESCRIPTION FROM ROSTERS a ";
                var reader = command.ExecuteReader();

                var rosters = new List<Roster>();
                while (reader.Read())
                {
                    var roster = new Roster
                    {
                        Code = reader["CODE"].ToString(),
                        Name = reader["DESCRIPTION"].ToString()
                    };
                    rosters.Add(roster);
                }
                return rosters;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static void GetTmpRosterDates(IDbConnection connection, IDbCommand command, ref Roster roster)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText =
                    $"SELECT a.CODE, a.ROSTERDATE, a.SHIFTS, a.DEDUCTHOURS, a.DEDUCTNT, a.DEDUCTOT1, a.DEDUCTOT2, a.DEDUCTOT3, a.DEDUCTOT4, a.TARGETNT, a.TARGETOT1, a.TARGETOT2, a.TARGETOT3, a.TARGETOT4, a.BALANCINGDIRECTION, a.MAXOT0, a.MAXOT1, a.MAXOT2, a.MAXOT3, a.MAXOT4 FROM ROSTERDATES a WHERE a.CODE = '{roster.Code}'";
                var reader = command.ExecuteReader();

                roster.RosterDates = new List<RosterDate>();
                while (reader.Read())
                {
                    var rosterDate = new RosterDate
                    {
                        Code = reader["CODE"].ToString(),
                        StartDate = Convert.ToDateTime(reader["ROSTERDATE"].ToString()),
                        Shifts = reader["SHIFTS"].ToString(),
                        DeductHours = reader["DEDUCTHOURS"].ToString()[0],
                        DeductNt = reader["DEDUCTNT"].ToString()[0],
                        DeductOt1 = reader["DEDUCTOT1"].ToString()[0],
                        DeductOt2 = reader["DEDUCTOT2"].ToString()[0],
                        DeductOt3 = reader["DEDUCTOT3"].ToString()[0],
                        DeductOt4 = reader["DEDUCTOT4"].ToString()[0],
                        TargetNt = (int) reader["TARGETNT"],
                        TargetOt1 = (int) reader["TARGETOT1"],
                        TargetOt2 = (int) reader["TARGETOT2"],
                        TargetOt3 = (int) reader["TARGETOT3"],
                        TargetOt4 = (int) reader["TARGETOT4"],
                        BalancingDirection = reader["BALANCINGDIRECTION"].ToString()[0],
                        MaxOt0 = (int) reader["MAXOT0"],
                        MaxOt1 = (int) reader["MAXOT1"],
                        MaxOt2 = (int) reader["MAXOT2"],
                        MaxOt3 = (int) reader["MAXOT3"],
                        MaxOt4 = (int) reader["MAXOT4"]
                    };
                    roster.RosterDates.Add(rosterDate);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                    connection.Dispose();
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
                ((FbCommand) command).Parameters.Add("@CODE", FbDbType.VarChar).Value = department.Id;
                ((FbCommand) command).Parameters.Add("@DESCRIPTION", FbDbType.VarChar).Value = department.Name.Length >
                                                                                               20
                    ? department.Name.Substring(0, 20)
                    : department.Name;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                connection.Close();
                connection.Dispose();
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
                ((FbCommand) command).Parameters.Add("@CODE", FbDbType.VarChar).Value = company.Code;
                ((FbCommand) command).Parameters.Add("@DESCRIPTION", FbDbType.VarChar).Value = company.Description;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                connection.Close();
                connection.Dispose();
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
                ((FbCommand) command).Parameters.Add("@IDNUMBER", FbDbType.VarChar).Value = employee.IdNumber.Length >
                                                                                            13
                    ? employee.IdNumber.Substring(0, 13)
                    : employee.IdNumber;
                ((FbCommand) command).Parameters.Add("@NAME", FbDbType.VarChar).Value = employee.Name.Length > 30
                    ? employee.Name.Substring(0, 30).ToUpper()
                    : employee.Name.ToUpper();
                ((FbCommand) command).Parameters.Add("@SURNAME", FbDbType.VarChar).Value = employee.LastName.Length > 30
                    ? employee.LastName.Substring(0, 30).ToUpper()
                    : employee.LastName.ToUpper();
                ((FbCommand) command).Parameters.Add("@EMPLOYMENTDATE", FbDbType.TimeStamp).Value = DateTime.Now;
                ((FbCommand) command).Parameters.Add("@DISCHARGEDATE", FbDbType.TimeStamp).Value = DateTime.Now;
                ((FbCommand) command).Parameters.Add("@BIRTHDATE", FbDbType.TimeStamp).Value = DateTime.Now;
                ((FbCommand) command).Parameters.Add("@ABSENTFROM", FbDbType.TimeStamp).Value = DateTime.MinValue;
                ((FbCommand) command).Parameters.Add("@ABSENTTO", FbDbType.TimeStamp).Value = DateTime.MinValue;
                ((FbCommand) command).Parameters.Add("@DEPARTMENT", FbDbType.VarChar).Value = employee.DepartmentNo;
                ((FbCommand) command).Parameters.Add("@COMPANY", FbDbType.VarChar).Value = employee.Employer == ""
                    ? "1"
                    : employee.Employer;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
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
                ((FbCommand) command).Parameters.Add("@BUTTONNO", FbDbType.VarChar).Value = employee.CardNumber.Length >
                                                                                            10
                    ? employee.CardNumber.Substring(0, 10)
                    : employee.CardNumber;
                ((FbCommand) command).Parameters.Add("@BUTTONHLDR", FbDbType.VarChar).Value =
                    employee.EmployeeeNo.Length > 10
                        ? employee.EmployeeeNo.Substring(0, 10)
                        : employee.EmployeeeNo;
                ((FbCommand) command).Parameters.Add("@EXPIRY", FbDbType.Date).Value = DateTime.MaxValue;

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                    connection.Dispose();
            }
        }

        private static void CreateTmpRosterDate(IDbConnection connection, IDbCommand command, RosterDate rosterDate)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                command.CommandText = "INSERT INTO ROSTERDATES " +
                                      "(CODE, ROSTERDATE, SHIFTS, DEDUCTHOURS, DEDUCTNT, DEDUCTOT1, DEDUCTOT2, DEDUCTOT3, DEDUCTOT4, TARGETNT, TARGETOT1, TARGETOT2, TARGETOT3, TARGETOT4, BALANCINGDIRECTION, MAXOT0, MAXOT1, MAXOT2, MAXOT3, MAXOT4) " +
                                      "" +
                                      "VALUES " +
                                      "(@CODE, @ROSTERDATE, @SHIFTS, @DEDUCTHOURS, @DEDUCTNT, @DEDUCTOT1, @DEDUCTOT2, @DEDUCTOT3, @DEDUCTOT4, @TARGETNT, @TARGETOT1, @TARGETOT2, @TARGETOT3, @TARGETOT4, @BALANCINGDIRECTION, @MAXOT0, @MAXOT1, @MAXOT2, @MAXOT3, @MAXOT4)";

                ((FbCommand) command).Parameters.Add("@CODE", FbDbType.VarChar).Value = rosterDate.Code;
                ((FbCommand) command).Parameters.Add("@ROSTERDATE", FbDbType.Date).Value = rosterDate.StartDate;
                ((FbCommand) command).Parameters.Add("@SHIFTS", FbDbType.VarChar).Value = rosterDate.Shifts;
                ((FbCommand) command).Parameters.Add("@DEDUCTHOURS", FbDbType.Char).Value = rosterDate.DeductHours;
                ((FbCommand) command).Parameters.Add("@DEDUCTNT", FbDbType.Char).Value = rosterDate.DeductNt;
                ((FbCommand) command).Parameters.Add("@DEDUCTOT1", FbDbType.Char).Value = rosterDate.DeductOt1;
                ((FbCommand) command).Parameters.Add("@DEDUCTOT2", FbDbType.Char).Value = rosterDate.DeductOt2;
                ((FbCommand) command).Parameters.Add("@DEDUCTOT3", FbDbType.Char).Value = rosterDate.DeductOt3;
                ((FbCommand) command).Parameters.Add("@DEDUCTOT4", FbDbType.Char).Value = rosterDate.DeductOt4;
                ((FbCommand) command).Parameters.Add("@TARGETNT", FbDbType.Integer).Value = rosterDate.TargetNt;
                ((FbCommand) command).Parameters.Add("@TARGETOT1", FbDbType.Integer).Value = rosterDate.TargetOt1;
                ((FbCommand) command).Parameters.Add("@TARGETOT2", FbDbType.Integer).Value = rosterDate.TargetOt2;
                ((FbCommand) command).Parameters.Add("@TARGETOT3", FbDbType.Integer).Value = rosterDate.TargetOt3;
                ((FbCommand) command).Parameters.Add("@TARGETOT4", FbDbType.Integer).Value = rosterDate.TargetOt4;
                ((FbCommand) command).Parameters.Add("@BALANCINGDIRECTION", FbDbType.Char).Value =
                    rosterDate.BalancingDirection;
                ((FbCommand) command).Parameters.Add("@MAXOT0", FbDbType.Integer).Value = rosterDate.MaxOt0;
                ((FbCommand) command).Parameters.Add("@MAXOT1", FbDbType.Integer).Value = rosterDate.MaxOt1;
                ((FbCommand) command).Parameters.Add("@MAXOT2", FbDbType.Integer).Value = rosterDate.MaxOt2;
                ((FbCommand) command).Parameters.Add("@MAXOT3", FbDbType.Integer).Value = rosterDate.MaxOt3;
                ((FbCommand) command).Parameters.Add("@MAXOT4", FbDbType.Integer).Value = rosterDate.MaxOt4;

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static void EditTmpEmployee(IDbConnection connection, IDbCommand command, Employee employee)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = "UPDATE EMP " +
                                      "SET ID_NUMBER = @IDNUMBER, NAME = @NAME, SURNAME = @SURNAME, DEPARTMENT = @DEPARTMENT, COMPANY = @COMPANY, DISCHARGED = @DISCHARGED " +
                                      "WHERE EMP_NO = @EMPNO";

                ((FbCommand) command).Parameters.Add("@EMPNO", FbDbType.VarChar).Value = employee.EmployeeeNo.Length > 10
                    ? employee.EmployeeeNo.Substring(0, 10)
                    : employee.EmployeeeNo;
                ((FbCommand) command).Parameters.Add("@IDNUMBER", FbDbType.VarChar).Value = employee.IdNumber.Length > 13
                    ? employee.IdNumber.Substring(0, 13)
                    : employee.IdNumber;
                ((FbCommand) command).Parameters.Add("@NAME", FbDbType.VarChar).Value = employee.Name.Length > 30
                    ? employee.Name.Substring(0, 30).ToUpper()
                    : employee.Name.ToUpper();
                ((FbCommand) command).Parameters.Add("@SURNAME", FbDbType.VarChar).Value = employee.LastName.Length > 30
                    ? employee.LastName.Substring(0, 30).ToUpper()
                    : employee.LastName.ToUpper();
                ((FbCommand) command).Parameters.Add("@DEPARTMENT", FbDbType.VarChar).Value = employee.DepartmentNo;
                ((FbCommand) command).Parameters.Add("@COMPANY", FbDbType.VarChar).Value = employee.Employer == ""
                    ? "1"
                    : employee.Employer;
                ((FbCommand) command).Parameters.Add("@DISCHARGED", FbDbType.Char).Value = employee.Suspended
                    ? 'Y'
                    : 'N';

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static void EditTmpAttachments(IDbConnection connection, IDbCommand command, string tmpEmployeeNumber, string updatedEmployeeNumber)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = "UPDATE ATTACHMENTS " +
                                      "SET EMP_NO = @NEW_EMPNO " +
                                      "WHERE EMP_NO = @OLD_EMPNO";

                ((FbCommand)command).Parameters.Add("@NEW_EMPNO", FbDbType.VarChar).Value = updatedEmployeeNumber.Length > 10
                    ? updatedEmployeeNumber.Substring(0, 10)
                    : updatedEmployeeNumber;

                ((FbCommand)command).Parameters.Add("@OLD_EMPNO", FbDbType.VarChar).Value = tmpEmployeeNumber.Length > 10
                    ? tmpEmployeeNumber.Substring(0, 10)
                    : tmpEmployeeNumber;

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static void EditTmpButton(IDbConnection connection, IDbCommand command, string tmpEmployeeNumber, string updatedEmployeeNumber)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = "UPDATE BUTTON " +
                                      "SET BUTTON_HLDR = @NEW_EMPNO " +
                                      "WHERE BUTTON_HLDR = @OLD_EMPNO";

                ((FbCommand)command).Parameters.Add("@NEW_EMPNO", FbDbType.VarChar).Value = updatedEmployeeNumber.Length > 10
                    ? updatedEmployeeNumber.Substring(0, 10)
                    : updatedEmployeeNumber;

                ((FbCommand)command).Parameters.Add("@OLD_EMPNO", FbDbType.VarChar).Value = tmpEmployeeNumber.Length > 10
                    ? tmpEmployeeNumber.Substring(0, 10)
                    : tmpEmployeeNumber;

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static void EditTmpClockD(IDbConnection connection, IDbCommand command, string tmpEmployeeNumber, string updatedEmployeeNumber)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = "UPDATE CLOCKD " +
                                      "SET EMPNO = @NEW_EMPNO " +
                                      "WHERE EMPNO = @OLD_EMPNO";

                ((FbCommand)command).Parameters.Add("@NEW_EMPNO", FbDbType.VarChar).Value = updatedEmployeeNumber.Length > 10
                    ? updatedEmployeeNumber.Substring(0, 10)
                    : updatedEmployeeNumber;

                ((FbCommand)command).Parameters.Add("@OLD_EMPNO", FbDbType.VarChar).Value = tmpEmployeeNumber.Length > 10
                    ? tmpEmployeeNumber.Substring(0, 10)
                    : tmpEmployeeNumber;

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static void EditTmpClocking(IDbConnection connection, IDbCommand command, string tmpEmployeeNumber, string updatedEmployeeNumber)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = "UPDATE CLOCKING " +
                                      "SET CODE = @NEW_EMPNO " +
                                      "WHERE CODE = @OLD_EMPNO";

                ((FbCommand)command).Parameters.Add("@NEW_EMPNO", FbDbType.VarChar).Value = updatedEmployeeNumber.Length > 10
                    ? updatedEmployeeNumber.Substring(0, 10)
                    : updatedEmployeeNumber;

                ((FbCommand)command).Parameters.Add("@OLD_EMPNO", FbDbType.VarChar).Value = tmpEmployeeNumber.Length > 10
                    ? tmpEmployeeNumber.Substring(0, 10)
                    : tmpEmployeeNumber;

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static void EditTmpEmp_EmpNo(IDbConnection connection, IDbCommand command, string tmpEmployeeNumber, string updatedEmployeeNumber)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = "UPDATE EMP " +
                                      "SET EMP_NO = @NEW_EMPNO " +
                                      "WHERE EMP_NO = @OLD_EMPNO";

                ((FbCommand) command).Parameters.Add("@NEW_EMPNO", FbDbType.VarChar).Value =
                    updatedEmployeeNumber.Length > 10
                        ? updatedEmployeeNumber.Substring(0, 10)
                        : updatedEmployeeNumber;

                ((FbCommand) command).Parameters.Add("@OLD_EMPNO", FbDbType.VarChar).Value = tmpEmployeeNumber.Length >
                                                                                             10
                    ? tmpEmployeeNumber.Substring(0, 10)
                    : tmpEmployeeNumber;

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static void EditTmpEmployeeDisciplinary(IDbConnection connection, IDbCommand command, string tmpEmployeeNumber, string updatedEmployeeNumber)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = "UPDATE EMPLOYEEDISCIPLINARY " +
                                      "SET EMP_NO = @NEW_EMPNO " +
                                      "WHERE EMP_NO = @OLD_EMPNO";

                ((FbCommand)command).Parameters.Add("@NEW_EMPNO", FbDbType.VarChar).Value = updatedEmployeeNumber.Length > 10
                    ? updatedEmployeeNumber.Substring(0, 10)
                    : updatedEmployeeNumber;

                ((FbCommand)command).Parameters.Add("@OLD_EMPNO", FbDbType.VarChar).Value = tmpEmployeeNumber.Length > 10
                    ? tmpEmployeeNumber.Substring(0, 10)
                    : tmpEmployeeNumber;

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static void EditTmpEmployeeLeaveInfo(IDbConnection connection, IDbCommand command, string tmpEmployeeNumber, string updatedEmployeeNumber)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = "UPDATE EMPLOYEELEAVEINFO " +
                                      "SET EMP_NO = @NEW_EMPNO " +
                                      "WHERE EMP_NO = @OLD_EMPNO";

                ((FbCommand)command).Parameters.Add("@NEW_EMPNO", FbDbType.VarChar).Value = updatedEmployeeNumber.Length > 10
                    ? updatedEmployeeNumber.Substring(0, 10)
                    : updatedEmployeeNumber;

                ((FbCommand)command).Parameters.Add("@OLD_EMPNO", FbDbType.VarChar).Value = tmpEmployeeNumber.Length > 10
                    ? tmpEmployeeNumber.Substring(0, 10)
                    : tmpEmployeeNumber;

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static void EditTmpEmployeeLicenses(IDbConnection connection, IDbCommand command, string tmpEmployeeNumber, string updatedEmployeeNumber)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = "UPDATE EMPLOYEELICENSES " +
                                      "SET EMP_NO = @NEW_EMPNO " +
                                      "WHERE EMP_NO = @OLD_EMPNO";

                ((FbCommand)command).Parameters.Add("@NEW_EMPNO", FbDbType.VarChar).Value = updatedEmployeeNumber.Length > 10
                    ? updatedEmployeeNumber.Substring(0, 10)
                    : updatedEmployeeNumber;

                ((FbCommand)command).Parameters.Add("@OLD_EMPNO", FbDbType.VarChar).Value = tmpEmployeeNumber.Length > 10
                    ? tmpEmployeeNumber.Substring(0, 10)
                    : tmpEmployeeNumber;

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static void EditTmpEmployeeLimitations(IDbConnection connection, IDbCommand command, string tmpEmployeeNumber, string updatedEmployeeNumber)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = "UPDATE EMPLOYEELIMITATIONS " +
                                      "SET EMP_NO = @NEW_EMPNO " +
                                      "WHERE EMP_NO = @OLD_EMPNO";

                ((FbCommand)command).Parameters.Add("@NEW_EMPNO", FbDbType.VarChar).Value = updatedEmployeeNumber.Length > 10
                    ? updatedEmployeeNumber.Substring(0, 10)
                    : updatedEmployeeNumber;

                ((FbCommand)command).Parameters.Add("@OLD_EMPNO", FbDbType.VarChar).Value = tmpEmployeeNumber.Length > 10
                    ? tmpEmployeeNumber.Substring(0, 10)
                    : tmpEmployeeNumber;

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static void EditTmpFlexiTime(IDbConnection connection, IDbCommand command, string tmpEmployeeNumber, string updatedEmployeeNumber)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = "UPDATE FLEXITIME " +
                                      "SET EMP_NO = @NEW_EMPNO " +
                                      "WHERE EMP_NO = @OLD_EMPNO";

                ((FbCommand)command).Parameters.Add("@NEW_EMPNO", FbDbType.VarChar).Value = updatedEmployeeNumber.Length > 10
                    ? updatedEmployeeNumber.Substring(0, 10)
                    : updatedEmployeeNumber;

                ((FbCommand)command).Parameters.Add("@OLD_EMPNO", FbDbType.VarChar).Value = tmpEmployeeNumber.Length > 10
                    ? tmpEmployeeNumber.Substring(0, 10)
                    : tmpEmployeeNumber;

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static void EditTmpHoursSummary(IDbConnection connection, IDbCommand command, string tmpEmployeeNumber, string updatedEmployeeNumber)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = "UPDATE HOURSSUMMARY " +
                                      "SET EMP_NO = @NEW_EMPNO " +
                                      "WHERE EMP_NO = @OLD_EMPNO";

                ((FbCommand)command).Parameters.Add("@NEW_EMPNO", FbDbType.VarChar).Value = updatedEmployeeNumber.Length > 10
                    ? updatedEmployeeNumber.Substring(0, 10)
                    : updatedEmployeeNumber;

                ((FbCommand)command).Parameters.Add("@OLD_EMPNO", FbDbType.VarChar).Value = tmpEmployeeNumber.Length > 10
                    ? tmpEmployeeNumber.Substring(0, 10)
                    : tmpEmployeeNumber;

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static void EditTmpLeaveRecords(IDbConnection connection, IDbCommand command, string tmpEmployeeNumber, string updatedEmployeeNumber)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = "UPDATE LEAVERECORDS " +
                                      "SET EMP_NO = @NEW_EMPNO " +
                                      "WHERE EMP_NO = @OLD_EMPNO";

                ((FbCommand)command).Parameters.Add("@NEW_EMPNO", FbDbType.VarChar).Value = updatedEmployeeNumber.Length > 10
                    ? updatedEmployeeNumber.Substring(0, 10)
                    : updatedEmployeeNumber;

                ((FbCommand)command).Parameters.Add("@OLD_EMPNO", FbDbType.VarChar).Value = tmpEmployeeNumber.Length > 10
                    ? tmpEmployeeNumber.Substring(0, 10)
                    : tmpEmployeeNumber;

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static void EditTmpOldLeaveRecords(IDbConnection connection, IDbCommand command, string tmpEmployeeNumber, string updatedEmployeeNumber)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = "UPDATE OLDLEAVERECORDS " +
                                      "SET EMP_NO = @NEW_EMPNO " +
                                      "WHERE EMP_NO = @OLD_EMPNO";

                ((FbCommand)command).Parameters.Add("@NEW_EMPNO", FbDbType.VarChar).Value = updatedEmployeeNumber.Length > 10
                    ? updatedEmployeeNumber.Substring(0, 10)
                    : updatedEmployeeNumber;

                ((FbCommand)command).Parameters.Add("@OLD_EMPNO", FbDbType.VarChar).Value = tmpEmployeeNumber.Length > 10
                    ? tmpEmployeeNumber.Substring(0, 10)
                    : tmpEmployeeNumber;

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static void EditTmpPhotos(IDbConnection connection, IDbCommand command, string tmpEmployeeNumber, string updatedEmployeeNumber)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = "UPDATE PHOTOS " +
                                      "SET EMPNO = @NEW_EMPNO " +
                                      "WHERE EMPNO = @OLD_EMPNO";

                ((FbCommand)command).Parameters.Add("@NEW_EMPNO", FbDbType.VarChar).Value = updatedEmployeeNumber.Length > 10
                    ? updatedEmployeeNumber.Substring(0, 10)
                    : updatedEmployeeNumber;

                ((FbCommand)command).Parameters.Add("@OLD_EMPNO", FbDbType.VarChar).Value = tmpEmployeeNumber.Length > 10
                    ? tmpEmployeeNumber.Substring(0, 10)
                    : tmpEmployeeNumber;

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static void EditTmpTrainingRecords(IDbConnection connection, IDbCommand command, string tmpEmployeeNumber, string updatedEmployeeNumber)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = "UPDATE TRAININGRECORDS " +
                                      "SET EMP_NO = @NEW_EMPNO " +
                                      "WHERE EMP_NO = @OLD_EMPNO";

                ((FbCommand)command).Parameters.Add("@NEW_EMPNO", FbDbType.VarChar).Value = updatedEmployeeNumber.Length > 10
                    ? updatedEmployeeNumber.Substring(0, 10)
                    : updatedEmployeeNumber;

                ((FbCommand)command).Parameters.Add("@OLD_EMPNO", FbDbType.VarChar).Value = tmpEmployeeNumber.Length > 10
                    ? tmpEmployeeNumber.Substring(0, 10)
                    : tmpEmployeeNumber;

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private static void EditTmpWorkPatternSchedules(IDbConnection connection, IDbCommand command, string tmpEmployeeNumber, string updatedEmployeeNumber)
        {
            try
            {
                command.Connection = connection;
                connection.Open();

                var transaction = connection.BeginTransaction();
                command.Transaction = transaction;
                command.CommandText = "UPDATE WORKPATTERNSCHEDULES " +
                                      "SET EMP_NO = @NEW_EMPNO " +
                                      "WHERE EMP_NO = @OLD_EMPNO";

                ((FbCommand)command).Parameters.Add("@NEW_EMPNO", FbDbType.VarChar).Value = updatedEmployeeNumber.Length > 10
                    ? updatedEmployeeNumber.Substring(0, 10)
                    : updatedEmployeeNumber;

                ((FbCommand)command).Parameters.Add("@OLD_EMPNO", FbDbType.VarChar).Value = tmpEmployeeNumber.Length > 10
                    ? tmpEmployeeNumber.Substring(0, 10)
                    : tmpEmployeeNumber;

                command.ExecuteNonQuery();
                transaction.Commit();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private void SyncEmployees()
        {
            try
            {
                var errors = new List<string>();
                var newEmployees = new List<string>();
                _bSyncRunning = true;

                //Save and get departments
                SaveDepartments();

                var departments = "";
                if (!Settings.Default.SyncAllDepartments)
                {
                    var deptObj = Settings.Default.SelectedDepartments;
                    if (deptObj == null)
                        return;

                    departments =
                        deptObj.Cast<string>().Aggregate("", (current, dept) => current + $"'{dept}',").TrimEnd(',');
                }
                else
                {
                    var departmentList = GetDepartments(GetConnectionObject(), GetCommandObject());
                    if (departmentList == null)
                        return;

                    departments = departmentList.Aggregate("", (current, dept) => current + $"'{dept.Id}',")
                        .TrimEnd(',');
                }

                var improDepartments = GetDepartments(GetConnectionObject(), GetCommandObject(), departments);
                if (improDepartments == null)
                    return;
                var tmpDepartments = GetTmpDepartments(GetTmpConnectionObject(), new FbCommand());
                if (tmpDepartments == null)
                    return;
                var improCompanies = GetImproCompanies(GetConnectionObject(), GetCommandObject());
                var tmpCompanies = GetTmpCompanies(GetTmpConnectionObject(), new FbCommand());

                var companies = improCompanies.Where(comp => tmpCompanies.All(i => i.Code != comp.Code));
                toolStripStatusLabel1.Text = @"Sync Companies";
                toolStripProgressBar1.Value = 0;
                var comps = companies as Company[] ?? companies.ToArray();
                toolStripProgressBar1.Maximum = comps.Count();
                //Determine if Companies already exist
                foreach (var comp in comps)
                {
                    //Create new company in TMP
                    CreateTmpCompany(GetTmpConnectionObject(), new FbCommand(),
                        new Company {Code = comp.Code, Description = "DEFAULT"});
                    toolStripProgressBar1.Increment(1);
                    Application.DoEvents();
                }

                var iDepartments = improDepartments.Where(dept => tmpDepartments.All(i => i.Id != dept.Id));
                toolStripStatusLabel1.Text = @"Sync Departments";
                toolStripProgressBar1.Value = 0;
                var depts = iDepartments as Department[] ?? iDepartments.ToArray();
                toolStripProgressBar1.Maximum = comps.Count();
                //Determine if Departments already exist
                foreach (var dept in depts)
                {
                    //Create new department in TMP
                    CreateTmpDepartment(GetTmpConnectionObject(), new FbCommand(),
                        new Department {Id = dept.Id, Name = dept.Name});
                    toolStripProgressBar1.Increment(1);
                    Application.DoEvents();
                }

                var improEmployees = GetImproEmployees(GetConnectionObject(), GetCommandObject(), departments);
                if (improEmployees == null)
                    return;
                var tmpEmployees = GetTmpEmployees(GetTmpConnectionObject(), new FbCommand());
                if (tmpEmployees == null)
                    return;

                //Determine if Employees already exist
                toolStripStatusLabel1.Text = @"Sync New Employees";
                toolStripProgressBar1.Value = 0;
                var enumerable = improEmployees as Employee[] ?? improEmployees.ToArray();
                if (Settings.Default.UseMSTSQ)
                {
                    var iEmpl = enumerable.Where(empl => tmpEmployees.All(i => i.CardNumber != empl.CardNumber));
                    var empls = iEmpl as Employee[] ?? iEmpl.ToArray();
                    toolStripProgressBar1.Maximum = empls.Count();
                    foreach (var empl in empls)
                    {
                        //if (empl.Suspended)
                        //    continue;
                        //Create new employee in TMP                
                        CreateTmpEmployee(GetTmpConnectionObject(), new FbCommand(), empl);
                        CreateTmpCardNumber(GetTmpConnectionObject(), new FbCommand(), empl);
                        newEmployees.Add($"Employee No: {empl.EmployeeeNo}, Name: {empl.Name}, Suname: {empl.LastName}");
                        toolStripProgressBar1.Increment(1);
                        Application.DoEvents();
                    }
                }
                else
                {
                    var iEmpl = enumerable.Where(empl => tmpEmployees.All(i => i.EmployeeeNo != empl.EmployeeeNo));
                    var empls = iEmpl as Employee[] ?? iEmpl.ToArray();
                    toolStripProgressBar1.Maximum = empls.Count();
                    foreach (var empl in empls)
                    {
                        //if (empl.Suspended)
                        //    continue;
                        //Create new employee in TMP                
                        CreateTmpEmployee(GetTmpConnectionObject(), new FbCommand(), empl);
                        newEmployees.Add($"Employee No: {empl.EmployeeeNo}, Name: {empl.Name}, Suname: {empl.LastName}");
                        toolStripProgressBar1.Increment(1);
                        Application.DoEvents();

                    }

                    //Determine if Employee card number already exist
                    foreach (
                        var empl in enumerable.Where(empl => tmpEmployees.All(i => i.CardNumber != empl.CardNumber)))
                    {
                        //Add TMP Card Number
                        CreateTmpCardNumber(GetTmpConnectionObject(), new FbCommand(), empl);
                        Application.DoEvents();
                    }
                }

                //Refresh after create
                improEmployees = GetImproEmployees(GetConnectionObject(), GetCommandObject(), departments);
                if (improEmployees == null)
                    return;
                tmpEmployees = GetTmpEmployees(GetTmpConnectionObject(), new FbCommand());
                if (tmpEmployees == null)
                    return;

                toolStripStatusLabel1.Text = @"Sync Existing Employees";
                toolStripProgressBar1.Maximum = enumerable.Length;
                toolStripProgressBar1.Value = 0;
                var tmpEmployees1 = tmpEmployees as IList<Employee> ?? tmpEmployees.ToList();
                var employees = tmpEmployees as IList<Employee> ?? tmpEmployees1.ToList();
                foreach (var improEmployee in enumerable)
                {
                    foreach (var tmpEmployee in employees)
                    {
                        if (Settings.Default.UseMSTSQ)
                        {
                            if (improEmployee.CardNumber != tmpEmployee.CardNumber) continue;
                        }
                        else
                        {
                            if (improEmployee.EmployeeeNo != tmpEmployee.EmployeeeNo) continue;
                        }
                        if (improEmployee.DepartmentNo != tmpEmployee.DepartmentNo ||
                            improEmployee.Suspended != tmpEmployee.Suspended ||
                            improEmployee.EmployeeeNo != tmpEmployee.EmployeeeNo)
                        {

                            try
                            {
                                EditTmpEmployee(GetTmpConnectionObject(), new FbCommand(), improEmployee);

                                //UPDATE ALL TABLES THAT USE EMPLOYEE NUMBER IF IT GETS UPDATED
                                if (improEmployee.EmployeeeNo != tmpEmployee.EmployeeeNo && Settings.Default.UseMSTSQ &&
                                    Settings.Default.UpdateEmployeeNumber)
                                {
                                    var employee = GetTmpEmployee(GetTmpConnectionObject(), new FbCommand(),
                                        improEmployee.EmployeeeNo);
                                    if (employee != null)
                                    {
                                        //Check if employees are the same employee
                                        if (employee.IdNumber == improEmployee.IdNumber)
                                        {
                                            RenameTmpEmployeeNumber(tmpEmployee.EmployeeeNo, improEmployee.EmployeeeNo);
                                            continue;
                                        }

                                        //Employee number already exists in tmp -- Rename old employee
                                        RenameTmpEmployeeNumber(tmpEmployee.EmployeeeNo, tmpEmployee.EmployeeeNo + "_cd");
                                        EditTmpEmp_EmpNo(GetTmpConnectionObject(), new FbCommand(), tmpEmployee.EmployeeeNo, tmpEmployee.EmployeeeNo + "_cd");
                                    }

                                    RenameTmpEmployeeNumber(tmpEmployee.EmployeeeNo, improEmployee.EmployeeeNo);
                                    EditTmpEmp_EmpNo(GetTmpConnectionObject(), new FbCommand(), tmpEmployee.EmployeeeNo, improEmployee.EmployeeeNo);
                                }
                            }
                            catch (Exception e)
                            {
                                errors.Add(e.ToString());
                            }
                        }
                        Application.DoEvents();
                    }
                    toolStripProgressBar1.Increment(1);
                    Application.DoEvents();
                }

                if (errors.Count > 0)
                {
                    File.WriteAllLines("SyncErrors.log", errors);
                }

                if (!chkEnableEmail.Checked) return;
                {
                    if (newEmployees.Count <= 0) return;
                    var html = "New employees have been added to Time Manager Platinum:\n\n";
                    html = newEmployees.Aggregate(html, (current, newEmployee) => current + (newEmployee + "\n"));
                    html += "\nKind Regards\nThe TMP Team";
                    Send_Email(txtToEmailAddress.Text, "New Employees Added to Time Manager Platinum", html);
                }
                toolStripStatusLabel1.Text = errors.Any() ? "Completed with errors" : @"Done";
            }
            finally
            {
                _bSyncRunning = false;
                
            }
        }

        private void RenameTmpEmployeeNumber(string tmpEmployeeNumber, string improEmployeeNumber)
        {
            EditTmpAttachments(GetTmpConnectionObject(), new FbCommand(),
                tmpEmployeeNumber,
                improEmployeeNumber);
            EditTmpButton(GetTmpConnectionObject(), new FbCommand(), tmpEmployeeNumber,
                improEmployeeNumber);
            EditTmpClockD(GetTmpConnectionObject(), new FbCommand(), tmpEmployeeNumber,
                improEmployeeNumber);
            EditTmpClocking(GetTmpConnectionObject(), new FbCommand(), tmpEmployeeNumber,
                improEmployeeNumber);
            EditTmpEmployeeDisciplinary(GetTmpConnectionObject(), new FbCommand(),
                tmpEmployeeNumber,
                improEmployeeNumber);
            EditTmpEmployeeLeaveInfo(GetTmpConnectionObject(), new FbCommand(),
                tmpEmployeeNumber,
                improEmployeeNumber);
            EditTmpEmployeeLicenses(GetTmpConnectionObject(), new FbCommand(),
                tmpEmployeeNumber,
                improEmployeeNumber);
            EditTmpEmployeeLimitations(GetTmpConnectionObject(), new FbCommand(),
                tmpEmployeeNumber,
                improEmployeeNumber);
            EditTmpFlexiTime(GetTmpConnectionObject(), new FbCommand(), tmpEmployeeNumber,
                improEmployeeNumber);
            EditTmpHoursSummary(GetTmpConnectionObject(), new FbCommand(),
                tmpEmployeeNumber,
                improEmployeeNumber);
            EditTmpLeaveRecords(GetTmpConnectionObject(), new FbCommand(),
                tmpEmployeeNumber,
                improEmployeeNumber);
            EditTmpOldLeaveRecords(GetTmpConnectionObject(), new FbCommand(),
                tmpEmployeeNumber,
                improEmployeeNumber);
            EditTmpPhotos(GetTmpConnectionObject(), new FbCommand(), tmpEmployeeNumber,
                improEmployeeNumber);
            EditTmpTrainingRecords(GetTmpConnectionObject(), new FbCommand(),
                tmpEmployeeNumber,
                improEmployeeNumber);
            EditTmpWorkPatternSchedules(GetTmpConnectionObject(), new FbCommand(),
                tmpEmployeeNumber,
                improEmployeeNumber);            
        }

        private void UpdateProcessed(IDbConnection connection, IDbCommand command)
        {
            try
            {
                connection.Open();

                var myTransaction = connection.BeginTransaction();
                command.Connection = connection;
                command.Transaction = myTransaction;
                command.CommandText = "UPDATE TRANSACK SET TR_PROCESSED = 1 WHERE TR_PROCESSED = 0";
                command.ExecuteNonQuery();
                myTransaction.Commit();
            }
            finally
            {
                connection.Close();
            }
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

                var terminals = GetImproTerminals(connection, command);
                foreach (var terminal in terminals)
                {
                    var sFound = singleMappings.FirstOrDefault(i => i.Contains(terminal.Name));
                    if (sFound == null)
                    {

                        dsTerminalMapping.Tables[0].Rows.Add(terminal.Id, terminal.Name, "");
                        continue;
                    }
                    if (sFound != "")
                    {
                        dsTerminalMapping.Tables[0].Rows.Add(terminal.Id, terminal.Name,
                            sFound.Split(';')[1]);
                    }

                    Application.DoEvents();
                }

                gridMappings.DataSource = dsTerminalMapping.Tables[0];

                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var toSave = new DataSet();
            toSave.Tables.Add(((DataTable) gridMappings.DataSource).Copy());

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
            lblSyncCountDown.Text = $"{_syncSeconds} second(s)";
            if (_syncSeconds == 0 && !_bSyncRunning)
            {
                lblSyncCountDown.Text = @"Sync in progress";
                tmrMain.Stop();
                SyncEmployees();
                _syncSeconds = Convert.ToInt32(txtSyncInterval.Text);
                tmrMain.Start();
            }
            else if (_syncSeconds < 0)
                _syncSeconds = Convert.ToInt32(txtSyncInterval.Text);
            else
                _syncSeconds--;
        }

        private void tmrSeconds_Tick(object sender, EventArgs e)
        {
            lblCountdown.Text = $"{_seconds} second(s)";
            if (_seconds == 0 && !_bRunning)
            {
                tmrSeconds.Stop();
                _bAutomated = true;

                dtFromDate.Value = GetLowestDT(DateTime.Now);
                dtToDate.Value = GetHighestDt(DateTime.Now);

                Process(GetLowestDT(DateTime.Now), GetHighestDt(DateTime.Now), GetConnectionObject(), GetCommandObject());                
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
            foreach (var dept in from object item in items select ((Department) item).Id.ToString())
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
            this.Text += " - " + Application.ProductVersion;
            var nextMonth = DateTime.Now;
            nextMonth = nextMonth.AddMonths(1);

            dtDuplicateScheduleStartDate.Value = new DateTime(nextMonth.Year, nextMonth.Month, 1);
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

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            chkDepartments.ClearSelected();
        }

        private void radUseStandardMapping_CheckedChanged(object sender, EventArgs e)
        {
            if (!radUseStandardMapping.Checked) return;
            tabctrlMappings.SelectedTab = tabpgStandardMapping;
            Settings.Default.MappingConfig = 0;
            Settings.Default.Save();
        }

        private void radUseDepartmentMapping_CheckedChanged(object sender, EventArgs e)
        {
            if (!radUseDepartmentMapping.Checked) return;
            tabctrlMappings.SelectedTab = tabpgDepartmentMapping;
            Settings.Default.MappingConfig = 1;
            Settings.Default.Save();
        }

        private void txtDepMappingTimeAndAttendanceCode_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.DepTimeAndAttendanceCode = txtDepMappingTimeAndAttendanceCode.Text;
            Settings.Default.Save();
        }

        private void txtDepMappingAccessControlCode_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.DepAccessControlCode = txtDepMappingAccessControlCode.Text;
            Settings.Default.Save();
        }

        private void btnDepFetchMappings_Click(object sender, EventArgs e)
        {
            treeDepMappings.Nodes.Clear();
            var departments = GetDepartments(GetConnectionObject(), GetCommandObject());
            var terminals = GetImproTerminals(GetConnectionObject(), GetCommandObject());
            var enumerable = terminals as Terminal[] ?? terminals.ToArray();
            var mappings = new List<DepartmentMapping>();
            if (File.Exists("DepartmentMappings.mxl"))
                mappings = DeSerializeObject<DepartmentMappings>("DepartmentMappings.mxl").DepartmentMappingses;
            foreach (var department in departments)
            {
                var node = treeDepMappings.Nodes.Add(department.Id.ToString(), department.Name);
                node.Tag = department.Id;
                var departmentMapping = mappings.FindAll(i => i.Department.Id == department.Id);
                foreach (var terminal in enumerable)
                {
                    var node2 = node.Nodes.Add(terminal.Id, terminal.Name);
                    node2.Tag = terminal.Id;
                    if (departmentMapping.Count > 0)
                    {
                        if (departmentMapping[0].Terminals.FindAll(i => i.Id == terminal.Id).Count > 0)
                            node2.Checked = true;
                    }
                }
            }
        }

        private void btnDepMappingsSelectAll_Click(object sender, EventArgs e)
        {
            //TreeView - myTreeview;
            treeDepMappings.BeginUpdate();
            //Loop through all the nodes of tree
            foreach (TreeNode node in treeDepMappings.Nodes)
            {
                //If node has child nodes
                if (node.Nodes.Count <= 0) continue;

                //Check all the child nodes.
                node.Checked = true;
                foreach (TreeNode childNode in node.Nodes)
                {
                    childNode.Checked = true;
                }
            }
            treeDepMappings.EndUpdate();
        }

        private void btnDepMappingsDeselectAll_Click(object sender, EventArgs e)
        {
            //TreeView - myTreeview;
            treeDepMappings.BeginUpdate();
            //Loop through all the nodes of tree
            foreach (TreeNode node in treeDepMappings.Nodes)
            {
                //If node has child nodes
                if (node.Nodes.Count <= 0) continue;

                //Check all the child nodes.
                node.Checked = false;
                foreach (TreeNode childNode in node.Nodes)
                {
                    childNode.Checked = false;
                }
            }
            treeDepMappings.EndUpdate();
        }

        private void treeDepMappings_AfterCheck(object sender, TreeViewEventArgs e)
        {
            CheckTreeViewNode(e.Node, e.Node.Checked);
        }

        private static void CheckTreeViewNode(TreeNode node, bool isChecked)
        {
            foreach (TreeNode item in node.Nodes)
            {
                item.Checked = isChecked;

                if (item.Nodes.Count > 0)
                {
                    CheckTreeViewNode(item, isChecked);
                }
            }
        }

        private void btnDepMappingsSave_Click(object sender, EventArgs e)
        {
            var mappings = new DepartmentMappings {DepartmentMappingses = new List<DepartmentMapping>()};
            foreach (TreeNode node in treeDepMappings.Nodes)
            {
                //If node has child nodes
                if (node.Nodes.Count <= 0) continue;

                //Check all the child nodes.
                var mapping = new DepartmentMapping
                {
                    Department = new Department {Id = node.Tag.ToString(), Name = node.Text},
                    Terminals = new List<Terminal>()
                };
                foreach (TreeNode childNode in node.Nodes)
                {
                    if (childNode.Checked)
                        mapping.Terminals.Add(new Terminal {Id = childNode.Tag.ToString(), Name = childNode.Text});
                }
                if (mapping.Terminals.Count > 0)
                    mappings.DepartmentMappingses.Add(mapping);
                if (mappings.DepartmentMappingses.Count > 0)
                {
                    SerializeObject(mappings, "DepartmentMappings.mxl");
                }
            }
            MessageBox.Show(@"Mappings saved!");
        }

        public void SerializeObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null)
            {
                return;
            }

            try
            {
                var xmlDocument = new XmlDocument();
                var serializer = new XmlSerializer(serializableObject.GetType());
                using (var stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public T DeSerializeObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return default(T);
            }

            var objectOut = default(T);

            try
            {
                var xmlDocument = new XmlDocument();
                if (!File.Exists(fileName))
                    return objectOut;
                xmlDocument.Load(fileName);
                var xmlString = xmlDocument.OuterXml;

                using (var read = new StringReader(xmlString))
                {
                    var outType = typeof (T);

                    var serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T) serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return objectOut;
        }

        private void chkSyncAccessControlDevices_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.SyncAccessControl = chkSyncAccessControlDevices.Checked;
            Settings.Default.Save();
        }

        private void txtSyncInterval_TextChanged(object sender, EventArgs e)
        {
            if (txtSyncInterval.Text == "") return;
            Settings.Default.SyncInterval = Convert.ToInt32(txtSyncInterval.Text);
            Settings.Default.Save();
        }

        private void chkSyncTimerEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSyncTimerEnabled.Checked)
            {
                _syncSeconds = Convert.ToInt32(txtSyncInterval.Text);
                tmrMain.Start();
            }
            else
                tmrMain.Stop();

            Settings.Default.SyncTimerEnabled = chkSyncTimerEnabled.Checked;
            Settings.Default.Save();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void txtSmtpHost_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.SmtpHost = txtSmtpHost.Text;
            Settings.Default.Save();
        }

        private void txtSmtpPort_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.SmtpPort = Convert.ToInt16(txtSmtpPort.Text);
            Settings.Default.Save();
        }

        private void txtEmailAddress_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.EmailAddress = txtEmailAddress.Text;
            Settings.Default.Save();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.Username = txtUsername.Text;
            Settings.Default.Save();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.Password = txtPassword.Text;
            Settings.Default.Save();
        }

        private void chkSSL_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.SSL = chkSSL.Checked;
            Settings.Default.Save();
        }

        private void chkEnableEmail_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.EnableEmail = chkEnableEmail.Checked;
            Settings.Default.Save();
        }

        private void txtToEmailAddress_TextChanged(object sender, EventArgs e)
        {
            Settings.Default.ToEmailAddress = txtToEmailAddress.Text;
            Settings.Default.Save();
        }

        private void btnTestEmail_Click(object sender, EventArgs e)
        {
            try
            {
                Send_Email(txtToEmailAddress.Text, "TMP Exporter Test Email", "Test Email");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException == null
                    ? $"{ex.Message}"
                    : $"{ex.Message} - {ex.InnerException.Message}");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.SyncAllDepartments = chkAlwaysSync.Checked;
            Settings.Default.Save();
        }

        private void chkSyncMstsq_CheckedChanged(object sender, EventArgs e)
        {
            chkChangeEmployeeNumber.Enabled = chkSyncMstsq.Checked;
            Settings.Default.UseMSTSQ = chkSyncMstsq.Checked;
            Settings.Default.Save();
        }

        private void CheckForDuplicateRosterDate(RosterDate rosterDate, string rosterCode)
        {
            var rosters = cmbRosters.Items.Cast<Roster>().ToList().Where(i => i.Code == rosterCode);
            if (rosters.Any(roster => roster.RosterDates.Any(rosterDate.CheckIfOverlap)))
            {
                throw new Exception("An overlapping Roster date has been found. Please correct this before continuing");
            }
        }

        private void btnGetRosters_Click(object sender, EventArgs e)
        {
            cmbRosters.Items.Clear();
            var rosters = GetTmpRosters(GetTmpConnectionObject(), GetTmpCommandObject());
            var enumerable = rosters as Roster[] ?? rosters.ToArray();
            for (var i = 0; i < enumerable.Count(); i++)
            {
                GetTmpRosterDates(GetTmpConnectionObject(), GetTmpCommandObject(), ref enumerable[i]);
                cmbRosters.Items.Add(enumerable[i]);
            }

            if (cmbRosters.Items.Count <= 0) return;
            cmbRosters.SelectedIndex = 0;
            if (cmbRosterStartMonth.Items.Count > 0)
            {
                cmbRosterStartMonth.SelectedIndex = 0;
            }
        }

        private void cmbRosters_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbRosterStartMonth.Items.Clear();
            var roster = (Roster) cmbRosters.SelectedItem;
            foreach (var rosterDate in roster.RosterDates)
            {
                cmbRosterStartMonth.Items.Add(rosterDate);
            }

            if (cmbRosterStartMonth.Items.Count > 0)
                cmbRosterStartMonth.SelectedIndex = 0;
        }

        private void btnDuplicateRoster_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbRosters.SelectedIndex == -1)
                {
                    MessageBox.Show(@"Please select a roster");
                    return;
                }
                if (cmbRosterStartMonth.SelectedIndex == -1)
                {
                    MessageBox.Show(@"Please select a roster date");
                    return;
                }

                var roster = (Roster) cmbRosters.SelectedItem;
                var rosterDate = (RosterDate) cmbRosterStartMonth.SelectedItem;


                var periods = numScheduleMonths.Value;
                if (radDuplicateMonths.Checked)
                {
                    for (var i = 0; i < periods; i++)
                    {
                        var startDate = dtDuplicateScheduleStartDate.Value;
                        if (i > 0)
                        {
                            var tempDate = startDate.AddMonths(i);
                            startDate = new DateTime(tempDate.Year, tempDate.Month, 1);
                        }
                        var monthSchedule = rosterDate.CreateMonthSchedule(startDate);
                        CheckForDuplicateRosterDate(monthSchedule, roster.Code);

                        CreateTmpRosterDate(GetTmpConnectionObject(), GetTmpCommandObject(), monthSchedule);
                    }
                }
                else
                {
                    var startDate = dtDuplicateScheduleStartDate.Value;
                    var monthSchedule = rosterDate.CreateWeekSchedule(startDate, (short)periods);
                    CheckForDuplicateRosterDate(monthSchedule, roster.Code);
                    CreateTmpRosterDate(GetTmpConnectionObject(), GetTmpCommandObject(), monthSchedule);
                }

                MessageBox.Show(@"Success!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radDuplicateMonths_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.DuplicateMonths = radDuplicateMonths.Checked;
            Settings.Default.Save();
        }

        private void radDuplicateWeeks_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.DuplicateMonths = !radDuplicateWeeks.Checked;
            Settings.Default.Save();
        }

        private void chkChangeEmployeeNumber_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.UpdateEmployeeNumber = chkChangeEmployeeNumber.Checked;
            Settings.Default.Save();
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_bSyncRunning)
                {
                    SyncEmployees();
                    MessageBox.Show(@"Sync complete");
                }
                else
                {
                    MessageBox.Show(@"Sync already running");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + @"\n\n" + ex);
            }
        }

        private static DateTime GetHighestDt(DateTime dateTimeCurrent)
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
