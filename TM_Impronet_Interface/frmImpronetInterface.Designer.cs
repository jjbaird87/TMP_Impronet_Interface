namespace TM_Impronet_Interface
{
    partial class frmImpronetInterface
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnConnect = new System.Windows.Forms.Button();
            this.dtToDate = new System.Windows.Forms.DateTimePicker();
            this.dtFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.grpImportSettings = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radEmployeeNum = new System.Windows.Forms.RadioButton();
            this.radMstsq = new System.Windows.Forms.RadioButton();
            this.radIXP220 = new System.Windows.Forms.RadioButton();
            this.radIXP400 = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbDirection = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReaderAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.btnBrowseOutput = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.btnBrowseFbDatabase = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFbDatabasePath = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnFetchReaders = new System.Windows.Forms.Button();
            this.gridMappings = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.lblCountdown = new System.Windows.Forms.Label();
            this.chkEnableTimer = new System.Windows.Forms.CheckBox();
            this.chkRunnerEnabled = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRunner = new System.Windows.Forms.TextBox();
            this.tmrMain = new System.Windows.Forms.Timer(this.components);
            this.tmrSeconds = new System.Windows.Forms.Timer(this.components);
            this.tabEmail = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.txtTmpDatabasePath = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radFirebird = new System.Windows.Forms.RadioButton();
            this.txtSqlConnectionString = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.radSql = new System.Windows.Forms.RadioButton();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label14 = new System.Windows.Forms.Label();
            this.lblSyncCountDown = new System.Windows.Forms.Label();
            this.chkSyncTimerEnabled = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSyncInterval = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.radUseDepartmentMapping = new System.Windows.Forms.RadioButton();
            this.radUseStandardMapping = new System.Windows.Forms.RadioButton();
            this.tabctrlMappings = new System.Windows.Forms.TabControl();
            this.tabpgStandardMapping = new System.Windows.Forms.TabPage();
            this.tabpgDepartmentMapping = new System.Windows.Forms.TabPage();
            this.btnDepMappingsDeselectAll = new System.Windows.Forms.Button();
            this.btnDepMappingsSelectAll = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.treeDepMappings = new System.Windows.Forms.TreeView();
            this.btnDepFetchMappings = new System.Windows.Forms.Button();
            this.btnDepMappingsRefresh = new System.Windows.Forms.Button();
            this.btnDepMappingsSave = new System.Windows.Forms.Button();
            this.txtDepMappingAccessControlCode = new System.Windows.Forms.TextBox();
            this.txtDepMappingTimeAndAttendanceCode = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDeselctAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnRefreshDepartments = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.chkDepartments = new System.Windows.Forms.CheckedListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.chkSyncMstsq = new System.Windows.Forms.CheckBox();
            this.chkAlwaysSync = new System.Windows.Forms.CheckBox();
            this.btnSync = new System.Windows.Forms.Button();
            this.chkSyncAccessControlDevices = new System.Windows.Forms.CheckBox();
            this.chkSynEmployees = new System.Windows.Forms.CheckBox();
            this.tabPgEmail = new System.Windows.Forms.TabPage();
            this.btnTestEmail = new System.Windows.Forms.Button();
            this.txtToEmailAddress = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtEmailAddress = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtSmtpPort = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtSmtpHost = new System.Windows.Forms.TextBox();
            this.chkEnableEmail = new System.Windows.Forms.CheckBox();
            this.chkSSL = new System.Windows.Forms.CheckBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.radDuplicateWeeks = new System.Windows.Forms.RadioButton();
            this.radDuplicateMonths = new System.Windows.Forms.RadioButton();
            this.label29 = new System.Windows.Forms.Label();
            this.dtDuplicateScheduleStartDate = new System.Windows.Forms.DateTimePicker();
            this.label27 = new System.Windows.Forms.Label();
            this.numScheduleMonths = new System.Windows.Forms.NumericUpDown();
            this.btnDuplicateRoster = new System.Windows.Forms.Button();
            this.btnGetRosters = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.cmbRosterStartMonth = new System.Windows.Forms.ComboBox();
            this.cmbRosters = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.chkChangeEmployeeNumber = new System.Windows.Forms.CheckBox();
            this.grpImportSettings.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMappings)).BeginInit();
            this.tabEmail.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabctrlMappings.SuspendLayout();
            this.tabpgStandardMapping.SuspendLayout();
            this.tabpgDepartmentMapping.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPgEmail.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numScheduleMonths)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(8, 109);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(790, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Test Connection";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // dtToDate
            // 
            this.dtToDate.Location = new System.Drawing.Point(16, 92);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.Size = new System.Drawing.Size(153, 20);
            this.dtToDate.TabIndex = 1;
            // 
            // dtFromDate
            // 
            this.dtFromDate.Location = new System.Drawing.Point(16, 48);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.Size = new System.Drawing.Size(153, 20);
            this.dtFromDate.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "From Date:";
            // 
            // grpImportSettings
            // 
            this.grpImportSettings.Controls.Add(this.groupBox3);
            this.grpImportSettings.Controls.Add(this.radIXP220);
            this.grpImportSettings.Controls.Add(this.radIXP400);
            this.grpImportSettings.Controls.Add(this.label6);
            this.grpImportSettings.Controls.Add(this.cmbDirection);
            this.grpImportSettings.Controls.Add(this.label5);
            this.grpImportSettings.Controls.Add(this.txtReaderAddress);
            this.grpImportSettings.Controls.Add(this.label2);
            this.grpImportSettings.Controls.Add(this.dtToDate);
            this.grpImportSettings.Controls.Add(this.dtFromDate);
            this.grpImportSettings.Controls.Add(this.label1);
            this.grpImportSettings.Location = new System.Drawing.Point(12, 12);
            this.grpImportSettings.Name = "grpImportSettings";
            this.grpImportSettings.Size = new System.Drawing.Size(184, 451);
            this.grpImportSettings.TabIndex = 4;
            this.grpImportSettings.TabStop = false;
            this.grpImportSettings.Text = "Import Settings";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radEmployeeNum);
            this.groupBox3.Controls.Add(this.radMstsq);
            this.groupBox3.Location = new System.Drawing.Point(16, 292);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(153, 110);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Export Field";
            // 
            // radEmployeeNum
            // 
            this.radEmployeeNum.AutoSize = true;
            this.radEmployeeNum.Checked = true;
            this.radEmployeeNum.Location = new System.Drawing.Point(6, 42);
            this.radEmployeeNum.Name = "radEmployeeNum";
            this.radEmployeeNum.Size = new System.Drawing.Size(111, 17);
            this.radEmployeeNum.TabIndex = 21;
            this.radEmployeeNum.TabStop = true;
            this.radEmployeeNum.Text = "Employee Number";
            this.radEmployeeNum.UseVisualStyleBackColor = true;
            // 
            // radMstsq
            // 
            this.radMstsq.AutoSize = true;
            this.radMstsq.Location = new System.Drawing.Point(6, 19);
            this.radMstsq.Name = "radMstsq";
            this.radMstsq.Size = new System.Drawing.Size(63, 17);
            this.radMstsq.TabIndex = 20;
            this.radMstsq.Text = "MSTSQ";
            this.radMstsq.UseVisualStyleBackColor = true;
            // 
            // radIXP220
            // 
            this.radIXP220.AutoSize = true;
            this.radIXP220.Location = new System.Drawing.Point(16, 254);
            this.radIXP220.Name = "radIXP220";
            this.radIXP220.Size = new System.Drawing.Size(63, 17);
            this.radIXP220.TabIndex = 19;
            this.radIXP220.Text = "IXP 220";
            this.radIXP220.UseVisualStyleBackColor = true;
            // 
            // radIXP400
            // 
            this.radIXP400.AutoSize = true;
            this.radIXP400.Checked = true;
            this.radIXP400.Location = new System.Drawing.Point(16, 231);
            this.radIXP400.Name = "radIXP400";
            this.radIXP400.Size = new System.Drawing.Size(63, 17);
            this.radIXP400.TabIndex = 18;
            this.radIXP400.TabStop = true;
            this.radIXP400.Text = "IXP 400";
            this.radIXP400.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Direction:";
            // 
            // cmbDirection
            // 
            this.cmbDirection.DisplayMember = "0";
            this.cmbDirection.FormattingEnabled = true;
            this.cmbDirection.Items.AddRange(new object[] {
            "In",
            "Out",
            "All"});
            this.cmbDirection.Location = new System.Drawing.Point(16, 186);
            this.cmbDirection.Name = "cmbDirection";
            this.cmbDirection.Size = new System.Drawing.Size(153, 21);
            this.cmbDirection.TabIndex = 14;
            this.cmbDirection.Text = "All";
            this.cmbDirection.ValueMember = "2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Reader Address:";
            // 
            // txtReaderAddress
            // 
            this.txtReaderAddress.Location = new System.Drawing.Point(16, 138);
            this.txtReaderAddress.Name = "txtReaderAddress";
            this.txtReaderAddress.Size = new System.Drawing.Size(153, 20);
            this.txtReaderAddress.TabIndex = 12;
            this.txtReaderAddress.TextChanged += new System.EventHandler(this.txtReaderAddress_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "To Date:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Export Interval (seconds)";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(9, 93);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(153, 20);
            this.txtInterval.TabIndex = 16;
            this.txtInterval.Text = "30";
            this.txtInterval.TextChanged += new System.EventHandler(this.txtInterval_TextChanged);
            // 
            // btnBrowseOutput
            // 
            this.btnBrowseOutput.Location = new System.Drawing.Point(768, 35);
            this.btnBrowseOutput.Name = "btnBrowseOutput";
            this.btnBrowseOutput.Size = new System.Drawing.Size(28, 20);
            this.btnBrowseOutput.TabIndex = 11;
            this.btnBrowseOutput.Text = "...";
            this.btnBrowseOutput.UseVisualStyleBackColor = true;
            this.btnBrowseOutput.Click += new System.EventHandler(this.btnBrowseOutput_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "*.dat Output Path:";
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(9, 36);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(753, 20);
            this.txtOutputPath.TabIndex = 9;
            this.txtOutputPath.TextChanged += new System.EventHandler(this.txtOutputPath_TextChanged);
            // 
            // btnBrowseFbDatabase
            // 
            this.btnBrowseFbDatabase.Enabled = false;
            this.btnBrowseFbDatabase.Location = new System.Drawing.Point(338, 79);
            this.btnBrowseFbDatabase.Name = "btnBrowseFbDatabase";
            this.btnBrowseFbDatabase.Size = new System.Drawing.Size(28, 20);
            this.btnBrowseFbDatabase.TabIndex = 8;
            this.btnBrowseFbDatabase.Text = "...";
            this.btnBrowseFbDatabase.UseVisualStyleBackColor = true;
            this.btnBrowseFbDatabase.Click += new System.EventHandler(this.btnBrowseInput_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Database Path:";
            // 
            // txtFbDatabasePath
            // 
            this.txtFbDatabasePath.Enabled = false;
            this.txtFbDatabasePath.Location = new System.Drawing.Point(8, 79);
            this.txtFbDatabasePath.Name = "txtFbDatabasePath";
            this.txtFbDatabasePath.Size = new System.Drawing.Size(324, 20);
            this.txtFbDatabasePath.TabIndex = 6;
            this.txtFbDatabasePath.TextChanged += new System.EventHandler(this.txtDatabasePath_TextChanged);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(13, 469);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(1019, 23);
            this.btnStart.TabIndex = 16;
            this.btnStart.Text = "Start Export";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 516);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1048, 22);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(98, 17);
            this.toolStripStatusLabel1.Text = "0 record(s) found";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(168, 6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 22;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(87, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnFetchReaders
            // 
            this.btnFetchReaders.Location = new System.Drawing.Point(6, 6);
            this.btnFetchReaders.Name = "btnFetchReaders";
            this.btnFetchReaders.Size = new System.Drawing.Size(75, 23);
            this.btnFetchReaders.TabIndex = 12;
            this.btnFetchReaders.Text = "Fetch";
            this.btnFetchReaders.UseVisualStyleBackColor = true;
            this.btnFetchReaders.Click += new System.EventHandler(this.button1_Click);
            // 
            // gridMappings
            // 
            this.gridMappings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMappings.Location = new System.Drawing.Point(6, 35);
            this.gridMappings.Name = "gridMappings";
            this.gridMappings.Size = new System.Drawing.Size(796, 325);
            this.gridMappings.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 182);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 20);
            this.label9.TabIndex = 15;
            this.label9.Text = "Countdown:";
            // 
            // lblCountdown
            // 
            this.lblCountdown.AutoSize = true;
            this.lblCountdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountdown.Location = new System.Drawing.Point(6, 214);
            this.lblCountdown.Name = "lblCountdown";
            this.lblCountdown.Size = new System.Drawing.Size(194, 39);
            this.lblCountdown.TabIndex = 14;
            this.lblCountdown.Text = "0 second(s)";
            // 
            // chkEnableTimer
            // 
            this.chkEnableTimer.AutoSize = true;
            this.chkEnableTimer.Location = new System.Drawing.Point(134, 54);
            this.chkEnableTimer.Name = "chkEnableTimer";
            this.chkEnableTimer.Size = new System.Drawing.Size(133, 17);
            this.chkEnableTimer.TabIndex = 13;
            this.chkEnableTimer.Text = "Export Timer Enabled?";
            this.chkEnableTimer.UseVisualStyleBackColor = true;
            this.chkEnableTimer.CheckedChanged += new System.EventHandler(this.chkEnableTimer_CheckedChanged);
            // 
            // chkRunnerEnabled
            // 
            this.chkRunnerEnabled.AutoSize = true;
            this.chkRunnerEnabled.Location = new System.Drawing.Point(10, 54);
            this.chkRunnerEnabled.Name = "chkRunnerEnabled";
            this.chkRunnerEnabled.Size = new System.Drawing.Size(109, 17);
            this.chkRunnerEnabled.TabIndex = 12;
            this.chkRunnerEnabled.Text = "Runner Enabled?";
            this.chkRunnerEnabled.UseVisualStyleBackColor = true;
            this.chkRunnerEnabled.CheckedChanged += new System.EventHandler(this.chkRunnerEnabled_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(336, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 20);
            this.button1.TabIndex = 11;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Exe Path:";
            // 
            // txtRunner
            // 
            this.txtRunner.Location = new System.Drawing.Point(9, 28);
            this.txtRunner.Name = "txtRunner";
            this.txtRunner.Size = new System.Drawing.Size(324, 20);
            this.txtRunner.TabIndex = 9;
            this.txtRunner.TextChanged += new System.EventHandler(this.txtRunner_TextChanged);
            // 
            // tmrMain
            // 
            this.tmrMain.Interval = 1000;
            this.tmrMain.Tick += new System.EventHandler(this.tmrMain_Tick);
            // 
            // tmrSeconds
            // 
            this.tmrSeconds.Interval = 1000;
            this.tmrSeconds.Tick += new System.EventHandler(this.tmrSeconds_Tick);
            // 
            // tabEmail
            // 
            this.tabEmail.Controls.Add(this.tabPage4);
            this.tabEmail.Controls.Add(this.tabPage5);
            this.tabEmail.Controls.Add(this.tabPage1);
            this.tabEmail.Controls.Add(this.tabPage2);
            this.tabEmail.Controls.Add(this.tabPage3);
            this.tabEmail.Controls.Add(this.tabPgEmail);
            this.tabEmail.Controls.Add(this.tabPage6);
            this.tabEmail.Location = new System.Drawing.Point(212, 13);
            this.tabEmail.Name = "tabEmail";
            this.tabEmail.SelectedIndex = 0;
            this.tabEmail.Size = new System.Drawing.Size(824, 450);
            this.tabEmail.TabIndex = 21;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox4);
            this.tabPage4.Controls.Add(this.groupBox2);
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(816, 424);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Database Settings";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.txtOutputPath);
            this.groupBox4.Controls.Add(this.btnBrowseOutput);
            this.groupBox4.Location = new System.Drawing.Point(6, 281);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(804, 80);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Datafile Output path:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.txtTmpDatabasePath);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Location = new System.Drawing.Point(6, 158);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(804, 117);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Time Manager Platinum Database";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(9, 77);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(790, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Test Connection";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtTmpDatabasePath
            // 
            this.txtTmpDatabasePath.Location = new System.Drawing.Point(8, 46);
            this.txtTmpDatabasePath.Name = "txtTmpDatabasePath";
            this.txtTmpDatabasePath.Size = new System.Drawing.Size(754, 20);
            this.txtTmpDatabasePath.TabIndex = 9;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(768, 46);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(28, 20);
            this.button2.TabIndex = 11;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Database Path:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radFirebird);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.txtSqlConnectionString);
            this.groupBox1.Controls.Add(this.txtFbDatabasePath);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.btnBrowseFbDatabase);
            this.groupBox1.Controls.Add(this.radSql);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(804, 146);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Impro Database";
            // 
            // radFirebird
            // 
            this.radFirebird.AutoSize = true;
            this.radFirebird.Location = new System.Drawing.Point(11, 32);
            this.radFirebird.Name = "radFirebird";
            this.radFirebird.Size = new System.Drawing.Size(108, 17);
            this.radFirebird.TabIndex = 12;
            this.radFirebird.Text = "Firebird Database";
            this.radFirebird.UseVisualStyleBackColor = true;
            this.radFirebird.CheckedChanged += new System.EventHandler(this.radFirebird_CheckedChanged);
            // 
            // txtSqlConnectionString
            // 
            this.txtSqlConnectionString.Enabled = false;
            this.txtSqlConnectionString.Location = new System.Drawing.Point(390, 80);
            this.txtSqlConnectionString.Name = "txtSqlConnectionString";
            this.txtSqlConnectionString.Size = new System.Drawing.Size(406, 20);
            this.txtSqlConnectionString.TabIndex = 15;
            this.txtSqlConnectionString.TextChanged += new System.EventHandler(this.txtSqlConnectionString_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(387, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "SQL Connection String:";
            // 
            // radSql
            // 
            this.radSql.AutoSize = true;
            this.radSql.Location = new System.Drawing.Point(390, 32);
            this.radSql.Name = "radSql";
            this.radSql.Size = new System.Drawing.Size(95, 17);
            this.radSql.TabIndex = 13;
            this.radSql.Text = "SQL Database";
            this.radSql.UseVisualStyleBackColor = true;
            this.radSql.CheckedChanged += new System.EventHandler(this.radSql_CheckedChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label14);
            this.tabPage5.Controls.Add(this.lblSyncCountDown);
            this.tabPage5.Controls.Add(this.chkSyncTimerEnabled);
            this.tabPage5.Controls.Add(this.label13);
            this.tabPage5.Controls.Add(this.txtSyncInterval);
            this.tabPage5.Controls.Add(this.label9);
            this.tabPage5.Controls.Add(this.lblCountdown);
            this.tabPage5.Controls.Add(this.label7);
            this.tabPage5.Controls.Add(this.label8);
            this.tabPage5.Controls.Add(this.txtInterval);
            this.tabPage5.Controls.Add(this.txtRunner);
            this.tabPage5.Controls.Add(this.chkEnableTimer);
            this.tabPage5.Controls.Add(this.button1);
            this.tabPage5.Controls.Add(this.chkRunnerEnabled);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(816, 424);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Runner Settings";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(254, 182);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(211, 20);
            this.label14.TabIndex = 22;
            this.label14.Text = "Synchronization Countdown:";
            // 
            // lblSyncCountDown
            // 
            this.lblSyncCountDown.AutoSize = true;
            this.lblSyncCountDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSyncCountDown.Location = new System.Drawing.Point(254, 214);
            this.lblSyncCountDown.Name = "lblSyncCountDown";
            this.lblSyncCountDown.Size = new System.Drawing.Size(194, 39);
            this.lblSyncCountDown.TabIndex = 21;
            this.lblSyncCountDown.Text = "0 second(s)";
            // 
            // chkSyncTimerEnabled
            // 
            this.chkSyncTimerEnabled.AutoSize = true;
            this.chkSyncTimerEnabled.Location = new System.Drawing.Point(273, 52);
            this.chkSyncTimerEnabled.Name = "chkSyncTimerEnabled";
            this.chkSyncTimerEnabled.Size = new System.Drawing.Size(153, 17);
            this.chkSyncTimerEnabled.TabIndex = 20;
            this.chkSyncTimerEnabled.Text = "Data Sync Timer Enabled?";
            this.chkSyncTimerEnabled.UseVisualStyleBackColor = true;
            this.chkSyncTimerEnabled.CheckedChanged += new System.EventHandler(this.chkSyncTimerEnabled_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 122);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(169, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "Synchronization Interval (seconds)";
            // 
            // txtSyncInterval
            // 
            this.txtSyncInterval.Location = new System.Drawing.Point(10, 139);
            this.txtSyncInterval.Name = "txtSyncInterval";
            this.txtSyncInterval.Size = new System.Drawing.Size(153, 20);
            this.txtSyncInterval.TabIndex = 18;
            this.txtSyncInterval.Text = "30";
            this.txtSyncInterval.TextChanged += new System.EventHandler(this.txtSyncInterval_TextChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.radUseDepartmentMapping);
            this.tabPage1.Controls.Add(this.radUseStandardMapping);
            this.tabPage1.Controls.Add(this.tabctrlMappings);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(816, 424);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Device Mappings";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // radUseDepartmentMapping
            // 
            this.radUseDepartmentMapping.AutoSize = true;
            this.radUseDepartmentMapping.Location = new System.Drawing.Point(128, 7);
            this.radUseDepartmentMapping.Name = "radUseDepartmentMapping";
            this.radUseDepartmentMapping.Size = new System.Drawing.Size(124, 17);
            this.radUseDepartmentMapping.TabIndex = 25;
            this.radUseDepartmentMapping.TabStop = true;
            this.radUseDepartmentMapping.Text = "Department Mapping";
            this.radUseDepartmentMapping.UseVisualStyleBackColor = true;
            this.radUseDepartmentMapping.CheckedChanged += new System.EventHandler(this.radUseDepartmentMapping_CheckedChanged);
            // 
            // radUseStandardMapping
            // 
            this.radUseStandardMapping.AutoSize = true;
            this.radUseStandardMapping.Location = new System.Drawing.Point(6, 7);
            this.radUseStandardMapping.Name = "radUseStandardMapping";
            this.radUseStandardMapping.Size = new System.Drawing.Size(112, 17);
            this.radUseStandardMapping.TabIndex = 24;
            this.radUseStandardMapping.TabStop = true;
            this.radUseStandardMapping.Text = "Standard Mapping";
            this.radUseStandardMapping.UseVisualStyleBackColor = true;
            this.radUseStandardMapping.CheckedChanged += new System.EventHandler(this.radUseStandardMapping_CheckedChanged);
            // 
            // tabctrlMappings
            // 
            this.tabctrlMappings.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabctrlMappings.Controls.Add(this.tabpgStandardMapping);
            this.tabctrlMappings.Controls.Add(this.tabpgDepartmentMapping);
            this.tabctrlMappings.Location = new System.Drawing.Point(0, 30);
            this.tabctrlMappings.Name = "tabctrlMappings";
            this.tabctrlMappings.SelectedIndex = 0;
            this.tabctrlMappings.Size = new System.Drawing.Size(816, 394);
            this.tabctrlMappings.TabIndex = 23;
            // 
            // tabpgStandardMapping
            // 
            this.tabpgStandardMapping.Controls.Add(this.btnFetchReaders);
            this.tabpgStandardMapping.Controls.Add(this.gridMappings);
            this.tabpgStandardMapping.Controls.Add(this.btnSave);
            this.tabpgStandardMapping.Controls.Add(this.btnRefresh);
            this.tabpgStandardMapping.Location = new System.Drawing.Point(4, 25);
            this.tabpgStandardMapping.Name = "tabpgStandardMapping";
            this.tabpgStandardMapping.Padding = new System.Windows.Forms.Padding(3);
            this.tabpgStandardMapping.Size = new System.Drawing.Size(808, 365);
            this.tabpgStandardMapping.TabIndex = 0;
            this.tabpgStandardMapping.Text = "tabPage6";
            this.tabpgStandardMapping.UseVisualStyleBackColor = true;
            // 
            // tabpgDepartmentMapping
            // 
            this.tabpgDepartmentMapping.Controls.Add(this.btnDepMappingsDeselectAll);
            this.tabpgDepartmentMapping.Controls.Add(this.btnDepMappingsSelectAll);
            this.tabpgDepartmentMapping.Controls.Add(this.label17);
            this.tabpgDepartmentMapping.Controls.Add(this.treeDepMappings);
            this.tabpgDepartmentMapping.Controls.Add(this.btnDepFetchMappings);
            this.tabpgDepartmentMapping.Controls.Add(this.btnDepMappingsRefresh);
            this.tabpgDepartmentMapping.Controls.Add(this.btnDepMappingsSave);
            this.tabpgDepartmentMapping.Controls.Add(this.txtDepMappingAccessControlCode);
            this.tabpgDepartmentMapping.Controls.Add(this.txtDepMappingTimeAndAttendanceCode);
            this.tabpgDepartmentMapping.Controls.Add(this.label16);
            this.tabpgDepartmentMapping.Controls.Add(this.label15);
            this.tabpgDepartmentMapping.Location = new System.Drawing.Point(4, 25);
            this.tabpgDepartmentMapping.Name = "tabpgDepartmentMapping";
            this.tabpgDepartmentMapping.Padding = new System.Windows.Forms.Padding(3);
            this.tabpgDepartmentMapping.Size = new System.Drawing.Size(808, 365);
            this.tabpgDepartmentMapping.TabIndex = 1;
            this.tabpgDepartmentMapping.Text = "tabPage7";
            this.tabpgDepartmentMapping.UseVisualStyleBackColor = true;
            // 
            // btnDepMappingsDeselectAll
            // 
            this.btnDepMappingsDeselectAll.Location = new System.Drawing.Point(598, 63);
            this.btnDepMappingsDeselectAll.Name = "btnDepMappingsDeselectAll";
            this.btnDepMappingsDeselectAll.Size = new System.Drawing.Size(204, 23);
            this.btnDepMappingsDeselectAll.TabIndex = 10;
            this.btnDepMappingsDeselectAll.Text = "Deselect All";
            this.btnDepMappingsDeselectAll.UseVisualStyleBackColor = true;
            this.btnDepMappingsDeselectAll.Click += new System.EventHandler(this.btnDepMappingsDeselectAll_Click);
            // 
            // btnDepMappingsSelectAll
            // 
            this.btnDepMappingsSelectAll.Location = new System.Drawing.Point(388, 63);
            this.btnDepMappingsSelectAll.Name = "btnDepMappingsSelectAll";
            this.btnDepMappingsSelectAll.Size = new System.Drawing.Size(204, 23);
            this.btnDepMappingsSelectAll.TabIndex = 9;
            this.btnDepMappingsSelectAll.Text = "Select All";
            this.btnDepMappingsSelectAll.UseVisualStyleBackColor = true;
            this.btnDepMappingsSelectAll.Click += new System.EventHandler(this.btnDepMappingsSelectAll_Click);
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(6, 55);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(323, 31);
            this.label17.TabIndex = 8;
            this.label17.Text = "Select the time and attendance devices per department: (Unselected devices are tr" +
    "eated as access control devices)";
            // 
            // treeDepMappings
            // 
            this.treeDepMappings.Location = new System.Drawing.Point(6, 92);
            this.treeDepMappings.Name = "treeDepMappings";
            this.treeDepMappings.Size = new System.Drawing.Size(796, 265);
            this.treeDepMappings.TabIndex = 7;
            this.treeDepMappings.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeDepMappings_AfterCheck);
            // 
            // btnDepFetchMappings
            // 
            this.btnDepFetchMappings.Location = new System.Drawing.Point(6, 11);
            this.btnDepFetchMappings.Name = "btnDepFetchMappings";
            this.btnDepFetchMappings.Size = new System.Drawing.Size(101, 23);
            this.btnDepFetchMappings.TabIndex = 6;
            this.btnDepFetchMappings.Text = "Fetch Mappings";
            this.btnDepFetchMappings.UseVisualStyleBackColor = true;
            this.btnDepFetchMappings.Click += new System.EventHandler(this.btnDepFetchMappings_Click);
            // 
            // btnDepMappingsRefresh
            // 
            this.btnDepMappingsRefresh.Location = new System.Drawing.Point(220, 11);
            this.btnDepMappingsRefresh.Name = "btnDepMappingsRefresh";
            this.btnDepMappingsRefresh.Size = new System.Drawing.Size(101, 23);
            this.btnDepMappingsRefresh.TabIndex = 5;
            this.btnDepMappingsRefresh.Text = "Refresh";
            this.btnDepMappingsRefresh.UseVisualStyleBackColor = true;
            // 
            // btnDepMappingsSave
            // 
            this.btnDepMappingsSave.Location = new System.Drawing.Point(113, 11);
            this.btnDepMappingsSave.Name = "btnDepMappingsSave";
            this.btnDepMappingsSave.Size = new System.Drawing.Size(101, 23);
            this.btnDepMappingsSave.TabIndex = 4;
            this.btnDepMappingsSave.Text = "Save";
            this.btnDepMappingsSave.UseVisualStyleBackColor = true;
            this.btnDepMappingsSave.Click += new System.EventHandler(this.btnDepMappingsSave_Click);
            // 
            // txtDepMappingAccessControlCode
            // 
            this.txtDepMappingAccessControlCode.Location = new System.Drawing.Point(649, 32);
            this.txtDepMappingAccessControlCode.Name = "txtDepMappingAccessControlCode";
            this.txtDepMappingAccessControlCode.Size = new System.Drawing.Size(153, 20);
            this.txtDepMappingAccessControlCode.TabIndex = 3;
            this.txtDepMappingAccessControlCode.TextChanged += new System.EventHandler(this.txtDepMappingAccessControlCode_TextChanged);
            // 
            // txtDepMappingTimeAndAttendanceCode
            // 
            this.txtDepMappingTimeAndAttendanceCode.Location = new System.Drawing.Point(649, 6);
            this.txtDepMappingTimeAndAttendanceCode.Name = "txtDepMappingTimeAndAttendanceCode";
            this.txtDepMappingTimeAndAttendanceCode.Size = new System.Drawing.Size(153, 20);
            this.txtDepMappingTimeAndAttendanceCode.TabIndex = 2;
            this.txtDepMappingTimeAndAttendanceCode.TextChanged += new System.EventHandler(this.txtDepMappingTimeAndAttendanceCode_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(500, 35);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(146, 13);
            this.label16.TabIndex = 1;
            this.label16.Text = "Access Control Device Code:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(471, 9);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(175, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Time and Attendance device Code:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDeselctAll);
            this.tabPage2.Controls.Add(this.btnSelectAll);
            this.tabPage2.Controls.Add(this.btnRefreshDepartments);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.chkDepartments);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(816, 424);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Department Selection";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDeselctAll
            // 
            this.btnDeselctAll.Location = new System.Drawing.Point(233, 53);
            this.btnDeselctAll.Name = "btnDeselctAll";
            this.btnDeselctAll.Size = new System.Drawing.Size(204, 23);
            this.btnDeselctAll.TabIndex = 5;
            this.btnDeselctAll.Text = "Deselect All";
            this.btnDeselctAll.UseVisualStyleBackColor = true;
            this.btnDeselctAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(23, 53);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(204, 23);
            this.btnSelectAll.TabIndex = 4;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnRefreshDepartments
            // 
            this.btnRefreshDepartments.Location = new System.Drawing.Point(404, 14);
            this.btnRefreshDepartments.Name = "btnRefreshDepartments";
            this.btnRefreshDepartments.Size = new System.Drawing.Size(397, 23);
            this.btnRefreshDepartments.TabIndex = 3;
            this.btnRefreshDepartments.Text = "Refresh";
            this.btnRefreshDepartments.UseVisualStyleBackColor = true;
            this.btnRefreshDepartments.Click += new System.EventHandler(this.btnRefreshDepartments_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 37);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(280, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Please select the departments that you would like to sync:";
            // 
            // chkDepartments
            // 
            this.chkDepartments.FormattingEnabled = true;
            this.chkDepartments.Location = new System.Drawing.Point(23, 78);
            this.chkDepartments.Name = "chkDepartments";
            this.chkDepartments.Size = new System.Drawing.Size(779, 334);
            this.chkDepartments.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.chkChangeEmployeeNumber);
            this.tabPage3.Controls.Add(this.chkSyncMstsq);
            this.tabPage3.Controls.Add(this.chkAlwaysSync);
            this.tabPage3.Controls.Add(this.btnSync);
            this.tabPage3.Controls.Add(this.chkSyncAccessControlDevices);
            this.tabPage3.Controls.Add(this.chkSynEmployees);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(816, 424);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Sync Options";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // chkSyncMstsq
            // 
            this.chkSyncMstsq.AutoSize = true;
            this.chkSyncMstsq.Location = new System.Drawing.Point(199, 53);
            this.chkSyncMstsq.Name = "chkSyncMstsq";
            this.chkSyncMstsq.Size = new System.Drawing.Size(224, 17);
            this.chkSyncMstsq.TabIndex = 19;
            this.chkSyncMstsq.Text = "Use MSTSQ instead of Employee Number";
            this.chkSyncMstsq.UseVisualStyleBackColor = true;
            this.chkSyncMstsq.CheckedChanged += new System.EventHandler(this.chkSyncMstsq_CheckedChanged);
            // 
            // chkAlwaysSync
            // 
            this.chkAlwaysSync.AutoSize = true;
            this.chkAlwaysSync.Location = new System.Drawing.Point(199, 25);
            this.chkAlwaysSync.Name = "chkAlwaysSync";
            this.chkAlwaysSync.Size = new System.Drawing.Size(447, 17);
            this.chkAlwaysSync.TabIndex = 18;
            this.chkAlwaysSync.Text = "Always Sync ALL companies and departments (Department Selection Tab will be ignor" +
    "ed)";
            this.chkAlwaysSync.UseVisualStyleBackColor = true;
            this.chkAlwaysSync.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(19, 89);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(785, 23);
            this.btnSync.TabIndex = 17;
            this.btnSync.Text = "Synchronize now";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // chkSyncAccessControlDevices
            // 
            this.chkSyncAccessControlDevices.AutoSize = true;
            this.chkSyncAccessControlDevices.Location = new System.Drawing.Point(19, 53);
            this.chkSyncAccessControlDevices.Name = "chkSyncAccessControlDevices";
            this.chkSyncAccessControlDevices.Size = new System.Drawing.Size(164, 17);
            this.chkSyncAccessControlDevices.TabIndex = 1;
            this.chkSyncAccessControlDevices.Text = "Sync Access Control devices";
            this.chkSyncAccessControlDevices.UseVisualStyleBackColor = true;
            this.chkSyncAccessControlDevices.CheckedChanged += new System.EventHandler(this.chkSyncAccessControlDevices_CheckedChanged);
            // 
            // chkSynEmployees
            // 
            this.chkSynEmployees.AutoSize = true;
            this.chkSynEmployees.Location = new System.Drawing.Point(19, 25);
            this.chkSynEmployees.Name = "chkSynEmployees";
            this.chkSynEmployees.Size = new System.Drawing.Size(174, 17);
            this.chkSynEmployees.TabIndex = 0;
            this.chkSynEmployees.Text = "Sync TMP database with Impro";
            this.chkSynEmployees.UseVisualStyleBackColor = true;
            this.chkSynEmployees.CheckedChanged += new System.EventHandler(this.chkSynEmployees_CheckedChanged);
            // 
            // tabPgEmail
            // 
            this.tabPgEmail.Controls.Add(this.btnTestEmail);
            this.tabPgEmail.Controls.Add(this.txtToEmailAddress);
            this.tabPgEmail.Controls.Add(this.label23);
            this.tabPgEmail.Controls.Add(this.txtEmailAddress);
            this.tabPgEmail.Controls.Add(this.txtUsername);
            this.tabPgEmail.Controls.Add(this.txtSmtpPort);
            this.tabPgEmail.Controls.Add(this.txtPassword);
            this.tabPgEmail.Controls.Add(this.txtSmtpHost);
            this.tabPgEmail.Controls.Add(this.chkEnableEmail);
            this.tabPgEmail.Controls.Add(this.chkSSL);
            this.tabPgEmail.Controls.Add(this.label22);
            this.tabPgEmail.Controls.Add(this.label21);
            this.tabPgEmail.Controls.Add(this.label20);
            this.tabPgEmail.Controls.Add(this.label19);
            this.tabPgEmail.Controls.Add(this.label18);
            this.tabPgEmail.Location = new System.Drawing.Point(4, 22);
            this.tabPgEmail.Name = "tabPgEmail";
            this.tabPgEmail.Padding = new System.Windows.Forms.Padding(3);
            this.tabPgEmail.Size = new System.Drawing.Size(816, 424);
            this.tabPgEmail.TabIndex = 5;
            this.tabPgEmail.Text = "Email Options";
            this.tabPgEmail.UseVisualStyleBackColor = true;
            // 
            // btnTestEmail
            // 
            this.btnTestEmail.Location = new System.Drawing.Point(21, 231);
            this.btnTestEmail.Name = "btnTestEmail";
            this.btnTestEmail.Size = new System.Drawing.Size(774, 23);
            this.btnTestEmail.TabIndex = 14;
            this.btnTestEmail.Text = "Send Test Email";
            this.btnTestEmail.UseVisualStyleBackColor = true;
            this.btnTestEmail.Click += new System.EventHandler(this.btnTestEmail_Click);
            // 
            // txtToEmailAddress
            // 
            this.txtToEmailAddress.Location = new System.Drawing.Point(548, 105);
            this.txtToEmailAddress.Name = "txtToEmailAddress";
            this.txtToEmailAddress.Size = new System.Drawing.Size(247, 20);
            this.txtToEmailAddress.TabIndex = 13;
            this.txtToEmailAddress.TextChanged += new System.EventHandler(this.txtToEmailAddress_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(415, 108);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(92, 13);
            this.label23.TabIndex = 12;
            this.label23.Text = "To Email Address:";
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.Location = new System.Drawing.Point(151, 105);
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Size = new System.Drawing.Size(247, 20);
            this.txtEmailAddress.TabIndex = 11;
            this.txtEmailAddress.TextChanged += new System.EventHandler(this.txtEmailAddress_TextChanged);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(151, 131);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(247, 20);
            this.txtUsername.TabIndex = 10;
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // txtSmtpPort
            // 
            this.txtSmtpPort.Location = new System.Drawing.Point(151, 76);
            this.txtSmtpPort.Name = "txtSmtpPort";
            this.txtSmtpPort.Size = new System.Drawing.Size(247, 20);
            this.txtSmtpPort.TabIndex = 9;
            this.txtSmtpPort.Text = "0";
            this.txtSmtpPort.TextChanged += new System.EventHandler(this.txtSmtpPort_TextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(151, 159);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(247, 20);
            this.txtPassword.TabIndex = 8;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // txtSmtpHost
            // 
            this.txtSmtpHost.Location = new System.Drawing.Point(151, 50);
            this.txtSmtpHost.Name = "txtSmtpHost";
            this.txtSmtpHost.Size = new System.Drawing.Size(247, 20);
            this.txtSmtpHost.TabIndex = 7;
            this.txtSmtpHost.TextChanged += new System.EventHandler(this.txtSmtpHost_TextChanged);
            // 
            // chkEnableEmail
            // 
            this.chkEnableEmail.AutoSize = true;
            this.chkEnableEmail.Location = new System.Drawing.Point(21, 15);
            this.chkEnableEmail.Name = "chkEnableEmail";
            this.chkEnableEmail.Size = new System.Drawing.Size(409, 17);
            this.chkEnableEmail.TabIndex = 6;
            this.chkEnableEmail.Text = "Enable E-mail (E-mail will sent when new users are created in the Impro database)" +
    "";
            this.chkEnableEmail.UseVisualStyleBackColor = true;
            this.chkEnableEmail.CheckedChanged += new System.EventHandler(this.chkEnableEmail_CheckedChanged);
            // 
            // chkSSL
            // 
            this.chkSSL.AutoSize = true;
            this.chkSSL.Location = new System.Drawing.Point(21, 189);
            this.chkSSL.Name = "chkSSL";
            this.chkSSL.Size = new System.Drawing.Size(71, 17);
            this.chkSSL.TabIndex = 5;
            this.chkSSL.Text = "TLS/SSL";
            this.chkSSL.UseVisualStyleBackColor = true;
            this.chkSSL.CheckedChanged += new System.EventHandler(this.chkSSL_CheckedChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(18, 108);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(106, 13);
            this.label22.TabIndex = 4;
            this.label22.Text = "From E-Mail Address:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(18, 134);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(58, 13);
            this.label21.TabIndex = 3;
            this.label21.Text = "Username:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(18, 162);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(56, 13);
            this.label20.TabIndex = 2;
            this.label20.Text = "Password:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(18, 79);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(62, 13);
            this.label19.TabIndex = 1;
            this.label19.Text = "SMTP Port:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(18, 53);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "SMTP Host:";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.radDuplicateWeeks);
            this.tabPage6.Controls.Add(this.radDuplicateMonths);
            this.tabPage6.Controls.Add(this.label29);
            this.tabPage6.Controls.Add(this.dtDuplicateScheduleStartDate);
            this.tabPage6.Controls.Add(this.label27);
            this.tabPage6.Controls.Add(this.numScheduleMonths);
            this.tabPage6.Controls.Add(this.btnDuplicateRoster);
            this.tabPage6.Controls.Add(this.btnGetRosters);
            this.tabPage6.Controls.Add(this.label26);
            this.tabPage6.Controls.Add(this.label25);
            this.tabPage6.Controls.Add(this.cmbRosterStartMonth);
            this.tabPage6.Controls.Add(this.cmbRosters);
            this.tabPage6.Controls.Add(this.label24);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(816, 424);
            this.tabPage6.TabIndex = 6;
            this.tabPage6.Text = "Roster Duplication Tool";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // radDuplicateWeeks
            // 
            this.radDuplicateWeeks.AutoSize = true;
            this.radDuplicateWeeks.Location = new System.Drawing.Point(248, 217);
            this.radDuplicateWeeks.Name = "radDuplicateWeeks";
            this.radDuplicateWeeks.Size = new System.Drawing.Size(65, 17);
            this.radDuplicateWeeks.TabIndex = 13;
            this.radDuplicateWeeks.Text = "Week(s)";
            this.radDuplicateWeeks.UseVisualStyleBackColor = true;
            this.radDuplicateWeeks.CheckedChanged += new System.EventHandler(this.radDuplicateWeeks_CheckedChanged);
            // 
            // radDuplicateMonths
            // 
            this.radDuplicateMonths.AutoSize = true;
            this.radDuplicateMonths.Checked = true;
            this.radDuplicateMonths.Location = new System.Drawing.Point(248, 194);
            this.radDuplicateMonths.Name = "radDuplicateMonths";
            this.radDuplicateMonths.Size = new System.Drawing.Size(66, 17);
            this.radDuplicateMonths.TabIndex = 12;
            this.radDuplicateMonths.TabStop = true;
            this.radDuplicateMonths.Text = "Month(s)";
            this.radDuplicateMonths.UseVisualStyleBackColor = true;
            this.radDuplicateMonths.CheckedChanged += new System.EventHandler(this.radDuplicateMonths_CheckedChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(9, 172);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(58, 13);
            this.label29.TabIndex = 11;
            this.label29.Text = "Start Date:";
            // 
            // dtDuplicateScheduleStartDate
            // 
            this.dtDuplicateScheduleStartDate.Location = new System.Drawing.Point(121, 166);
            this.dtDuplicateScheduleStartDate.Name = "dtDuplicateScheduleStartDate";
            this.dtDuplicateScheduleStartDate.Size = new System.Drawing.Size(200, 20);
            this.dtDuplicateScheduleStartDate.TabIndex = 10;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(9, 193);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(101, 13);
            this.label27.TabIndex = 8;
            this.label27.Text = "Duplicate Roster for";
            // 
            // numScheduleMonths
            // 
            this.numScheduleMonths.Location = new System.Drawing.Point(121, 192);
            this.numScheduleMonths.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numScheduleMonths.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numScheduleMonths.Name = "numScheduleMonths";
            this.numScheduleMonths.Size = new System.Drawing.Size(120, 20);
            this.numScheduleMonths.TabIndex = 7;
            this.numScheduleMonths.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnDuplicateRoster
            // 
            this.btnDuplicateRoster.Location = new System.Drawing.Point(12, 251);
            this.btnDuplicateRoster.Name = "btnDuplicateRoster";
            this.btnDuplicateRoster.Size = new System.Drawing.Size(787, 23);
            this.btnDuplicateRoster.TabIndex = 6;
            this.btnDuplicateRoster.Text = "Duplicate Rosters";
            this.btnDuplicateRoster.UseVisualStyleBackColor = true;
            this.btnDuplicateRoster.Click += new System.EventHandler(this.btnDuplicateRoster_Click);
            // 
            // btnGetRosters
            // 
            this.btnGetRosters.Location = new System.Drawing.Point(9, 22);
            this.btnGetRosters.Name = "btnGetRosters";
            this.btnGetRosters.Size = new System.Drawing.Size(117, 23);
            this.btnGetRosters.TabIndex = 5;
            this.btnGetRosters.Text = "Get Rosters";
            this.btnGetRosters.UseVisualStyleBackColor = true;
            this.btnGetRosters.Click += new System.EventHandler(this.btnGetRosters_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(356, 89);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(125, 13);
            this.label26.TabIndex = 4;
            this.label26.Text = "Roster Start Date (Days):";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 89);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(72, 13);
            this.label25.TabIndex = 3;
            this.label25.Text = "Roster Name:";
            // 
            // cmbRosterStartMonth
            // 
            this.cmbRosterStartMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRosterStartMonth.FormattingEnabled = true;
            this.cmbRosterStartMonth.Location = new System.Drawing.Point(359, 110);
            this.cmbRosterStartMonth.Name = "cmbRosterStartMonth";
            this.cmbRosterStartMonth.Size = new System.Drawing.Size(207, 21);
            this.cmbRosterStartMonth.TabIndex = 2;
            // 
            // cmbRosters
            // 
            this.cmbRosters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRosters.FormattingEnabled = true;
            this.cmbRosters.Location = new System.Drawing.Point(9, 110);
            this.cmbRosters.Name = "cmbRosters";
            this.cmbRosters.Size = new System.Drawing.Size(343, 21);
            this.cmbRosters.TabIndex = 1;
            this.cmbRosters.SelectedIndexChanged += new System.EventHandler(this.cmbRosters_SelectedIndexChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(6, 69);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(218, 13);
            this.label24.TabIndex = 0;
            this.label24.Text = "Select a roster name and month to duplicate:";
            // 
            // chkChangeEmployeeNumber
            // 
            this.chkChangeEmployeeNumber.AutoSize = true;
            this.chkChangeEmployeeNumber.Enabled = false;
            this.chkChangeEmployeeNumber.Location = new System.Drawing.Point(438, 53);
            this.chkChangeEmployeeNumber.Name = "chkChangeEmployeeNumber";
            this.chkChangeEmployeeNumber.Size = new System.Drawing.Size(326, 17);
            this.chkChangeEmployeeNumber.TabIndex = 20;
            this.chkChangeEmployeeNumber.Text = "Change Employee Number across TMP when changed in Impro";
            this.chkChangeEmployeeNumber.UseVisualStyleBackColor = true;
            this.chkChangeEmployeeNumber.CheckedChanged += new System.EventHandler(this.chkChangeEmployeeNumber_CheckedChanged);
            // 
            // frmImpronetInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 538);
            this.Controls.Add(this.tabEmail);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.grpImportSettings);
            this.Name = "frmImpronetInterface";
            this.ShowIcon = false;
            this.Text = "Impro to Time Manager Conversion Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpImportSettings.ResumeLayout(false);
            this.grpImportSettings.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMappings)).EndInit();
            this.tabEmail.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabctrlMappings.ResumeLayout(false);
            this.tabpgStandardMapping.ResumeLayout(false);
            this.tabpgDepartmentMapping.ResumeLayout(false);
            this.tabpgDepartmentMapping.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPgEmail.ResumeLayout(false);
            this.tabPgEmail.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numScheduleMonths)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.DateTimePicker dtToDate;
        private System.Windows.Forms.DateTimePicker dtFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpImportSettings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowseFbDatabase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFbDatabasePath;
        private System.Windows.Forms.Button btnBrowseOutput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbDirection;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtReaderAddress;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button btnFetchReaders;
        private System.Windows.Forms.DataGridView gridMappings;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkRunnerEnabled;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRunner;
        private System.Windows.Forms.Timer tmrMain;
        private System.Windows.Forms.Timer tmrSeconds;
        private System.Windows.Forms.CheckBox chkEnableTimer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.RadioButton radIXP220;
        private System.Windows.Forms.RadioButton radIXP400;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radEmployeeNum;
        private System.Windows.Forms.RadioButton radMstsq;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblCountdown;
        private System.Windows.Forms.TabControl tabEmail;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTmpDatabasePath;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radFirebird;
        private System.Windows.Forms.TextBox txtSqlConnectionString;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton radSql;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckedListBox chkDepartments;
        private System.Windows.Forms.Button btnRefreshDepartments;
        private System.Windows.Forms.CheckBox chkSynEmployees;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnDeselctAll;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.TabControl tabctrlMappings;
        private System.Windows.Forms.TabPage tabpgStandardMapping;
        private System.Windows.Forms.TabPage tabpgDepartmentMapping;
        private System.Windows.Forms.RadioButton radUseDepartmentMapping;
        private System.Windows.Forms.RadioButton radUseStandardMapping;
        private System.Windows.Forms.Button btnDepMappingsDeselectAll;
        private System.Windows.Forms.Button btnDepMappingsSelectAll;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TreeView treeDepMappings;
        private System.Windows.Forms.Button btnDepFetchMappings;
        private System.Windows.Forms.Button btnDepMappingsRefresh;
        private System.Windows.Forms.Button btnDepMappingsSave;
        private System.Windows.Forms.TextBox txtDepMappingAccessControlCode;
        private System.Windows.Forms.TextBox txtDepMappingTimeAndAttendanceCode;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox chkSyncAccessControlDevices;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSyncInterval;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.CheckBox chkSyncTimerEnabled;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblSyncCountDown;
        private System.Windows.Forms.TabPage tabPgEmail;
        private System.Windows.Forms.TextBox txtEmailAddress;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtSmtpPort;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtSmtpHost;
        private System.Windows.Forms.CheckBox chkEnableEmail;
        private System.Windows.Forms.CheckBox chkSSL;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtToEmailAddress;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnTestEmail;
        private System.Windows.Forms.CheckBox chkAlwaysSync;
        private System.Windows.Forms.CheckBox chkSyncMstsq;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Button btnGetRosters;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox cmbRosterStartMonth;
        private System.Windows.Forms.ComboBox cmbRosters;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.NumericUpDown numScheduleMonths;
        private System.Windows.Forms.Button btnDuplicateRoster;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.DateTimePicker dtDuplicateScheduleStartDate;
        private System.Windows.Forms.RadioButton radDuplicateWeeks;
        private System.Windows.Forms.RadioButton radDuplicateMonths;
        private System.Windows.Forms.CheckBox chkChangeEmployeeNumber;
    }
}

