namespace V3ToLogo.UIL
{
    partial class formSettings
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtLogoAccount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLogoPeriodNr = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLogoFirmNr = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLogoUserPass = new System.Windows.Forms.TextBox();
            this.txtLogoUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSQLServerDBName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSQLServerUserPass = new System.Windows.Forms.TextBox();
            this.txtSQLServerUserName = new System.Windows.Forms.TextBox();
            this.txtSQLServerName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewUnitSet = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtUnitSetV3Code = new System.Windows.Forms.TextBox();
            this.txtUnitSetLogoCode = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.contextMenuStripForUnitSetCode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.txtProductLogoCode = new System.Windows.Forms.TextBox();
            this.txtProductV3Code = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dataGridViewProduct = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.txtCustomerLogoCode = new System.Windows.Forms.TextBox();
            this.txtCustomerV3Code = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.dataGridViewCustomer = new System.Windows.Forms.DataGridView();
            this.contextMenuStripForCustomer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStripForProduct = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.unitSetDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CustomerDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUnitSet)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.contextMenuStripForUnitSetCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomer)).BeginInit();
            this.contextMenuStripForCustomer.SuspendLayout();
            this.contextMenuStripForProduct.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 414);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(460, 58);
            this.panel2.TabIndex = 3;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(362, 15);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 28);
            this.btnEdit.TabIndex = 5;
            this.btnEdit.Text = "&Vazgeç";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(281, 15);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 28);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "&Tamam";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(460, 414);
            this.tabControl1.TabIndex = 25;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(452, 385);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Bağlantı Ayarları";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.txtUnitSetLogoCode);
            this.tabPage2.Controls.Add(this.txtUnitSetV3Code);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.dataGridViewUnitSet);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(452, 385);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Birim Seti Ayarları";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtLogoAccount);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtLogoPeriodNr);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtLogoFirmNr);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtLogoUserPass);
            this.groupBox2.Controls.Add(this.txtLogoUserName);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(30, 175);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(394, 199);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Logo Ayarları";
            // 
            // txtLogoAccount
            // 
            this.txtLogoAccount.Location = new System.Drawing.Point(127, 163);
            this.txtLogoAccount.Name = "txtLogoAccount";
            this.txtLogoAccount.Size = new System.Drawing.Size(250, 22);
            this.txtLogoAccount.TabIndex = 31;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 17);
            this.label9.TabIndex = 30;
            this.label9.Text = "Logo Cari";
            // 
            // txtLogoPeriodNr
            // 
            this.txtLogoPeriodNr.Location = new System.Drawing.Point(127, 130);
            this.txtLogoPeriodNr.Name = "txtLogoPeriodNr";
            this.txtLogoPeriodNr.Size = new System.Drawing.Size(250, 22);
            this.txtLogoPeriodNr.TabIndex = 30;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 17);
            this.label7.TabIndex = 28;
            this.label7.Text = "Period No";
            // 
            // txtLogoFirmNr
            // 
            this.txtLogoFirmNr.Location = new System.Drawing.Point(127, 97);
            this.txtLogoFirmNr.Name = "txtLogoFirmNr";
            this.txtLogoFirmNr.Size = new System.Drawing.Size(250, 22);
            this.txtLogoFirmNr.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 17);
            this.label6.TabIndex = 26;
            this.label6.Text = "Firma No";
            // 
            // txtLogoUserPass
            // 
            this.txtLogoUserPass.Location = new System.Drawing.Point(127, 64);
            this.txtLogoUserPass.Name = "txtLogoUserPass";
            this.txtLogoUserPass.Size = new System.Drawing.Size(250, 22);
            this.txtLogoUserPass.TabIndex = 28;
            this.txtLogoUserPass.UseSystemPasswordChar = true;
            // 
            // txtLogoUserName
            // 
            this.txtLogoUserName.Location = new System.Drawing.Point(127, 31);
            this.txtLogoUserName.Name = "txtLogoUserName";
            this.txtLogoUserName.Size = new System.Drawing.Size(250, 22);
            this.txtLogoUserName.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 17);
            this.label4.TabIndex = 22;
            this.label4.Text = "Parola";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 17);
            this.label5.TabIndex = 21;
            this.label5.Text = "Kullanıcı Adı";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSQLServerDBName);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtSQLServerUserPass);
            this.groupBox1.Controls.Add(this.txtSQLServerUserName);
            this.groupBox1.Controls.Add(this.txtSQLServerName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(30, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(394, 163);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Veritabanı Ayarları";
            // 
            // txtSQLServerDBName
            // 
            this.txtSQLServerDBName.Location = new System.Drawing.Point(127, 60);
            this.txtSQLServerDBName.Name = "txtSQLServerDBName";
            this.txtSQLServerDBName.Size = new System.Drawing.Size(250, 22);
            this.txtSQLServerDBName.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 17);
            this.label8.TabIndex = 26;
            this.label8.Text = "Veritabanı Adı";
            // 
            // txtSQLServerUserPass
            // 
            this.txtSQLServerUserPass.Location = new System.Drawing.Point(127, 120);
            this.txtSQLServerUserPass.Name = "txtSQLServerUserPass";
            this.txtSQLServerUserPass.Size = new System.Drawing.Size(250, 22);
            this.txtSQLServerUserPass.TabIndex = 26;
            this.txtSQLServerUserPass.UseSystemPasswordChar = true;
            // 
            // txtSQLServerUserName
            // 
            this.txtSQLServerUserName.Location = new System.Drawing.Point(127, 90);
            this.txtSQLServerUserName.Name = "txtSQLServerUserName";
            this.txtSQLServerUserName.Size = new System.Drawing.Size(250, 22);
            this.txtSQLServerUserName.TabIndex = 25;
            // 
            // txtSQLServerName
            // 
            this.txtSQLServerName.Location = new System.Drawing.Point(127, 30);
            this.txtSQLServerName.Name = "txtSQLServerName";
            this.txtSQLServerName.Size = new System.Drawing.Size(250, 22);
            this.txtSQLServerName.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 17);
            this.label3.TabIndex = 22;
            this.label3.Text = "Parola";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "Kullanıcı Adı";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "Sunucu Adresi";
            // 
            // dataGridViewUnitSet
            // 
            this.dataGridViewUnitSet.AllowUserToAddRows = false;
            this.dataGridViewUnitSet.AllowUserToDeleteRows = false;
            this.dataGridViewUnitSet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewUnitSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUnitSet.ContextMenuStrip = this.contextMenuStripForUnitSetCode;
            this.dataGridViewUnitSet.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewUnitSet.Location = new System.Drawing.Point(3, 105);
            this.dataGridViewUnitSet.Name = "dataGridViewUnitSet";
            this.dataGridViewUnitSet.ReadOnly = true;
            this.dataGridViewUnitSet.RowTemplate.Height = 24;
            this.dataGridViewUnitSet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUnitSet.Size = new System.Drawing.Size(446, 277);
            this.dataGridViewUnitSet.TabIndex = 0;
            this.dataGridViewUnitSet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewUnitSet_CellContentClick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 17);
            this.label10.TabIndex = 1;
            this.label10.Text = "V3 Kodu";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 17);
            this.label11.TabIndex = 2;
            this.label11.Text = "Logo Kodu";
            // 
            // txtUnitSetV3Code
            // 
            this.txtUnitSetV3Code.Location = new System.Drawing.Point(105, 15);
            this.txtUnitSetV3Code.Name = "txtUnitSetV3Code";
            this.txtUnitSetV3Code.Size = new System.Drawing.Size(228, 22);
            this.txtUnitSetV3Code.TabIndex = 4;
            // 
            // txtUnitSetLogoCode
            // 
            this.txtUnitSetLogoCode.Location = new System.Drawing.Point(105, 43);
            this.txtUnitSetLogoCode.Name = "txtUnitSetLogoCode";
            this.txtUnitSetLogoCode.Size = new System.Drawing.Size(228, 22);
            this.txtUnitSetLogoCode.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Ekle";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.txtProductLogoCode);
            this.tabPage3.Controls.Add(this.txtProductV3Code);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.dataGridViewProduct);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(452, 385);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Ürün Ayarları";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.button3);
            this.tabPage4.Controls.Add(this.txtCustomerLogoCode);
            this.tabPage4.Controls.Add(this.txtCustomerV3Code);
            this.tabPage4.Controls.Add(this.label14);
            this.tabPage4.Controls.Add(this.label15);
            this.tabPage4.Controls.Add(this.dataGridViewCustomer);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(452, 385);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Cari Ayarları";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // contextMenuStripForUnitSetCode
            // 
            this.contextMenuStripForUnitSetCode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unitSetDeleteToolStripMenuItem});
            this.contextMenuStripForUnitSetCode.Name = "contextMenuStripForUnitSetCode";
            this.contextMenuStripForUnitSetCode.Size = new System.Drawing.Size(95, 28);
            this.contextMenuStripForUnitSetCode.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripForUnitSetCode_Opening);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(19, 72);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Ekle";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtProductLogoCode
            // 
            this.txtProductLogoCode.Location = new System.Drawing.Point(107, 42);
            this.txtProductLogoCode.Name = "txtProductLogoCode";
            this.txtProductLogoCode.Size = new System.Drawing.Size(228, 22);
            this.txtProductLogoCode.TabIndex = 11;
            // 
            // txtProductV3Code
            // 
            this.txtProductV3Code.Location = new System.Drawing.Point(107, 14);
            this.txtProductV3Code.Name = "txtProductV3Code";
            this.txtProductV3Code.Size = new System.Drawing.Size(228, 22);
            this.txtProductV3Code.TabIndex = 10;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 17);
            this.label12.TabIndex = 9;
            this.label12.Text = "Logo Kodu";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 17);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 17);
            this.label13.TabIndex = 8;
            this.label13.Text = "V3 Kodu";
            // 
            // dataGridViewProduct
            // 
            this.dataGridViewProduct.AllowUserToAddRows = false;
            this.dataGridViewProduct.AllowUserToDeleteRows = false;
            this.dataGridViewProduct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProduct.ContextMenuStrip = this.contextMenuStripForProduct;
            this.dataGridViewProduct.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewProduct.Location = new System.Drawing.Point(0, 108);
            this.dataGridViewProduct.Name = "dataGridViewProduct";
            this.dataGridViewProduct.ReadOnly = true;
            this.dataGridViewProduct.RowTemplate.Height = 24;
            this.dataGridViewProduct.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProduct.Size = new System.Drawing.Size(452, 277);
            this.dataGridViewProduct.TabIndex = 7;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(13, 74);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Ekle";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtCustomerLogoCode
            // 
            this.txtCustomerLogoCode.Location = new System.Drawing.Point(101, 44);
            this.txtCustomerLogoCode.Name = "txtCustomerLogoCode";
            this.txtCustomerLogoCode.Size = new System.Drawing.Size(228, 22);
            this.txtCustomerLogoCode.TabIndex = 11;
            // 
            // txtCustomerV3Code
            // 
            this.txtCustomerV3Code.Location = new System.Drawing.Point(101, 16);
            this.txtCustomerV3Code.Name = "txtCustomerV3Code";
            this.txtCustomerV3Code.Size = new System.Drawing.Size(228, 22);
            this.txtCustomerV3Code.TabIndex = 10;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 44);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 17);
            this.label14.TabIndex = 9;
            this.label14.Text = "Logo Kodu";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 19);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 17);
            this.label15.TabIndex = 8;
            this.label15.Text = "V3 Kodu";
            // 
            // dataGridViewCustomer
            // 
            this.dataGridViewCustomer.AllowUserToAddRows = false;
            this.dataGridViewCustomer.AllowUserToDeleteRows = false;
            this.dataGridViewCustomer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCustomer.ContextMenuStrip = this.contextMenuStripForCustomer;
            this.dataGridViewCustomer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewCustomer.Location = new System.Drawing.Point(0, 108);
            this.dataGridViewCustomer.Name = "dataGridViewCustomer";
            this.dataGridViewCustomer.ReadOnly = true;
            this.dataGridViewCustomer.RowTemplate.Height = 24;
            this.dataGridViewCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCustomer.Size = new System.Drawing.Size(452, 277);
            this.dataGridViewCustomer.TabIndex = 7;
            // 
            // contextMenuStripForCustomer
            // 
            this.contextMenuStripForCustomer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CustomerDeleteToolStripMenuItem});
            this.contextMenuStripForCustomer.Name = "contextMenuStripForCustomer";
            this.contextMenuStripForCustomer.Size = new System.Drawing.Size(95, 28);
            // 
            // contextMenuStripForProduct
            // 
            this.contextMenuStripForProduct.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productToolStripMenuItem1});
            this.contextMenuStripForProduct.Name = "contextMenuStripForProduct";
            this.contextMenuStripForProduct.Size = new System.Drawing.Size(95, 28);
            // 
            // unitSetDeleteToolStripMenuItem
            // 
            this.unitSetDeleteToolStripMenuItem.Name = "unitSetDeleteToolStripMenuItem";
            this.unitSetDeleteToolStripMenuItem.Size = new System.Drawing.Size(94, 24);
            this.unitSetDeleteToolStripMenuItem.Text = "Sil";
            this.unitSetDeleteToolStripMenuItem.Click += new System.EventHandler(this.birimSilToolStripMenuItem_Click);
            // 
            // CustomerDeleteToolStripMenuItem
            // 
            this.CustomerDeleteToolStripMenuItem.Name = "CustomerDeleteToolStripMenuItem";
            this.CustomerDeleteToolStripMenuItem.Size = new System.Drawing.Size(94, 24);
            this.CustomerDeleteToolStripMenuItem.Text = "Sil";
            this.CustomerDeleteToolStripMenuItem.Click += new System.EventHandler(this.CustomerDeleteToolStripMenuItem_Click);
            // 
            // productToolStripMenuItem1
            // 
            this.productToolStripMenuItem1.Name = "productToolStripMenuItem1";
            this.productToolStripMenuItem1.Size = new System.Drawing.Size(94, 24);
            this.productToolStripMenuItem1.Text = "Sil";
            this.productToolStripMenuItem1.Click += new System.EventHandler(this.productToolStripMenuItem1_Click);
            // 
            // formSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 472);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel2);
            this.Name = "formSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bağlantı Ayarları";
            this.Load += new System.EventHandler(this.formSettings_Load);
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUnitSet)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.contextMenuStripForUnitSetCode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCustomer)).EndInit();
            this.contextMenuStripForCustomer.ResumeLayout(false);
            this.contextMenuStripForProduct.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtLogoAccount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtLogoPeriodNr;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLogoFirmNr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLogoUserPass;
        private System.Windows.Forms.TextBox txtLogoUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSQLServerDBName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSQLServerUserPass;
        private System.Windows.Forms.TextBox txtSQLServerUserName;
        private System.Windows.Forms.TextBox txtSQLServerName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtUnitSetLogoCode;
        private System.Windows.Forms.TextBox txtUnitSetV3Code;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dataGridViewUnitSet;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripForUnitSetCode;
        private System.Windows.Forms.ToolStripMenuItem unitSetDeleteToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtProductLogoCode;
        private System.Windows.Forms.TextBox txtProductV3Code;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridView dataGridViewProduct;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripForProduct;
        private System.Windows.Forms.ToolStripMenuItem productToolStripMenuItem1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtCustomerLogoCode;
        private System.Windows.Forms.TextBox txtCustomerV3Code;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridView dataGridViewCustomer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripForCustomer;
        private System.Windows.Forms.ToolStripMenuItem CustomerDeleteToolStripMenuItem;
    }
}