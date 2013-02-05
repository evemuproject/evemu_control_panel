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
            foreach (DataRow record in DBConnect.AQuery("SELECT typeName from invTypes WHERE typeID = " + loadOreID).Rows)
            {               
               oreName.Text = record[0].ToString();
            }

            string query;
                if (mineralID == "1")
                {
                    query = "SELECT quantity from invTypeMaterials WHERE typeID = " + loadOreID + " and materialTypeID = " + mineralID;
                    foreach (DataRow record in DBConnect.AQuery(query).Rows)
                    {                        
                        quantity.Text = record[0].ToString();
                    }
                }
                if (oldmineralid != "")
                {
                    foreach (DataRow record in DBConnect.AQuery("SELECT typeName from invTypes WHERE typeID = " + oldmineralid).Rows)
                    {
                        mineral.Text = record[0].ToString();
                    }
                }
        }

        private void EditOre_Load(object sender, EventArgs e)
        {
            foreach (DataRow record in DBConnect.AQuery("SELECT typeName from invTypes WHERE groupid = 18").Rows)
            {
                mineral.Items.Add(record[0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (oldmineralid == "0")
            {
                //DBConnect.SQuery("INSERT INTO invTypeMaterials (typeID, activityid, materialTypeID, quantity, damageperjob, recycle) VALUES (" + oreID.Text + ", 6, " + newmineralid + "," + quantity.Text + ", 1, 1)");
                DBConnect.SQuery("INSERT INTO invTypeMaterials (typeID, materialTypeID, quantity) VALUES (" + oreID.Text + ", " + newmineralid + "," + quantity.Text+")");
            }
            else
            {
                DBConnect.SQuery("UPDATE invTypeMaterials set materialTypeID = " + newmineralid + ", quantity = " + quantity.Text + " WHERE typeID = " + oreID.Text + " and materialTypeID = " + oldmineralid);
            }
            Program.m.SELECTOre_SelectedIndexChanged(null, null);
            this.Close();
        }

        private void mineral_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataRow record in DBConnect.AQuery("SELECT typeID from invTypes WHERE typeName = '" + mineral.Text + "'").Rows)
            {
                newmineralid = record[0].ToString();                
            }
        }
    }
}
