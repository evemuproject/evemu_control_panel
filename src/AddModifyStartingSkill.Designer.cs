namespace Evemu_DB_Editor
{
    partial class AddModifyStartingSkill
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
            this.skillID = new System.Windows.Forms.TextBox();
            this.level = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.RaceOrCareer = new System.Windows.Forms.ComboBox();
            this.skillName = new System.Windows.Forms.ComboBox();
            this.raceOrCareerID = new System.Windows.Forms.TextBox();
            this.NewStartingSkill = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // skillID
            // 
            this.skillID.Location = new System.Drawing.Point(12, 25);
            this.skillID.Name = "skillID";
            this.skillID.ReadOnly = true;
            this.skillID.Size = new System.Drawing.Size(62, 20);
            this.skillID.TabIndex = 0;
            // 
            // level
            // 
            this.level.Location = new System.Drawing.Point(80, 25);
            this.level.Name = "level";
            this.level.Size = new System.Drawing.Size(62, 20);
            this.level.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(80, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(148, 51);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(62, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // RaceOrCareer
            // 
            this.RaceOrCareer.FormattingEnabled = true;
            this.RaceOrCareer.Items.AddRange(new object[] {
            "Race",
            "Career"});
            this.RaceOrCareer.Location = new System.Drawing.Point(236, 24);
            this.RaceOrCareer.Name = "RaceOrCareer";
            this.RaceOrCareer.Size = new System.Drawing.Size(82, 21);
            this.RaceOrCareer.TabIndex = 6;
            // 
            // skillName
            // 
            this.skillName.FormattingEnabled = true;
            this.skillName.Location = new System.Drawing.Point(148, 24);
            this.skillName.Name = "skillName";
            this.skillName.Size = new System.Drawing.Size(82, 21);
            this.skillName.TabIndex = 7;
            this.skillName.SelectedIndexChanged += new System.EventHandler(this.skillName_SelectedIndexChanged);
            // 
            // raceOrCareerID
            // 
            this.raceOrCareerID.Location = new System.Drawing.Point(236, 51);
            this.raceOrCareerID.Name = "raceOrCareerID";
            this.raceOrCareerID.Size = new System.Drawing.Size(23, 20);
            this.raceOrCareerID.TabIndex = 8;
            this.raceOrCareerID.Visible = false;
            // 
            // NewStartingSkill
            // 
            this.NewStartingSkill.AutoSize = true;
            this.NewStartingSkill.Location = new System.Drawing.Point(14, 61);
            this.NewStartingSkill.Name = "NewStartingSkill";
            this.NewStartingSkill.Size = new System.Drawing.Size(15, 14);
            this.NewStartingSkill.TabIndex = 9;
            this.NewStartingSkill.UseVisualStyleBackColor = true;
            this.NewStartingSkill.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Skill ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(77, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Skill level";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(145, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Skill name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(233, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Race or career";
            // 
            // AddModifyStartingSkill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 77);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NewStartingSkill);
            this.Controls.Add(this.raceOrCareerID);
            this.Controls.Add(this.skillName);
            this.Controls.Add(this.RaceOrCareer);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.level);
            this.Controls.Add(this.skillID);
            this.Name = "AddModifyStartingSkill";
            this.Text = "AddModifyStartingSkill";
            this.Load += new System.EventHandler(this.AddModifyStartingSkill_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.ComboBox RaceOrCareer;
        public System.Windows.Forms.TextBox skillID;
        public System.Windows.Forms.TextBox level;
        public System.Windows.Forms.ComboBox skillName;
        public System.Windows.Forms.TextBox raceOrCareerID;
        public System.Windows.Forms.CheckBox NewStartingSkill;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}