namespace Evemu_DB_Editor
{
    partial class Preference
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Preference));
            this.grpLanguages = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rdbFrancais = new System.Windows.Forms.RadioButton();
            this.rdbEspanol = new System.Windows.Forms.RadioButton();
            this.rdbItaliano = new System.Windows.Forms.RadioButton();
            this.rdbDeutsch = new System.Windows.Forms.RadioButton();
            this.rdbEnglish = new System.Windows.Forms.RadioButton();
            this.bttChoseSqlDir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMysqlPath = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dbNameTxtBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.portTxtBox = new System.Windows.Forms.TextBox();
            this.passwordTxtBox = new System.Windows.Forms.TextBox();
            this.usernameTxtBox = new System.Windows.Forms.TextBox();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grpLanguages.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLanguages
            // 
            this.grpLanguages.Controls.Add(this.label2);
            this.grpLanguages.Controls.Add(this.rdbFrancais);
            this.grpLanguages.Controls.Add(this.rdbEspanol);
            this.grpLanguages.Controls.Add(this.rdbItaliano);
            this.grpLanguages.Controls.Add(this.rdbDeutsch);
            this.grpLanguages.Controls.Add(this.rdbEnglish);
            this.grpLanguages.Enabled = false;
            this.grpLanguages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpLanguages.Location = new System.Drawing.Point(206, 42);
            this.grpLanguages.Name = "grpLanguages";
            this.grpLanguages.Size = new System.Drawing.Size(112, 153);
            this.grpLanguages.TabIndex = 11;
            this.grpLanguages.TabStop = false;
            this.grpLanguages.Text = "Languages";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Not implemented";
            // 
            // rdbFrancais
            // 
            this.rdbFrancais.AutoSize = true;
            this.rdbFrancais.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbFrancais.Location = new System.Drawing.Point(6, 111);
            this.rdbFrancais.Name = "rdbFrancais";
            this.rdbFrancais.Size = new System.Drawing.Size(65, 17);
            this.rdbFrancais.TabIndex = 4;
            this.rdbFrancais.TabStop = true;
            this.rdbFrancais.Text = "Francais";
            this.rdbFrancais.UseVisualStyleBackColor = true;
            // 
            // rdbEspanol
            // 
            this.rdbEspanol.AutoSize = true;
            this.rdbEspanol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbEspanol.Location = new System.Drawing.Point(6, 88);
            this.rdbEspanol.Name = "rdbEspanol";
            this.rdbEspanol.Size = new System.Drawing.Size(63, 17);
            this.rdbEspanol.TabIndex = 3;
            this.rdbEspanol.TabStop = true;
            this.rdbEspanol.Text = "Espanol";
            this.rdbEspanol.UseVisualStyleBackColor = true;
            // 
            // rdbItaliano
            // 
            this.rdbItaliano.AutoSize = true;
            this.rdbItaliano.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbItaliano.Location = new System.Drawing.Point(6, 65);
            this.rdbItaliano.Name = "rdbItaliano";
            this.rdbItaliano.Size = new System.Drawing.Size(59, 17);
            this.rdbItaliano.TabIndex = 2;
            this.rdbItaliano.TabStop = true;
            this.rdbItaliano.Text = "Italiano";
            this.rdbItaliano.UseVisualStyleBackColor = true;
            // 
            // rdbDeutsch
            // 
            this.rdbDeutsch.AutoSize = true;
            this.rdbDeutsch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbDeutsch.Location = new System.Drawing.Point(6, 42);
            this.rdbDeutsch.Name = "rdbDeutsch";
            this.rdbDeutsch.Size = new System.Drawing.Size(65, 17);
            this.rdbDeutsch.TabIndex = 1;
            this.rdbDeutsch.TabStop = true;
            this.rdbDeutsch.Text = "Deutsch";
            this.rdbDeutsch.UseVisualStyleBackColor = true;
            // 
            // rdbEnglish
            // 
            this.rdbEnglish.AutoSize = true;
            this.rdbEnglish.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbEnglish.Location = new System.Drawing.Point(6, 19);
            this.rdbEnglish.Name = "rdbEnglish";
            this.rdbEnglish.Size = new System.Drawing.Size(59, 17);
            this.rdbEnglish.TabIndex = 0;
            this.rdbEnglish.TabStop = true;
            this.rdbEnglish.Text = "English";
            this.rdbEnglish.UseVisualStyleBackColor = true;
            // 
            // bttChoseSqlDir
            // 
            this.bttChoseSqlDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bttChoseSqlDir.Location = new System.Drawing.Point(436, 4);
            this.bttChoseSqlDir.Name = "bttChoseSqlDir";
            this.bttChoseSqlDir.Size = new System.Drawing.Size(31, 23);
            this.bttChoseSqlDir.TabIndex = 10;
            this.bttChoseSqlDir.Text = "...";
            this.bttChoseSqlDir.UseVisualStyleBackColor = true;
            this.bttChoseSqlDir.Click += new System.EventHandler(this.bttChoseSqlDir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "MySQL Path";
            // 
            // txtMysqlPath
            // 
            this.txtMysqlPath.Location = new System.Drawing.Point(85, 6);
            this.txtMysqlPath.Name = "txtMysqlPath";
            this.txtMysqlPath.Size = new System.Drawing.Size(345, 20);
            this.txtMysqlPath.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dbNameTxtBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.portTxtBox);
            this.groupBox1.Controls.Add(this.passwordTxtBox);
            this.groupBox1.Controls.Add(this.usernameTxtBox);
            this.groupBox1.Controls.Add(this.hostTextBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(15, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(185, 153);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DB Settings";
            this.toolTip1.SetToolTip(this.groupBox1, "Leave empty for manual connect");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Database: ";
            // 
            // dbNameTxtBox
            // 
            this.dbNameTxtBox.Location = new System.Drawing.Point(73, 123);
            this.dbNameTxtBox.Name = "dbNameTxtBox";
            this.dbNameTxtBox.Size = new System.Drawing.Size(100, 20);
            this.dbNameTxtBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Port: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Username: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Host: ";
            // 
            // portTxtBox
            // 
            this.portTxtBox.Location = new System.Drawing.Point(73, 97);
            this.portTxtBox.Name = "portTxtBox";
            this.portTxtBox.Size = new System.Drawing.Size(100, 20);
            this.portTxtBox.TabIndex = 5;
            // 
            // passwordTxtBox
            // 
            this.passwordTxtBox.Location = new System.Drawing.Point(73, 71);
            this.passwordTxtBox.Name = "passwordTxtBox";
            this.passwordTxtBox.Size = new System.Drawing.Size(100, 20);
            this.passwordTxtBox.TabIndex = 4;
            // 
            // usernameTxtBox
            // 
            this.usernameTxtBox.Location = new System.Drawing.Point(73, 45);
            this.usernameTxtBox.Name = "usernameTxtBox";
            this.usernameTxtBox.Size = new System.Drawing.Size(100, 20);
            this.usernameTxtBox.TabIndex = 3;
            // 
            // hostTextBox
            // 
            this.hostTextBox.Location = new System.Drawing.Point(73, 19);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(100, 20);
            this.hostTextBox.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Password: ";
            // 
            // Preference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 205);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpLanguages);
            this.Controls.Add(this.bttChoseSqlDir);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMysqlPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Preference";
            this.Text = "Preference";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Preference_FormClosing);
            this.grpLanguages.ResumeLayout(false);
            this.grpLanguages.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLanguages;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdbFrancais;
        private System.Windows.Forms.RadioButton rdbEspanol;
        private System.Windows.Forms.RadioButton rdbItaliano;
        private System.Windows.Forms.RadioButton rdbDeutsch;
        private System.Windows.Forms.RadioButton rdbEnglish;
        private System.Windows.Forms.Button bttChoseSqlDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMysqlPath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox dbNameTxtBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox portTxtBox;
        internal System.Windows.Forms.TextBox passwordTxtBox;
        internal System.Windows.Forms.TextBox usernameTxtBox;
        internal System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}