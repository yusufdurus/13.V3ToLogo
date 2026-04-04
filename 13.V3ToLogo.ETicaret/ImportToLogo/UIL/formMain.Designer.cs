namespace ImportToLogo.UIL
{
    partial class formMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formMain));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonTimerList = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LABEL1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_malzeme_success = new System.Windows.Forms.Label();
            this.label_malzeme_error = new System.Windows.Forms.Label();
            this.label_cari_success = new System.Windows.Forms.Label();
            this.label_cari_error = new System.Windows.Forms.Label();
            this.label_fatura_success = new System.Windows.Forms.Label();
            this.label_fatura_error = new System.Windows.Forms.Label();
            this.timerCount = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.buttonStart, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonStop, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonRun, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonTimerList, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.button2, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(531, 97);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // buttonStart
            // 
            this.buttonStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonStart.Image = global::ImportToLogo.Properties.Resources.control_play;
            this.buttonStart.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonStart.Location = new System.Drawing.Point(2, 2);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(102, 93);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Başlat";
            this.buttonStart.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonStart.UseCompatibleTextRendering = true;
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonStop.Image = global::ImportToLogo.Properties.Resources.control_stop;
            this.buttonStop.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonStop.Location = new System.Drawing.Point(108, 2);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(102, 93);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "Durdur";
            this.buttonStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonRun
            // 
            this.buttonRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRun.Image = global::ImportToLogo.Properties.Resources.control_play;
            this.buttonRun.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonRun.Location = new System.Drawing.Point(426, 2);
            this.buttonRun.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(103, 93);
            this.buttonRun.TabIndex = 3;
            this.buttonRun.Text = "Şimdi çalıştır";
            this.buttonRun.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonTimerList
            // 
            this.buttonTimerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTimerList.Image = global::ImportToLogo.Properties.Resources.Time_icon;
            this.buttonTimerList.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonTimerList.Location = new System.Drawing.Point(214, 2);
            this.buttonTimerList.Margin = new System.Windows.Forms.Padding(2);
            this.buttonTimerList.Name = "buttonTimerList";
            this.buttonTimerList.Size = new System.Drawing.Size(102, 93);
            this.buttonTimerList.TabIndex = 2;
            this.buttonTimerList.Text = "Zaman Ayarları";
            this.buttonTimerList.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonTimerList.UseVisualStyleBackColor = true;
            this.buttonTimerList.Click += new System.EventHandler(this.buttonTimerList_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Image = global::ImportToLogo.Properties.Resources.settings_icon;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(320, 2);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 93);
            this.button2.TabIndex = 4;
            this.button2.Text = "Bağlantı Ayarları";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // timerMain
            // 
            this.timerMain.Interval = 5000;
            this.timerMain.Tick += new System.EventHandler(this.timerMain_TickAsync);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.LABEL1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label_malzeme_success, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label_malzeme_error, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label_cari_success, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label_cari_error, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.label_fatura_success, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.label_fatura_error, 3, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 101);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.0303F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.9697F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(529, 84);
            this.tableLayoutPanel2.TabIndex = 1;
            this.tableLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel2_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(398, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "FATURA";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(266, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "CARİ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LABEL1
            // 
            this.LABEL1.AutoSize = true;
            this.LABEL1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LABEL1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.LABEL1.Location = new System.Drawing.Point(134, 0);
            this.LABEL1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LABEL1.Name = "LABEL1";
            this.LABEL1.Size = new System.Drawing.Size(128, 29);
            this.LABEL1.TabIndex = 0;
            this.LABEL1.Text = "MALZEME";
            this.LABEL1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(2, 29);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 26);
            this.label4.TabIndex = 3;
            this.label4.Text = "Başarılı";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(2, 55);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 29);
            this.label5.TabIndex = 4;
            this.label5.Text = "Başarısız";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_malzeme_success
            // 
            this.label_malzeme_success.AutoSize = true;
            this.label_malzeme_success.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_malzeme_success.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_malzeme_success.Location = new System.Drawing.Point(134, 29);
            this.label_malzeme_success.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_malzeme_success.Name = "label_malzeme_success";
            this.label_malzeme_success.Size = new System.Drawing.Size(128, 26);
            this.label_malzeme_success.TabIndex = 5;
            this.label_malzeme_success.Text = "0";
            this.label_malzeme_success.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_malzeme_error
            // 
            this.label_malzeme_error.AutoSize = true;
            this.label_malzeme_error.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_malzeme_error.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_malzeme_error.Location = new System.Drawing.Point(134, 55);
            this.label_malzeme_error.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_malzeme_error.Name = "label_malzeme_error";
            this.label_malzeme_error.Size = new System.Drawing.Size(128, 29);
            this.label_malzeme_error.TabIndex = 6;
            this.label_malzeme_error.Text = "0";
            this.label_malzeme_error.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_cari_success
            // 
            this.label_cari_success.AutoSize = true;
            this.label_cari_success.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_cari_success.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_cari_success.Location = new System.Drawing.Point(266, 29);
            this.label_cari_success.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_cari_success.Name = "label_cari_success";
            this.label_cari_success.Size = new System.Drawing.Size(128, 26);
            this.label_cari_success.TabIndex = 7;
            this.label_cari_success.Text = "0";
            this.label_cari_success.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_cari_error
            // 
            this.label_cari_error.AutoSize = true;
            this.label_cari_error.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_cari_error.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_cari_error.Location = new System.Drawing.Point(266, 55);
            this.label_cari_error.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_cari_error.Name = "label_cari_error";
            this.label_cari_error.Size = new System.Drawing.Size(128, 29);
            this.label_cari_error.TabIndex = 8;
            this.label_cari_error.Text = "0";
            this.label_cari_error.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_fatura_success
            // 
            this.label_fatura_success.AutoSize = true;
            this.label_fatura_success.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_fatura_success.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_fatura_success.Location = new System.Drawing.Point(398, 29);
            this.label_fatura_success.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_fatura_success.Name = "label_fatura_success";
            this.label_fatura_success.Size = new System.Drawing.Size(129, 26);
            this.label_fatura_success.TabIndex = 9;
            this.label_fatura_success.Text = "0";
            this.label_fatura_success.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_fatura_error
            // 
            this.label_fatura_error.AutoSize = true;
            this.label_fatura_error.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_fatura_error.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label_fatura_error.Location = new System.Drawing.Point(398, 55);
            this.label_fatura_error.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_fatura_error.Name = "label_fatura_error";
            this.label_fatura_error.Size = new System.Drawing.Size(129, 29);
            this.label_fatura_error.TabIndex = 10;
            this.label_fatura_error.Text = "0";
            this.label_fatura_error.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerCount
            // 
            this.timerCount.Enabled = true;
            this.timerCount.Tick += new System.EventHandler(this.timerCount_Tick);
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 185);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "formMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "From V3 To Logo Tiger";
            this.Load += new System.EventHandler(this.formMain_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonTimerList;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LABEL1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_malzeme_success;
        private System.Windows.Forms.Label label_malzeme_error;
        private System.Windows.Forms.Label label_cari_success;
        private System.Windows.Forms.Label label_cari_error;
        private System.Windows.Forms.Label label_fatura_success;
        private System.Windows.Forms.Label label_fatura_error;
        private System.Windows.Forms.Timer timerCount;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Button button2;

    }
}

