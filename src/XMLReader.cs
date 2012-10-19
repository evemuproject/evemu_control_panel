using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace Evemu_DB_Editor.src
{
    static class XMLReader
    {
        private static bool xmlSave(string username, string password, string host, string port, string dbName)
        {
            // Create the information we will need.
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode xmlInfo = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(xmlInfo);

            // Make the main Node.
            XmlNode ecpMain = xmlDoc.CreateElement("ecp_db");
            xmlDoc.AppendChild(ecpMain);

            // Settings
            XmlNode settings = xmlDoc.CreateElement("settings");
            ecpMain.AppendChild(settings);

            // Server
            XmlNode _host = xmlDoc.CreateElement("host");
            _host.AppendChild(xmlDoc.CreateTextNode(host));
            settings.AppendChild(_host);

            // Username
            XmlNode _username = xmlDoc.CreateElement("username");
            _username.AppendChild(xmlDoc.CreateTextNode(username));
            settings.AppendChild(_username);

            // Password
            XmlNode _password = xmlDoc.CreateElement("password");
            _password.AppendChild(xmlDoc.CreateTextNode(password));
            settings.AppendChild(_password);

            // Port
            XmlNode _port = xmlDoc.CreateElement("port");
            _port.AppendChild(xmlDoc.CreateTextNode(port));
            settings.AppendChild(_port);

            // Database
            XmlNode _database = xmlDoc.CreateElement("database");
            _database.AppendChild(xmlDoc.CreateTextNode(dbName));
            settings.AppendChild(_database);

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\EvECP\\ecp_config.xml"))
            {
                // File already exists so lets save, no fuss, no muss...
                xmlDoc.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\EvECP\\ecp_config.xml");

                return true;
            }
            else
            {
                // Create the directory, if it exists from before there should not be a problem...
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\EvECP\\");

                // Create the XML, then close afterwards.
                FileStream fs = new FileStream((Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\EvECP\\ecp_config.xml"), FileMode.Create);
                fs.Close();

                // It's save time.
                xmlDoc.Save(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\EvECP\\ecp_config.xml");

                // Return true as it saved.
                return true;
            }
        }

        public static string[] xmlLoad()
        {
            string[] dbcon = new string[5];

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\EvECP\\ecp_config.xml"))
            {
                XmlTextReader xmltext = new XmlTextReader(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\EvECP\\ecp_config.xml");

                int i = 0;
                while (xmltext.Read())
                {
                    switch (xmltext.NodeType)
                    {
                        case XmlNodeType.Text:
                            dbcon[i] = xmltext.Value;
                            i++;
                            break;
                    }
                }
                // Close it or the app goes nuts at me...
                xmltext.Close();
                return dbcon;
            }
            else
            {
                MessageBox.Show("Could not find a XML config file saved, please create a new one.", "Error");
                return null;
            }
        }
    }
}
