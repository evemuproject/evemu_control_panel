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
    public partial class EditOre : Form
    {       
        public string oldmineralid = "";
        public string newmineralid = "";

        public EditOre()
        {
            InitializeComponent();
        }

        public void loadOre(string loadOreID, string mineralID)
        {
            oldmineralid = mineralID;
            newmineralid = mineralID;
            oreID.Text = loadOreID;
            foreach (DataRow record in Program.m.SelectSQL("SELECT typeName from invTypes WHERE typeID = " + loadOreID).Rows)
            {               
               oreName.Text = record[0].ToString();
            }

            string query;
                if (mineralID == "1")
                {
                    query = "SELECT quantity from typeactivitymaterials WHERE typeID = " + loadOreID + " and requiredtypeID = " + mineralID;
                    foreach (DataRow record in Program.m.SelectSQL(query).Rows)
                    {                        
                        quantity.Text = record[0].ToString();
                    }
                }
                if (oldmineralid != "")
                {
                    foreach (DataRow record in Program.m.SelectSQL("SELECT typeName from invTypes WHERE typeID = " + oldmineralid).Rows)
                    {
                        mineral.Text = record[0].ToString();
                    }
                }
        }

        private void EditOre_Load(object sender, EventArgs e)
        {
            foreach (DataRow record in Program.m.SelectSQL("SELECT typeName from invTypes WHERE groupid = 18").Rows)
            {
                mineral.Items.Add(record[0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (oldmineralid == "0")
            {
                Program.m.InsertSQL("INSERT INTO typeactivitymaterials (typeID, activityid, requiredtypeID, quantity, damageperjob, recycle) VALUES (" + oreID.Text + ", 6, " + newmineralid + "," + quantity.Text + ", 1, 1)");
            }
            else
            {
                Program.m.InsertSQL("update typeactivitymaterials set requiredtypeID = " + newmineralid + ", quantity = " + quantity.Text + " WHERE typeID = " + oreID.Text + " and requiredtypeID = " + oldmineralid);
            }
            Program.m.SELECTOre_SelectedIndexChanged(null, null);
            this.Close();
        }

        private void mineral_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataRow record in Program.m.SelectSQL("SELECT typeID from invTypes WHERE typeName = '" + mineral.Text +"'").Rows)
            {
                newmineralid = record[0].ToString();                
            }
        }
    }
}
