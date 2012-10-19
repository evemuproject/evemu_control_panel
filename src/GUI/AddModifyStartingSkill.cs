using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Evemu_DB_Editor.src;

namespace Evemu_DB_Editor
{
    public partial class AddModifyStartingSkill : Form
    {
        public AddModifyStartingSkill()
        {
            InitializeComponent();
        }

        private void AddModifyStartingSkill_Load(object sender, EventArgs e)
        {
            foreach (DataRow record in DBConnect.AQuery("SELECT typeName from invTypes WHERE groupid in (SELECT groupid from invGroups WHERE categoryid = 16)").Rows)
            {
                skillName.Items.Add(record[0].ToString());
            }
        }

        private void skillName_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataRow record in DBConnect.AQuery("SELECT typeID from invTypes WHERE groupid IN (SELECT groupid from invGroups WHERE categoryid = 16) AND typeName = '" + skillName.Text + "'").Rows)
            {
                skillID.Text = record[0].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (NewStartingSkill.Checked == false)
            {
                switch (RaceOrCareer.Text)
                {
                    case "Race":
                        DBConnect.SQuery("UPDATE chrRaceskills SET levels =" + level.Text + " WHERE SkilltypeID = " + skillID.Text + " AND raceID = " + raceOrCareerID.Text);
                        break;
                    case "Career":
                        //Todo
                        break;
                }
            }
            else
            {
                switch (RaceOrCareer.Text)
                {
                    case "Race":
                        DBConnect.SQuery("INSERT INTO chrRaceskills (raceID, SkilltypeID, levels) VALUES (" + raceOrCareerID.Text + "," + skillID.Text + "," + level.Text + ")");
                        break;
                    case "Career":
                        //Todo
                        break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
