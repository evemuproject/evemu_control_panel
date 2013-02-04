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
    public partial class Preference : Form
    {
        public Preference()
        {
            InitializeComponent();
            if (!System.IO.File.Exists(Environment.CurrentDirectory + "\\evemu_editor.ini"))
            {
                main.ini.IniWriteValue("Path", "CurrentDir", Environment.CurrentDirectory);
            }
            else
            {
                txtMysqlPath.Text = main.ini.IniReadValue("Path", "mySQLPath");
                hostTextBox.Text = main.ini.IniReadValue("AutoConnect", "hostname");
                usernameTxtBox.Text = main.ini.IniReadValue("AutoConnect", "username");
                passwordTxtBox.Text = main.ini.IniReadValue("AutoConnect", "password");
                portTxtBox.Text = main.ini.IniReadValue("AutoConnect", "port");
                dbNameTxtBox.Text = main.ini.IniReadValue("AutoConnect", "database");
            }

            string lingua = main.ini.IniReadValue("Settings", "Languages");
            if (lingua != "")
            {
                RadioButton myr = ((RadioButton)this.Controls["grpLanguages"].Controls["rdb" + lingua]);
                myr.Checked = true;
            }
        }

        private void bttChoseSqlDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog foldSet = new FolderBrowserDialog();
            if (foldSet.ShowDialog() == DialogResult.OK)
            {
                txtMysqlPath.Text = foldSet.SelectedPath;
                main.ini.IniWriteValue("Path", "mySQLPath", foldSet.SelectedPath);
            }
        }

        private void rdbItaliano_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton stoRdb = (RadioButton)sender;
            if (stoRdb.Checked)
            {
                main.ini.IniWriteValue("Settings", "Languages", stoRdb.Text);
            }
        }

        private void Preference_FormClosing(object sender, FormClosingEventArgs e)
        {
            main.ini.IniWriteValue("AutoConnect", "hostname", hostTextBox.Text);
            main.ini.IniWriteValue("AutoConnect", "username", usernameTxtBox.Text);
            main.ini.IniWriteValue("AutoConnect", "password", passwordTxtBox.Text);
            main.ini.IniWriteValue("AutoConnect", "port", portTxtBox.Text);
            main.ini.IniWriteValue("AutoConnect", "database", dbNameTxtBox.Text);
        }

    }
}
