namespace TM_Impronet_Interface
{
    partial class Form1
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
            this.label8 = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbDirection = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReaderAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnRefreshDepartments = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.chkDepartments = new System.Windows.Forms.CheckedListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.chkSynEmployees = new System.Windows.Forms.CheckBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnDeselctAll = new System.Windows.Forms.Button();
            this.grpImportSettings.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMappings)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
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
            this.grpImportSettings.Controls.Add(this.label8);
            this.grpImportSettings.Controls.Add(this.txtInterval);
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
            this.groupBox3.Location = new System.Drawing.Point(16, 325);
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
            this.radIXP220.Location = new System.Drawing.Point(16, 287);
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
            this.radIXP400.Location = new System.Drawing.Point(16, 264);
            this.radIXP400.Name = "radIXP400";
            this.radIXP400.Size = new System.Drawing.Size(63, 17);
            this.radIXP400.TabIndex = 18;
            this.radIXP400.TabStop = true;
            this.radIXP400.Text = "IXP 400";
            this.radIXP400.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 210);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Interval (seconds)";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(16, 227);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(153, 20);
            this.txtInterval.TabIndex = 16;
            this.txtInterval.Text = "30";
            this.txtInterval.TextChanged += new System.EventHandler(this.txtInterval_TextChanged);
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
            this.gridMappings.Size = new System.Drawing.Size(437, 417);
            this.gridMappings.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 92);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 20);
            this.label9.TabIndex = 15;
            this.label9.Text = "Countdown:";
            // 
            // lblCountdown
            // 
            this.lblCountdown.AutoSize = true;
            this.lblCountdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountdown.Location = new System.Drawing.Point(6, 124);
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
            this.chkEnableTimer.Size = new System.Drawing.Size(100, 17);
            this.chkEnableTimer.TabIndex = 13;
            this.chkEnableTimer.Text = "Timer Enabled?";
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
            this.tmrMain.Interval = 300000;
            this.tmrMain.Tick += new System.EventHandler(this.tmrMain_Tick);
            // 
            // tmrSeconds
            // 
            this.tmrSeconds.Interval = 1000;
            this.tmrSeconds.Tick += new System.EventHandler(this.tmrSeconds_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(212, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(824, 450);
            this.tabControl1.TabIndex = 21;
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
            this.tabPage5.Controls.Add(this.label9);
            this.tabPage5.Controls.Add(this.lblCountdown);
            this.tabPage5.Controls.Add(this.label7);
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
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gridMappings);
            this.tabPage1.Controls.Add(this.btnRefresh);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.btnFetchReaders);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(816, 424);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Device Mappings";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.label12.Location = new System.Drawing.Point(20, 25);
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
            this.tabPage3.Controls.Add(this.chkSynEmployees);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(816, 424);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Sync Options";
            this.tabPage3.UseVisualStyleBackColor = true;
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
            // btnDeselctAll
            // 
            this.btnDeselctAll.Location = new System.Drawing.Point(233, 53);
            this.btnDeselctAll.Name = "btnDeselctAll";
            this.btnDeselctAll.Size = new System.Drawing.Size(204, 23);
            this.btnDeselctAll.TabIndex = 5;
            this.btnDeselctAll.Text = "Deselect All";
            this.btnDeselctAll.UseVisualStyleBackColor = true;
            this.btnDeselctAll.Click += new System.EventHandler(this.btnDeselctAll_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 538);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.grpImportSettings);
            this.Name = "Form1";
            this.Text = "Impro to Time Manager Conversion Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpImportSettings.ResumeLayout(false);
            this.grpImportSettings.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMappings)).EndInit();
            this.tabControl1.ResumeLayout(false);
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
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
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
        private System.Windows.Forms.TabControl tabControl1;
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
    }
}

