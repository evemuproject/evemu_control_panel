using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Evemu_DB_Editor
{    
    public partial class editCharacterSkill : Form
    {
        public int newskill = 0;
        public editCharacterSkill()
        {
            InitializeComponent();
        }

        private void editCharacterSkill_Load(object sender, EventArgs e)
        {
            foreach (DataRow record in Program.m.SelectSQL("SELECT typeName from invTypes WHERE groupid in (SELECT groupid from invGroups WHERE categoryid = 16) order by typeName").Rows)
            {
                skillName.Items.Add(record[0].ToString());
            }
        }

        private void skillName_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataRow record in Program.m.SelectSQL("SELECT typeID from invTypes WHERE typeName = '" + skillName.Text + "'").Rows)
            {
                skillID.Text = record[0].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(skillLevel.Text) < 5)
            {
                skillLevel.Text = Convert.ToString(Convert.ToInt16(skillLevel.Text) + 1); //Really, all the converting, vb is so much better
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(skillLevel.Text) > 0)
            {
                skillLevel.Text = Convert.ToString(Convert.ToInt16(skillLevel.Text) - 1); //See above comment, crappy C#
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (newskill == 1)
            {
                Program.m.InsertSQL("INSERT INTO entity (itemName, typeID, ownerID, locationID, flag, contraband, singleton, quantity, x, y ,z) VALUES ('" + skillName.Text + "'," + skillID.Text + "," + characterID.Text + "," + characterID.Text + ", 7,0,1,1,0,0,0)");
                Program.m.InsertSQL("INSERT INTO entity_attributes (itemID, attributeID, valueInt) VALUES ((SELECT itemID from entity WHERE ownerID = " + characterID.Text + " and itemName = '" + skillName.Text + "'), 276, " + skillLevel.Text + ")");
                Program.m.InsertSQL("INSERT INTO entity_attributes (itemID, attributeID, valueInt) VALUES ((SELECT itemID from entity WHERE ownerID = " + characterID.Text + " and itemName = '" + skillName.Text + "'), 280, " + skillPoints.Text + ")");
            }
            else
            {
            }
            this.Close();
        }
    }
}
