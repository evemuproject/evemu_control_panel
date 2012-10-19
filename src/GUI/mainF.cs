/*------------------------------------------------------------------------------------
LICENSE:
------------------------------------------------------------------------------------
This file is part of EVEmu: EVE Online Server Emulator
Copyright 2006 - 2011 The EVEmu Team
For the latest information visit http://evemu.org
------------------------------------------------------------------------------------
This program is free software; you can redistribute it and/or modify it under
the terms of the GNU Lesser General Public License as published by the Free Software
Foundation; either version 2 of the License, or (at your option) any later
version.

This program is distributed in the hope that it will be useful, but WITHOUT
ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License along with
this program; if not, write to the Free Software Foundation, Inc., 59 Temple
Place - Suite 330, Boston, MA 02111-1307, USA, or go to
http://www.gnu.org/copyleft/lesser.txt.
------------------------------------------------------------------------------------
*/
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Evemu_DB_Editor.src;
using MySql.Data.MySqlClient;
using StuffArchiver.src;

namespace Evemu_DB_Editor
{
    public partial class main : Form
    {
        internal MySqlConnection conn;
        internal string connection;
        internal bool recordqueries = false;

        [StructLayout(LayoutKind.Sequential)]

        public struct FILE_TIME
        {
            public UInt32 dwLowDateTime;
            public UInt32 dwHighDateTime;
        }

        [DllImport("kernel32")]
        public static extern void GetSystemTimeAsFileTime(ref FILE_TIME lpSystemTimeAsFileTime);

        #region Main
        public main()
        {
            InitializeComponent();
            queryMarketSeedTxtBox.Text = "* Some market items require NULL for their raceID, so in order to SEED these items, DO NOT select ANY race above." + Environment.NewLine + Environment.NewLine;
            queryMarketSeedTxtBox.Text += "* MySQL queries will appear in this window after query completes its operation on the Database." + Environment.NewLine + Environment.NewLine;
            queryMarketSeedTxtBox.Text += "* UN-check check box to show query without affecting the Database." + Environment.NewLine + Environment.NewLine;
            queryMarketSeedTxtBox.Text += "* Click the Clear Query button to clear this window at any time." + Environment.NewLine + Environment.NewLine;
            queryMarketSeedTxtBox.Text += "* Each click of the Seed Market button will replace this window's current contents with the new query.";
        }

        private void main_Load(object sender, EventArgs e)
        {
            connection = ("server=" + hostTextBox.Text + ";username=" + usernameTxtBox.Text + ";password=" + passwordTxtBox.Text + ";port=" + portTxtBox.Text + ";database=" + dbNameTxtBox.Text);
            if (System.IO.File.Exists(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\EvEMU DB\\EvemuDBEditor.ini"))
            {
                System.IO.StreamReader f = new System.IO.StreamReader(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\EvEMU DB\\EvemuDBEditor.ini");
                while (f.Peek() >= 0)
                {
                    String Line = f.ReadLine();
                    if (Line.Contains("Hostname="))
                    {
                        hostTextBox.Text = Line.Replace("Hostname=", "");
                    }
                    if (Line.Contains("Username="))
                    {
                        usernameTxtBox.Text = Line.Replace("Username=", "");
                    }
                    if (Line.Contains("Password="))
                    {
                        passwordTxtBox.Text = Line.Replace("Password=", "");
                    }
                    if (Line.Contains("Port="))
                    {
                        portTxtBox.Text = Line.Replace("Port=", "");
                    }
                    if (Line.Contains("Database="))
                    {
                        dbNameTxtBox.Text = Line.Replace("Database=", "");
                    }
                }
                f.Close();
                connectBtn_Click(null, null);
            }
        }

        private void main_ResizeEnd(object sender, EventArgs e)
        {
            if (this.Width < 824)
            {
                this.Width = 824;
                this.Refresh();
            }
            if (this.Height < 329)
            {
                this.Height = 329;
                this.Refresh();
            }
        }
        #endregion

        #region File Tab

        #endregion

        #region Options Tab
        private void recordQueriesDisabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (recordqueries == false)
            {
                recordqueries = true;
                recordQueriesDisabledToolStripMenuItem.Text = "Record queries (Enabled)";
            }
            else
            {
                recordqueries = false;
                recordQueriesDisabledToolStripMenuItem.Text = "Record queries (Disabled)";
            }
        }
        #endregion

        #region Help Tab
        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            information info = new information();
            info.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made by: Matthew aka: Hurracane @ evemu.mmoforge.org\nAlpha Version.\n\nVersion: " + System.Reflection.Assembly.GetExecutingAssembly().
         GetName().Version.ToString());
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region SQL Settings Tab
        private void connectBtn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(hostTextBox.Text) || String.IsNullOrEmpty(usernameTxtBox.Text) || String.IsNullOrEmpty(passwordTxtBox.Text) || String.IsNullOrEmpty(dbNameTxtBox.Text))
            {
                MessageBox.Show("Please make sure all the input fields are filled in.");
            }
            else
            {
                DBConnect.OpenConnection(hostTextBox.Text, usernameTxtBox.Text, passwordTxtBox.Text, dbNameTxtBox.Text);
                if (DBConnect.isConnectionOpen)
                {
                    connectionStatusLbl.Text = "Connected";
                    connectionStatusLbl.ForeColor = Color.Green;
                    connectionStatusToolTip.Text = "Connected To DB";
                    connectionStatusToolTip.ForeColor = Color.Green;
                    connectBtn.Enabled = false;
                    disconnectBtn.Enabled = true;
                }
                else
                {
                    connectionStatusLbl.Text = "Error";
                    connectionStatusLbl.ForeColor = Color.Red;
                }
            }
        }

        private void disconnectBtn_Click(object sender, EventArgs e)
        {
            DBConnect.CloseConnection();
            connectionStatusLbl.Text = "Disconnected";
            connectionStatusLbl.ForeColor = Color.Black;
            connectionStatusToolTip.Text = "Disconnected from DB";
            connectionStatusToolTip.ForeColor = Color.Black;
            connectBtn.Enabled = true;
            disconnectBtn.Enabled = false;
        }

        private void runSQLFileBtn_Click(object sender, EventArgs e)
        {
            string query = "";
            MessageBox.Show("This will take 20+ minutes. Dump is very large");
            string sqlStr = "";
            StreamReader reader = new StreamReader(textBoxSQLFile.Text);
            sqlStr = reader.ReadToEnd();
            conn = new MySqlConnection();
            conn.ConnectionString = Program.m.connection;
            query = sqlStr;
            MySqlCommand cmd = new MySqlCommand(query, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            MessageBox.Show("Database Populated.");
        }

        private void openSQLFileBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                textBoxSQLFile.Text = openFileDialog2.FileName;
            }

        }

        private void createDatabaseBtn_Click(object sender, EventArgs e)
        {
            try
            {

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysql";
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = false;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2}",
                    usernameTxtBox, passwordTxtBox, hostTextBox);
                psi.Arguments = string.Format("create", @"{0}",
                    "database", dbNameTxtBox);
                psi.UseShellExecute = false;


                Process process = Process.Start(psi);
                process.StandardInput.Close();
                process.WaitForExit();
                process.Close();
            }
            catch (IOException)
            {
                MessageBox.Show("Error , Failed to create database.!");
            }
        }

        private void goToForumsBtn_Click(object sender, EventArgs e)
        {
            Process.Start("http://evemu.mmoforge.org/forum/");
        }
        #endregion

        #region Account/Character Editor Tab
        
        private void searchAccountBtn_Click(object sender, EventArgs e)
        {
            populateAccountList();
        }

        private void addAccountBtn_Click(object sender, EventArgs e)
        {
            string query = "";

            if (newUsername.Text != "" && newPassword.Text != "" && newAcctLevel.Text != "")
            {
                conn = new MySqlConnection(connection);
                string acctrole = newAcctLevel.Text;
                if (acctrole == "SUPER ADMIN")
                {
                    acctrole = "4294967231";
                }
                else if (acctrole == "ADMIN")
                {
                    acctrole = "32";
                }
                else if (acctrole == "GAME MASTER HIGH")
                {
                    acctrole = "16";
                }
                else if (acctrole == "GAME MASTER LOW")
                {
                    acctrole = "8";
                }
                else if (acctrole == "PLAYER")
                {
                    acctrole = "2";
                }
                else
                {
                    MessageBox.Show("You have not selected a role, the account will be \"2 (PLAYER)\" by default.");
                    acctrole = "2";
                }

                query = "INSERT INTO account (accountName, password, role) VALUES ('" + newUsername.Text + "', '" + newPassword.Text + "', '" + acctrole + "')";

                if (DBConnect.SQuery(query) == true)
                {
                    newUsername.Clear();
                    newPassword.Clear();
                    newAcctLevel.SelectedIndex = -1;
                    populateAccountList();
                    MessageBox.Show("Account Created!");
                }
            }
            else
            {
                MessageBox.Show("Please make sure all the fields are filled in.");
            }
        }

        private void deleteAccountToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(accountList.SelectedItems[0].Text))
            {
                MessageBox.Show("Please right click an account.");
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to delete the account: " + accountList.SelectedItems[0].SubItems[1].Text + "?", "Delete?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DBConnect.SQuery("DELETE FROM account WHERE accountID=" + accountList.SelectedItems[0].SubItems[0].Text);
                    accountList.Items.Remove(accountList.SelectedItems[0]);
                }
            }
        }

        private void editAccountToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(accountList.SelectedItems[0].Text))
            {
                acctEdit acc = new acctEdit();
                acc.Show();
                ListViewItem item = accountList.SelectedItems[0];
                acc.GetAccountInfo(item.SubItems[0].Text);
                acc.userID.Text = item.SubItems[0].Text;
            }
        }

        private void characterNameDropDown_DropDown(object sender, EventArgs e)
        {
            characterSkillsList.Items.Clear();
            foreach (DataRow record in DBConnect.AQuery("SELECT typeID, itemName, itemID AS currentitemID, (SELECT valueInt FROM entity_attributes WHERE itemID = currentitemID AND attributeid = 280) AS level, (SELECT valueInt FROM entity_attributes WHERE itemID = currentitemID AND attributeid = 276) AS skillpoints FROM entity WHERE flag = 7 AND ownerid = " + characterIDTxtBox.Text).Rows)
            {
                string[] temp = new string[4];
                temp[0] = record[0].ToString();
                temp[1] = record[1].ToString();
                temp[2] = record[3].ToString();
                temp[3] = record[4].ToString();
                ListViewItem temp2 = new ListViewItem(temp);
                characterSkillsList.Items.Add(temp2);
            }
        }

        private void characterNameDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataRow record in DBConnect.AQuery("SELECT itemID FROM entity WHERE itemName = '" + characterNameDropDown.Text + "'").Rows)
            {
                characterIDTxtBox.Text = record[0].ToString();
            }

            characterSkillsList.Items.Clear();
            foreach (DataRow record in DBConnect.AQuery("SELECT typeID, itemName, itemID AS currentitemID, (SELECT valueInt FROM entity_attributes WHERE itemID = currentitemID AND attributeid = 280) AS level, (SELECT valueInt FROM entity_attributes WHERE itemID = currentitemID AND attributeid = 276) AS skillpoints FROM entity WHERE flag = 7 AND ownerid = " + characterIDTxtBox.Text).Rows)
            {
                string[] temp = new string[4];
                temp[0] = record[0].ToString();
                temp[1] = record[1].ToString();
                temp[2] = record[3].ToString();
                temp[3] = record[4].ToString();
                ListViewItem temp2 = new ListViewItem(temp);
                characterSkillsList.Items.Add(temp2);
            }

        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            editCharacterSkill editSkill = new editCharacterSkill();
            editSkill.newskill = 1;
            editSkill.characterID.Text = characterIDTxtBox.Text;
            editSkill.ShowDialog();
            characterNameDropDown_SelectedIndexChanged(null, null);
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Too lazy to implement right now, use remove/add for now");
            //Code below is fine, too lazy to write the update queries

            //foreach (ListViewItem SelectedItem in characterSkills.SelectedItems)
            //{
            //editCharacterSkill editSkill = new editCharacterSkill();            
            //editSkill.skillID.Text = SelectedItem.SubItems[0].Text;
            //editSkill.skillName.Text = SelectedItem.SubItems[1].Text;
            //editSkill.skillLevel.Text = SelectedItem.SubItems[2].Text;
            //editSkill.skillPoints.Text = SelectedItem.SubItems[3].Text;
            //editSkill.characterID.Text = characterID.Text;
            //editSkill.newskill = 0;
            //editSkill.ShowDialog();
            //characterName_SelectedIndexChanged(null, null);
            //}  
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem SelectedItem in characterSkillsList.SelectedItems)
            {
                DBConnect.SQuery("DELETE FROM entity_attributes WHERE itemID in (SELECT itemID FROM entity WHERE ownerID = " + characterIDTxtBox.Text + " AND typeID = " + SelectedItem.SubItems[0].Text + ")");
                DBConnect.SQuery("DELETE FROM entity WHERE ownerID = " + characterIDTxtBox.Text + " AND typeID = " + SelectedItem.SubItems[0].Text);
            }
            characterNameDropDown_SelectedIndexChanged(null, null);
        }

        private void populateAccountList()
        {
            accountList.Items.Clear();
            string query = "SELECT accountID, accountName, role, banned, online FROM account WHERE(accountName like \'" + accountTxtBox.Text + "%\')";
            foreach (DataRow record in DBConnect.AQuery(query).Rows)
            {
                string[] list = { record[0].ToString(), record[1].ToString() };
                ListViewItem item = new ListViewItem(list);
                accountList.Items.Add(item);
            }
        }

        #endregion

        #region Item Editor

        private void tabItemEditor_Enter(object sender, EventArgs e)
        {
            foreach (DataRow record in DBConnect.AQuery("SELECT CategoryName from invCategories").Rows)
            {
                CategoryDropdown.Items.Add(record[0]);
            }

        }

        private void searchItem_Click(object sender, EventArgs e)
        {
            ItemList.Items.Clear();
            string SQLQuery = "SELECT invTypes.typeID, invTypes.typeName, invGroups.groupName, chrRaces.raceName, invTypes.description FROM (invCategories RIGHT JOIN (invTypes LEFT JOIN invGroups ON invTypes.groupID = invGroups.groupID) ON invCategories.categoryID = invGroups.categoryID) LEFT JOIN chrRaces ON invTypes.raceID = chrRaces.raceID WHERE (((invCategories.categoryName) = '" + CategoryDropdown.Text + "') and typeName like '%" + searchItemTxtBox.Text + "%')";

            DataTable data = DBConnect.AQuery(SQLQuery);
            String[] item = new string[data.Columns.Count];
            ListViewItem[] items = new ListViewItem[data.Rows.Count];
            int count = 0;

            foreach (DataRow row in data.Rows)
            {
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    item[i] = row[i].ToString();
                }

                ListViewItem item2 = new ListViewItem(item);
                items[count] = item2;
                count++;
            }
            ItemList.Items.AddRange(items);
        }
        
        private void searchItemTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchItem_Click(null, null);
            }
        }

        private void editItemBtn_Click(object sender, EventArgs e)
        {
            itemAddEdit add = new itemAddEdit();
            add.Show();
            add.extractItemInfo(int.Parse(ItemList.SelectedItems[0].SubItems[0].Text));
        }

        private void CategoryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemList.Items.Clear();
            string SQLQuery = "SELECT invTypes.typeID, invTypes.typeName, invGroups.groupName, chrRaces.raceName, invTypes.description FROM (invCategories RIGHT JOIN (invTypes LEFT JOIN invGroups ON invTypes.groupID = invGroups.groupID) ON invCategories.categoryID = invGroups.categoryID) LEFT JOIN chrRaces ON invTypes.raceID = chrRaces.raceID WHERE (((invCategories.categoryName) = '" + CategoryDropdown.Text + "'))";

            DataTable SQLData = DBConnect.AQuery(SQLQuery);
            String[] item = new string[SQLData.Columns.Count];
            ListViewItem[] items = new ListViewItem[SQLData.Rows.Count];
            int count = 0;

            foreach (DataRow row in SQLData.Rows)
            {
                for (int i = 0; i < SQLData.Columns.Count; i++)
                {
                    item[i] = row[i].ToString();
                }

                ListViewItem item2 = new ListViewItem(item);
                items[count] = item2;
                count++;
            }
            ItemList.Items.AddRange(items);
            searchItem.Visible = true;
            label35.Visible = true;
            searchItemTxtBox.Visible = true;
        }

        private void ItemList_DblClick(object sender, EventArgs e)
        {
            foreach (ListViewItem SelectedItem in ItemList.SelectedItems)
            {
                itemAddEdit add = new itemAddEdit();
                add.Show();
                add.extractItemInfo(int.Parse(ItemList.SelectedItems[0].SubItems[0].Text));
            }
        }

        private void copyItemRClkBtn_Click(object sender, EventArgs e)
        {
            DataTable FindNewID = DBConnect.AQuery("SELECT max(typeID) from invTypes");
            int newid = 0;
            foreach (DataRow record in FindNewID.Rows)
            {
                newid = Convert.ToInt16(record[0].ToString()) + 1;
            }

            foreach (ListViewItem SelectedItem in ItemList.SelectedItems)
            {
                int typeID = Convert.ToInt16(SelectedItem.SubItems[0].Text);
                DBConnect.SQuery("INSERT INTO invTypes(typeID, groupID, typeName, description, graphicID, radius, mass, volume, capacity, portionSize, raceID, basePrice, published, marketGroupID, chanceOfDuplicating) SELECT '" + newid + "', groupID, concat('Copy of ', typeName), description, graphicID, radius, mass, volume, capacity, portionSize, raceID, basePrice, published, marketGroupID, chanceOfDuplicating from invTypes WHERE typeID = '" + typeID + "'");
                DBConnect.SQuery("INSERT INTO dgmTypeAttributes SELECT '" + newid + "', attributeID, valueInt, valueFloat from dgmTypeAttributes WHERE typeID = '" + typeID + "'");
                DBConnect.SQuery("INSERT INTO invShipTypes SELECT '" + newid + "', weapontypeID, miningtypeID, skilltypeID from invShipTypes WHERE shiptypeID = '" + typeID + "'");
                DBConnect.SQuery("INSERT INTO dgmTypeEffects SELECT '" + newid + "', effectID, isDefault from dgmTypeEffects WHERE typeID = '" + typeID + "'");
            }
            CategoryDropdown_SelectedIndexChanged(null, null);

            itemAddEdit add = new itemAddEdit();
            add.Show();
            add.extractItemInfo(newid);
        }

        private void editItemRClkBtn_Click(object sender, EventArgs e)
        {
            ItemList_DblClick(null, null);
        }

        private void deleteItemRClkBtn_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem SelectedItem in ItemList.SelectedItems)
            {
                DBConnect.SQuery("DELETE FROM invTypes WHERE typeID = '" + SelectedItem.SubItems[0].Text + "'");
                DBConnect.SQuery("DELETE FROM dgmTypeAttributes WHERE typeID = '" + SelectedItem.SubItems[0].Text + "'");
                DBConnect.SQuery("DELETE FROM shiptypes WHERE shiptypeID = '" + SelectedItem.SubItems[0].Text + "'");
            }
            CategoryDropdown_SelectedIndexChanged(null, null);
        }

        #endregion

        #region Insurance Tab
        private void goBtn_Click(object sender, EventArgs e)
        {
            //not implemented
        }

        #endregion

        #region Race
        private void raceTab_Enter(object sender, EventArgs e)
        {
            raceDropdown.Items.Clear();
            foreach (DataRow record in DBConnect.AQuery("SELECT raceName from chrRaces").Rows)
            {
                raceDropdown.Items.Add(record[0]);
            }
        }

        private void raceDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            bloodlineDropdown.Items.Clear();
            careerDropdown.Items.Clear();
            raceDetails.Visible = true;
            startingSkills.Items.Clear();

            foreach (DataRow record in DBConnect.AQuery("SELECT bloodlineName from chrBloodlines WHERE raceID IN (SELECT raceID from chrRaces WHERE raceName = '" + raceDropdown.Text + "')").Rows)
            {
                bloodlineDropdown.Items.Add(record[0]);
            }

            foreach (DataRow record in DBConnect.AQuery("SELECT careerName from chrCareers WHERE raceID in (SELECT raceID from chrRaces WHERE raceName = '" + raceDropdown.Text + "')").Rows)
            {
                careerDropdown.Items.Add(record[0]);
            }

            foreach (DataRow record in DBConnect.AQuery("SELECT raceName, description, shortDescription from chrRaces WHERE raceName = '" + raceDropdown.Text + "'").Rows)
            {
                raceNameTxtBox.Text = record[0].ToString();
                raceDescriptionTxtBox.Text = record[1].ToString();
                raceDescriptionShortTxtBox.Text = record[2].ToString();
            }

            foreach (DataRow record in DBConnect.AQuery("SELECT chrRaceskills.*, invTypes.typeName from chrRaceskills, invTypes WHERE chrRaceskills.skilltypeID = invTypes.typeID and chrRaceskills.raceID in (SELECT raceID from chrRaces WHERE raceName = '" + raceDropdown.Text + "')").Rows)
            {
                string[] Skill = new string[4];
                Skill[0] = record[1].ToString();
                Skill[1] = record[2].ToString();
                Skill[2] = record[3].ToString();
                //Skill[3] = record[4].ToString();
                ListViewItem Skill2 = new ListViewItem(Skill);
                startingSkills.Items.Add(Skill2);
            }
        }

        private void bloodlineDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            bloodlineDetails.Visible = true;
            bloodlineStartingStats.Visible = true;

            foreach (DataRow record in DBConnect.AQuery("SELECT * from chrBloodlines WHERE bloodlineName = '" + bloodlineDropdown.Text + "'").Rows)
            {
                bloodlineNameTxtBox.Text = record[1].ToString();
                startingShipTypeTxtBox.Text = record[6].ToString();
                startingCorporationTxtBox.Text = record[7].ToString();
                bloodlineWillpowerTxtBox.Text = record[8].ToString();
                bloodlineMemoryTxtBox.Text = record[9].ToString();
                bloodlineIntelligenceTxtBox.Text = record[10].ToString();
                bloodlineCharismaTxtBox.Text = record[11].ToString();
                bloodlinePerceptionTxtBox.Text = record[12].ToString();
            }
            ancestryDropdown.Items.Clear();
            foreach (DataRow record in DBConnect.AQuery("SELECT ancestryName from chrAncestries WHERE bloodlineID in (SELECT bloodlineID from chrBloodlines WHERE bloodlineName = '" + bloodlineDropdown.Text + "')").Rows)
            {
                ancestryDropdown.Items.Add(record[0]);
            }
        }

        private void ancestryDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ancestryDetails.Visible = true;
            foreach (DataRow record in DBConnect.AQuery("SELECT * from chrAncestries WHERE ancestryName = '" + ancestryDropdown.Text + "'").Rows)
            {
                ancestryNameTxtBox.Text = record[1].ToString();
                ancestryWillpowerTxtBox.Text = record[5].ToString();
                ancestryMemoryTxtBox.Text = record[7].ToString();
                ancestryIntelligenceTxtBox.Text = record[8].ToString();
                ancestryCharismaTxtBox.Text = record[6].ToString();
                ancestryPerceptionTxtBox.Text = record[4].ToString();
            }
        }

        private void careerDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            //does the career even add skills anymore?

            //foreach (DataRow record in SelectSQL("SELECT chrCareerskills.*, invTypes.typeName from chrCareerskills, invTypes WHERE chrCareerskills.skilltypeID = invTypes.typeID and chrCareerskills.careerID in (SELECT careerID from chrCareers WHERE careerName = '" + careerDropdown.Text + "' and raceID in (SELECT raceID from chrRaces WHERE raceName = '" + raceDropdown.Text + "'))").Rows)
            //{
            //    string[] Skill = new string[4];
            //    Skill[0] = record[1].ToString();
            //    Skill[1] = record[2].ToString();
            //    Skill[2] = record[3].ToString();
            //    Skill[3] = "Career";
            //    ListViewItem Skill2 = new ListViewItem(Skill);
            //    startingSkills.Items.Add(Skill2);
            //}
        }

        private void saveRaceChangesBtn_Click(object sender, EventArgs e)
        {
            if (bloodlineDropdown.Text == "" | ancestryDropdown.Text == "")
            {
                MessageBox.Show("Please SELECT a bloodline and ancestry.");
            }
            else
            {
                //Save race changes
                DBConnect.SQuery("update chrRaces set raceName = '" + raceNameTxtBox.Text + "', description = '" + raceDescriptionTxtBox.Text + "', shortDescription = '" + raceDescriptionShortTxtBox.Text + "' WHERE raceName = '" + raceDropdown.Text + "'");
                raceTab_Enter(null, null);

                //save bloodline changes
                DBConnect.SQuery("update chrBloodlines set bloodlineName = '" + bloodlineNameTxtBox.Text + "', perception = '" + bloodlinePerceptionTxtBox.Text + "', willpower = '" + bloodlineWillpowerTxtBox.Text + "', memory = '" + bloodlineMemoryTxtBox.Text + "', intelligence = '" + bloodlineIntelligenceTxtBox.Text + "', charisma = '" + bloodlineCharismaTxtBox.Text + "', shiptypeID = '" + startingShipTypeTxtBox.Text + "', corporationid = '" + startingCorporationTxtBox.Text + "' WHERE bloodlineName = '" + bloodlineDropdown.Text + "'");

                //save ancestry changes
                DBConnect.SQuery("update chrAncestries set ancestryName = '" + ancestryNameTxtBox.Text + "', perception = '" + ancestryPerceptionTxtBox.Text + "', willpower = '" + ancestryWillpowerTxtBox.Text + "', memory = '" + ancestryMemoryTxtBox.Text + "', intelligence = '" + ancestryIntelligenceTxtBox.Text + "', charisma = '" + ancestryCharismaTxtBox.Text + "' WHERE ancestryName = '" + ancestryDropdown.Text + "'");

                //Save career changes, yet to be written if it even exists nowadays
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddModifyStartingSkill PopupWindow = new AddModifyStartingSkill();
            PopupWindow.Show();

            PopupWindow.NewStartingSkill.Checked = true;

            foreach (ListViewItem Selectedskill in startingSkills.SelectedItems)
            {
                switch (Selectedskill.SubItems[3].Text)
                {
                    //have to add career/race id
                    case "Race":
                        PopupWindow.raceOrCareerID.Text = "1"; //always caldari
                        break;
                    case "Career":
                        PopupWindow.raceOrCareerID.Text = "";
                        break;
                }
            }
        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddModifyStartingSkill PopupWindow = new AddModifyStartingSkill();
            PopupWindow.Show();
            foreach (ListViewItem Selectedskill in startingSkills.SelectedItems)
            {
                PopupWindow.skillID.Text = Selectedskill.SubItems[0].Text;
                PopupWindow.level.Text = Selectedskill.SubItems[1].Text;
                PopupWindow.skillName.Text = Selectedskill.SubItems[2].Text;
                PopupWindow.RaceOrCareer.Text = Selectedskill.SubItems[3].Text;
                PopupWindow.NewStartingSkill.Checked = false;

                switch (Selectedskill.SubItems[3].Text)
                {
                    //have to add career/race id
                    case "Race":
                        PopupWindow.raceOrCareerID.Text = "";
                        break;
                    case "Career":
                        PopupWindow.raceOrCareerID.Text = "";
                        break;
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Selectedskill in startingSkills.SelectedItems)
            {
                switch (Selectedskill.SubItems[3].Text)
                {
                    case "Career":
                        //InsertSQL("DELETE FROM chrCareerskills WHERE skilltypeID = " + Selectedskill.SubItems[1].Text + " and careerID");
                        break;
                    case "Race":
                        DBConnect.SQuery("DELETE FROM chrRaceskills WHERE skilltypeID = " + Selectedskill.SubItems[0].Text + " AND raceID IN (SELECT raceID FROM chrRaces WHERE raceName = '" + raceDropdown.Text + "')");
                        raceDropdown_SelectedIndexChanged(null, null);
                        break;
                }
            }
        }
        #endregion

        #region Market
        private void marketTab_Enter(object sender, EventArgs e)
        {
            //marketRaces.Items.Add("*");
            marketRacesTxtBox.Items.Clear();
            foreach (DataRow record in DBConnect.AQuery("SELECT raceName, raceID FROM chrRaces ORDER BY raceName").Rows)
            {
                string[] temp = new string[2];
                temp[0] = record[0].ToString();
                temp[1] = record[1].ToString();
                ListViewItem temp2 = new ListViewItem(temp);
                marketRacesTxtBox.Items.Add(temp2);
            }

            //marketRegions.Items.Add("*");
            marketRegionsTxtBox.Items.Clear();
            foreach (DataRow record in DBConnect.AQuery("SELECT regionName, regionID FROM mapRegions ORDER BY regionName").Rows)
            {
                string[] temp = new string[2];
                temp[0] = record[0].ToString();
                temp[1] = record[1].ToString();
                ListViewItem temp2 = new ListViewItem(temp);
                marketRegionsTxtBox.Items.Add(temp2);
            }

            //marketSystems.Items.Add("*");
            marketSystemsTxtBox.Items.Clear();
            foreach (DataRow record in DBConnect.AQuery("SELECT solarSystemName, solarSystemID, security FROM mapSolarSystems ORDER BY solarSystemName").Rows)
            {
                string[] temp = new string[3];
                temp[0] = record[0].ToString();
                temp[1] = record[1].ToString();
                temp[2] = record[2].ToString();
                ListViewItem temp2 = new ListViewItem(temp);
                marketSystemsTxtBox.Items.Add(temp2);
            }

            //marketCategories.Items.Add("*");
            marketCategoriesTxtBox.Items.Clear();
            foreach (DataRow record in DBConnect.AQuery("SELECT categoryName FROM invCategories ORDER BY categoryName").Rows)
            {
                marketCategoriesTxtBox.Items.Add(record[0].ToString());
            }
        }

        private void marketCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Selectedcategories = "";
            foreach (ListViewItem Selectedcategory in marketCategoriesTxtBox.SelectedItems)
            {
                Selectedcategories = Selectedcategories + ", '" + Selectedcategory.SubItems[0].Text + "'";
            }
            if (Selectedcategories != (""))
            {
                Selectedcategories = Selectedcategories.Remove(0, 2);
            }
            else
            {
                Selectedcategories = "''";
            }

            marketGroupsTxtBox.Items.Clear();
            //marketGroups.Items.Add("*");
            foreach (DataRow record in DBConnect.AQuery("SELECT groupName, groupID FROM invGroups WHERE groupID IN (SELECT groupID FROM invGroups WHERE categoryID IN (SELECT categoryID FROM invCategories WHERE categoryName IN (" + Selectedcategories + "))) ORDER BY groupName").Rows)
            {
                string[] temp = new string[2];
                temp[0] = record[0].ToString();
                temp[1] = record[1].ToString();
                ListViewItem temp2 = new ListViewItem(temp);
                marketGroupsTxtBox.Items.Add(temp2);
            }
        }

        private void seedMarketBtn_Click(object sender, EventArgs e)
        {
            // Create Selected Races for SQL query:
            string Selectedraces = "";
            foreach (ListViewItem Selected in marketRacesTxtBox.SelectedItems)
            {
                Selectedraces = Selectedraces + ", '" + Selected.SubItems[1].Text + "'";
            }
            if (Selectedraces != (""))
            {
                Selectedraces = Selectedraces.Remove(0, 2);
            }
            else
            {
                Selectedraces = "NULL";
            }

            // Create Selected Groups for SQL query:
            string Selectedgroups = "";
            foreach (ListViewItem Selected in marketGroupsTxtBox.SelectedItems)
            {
                Selectedgroups = Selectedgroups + ", '" + Selected.SubItems[1].Text + "'";
            }
            if (Selectedgroups != (""))
            {
                Selectedgroups = Selectedgroups.Remove(0, 2);
            }
            else
            {
                Selectedgroups = "''";
            }

            // Create Selected Systems for SQL query:
            CultureInfo culture = new CultureInfo("en-US");
            string Selectedsystems = "";
            long fullIndex = 0;
            float index = 0;
            float divisor = ((float)100.0) / ((float)(trackBar1.Value));
            index += divisor;
            foreach (ListViewItem Selected in marketSystemsTxtBox.SelectedItems)
            {
                ++fullIndex;
                if (((long)index) == fullIndex)
                {
                    if (((System.Convert.ToSingle(Selected.SubItems[2].Text, culture) >= System.Convert.ToSingle(marketSecurityTxtBox.Text, culture)) && (radioButton3.Checked))
                        || ((System.Convert.ToSingle(Selected.SubItems[2].Text, culture) <= System.Convert.ToSingle(marketSecurityTxtBox.Text, culture)) && (radioButton4.Checked)))
                    {
                        Selectedsystems = Selectedsystems + ", '" + Selected.SubItems[1].Text + "'";
                    }
                    index += divisor;
                }
            }
            if (Selectedsystems != (""))
            {
                Selectedsystems = Selectedsystems.Remove(0, 2);
            }
            else
            {
                Selectedsystems = "''";
            }

            // Create Selected Systems for SQL query from Selected Regions:  --- THIS OVERRIDES WHATEVER SELECTED SYSTEMS DETERMINED ABOVE ---
            string Selectedregions = "";
            foreach (ListViewItem Selected in marketRegionsTxtBox.SelectedItems)
            {
                Selectedregions = Selectedregions + ", '" + Selected.SubItems[1].Text + "'";
            }
            if (Selectedregions != (""))
            {
                Selectedregions = Selectedregions.Remove(0, 2);
                Selectedsystems = "";
                fullIndex = 0;
                index = 0;
                divisor = ((float)100.0) / ((float)(trackBar1.Value));
                index += divisor;
                foreach (DataRow record in DBConnect.AQuery("SELECT solarSystemID, security FROM mapSolarSystems WHERE regionid IN (" + Selectedregions + ")").Rows)
                {
                    ++fullIndex;
                    if (((long)index) == fullIndex)
                    {
                        if (((System.Convert.ToSingle(record[1].ToString(), culture) >= System.Convert.ToSingle(marketSecurityTxtBox.Text, culture)) && (radioButton3.Checked))
                            || ((System.Convert.ToSingle(record[1].ToString(), culture) <= System.Convert.ToSingle(marketSecurityTxtBox.Text, culture)) && (radioButton4.Checked)))
                        {
                            Selectedsystems = Selectedsystems + ", '" + record[0].ToString() + "'";
                        }
                        index += divisor;
                    }
                }
                if (Selectedsystems != (""))
                {
                    Selectedsystems = Selectedsystems.Remove(0, 2);
                }
                else
                {
                    Selectedsystems = "''";
                }
            }
            else
            {
                Selectedregions = "''";
            }

            long bid = 0;
            if (radioButton1.Checked)
                // Make this query full of Sell Orders
                bid = 0;
            else
            {
                if (radioButton2.Checked)
                    // Make this query full of Buy Orders
                    bid = 1;
                else
                    bid = 0;     // If execution reaches this line, there is some problem with radioButton1 and radioButton2 interaction.
            }

            FILE_TIME ft1 = new FILE_TIME();
            GetSystemTimeAsFileTime(ref ft1);
            UInt64 integerTime = (((UInt64)(ft1.dwHighDateTime)) << 32) | ((UInt64)(ft1.dwLowDateTime));
            string str_MySQL_Query;

            if (Selectedraces != "NULL")
            {
                str_MySQL_Query = "INSERT INTO market_orders (typeID, charID, regionID, stationID, bid, price, volEntered, volRemaining, issued, orderState, minVolume, contraband, accountID, duration, isCorp, solarSystemID, escrow, jumps) SELECT typeID, 1 as charID, regionID, stationID, " + bid.ToString() + " as bid, basePrice as price, " + marketQuantityTxtBox.Text + " as volEntered, " + marketQuantityTxtBox.Text + " as volRemaining, " + integerTime.ToString() + " as issued, 1 as orderState, 1 as minVolume,0 as contraband, 0 as accountID, 18250 as duration,0 as isCorp, solarSystemID, 0 as escrow, 1 as jumps FROM staStations, invTypes WHERE solarSystemID in (" + Selectedsystems + ") AND published = 1 and raceID in (" + Selectedraces + ") and groupID in (" + Selectedgroups + ")";
                queryMarketSeedTxtBox.Text = str_MySQL_Query;
                if (applyQueryToDBChkBox.Checked)
                    DBConnect.SQuery(str_MySQL_Query);
            }
            else
            {
                str_MySQL_Query = "INSERT INTO market_orders (typeID, charID, regionID, stationID, bid, price, volEntered, volRemaining, issued, orderState, minVolume, contraband, accountID, duration, isCorp, solarSystemID, escrow, jumps) SELECT typeID, 1 as charID, regionID, stationID, " + bid.ToString() + " as bid, basePrice as price, " + marketQuantityTxtBox.Text + " as volEntered, " + marketQuantityTxtBox.Text + " as volRemaining, " + integerTime.ToString() + " as issued, 1 as orderState, 1 as minVolume,0 as contraband, 0 as accountID, 18250 as duration,0 as isCorp, solarSystemID, 0 as escrow, 1 as jumps FROM staStations, invTypes WHERE solarSystemID in (" + Selectedsystems + ") AND published = 1 and raceID is NULL and groupID in (" + Selectedgroups + ")";
                queryMarketSeedTxtBox.Text = str_MySQL_Query;
                if (applyQueryToDBChkBox.Checked)
                    DBConnect.SQuery(str_MySQL_Query);
            }
        }
        #endregion

        #region ORE
        private void oreTab_Enter(object sender, EventArgs e)
        {
            SELECTOre.Items.Clear();
            foreach (DataRow record in DBConnect.AQuery("SELECT typeName FROM invTypes WHERE groupID IN (SELECT groupid FROM invGroups WHERE categoryid = 25)").Rows)
            {
                SELECTOre.Items.Add(record[0].ToString());
            }
        }

        public void SELECTOre_SelectedIndexChanged(object sender, EventArgs e)
        {
            mineralView.Items.Clear();

            foreach (DataRow record in DBConnect.AQuery("SELECT typeID from invTypes WHERE typeName = '" + SELECTOre.Text + "'").Rows)
            {
                SelectedOretypeID.Text = record[0].ToString();
            }
            foreach (DataRow record in DBConnect.AQuery("SELECT typeID, requiredtypeID, quantity, (SELECT typeName FROM invTypes WHERE typeID = requiredtypeID) AS mineralname FROM typeactivitymaterials WHERE typeID IN (SELECT typeID FROM invTypes WHERE typeName = '" + SELECTOre.Text + "')").Rows)
            {
                string[] temp = new string[4];
                temp[0] = record[0].ToString();
                temp[1] = record[1].ToString();
                temp[2] = record[2].ToString();
                temp[3] = record[3].ToString();
                ListViewItem temp2 = new ListViewItem(temp);
                mineralView.Items.Add(temp2);
            }
        }
       
        

        private void button7_Click(object sender, EventArgs e)
        {
            if (SelectedOretypeID.Text != "")
            {
                itemAddEdit add = new itemAddEdit();
                add.Show();
                add.extractItemInfo(Convert.ToInt16(SelectedOretypeID.Text));
            }
        }

        private void removeMineralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem SelectedItem in mineralView.SelectedItems)
            {
                DBConnect.SQuery("DELETE FROM typeactivitymaterials WHERE typeID = " + SelectedItem.SubItems[0].Text + " AND requiredtypeID = " + SelectedItem.SubItems[1].Text);
            }
            SELECTOre_SelectedIndexChanged(null, null);
        }

        private void addMineralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedOretypeID.Text != "")
            {
                EditOre editore = new EditOre();
                editore.Show();
                editore.oreID.Text = SelectedOretypeID.Text;
                editore.loadOre(SelectedOretypeID.Text, "0");
            }
        }

        private void changeQuantityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedOretypeID.Text != "")
            {
                EditOre editore = new EditOre();
                editore.Show();
                editore.oreID.Text = SelectedOretypeID.Text;
                foreach (ListViewItem SelectedItem in mineralView.SelectedItems)
                {
                    editore.loadOre(SelectedItem.SubItems[0].Text, SelectedItem.SubItems[1].Text);
                    editore.quantity.Text = SelectedItem.SubItems[2].Text;
                }
            }
        }
        #endregion

        #region Unsorted
        private void openApplicationDataFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process explorer = new Process();
            explorer.StartInfo.FileName = "explorer.exe";
            explorer.StartInfo.Arguments = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            explorer.Start();
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            marketGroupsTree.Nodes.Clear();
            foreach (DataRow record in DBConnect.AQuery("SELECT marketGroupID, marketGroupName FROM invMarketGroups WHERE parentGroupID IS NULL").Rows)
            {
                marketGroupsTree.Nodes.Add("", record[1].ToString(), record[0].ToString());
            }
        }

        private void marketGroupsTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int level0 = 0;
            int level1 = 0;
            int level2 = 0;

            if (marketGroupsTree.SelectedNode.Parent != null)
            {
                level0 = marketGroupsTree.SelectedNode.Parent.Index;
                if (marketGroupsTree.SelectedNode.Parent.Parent != null)
                {
                    level1 = marketGroupsTree.SelectedNode.Parent.Parent.Index;
                    if (marketGroupsTree.SelectedNode.Parent.Parent.Parent != null)
                    {
                        level2 = marketGroupsTree.SelectedNode.Parent.Parent.Parent.Index;
                    }
                }
            }

            switch (marketGroupsTree.SelectedNode.Level.ToString())
            {
                case "0":
                    marketGroupsTree.Nodes[marketGroupsTree.SelectedNode.Index].Nodes.Clear();
                    foreach (DataRow record in DBConnect.AQuery("SELECT marketGroupID, marketGroupName FROM invMarketGroups WHERE parentGroupID IN (SELECT marketGroupID FROM invMarketGroups where marketGroupID = '" + marketGroupsTree.SelectedNode.ImageKey.ToString() + "')").Rows)
                    {
                        marketGroupsTree.Nodes[marketGroupsTree.SelectedNode.Index].Nodes.Add("", record[1].ToString(), record[0].ToString());
                    }
                    marketGroupsTree.SelectedNode.Expand();
                    break;
                case "1":
                    marketGroupsTree.Nodes[level0].Nodes[marketGroupsTree.SelectedNode.Index].Nodes.Clear();
                    foreach (DataRow record in DBConnect.AQuery("SELECT marketGroupID, marketGroupName FROM invMarketGroups WHERE parentGroupID IN (SELECT marketGroupID FROM invMarketGroups where marketGroupID = '" + marketGroupsTree.SelectedNode.ImageKey.ToString() + "')").Rows)
                    {
                        marketGroupsTree.Nodes[level0].Nodes[marketGroupsTree.SelectedNode.Index].Nodes.Add("", record[1].ToString(), record[0].ToString());
                    }
                    marketGroupsTree.SelectedNode.Expand();
                    break;
                case "2":
                    marketGroupsTree.Nodes[level0].Nodes[level1].Nodes[marketGroupsTree.SelectedNode.Index].Nodes.Clear();
                    foreach (DataRow record in DBConnect.AQuery("SELECT marketGroupID, marketGroupName FROM invMarketGroups WHERE parentGroupID IN (SELECT marketGroupID FROM invMarketGroups where marketGroupID = '" + marketGroupsTree.SelectedNode.ImageKey.ToString() + "')").Rows)
                    {
                        marketGroupsTree.Nodes[level0].Nodes[level1].Nodes[marketGroupsTree.SelectedNode.Index].Nodes.Add("", record[1].ToString(), record[0].ToString());
                    }
                    marketGroupsTree.SelectedNode.Expand();
                    break;
                case "3":
                    marketGroupsTree.Nodes[level0].Nodes[level1].Nodes[level2].Nodes[marketGroupsTree.SelectedNode.Index].Nodes.Clear();
                    foreach (DataRow record in DBConnect.AQuery("SELECT marketGroupID, marketGroupName FROM invMarketGroups WHERE parentGroupID IN (SELECT marketGroupID FROM invMarketGroups where marketGroupID = '" + marketGroupsTree.SelectedNode.ImageKey.ToString() + "')").Rows)
                    {
                        marketGroupsTree.Nodes[level0].Nodes[level1].Nodes[level2].Nodes[marketGroupsTree.SelectedNode.Index].Nodes.Add("", record[1].ToString(), record[0].ToString());
                    }
                    marketGroupsTree.SelectedNode.Expand();
                    break;
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string value = marketGroupsTree.SelectedNode.Text;
            if (inputBox("Rename Market Group", "Please input a new market name.", ref value) == DialogResult.OK)
            {
                DBConnect.SQuery("UPDATE invMarketGroups SET marketGroupName = '" + value + "' WHERE marketGroupID = " + marketGroupsTree.SelectedNode.ImageKey.ToString());
            }
            tabPage1_Enter(null, null);
        }

        private void addMarketGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string value = "";
            string newName = "";
            if (inputBox("Add Market Group", "Please input a market name.", ref value) == DialogResult.OK)
            {
                newName = value;
            }
            int newid = 0;
            foreach (DataRow record in DBConnect.AQuery("SELECT MAX(marketGroupID) FROM invMarketGroups").Rows)
            {
                newid = Convert.ToInt32(record[0].ToString()) + 1;
            }
            DBConnect.SQuery("INSERT INTO invMarketGroups (marketGroupID, parentGroupID, marketGroupName) VALUES (" + newid + ", " + marketGroupsTree.SelectedNode.ImageKey.ToString() + ", '" + newName + "')");
            tabPage1_Enter(null, null);
        }

        private void deleteMarketGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult confirmDelete = MessageBox.Show("Are you sure you want to delete " + marketGroupsTree.SelectedNode.Text + "?", "Confirm delete", MessageBoxButtons.YesNo);
            if (confirmDelete == DialogResult.Yes)
            {
                DBConnect.SQuery("DELETE FROM invMarketGroups WHERE marketGroupID = " + marketGroupsTree.SelectedNode.ImageKey.ToString());
                tabPage1_Enter(null, null);
            }
        }

        public static DialogResult inputBox(string title, string body, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = body;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        private void loadLogonInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XMLReader.xmlLoad();
            MessageBox.Show("To be added.");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                radioButton2.Checked = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                radioButton1.Checked = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                radioButton4.Checked = false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
                radioButton3.Checked = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog xmlfile = new OpenFileDialog();
            if (xmlfile.ShowDialog() == DialogResult.OK)
            {
                string xmlfilepath = xmlfile.FileName;
                FileStream xmlread = new FileStream(xmlfilepath, FileMode.Open, FileAccess.Read);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(xmlread);

                XmlNodeList general = xmldoc.GetElementsByTagName("account");

                // auto account feature / logon message
                autoAcct.Text = general[0].ChildNodes[0].InnerText;
                logonMsg.Text = general[0].ChildNodes[1].InnerText;
                // starting ISK
                XmlNodeList isk = xmldoc.GetElementsByTagName("character");
                startIsk.Text = isk[0].ChildNodes[0].InnerText;
                // database information
                XmlNodeList dbinfo = xmldoc.GetElementsByTagName("database");
                dbHost.Text = dbinfo[0].ChildNodes[0].InnerText;
                dbUser.Text = dbinfo[0].ChildNodes[1].InnerText;
                dbPass.Text = dbinfo[0].ChildNodes[2].InnerText;
                dbDB.Text = dbinfo[0].ChildNodes[3].InnerText;
                dbPort.Text = dbinfo[0].ChildNodes[4].InnerText;
                // file config info
                XmlNodeList files = xmldoc.GetElementsByTagName("files");
                logFile.Text = files[0].ChildNodes[0].InnerText;
                logStt.Text = files[0].ChildNodes[1].InnerText;
                cacheDir.Text = files[0].ChildNodes[2].InnerText;
                // server port
                XmlNodeList net = xmldoc.GetElementsByTagName("net");
                servPort.Text = net[0].ChildNodes[0].InnerText;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            /*OpenFileDialog xmlfile = new OpenFileDialog();
            if (xmlfile.ShowDialog() == DialogResult.OK)
            {
                string xmlfilepath = xmlfile.FileName;
                FileStream xmlread = new FileStream(xmlfilepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(xmlread);

                XmlNodeList general = xmldoc.GetElementsByTagName("account");

                // auto account feature / logon message
                general[0].ChildNodes[0].InnerText = textBox2.Text;
                general[0].ChildNodes[1].InnerText = textBox3.Text;
                // starting ISK
                XmlNodeList isk = xmldoc.GetElementsByTagName("character");
                isk[0].ChildNodes[0].InnerText = textBox4.Text;
                // database information
                XmlNodeList dbinfo = xmldoc.GetElementsByTagName("database");
                dbinfo[0].ChildNodes[0].InnerText = textBox5.Text;
                dbinfo[0].ChildNodes[1].InnerText = textBox7.Text;
                dbinfo[0].ChildNodes[2].InnerText = textBox8.Text;
                dbinfo[0].ChildNodes[3].InnerText = textBox9.Text;
                dbinfo[0].ChildNodes[4].InnerText = textBox10.Text;
                // file config info
                XmlNodeList files = xmldoc.GetElementsByTagName("files");
                files[0].ChildNodes[0].InnerText = textBox11.Text;
                files[0].ChildNodes[1].InnerText = textBox12.Text;
                files[0].ChildNodes[2].InnerText = textBox13.Text;
                // server port
                XmlNodeList net = xmldoc.GetElementsByTagName("net");
                net[0].ChildNodes[0].InnerText = textBox14.Text;

                //FileStream xmlsave = new FileStream(xmlfilepath, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                xmldoc.Save(xmlread);
            }*/
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Clear MySql query in Seed Market query text box
            queryMarketSeedTxtBox.Text = "";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            workDir.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            eveDir.Text = folderBrowserDialog1.SelectedPath;
            workDir.Text = folderBrowserDialog1.SelectedPath + "/workdir";


            //add stuff files to listview
            foreach (string StuffFile in Directory.GetFiles(eveDir.Text))
            {
                if (StuffFile.ToLower().EndsWith(".stuff"))
                {
                    stuffFiles.Items.Add(StuffFile);
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Int16 fileCount = 0;
            foreach (ListViewItem StuffArchive in stuffFiles.SelectedItems)
            {
                if (StuffArchive.SubItems[0].Text.ToLower().EndsWith(".stuff"))
                {
                    fileCount++;
                }
            }

            insuranceProgressBar.Maximum = fileCount;
            insuranceProgressBar.Value = 0;
            StuffFile s = new StuffFile();
            foreach (ListViewItem StuffArchive in stuffFiles.SelectedItems)
            {
                if (StuffArchive.SubItems[0].Text.ToLower().EndsWith(".stuff"))
                {
                    progress.Text = "Current file: " + StuffArchive.SubItems[0].Text + ".stuff";
                    Application.DoEvents();
                    s.Extract(StuffArchive.SubItems[0].Text, @workDir.Text + @"\extract\" + Path.GetFileNameWithoutExtension(StuffArchive.SubItems[0].Text));
                    insuranceProgressBar.Value++;
                }
            }
            progress.Text = "Idle";
        }

        private void button1_Click(object sender, EventArgs e) //Where is button 1? This seem to be the same as button15. Remove it if it's a mistake.
        {
            insuranceProgressBar.Maximum = Directory.GetDirectories(@workDir.Text + @"\extract").Count();
            insuranceProgressBar.Value = 0;

            StuffFile s = new StuffFile();
            foreach (string StuffDir in Directory.GetDirectories(@workDir.Text + @"\extract"))
            {
                progress.Text = "Current file: " + StuffDir + ".stuff";
                Application.DoEvents();
                s.Archive(@StuffDir, @eveDir.Text + @"\" + Path.GetFileName(StuffDir) + ".stuff");
                insuranceProgressBar.Value++;
            }
            progress.Text = "Idle";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            insuranceProgressBar.Maximum = Directory.GetDirectories(@workDir.Text + @"\extract").Count();
            insuranceProgressBar.Value = 0;
            StuffFile s = new StuffFile();

            foreach (string StuffDir in Directory.GetDirectories(@workDir.Text + @"\extract"))
            {
                progress.Text = "Current file: " + StuffDir + ".stuff";
                Application.DoEvents();
                foreach (ListViewItem StuffArchive in stuffFiles.SelectedItems)
                {
                    if (Path.GetFileName(StuffArchive.SubItems[0].Text) == Path.GetFileName(StuffDir) + ".stuff")
                    {
                        s.Archive(@StuffDir, @destDir.Text + @"\" + Path.GetFileName(StuffDir) + ".stuff");
                    }
                }
                insuranceProgressBar.Value++;
            }
            progress.Text = "Idle";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            destDir.Text = folderBrowserDialog1.SelectedPath;
        }

        private void tabPage5_Enter(object sender, EventArgs e)
        {
            if (Directory.Exists(workDir.Text + @"/extract"))
            {
                models.Items.Clear();
                scanRed(workDir.Text + @"/extract");
            }
            else
            {
                MessageBox.Show("Please select your eve directory/work directory and unpack your stuff files first.");
            }
        }

        private void scanRed(string currentdir)
        {
            foreach (string redFile in Directory.GetFiles(currentdir))
            {
                if (redFile.ToLower().EndsWith(".red"))
                {
                    string[] redListView = new string[2];
                    redListView[0] = Path.GetFileNameWithoutExtension(redFile);
                    redListView[1] = redFile;

                    ListViewItem redListView2 = new ListViewItem(redListView);
                    models.Items.Add(redListView2);
                }
            }
            foreach (string subDir in Directory.GetDirectories(currentdir))
            {
                scanRed(subDir);
            }
        }

        private void models_DoubleClick(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedRed in models.SelectedItems)
            {
                Process.Start("notepad.exe", selectedRed.SubItems[1].Text);
            }

        }

        private void models_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedRed in models.SelectedItems)
            {
                readRed(selectedRed.SubItems[1].Text);
            }
        }
        private void readRed(string redFilePath)
        {
            usedFiles.Items.Clear();
            usedFiles.Items.Add("res:" + redFilePath.Replace(workDir.Text + "/extract", "").Replace("\\", "/")); //whatever, but it works.
            StreamReader redFileData = new StreamReader(redFilePath);

            while (redFileData.Peek() > -1)
            {
                string temp = redFileData.ReadLine();
                if (temp.Contains("res:"))
                {
                    temp = temp.Substring(temp.IndexOf("\"") + 1).Remove(temp.Substring(temp.IndexOf("\"") + 1).IndexOf("\"")); //LOL, that's some odd code, what was I thinking?

                    foreach (ListViewItem alreadyAddedItem in usedFiles.Items)
                    {
                        if (alreadyAddedItem.SubItems[0].Text == temp)
                        {
                            return;
                        }
                    }

                    ListViewItem usedFilesView = new ListViewItem(temp);
                    usedFiles.Items.Add(usedFilesView);
                }
            }
            redFileData.Close();

        }

        private void button16_Click(object sender, EventArgs e)
        {


            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.RedirectStandardOutput = false;
            process.StartInfo.RedirectStandardError = false;
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.FileName = serverDir.Text;
            process.StartInfo.WorkingDirectory = servworkDir.Text;
            process.Start();

        }

        private void button17_Click(object sender, EventArgs e)
        {
            Process[] prs = Process.GetProcesses();


            foreach (Process pr in prs)
            {
                if (pr.ProcessName == "eve-server")
                {

                    pr.Kill();

                }

            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                serverDir.Text = openFileDialog1.FileName;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Process[] prs = Process.GetProcesses();


            foreach (Process pr in prs)
            {
                if (pr.ProcessName == "eve-server")
                {

                    pr.Kill();
                }
            }
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.RedirectStandardOutput = false;
            process.StartInfo.RedirectStandardError = false;
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.FileName = serverDir.Text;
            process.StartInfo.WorkingDirectory = servworkDir.Text;
            process.Start();
        }

        private void button20_Click_1(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                servworkDir.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void saveLogonInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.Directory.CreateDirectory(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\EvEMU DB\\");
            }
            catch (Exception ex)
            {
                LogSystem.addLog("Exception: " + ex.Message);
            }
            try
            {
                System.IO.StreamWriter f = new System.IO.StreamWriter(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\EvEMU DB\\" + hostTextBox.Text + ".ini");
                string LogonData;
                LogonData = "Hostname=" + hostTextBox.Text + "\r\nUsername=" + usernameTxtBox.Text + "\r\nPassword=" + passwordTxtBox.Text + "\r\nPort=" + portTxtBox.Text + "\r\nDatabase=" + dbNameTxtBox.Text;
                f.Write(LogonData);
                f.Close();
                System.IO.File.Copy(Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\EvEMU DB\\" + hostTextBox.Text + ".ini", Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\EvEMU DB\\EvemuDBEditor.ini", true);
                MessageBox.Show("Logon data saved to: " + Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\EvEMU DB\\" + hostTextBox.Text + ".ini and has been set as default.");
            }
            catch (Exception ex)
            {
                LogSystem.addLog("Exception: " + ex.Message);
                MessageBox.Show(ex.ToString());
            }
        }

        public static string CalculateMD5Hash(string strInput)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(strInput);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        internal void doubleInsert(string typeID, int attributeID, double value)
        {
            string query = "";
            conn = new MySqlConnection();
            conn.ConnectionString = Program.m.connection;
            query = "INSERT INTO dgmTypeAttributes (typeID, attributeID, valueFloat) VALUES(" + typeID + ", " + attributeID + ", " + value + ")";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                LogSystem.addLog("Exception: " + ex.Message);
                MessageBox.Show(ex.Message);
            }
            catch (Exception ec)
            {
                LogSystem.addLog("Exception: " + ec.Message);
                MessageBox.Show(ec.Message);
            }
        }

        internal void intInsert(string typeID, int attributeID, int value)
        {
            string query = "";
            conn = new MySqlConnection();
            conn.ConnectionString = Program.m.connection;
            query = "INSERT INTO dgmTypeAttributes (typeID, attributeID, valueInt) VALUES(" + typeID + ", " + attributeID + ", " + value + ")";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                LogSystem.addLog("Exception: " + ex.Message);
                MessageBox.Show(ex.Message);
            }
            catch (Exception ec)
            {
                LogSystem.addLog("Exception: " + ec.Message);
                MessageBox.Show(ec.Message);
            }
        }

        internal void doubleUpdate(string typeID, int attributeID, double value)
        {
            string query = "";
            conn = new MySqlConnection();
            conn.ConnectionString = Program.m.connection;
            query = "UPDATE dgmTypeAttributes SET valueFloat=" + value + " WHERE typeID=" + typeID + " AND attributeID=" + attributeID;
            MySqlCommand cmd = new MySqlCommand(query, conn);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                LogSystem.addLog("Exception: " + ex.Message);
                MessageBox.Show(ex.Message);
            }
            catch (Exception ec)
            {
                LogSystem.addLog("Exception: " + ec.Message);
                MessageBox.Show(ec.Message);
            }
        }

        internal void intUpdate(string typeID, int attributeID, int value)
        {
            string query = "";
            conn = new MySqlConnection();
            conn.ConnectionString = Program.m.connection;
            query = "UPDATE dgmTypeAttributes SET valueInt=" + value + " WHERE typeID=" + typeID + " AND attributeID=" + attributeID;
            MySqlCommand cmd = new MySqlCommand(query, conn);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                LogSystem.addLog("Exception: " + ex.Message);
                MessageBox.Show(ex.Message);
            }
            catch (Exception ec)
            {
                LogSystem.addLog("Exception: " + ec.Message);
                MessageBox.Show(ec.Message);
            }
        }
        #endregion
    }
}

