using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Evemu_DB_Editor
{
    public partial class acctEdit : Form
    {
        public acctEdit()
        {
            InitializeComponent();
        }
        main m = new main();        

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

        public void GetAccountInfo(string id)
        {
            string acctLevel = "";
            foreach (DataRow record in Program.m.SelectSQL("SELECT accountName, role FROM account WHERE accountID=" + id).Rows)
            {
                usertextbox.Text = record[0].ToString();
                acctLevel = record[1].ToString();
            }
                        
            switch (Int64.Parse(acctLevel))
            {
                case 2:
                    accountLevel.SelectedIndex = 4;
                    break;
                case 8:
                    accountLevel.SelectedIndex = 3;
                    break;
                case 16:
                    accountLevel.SelectedIndex = 2;
                    break;
                case 32:
                    accountLevel.SelectedIndex = 1;
                    break;
                default:
                    accountLevel.SelectedIndex = 0;
                    break;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.Enabled = true;
            }
            else
            {
                textBox2.Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                accountLevel.Enabled = true;
            }
            else
            {
                accountLevel.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string acctrole = accountLevel.Text;
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
                else
                {
                    acctrole = "2";
                }

                if (checkBox1.Checked == true || checkBox2.Checked == true)
                {
                    if (checkBox1.Checked && !checkBox2.Checked)
                    {
                       Program.m.InsertSQL("UPDATE account SET accountName='" + usertextbox.Text + "', password='" + CalculateMD5Hash(textBox2.Text) + "' WHERE accountID=" + userID.Text);
                    }
                    else if (checkBox2.Checked && !checkBox1.Checked)
                    {
                        Program.m.InsertSQL("UPDATE account SET accountName='" + usertextbox.Text + "', role=" + acctrole + " WHERE accountID=" + userID.Text);
                    }
                    else if (checkBox1.Checked && checkBox2.Checked)
                    {
                        Program.m.InsertSQL("UPDATE account SET accountName='" + usertextbox.Text + "', password='" + CalculateMD5Hash(textBox2.Text) + "', role=" + acctrole + " WHERE accountID=" + userID.Text);
                    }

                }
                else
                {
                    Program.m.InsertSQL("UPDATE account SET accountName='" + usertextbox.Text + "' WHERE accountID=" + userID.Text);
                }
        }
            }
}
