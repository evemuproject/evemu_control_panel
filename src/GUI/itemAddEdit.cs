using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using Evemu_DB_Editor.src;



namespace Evemu_DB_Editor
{
    public partial class itemAddEdit : Form
    {
        public itemAddEdit()
        {
            int x = 0;
            int y = 0;
            InitializeComponent();
            x = Evemu_DB_Editor.Program.m.GetDesktopX() + 50;
            y = Evemu_DB_Editor.Program.m.GetDesktopY() + 50;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(x, y);
        }

        public void extractItemInfo(int itemID)
        {
            DataTable invTypesdata = DBConnect.AQuery("SELECT * FROM invTypes WHERE typeID=" + itemID);
            foreach (DataRow record in invTypesdata.Rows)
            {
                typeID1.Text = record["typeID"].ToString();
                groupID.Text = record["GroupID"].ToString();
                typeName.Text = record["typeName"].ToString();
                description.Text = record["description"].ToString();
                graphicID.Text = record["graphicID"].ToString();
                radius.Text = record["radius"].ToString();
                mass.Text = record["mass"].ToString();
                volume.Text = record["volume"].ToString();
                capacity.Text = record["capacity"].ToString();
                portionSize.Text = record["portionSize"].ToString();
                raceID.Text = record["RaceId"].ToString();
                basePrice.Text = record["basePrice"].ToString();
                published.Text = record["published"].ToString();
                marketGroupID.Text = record["marketGroupID"].ToString();
                chanceOfDuplicating.Text = record["chanceOfDuplicating"].ToString();

                //Populate the Group ComboBox
                int groupIndex = groupSelector.FindString(groupID.Text + " -");
                groupSelector.SelectedIndex = groupIndex;

                //Populate the MarketGroup ComboBox
                int marketIndex = marketGroupSelector.FindString(marketGroupID.Text + " -");
                marketGroupSelector.SelectedIndex = marketIndex;
                
                itemAddEdit.ActiveForm.Text = typeName.Text;                
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            information info = new information();
            info.ShowDialog();
        }

        private void itemRefresh_Click(object sender, EventArgs e)
        {
            extractItemInfo(int.Parse(typeID1.Text));
        }        

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            itemAttributes.Items.Clear();
            foreach (DataRow record in DBConnect.AQuery("SELECT dgmTypeAttributes.attributeID, attributeName, valueInt, valueFloat FROM dgmTypeAttributes LEFT JOIN dgmAttributeTypes ON dgmTypeAttributes.attributeID = dgmAttributeTypes.attributeID WHERE (((dgmTypeAttributes.typeID)= " + typeID1.Text + "))").Rows)
            {
                string[] attribute = new string[4];
                attribute[0] = record[0].ToString();
                attribute[1] = record[1].ToString();
                attribute[2] = record[2].ToString();
                attribute[3] = record[3].ToString();  
                ListViewItem attribute2 = new ListViewItem(attribute);
                itemAttributes.Items.Add(attribute2);
            }

            foreach (DataRow record in DBConnect.AQuery("SELECT attributeName FROM dgmAttributeTypes ORDER BY attributeName").Rows)
            {
                attributeDescription.Items.Add(record[0].ToString());
            }
        }

        private void itemAttributes_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem SelectedItem in itemAttributes.SelectedItems)
            {
                attributeID.Text = SelectedItem.SubItems[0].Text;
                attributeDescription.Text = SelectedItem.SubItems[1].Text;
                attributeInt.Text = SelectedItem.SubItems[2].Text;
                attributeFloat.Text = SelectedItem.SubItems[3].Text;
            }
        }
        
        private void attributeDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataRow record in DBConnect.AQuery("SELECT attributeID FROM dgmAttributeTypes WHERE attributeName = '" + attributeDescription.Text + "'").Rows)
            {
                attributeID.Text = record[0].ToString();
            }            
        }

        private void attributeAdd_Click(object sender, EventArgs e)
        {
           if (attributeInt.Text != "") 
           {
               DBConnect.SQuery("INSERT INTO dgmTypeAttributes (typeID, attributeID, valueInt) VALUES (" + typeID1.Text + "," + attributeID.Text + "," + attributeInt.Text + ")");
           }
           else
           {
               DBConnect.SQuery("INSERT INTO dgmTypeAttributes (typeID, attributeID, valueFloat) VALUES (" + typeID1.Text + "," + attributeID.Text + ",'" + attributeFloat.Text + "')");
           }                        
            tabPage2_Enter(null,null);
        }

        private void attributeChange_Click(object sender, EventArgs e)
        {
            if (attributeInt.Text != "")
            {
                DBConnect.SQuery("UPDATE dgmTypeAttributes SET valueInt = " + attributeInt.Text + " WHERE typeID = " + typeID1.Text + " AND attributeID = " + attributeID.Text);
            }
            else
            {
                DBConnect.SQuery("UPDATE dgmTypeAttributes SET valueFloat = '" + attributeFloat.Text + "' WHERE typeID = " + typeID1.Text + " AND attributeID = " + attributeID.Text);
            }
            tabPage2_Enter(null, null);
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (raceID.Text == "")
            {
                raceID.Text = "0";
            }
            DBConnect.SQuery("UPDATE invTypes SET groupID = " + groupID.Text + ", published = " + published.Text + ", marketGroupID = " + marketGroupID.Text + ", typeName = '" + typeName.Text + "', graphicID = " + graphicID.Text + ", radius = " + radius.Text + ", mass = " + mass.Text + ", volume = " + volume.Text + ", capacity = " + capacity.Text + ", portionSize = " + portionSize.Text + ", raceID = " + raceID.Text + ", basePrice = " + basePrice.Text + ", description = '" + description.Text + "' WHERE typeID = " + typeID1.Text);
        }

        private void itemAddEdit_Load(object sender, EventArgs e)
        {
            

            marketGroupSelector.Items.Clear();
            foreach (DataRow record in DBConnect.AQuery("SELECT marketGroupID, parentGroupID AS parent, (SELECT marketGroupName FROM invMarketGroups WHERE marketGroupID = parent) as marketParentName, marketGroupName FROM invMarketGroups ORDER BY marketParentName").Rows)
            {
                marketGroupSelector.Items.Add(record[0].ToString() + " - " + record[2].ToString() + " - " + record[3].ToString());
            }

            groupSelector.Items.Clear();
            foreach (DataRow record in DBConnect.AQuery("SELECT groupID, groupName FROM invGroups ORDER BY groupName").Rows)
            {
               groupSelector.Items.Add(record[0].ToString() + " - " + record[1].ToString());
            }   
        }

        private void groupSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupID.Text = groupSelector.Text.Substring(0, groupSelector.Text.IndexOf(" "));          
        }

        private void marketGroupSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            marketGroupID.Text = marketGroupSelector.Text.Substring(0, marketGroupSelector.Text.IndexOf(" "));
            
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            itemEffects.Items.Clear();
            foreach (DataRow record in DBConnect.AQuery("SELECT effectID AS effect, (SELECT effectname FROM dgmEffects WHERE effectID = effect) as effectName, isDefault FROM dgmTypeEffects WHERE typeID = " + typeID1.Text).Rows)
            {
                string[] attribute = new string[3];
                attribute[0] = record[0].ToString();
                attribute[1] = record[1].ToString();
                attribute[2] = record[2].ToString();                
                ListViewItem attribute2 = new ListViewItem(attribute);
                itemEffects.Items.Add(attribute2);
            }

            foreach (DataRow record in DBConnect.AQuery("SELECT effectName FROM dgmEffects ORDER BY effectName").Rows)
            {
                effectDescription.Items.Add(record[0].ToString());
            }
        }

        private void effectDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataRow record in DBConnect.AQuery("SELECT effectID FROM dgmEffects WHERE effectName = '" + effectDescription.Text + "'").Rows)
            {
                effectID.Text = record[0].ToString();
            }             
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBConnect.SQuery("INSERT INTO dgmTypeEffects (typeID, effectID, isDefault) VALUES (" + typeID1.Text + "," + effectID.Text + "," + isDefault.Text + ")");
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem SelectedItem in itemAttributes.SelectedItems)
            {
                DialogResult confirmDelete = MessageBox.Show("Are you sure you want to delete " + SelectedItem.SubItems[1].Text  + "?", "Confirm delete", MessageBoxButtons.YesNo);
                if (confirmDelete == DialogResult.Yes)
                {
                    DBConnect.SQuery("DELETE FROM dgmTypeAttributes WHERE typeID = " + typeID1.Text + " AND attributeID = " + SelectedItem.SubItems[0].Text);
                    tabPage2_Enter(null, null);
                }
            }
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem SelectedItem in itemEffects.SelectedItems)
            {
                DialogResult confirmDelete = MessageBox.Show("Are you sure you want to delete " + SelectedItem.SubItems[1].Text + "?", "Confirm delete", MessageBoxButtons.YesNo);
                if (confirmDelete == DialogResult.Yes)
                {
                    DBConnect.SQuery("DELETE FROM dgmTypeEffects WHERE typeID = " + typeID1.Text + " AND effectID = " + SelectedItem.SubItems[0].Text);
                    tabPage2_Enter(null, null);
                }
            }
        }

        private void itemEffects_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem SelectedItem in itemEffects.SelectedItems)
            {
                effectDescription.Text = SelectedItem.SubItems[1].Text;
                tabPage3_Enter(null, null);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            DBConnect.SQuery("DELETE FROM invTypes WHERE typeID=" + typeID1.Text); // the item itself
            DBConnect.SQuery("DELETE FROM dgmTypeAttributes WHERE typeID=" + typeID1.Text); // all it's attributes
            DBConnect.SQuery("DELETE FROM invShipTypes WHERE shiptypeID=" + typeID1.Text); // if it's a ship, delete it from ship types...

            this.Close(); // This item does not exist anymore, no point providing any info anymore...
        }
    }
}
