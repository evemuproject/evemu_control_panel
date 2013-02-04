using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;

namespace Evemu_DB_Editor
{
    class xHelp
    {
        private XmlDocument cfg = new XmlDocument();
        /// <summary>
        /// DataRecords is string[parent-node,child-node] build so: 
        /// <para>child-node: 0.tag-xml 1.Tabel name(tag) 2.fields list(fields) 3.richtext(cdata);</para>
        /// </summary>
        public DataRecord tableHelp;
        public string DataBaseName;

        public xHelp(string pDataBaseName)
        {
            this.DataBaseName = pDataBaseName;
            if (CheckFile())
            {
                tableHelp = new DataRecord(QuantiFigli(this.DataBaseName), 3, this.DataBaseName);
            }
        }

        /*************************** set xHelp ********************************/
        #region set xHelp
        /// <summary>
        /// check and build file xml
        /// </summary>
        /// <returns>do it or no</returns>
        private bool CheckFile()
        {
            if (!System.IO.File.Exists(DataRecord.strNomeFileHelp))
            {
                try
                {
                    XmlDocument cfg = new XmlDocument();
                    //Create an XML declaration. 
                    XmlDeclaration xmldecl;
                    xmldecl = cfg.CreateXmlDeclaration("1.0", null, null);
                    cfg.AppendChild(xmldecl);
                    //root node
                    cfg.AppendChild(cfg.CreateElement("xHelp"));

                    //sub node
                    XmlNode newNodo;
                    newNodo = cfg.CreateNode(XmlNodeType.Element, this.DataBaseName, "");
                    cfg.DocumentElement.AppendChild(newNodo);
                    //save xml
                    cfg.Save(DataRecord.strNomeFileHelp);
                }
                catch (Exception errore)
                {  
                    Console.WriteLine("Errore: {0}", errore.ToString());
                    return false;
                }
            }
            //check for db name TODO
            return true;
        }

        private int QuantiFigli(string Name)
        {
            int ritorno = 0;
            XmlTextReader myCfg = new XmlTextReader(DataRecord.strNomeFileHelp);
            while (myCfg.Read())
            {
                // if is valid element
                if (myCfg.NodeType == XmlNodeType.Element && myCfg.IsStartElement())
                {
                    if (myCfg.Name == Name)
                    {
                        XmlReader nodoxml = (XmlReader)myCfg.ReadSubtree();

                        nodoxml.Read();
                        while (nodoxml.Read())
                        {
                            if (nodoxml.IsStartElement())
                            {
                                ritorno++;
                            }
                        }
                        return ritorno;
                    }
                }
            }
            myCfg.Close();
            return ritorno;
        }
        #endregion
        /***********************************************************************************/
    }

    class DataRecord
    {
        public const string strNomeFileHelp = "xHelp.xml";
        
        int righe;
        int colonne;
        string nomeTabella;
        string[,] dataRecord;//
        XmlDocument fileCfg = new XmlDocument();

        /// <summary>
        /// DataRecord is a list of each node
        /// </summary>
        /// <param name="pRighe">number of parents-nodes</param>
        /// <param name="pColonne">number of child-nodes</param>
        /// <param name="pNomeTabella">name of root node</param>
        public DataRecord(int pRighe, int pColonne, string pNomeTabella)
        {
            righe = pRighe;
            colonne = pColonne;
            nomeTabella = pNomeTabella;

            dataRecord = new string[righe, colonne];
            CaricaTabella();
            fileCfg.Load(strNomeFileHelp);
        }
        /// <summary>
        /// How many deep
        /// </summary>
        public int DataRecordDeep
        {
            get { return dataRecord.Length / colonne; }//
        }
        /// <summary>
        /// Load XML
        /// </summary>
        private void CaricaTabella()
        {
            XmlTextReader myCfg = new XmlTextReader(strNomeFileHelp);
            while (myCfg.Read())
            {
                if (myCfg.NodeType == XmlNodeType.Element && myCfg.IsStartElement())
                {
                    if (myCfg.Name == this.nomeTabella)
                    {
                        XmlReader nodoxml = (XmlReader)myCfg.ReadSubtree();
                        CaricaRecord(nodoxml);
                    }
                }
            }
            myCfg.Close();
        }
        /// <summary>
        /// Load each line in xml
        /// </summary>
        /// <param name="myCfg"></param>
        private void CaricaRecord(XmlReader myCfg)
        {
            int deep = 0;

            while (myCfg.Read())
            {
                if (myCfg.IsStartElement() && myCfg.Name != this.nomeTabella)
                {
                    ArrayList ritorno = new ArrayList();
                    ritorno.Add(myCfg.Name);
                    int qAttr = myCfg.AttributeCount;
                    for (int i = 0; i < qAttr; i++)
                    {
                        ritorno.Add(myCfg.GetAttribute(i));
                    }
                    myCfg.Read();
                    string ddv = myCfg.Value;
                    ritorno.Add(ddv);
                    
                    if (AddaRiga(ritorno, deep))
                        deep++;
                }
            }
        }
        /// <summary>
        /// Add row from xml file to DataRecords
        /// </summary>
        /// <param name="aRiga">List of each elements to put in xml</param>
        /// <param name="deep">Where</param>
        /// <returns></returns>
        private bool AddaRiga(ArrayList aRiga, int deep)
        {
            try
            {
                //ArrayList[,] temp = new ArrayList[this.righe,this.colonne];

                //aRiga.CopyTo(dataRecord);
                for (int i = 0; i < aRiga.Count; i++)
                {
                    //string tt = aRiga[i].ToString();
                    dataRecord[deep, i] = (aRiga[i].ToString());
                }
                return true;
            }
            catch (Exception er)
            {
                Console.WriteLine("Errore :" + er.Message);
                return false;
            }
        }

        public string[] RecuperaRiga(int nRiga)
        {
            string[] ritorno = new string[colonne];
            for (int i = 0; i < colonne; i++)
            {
                ritorno[i] = dataRecord[nRiga, i];
            }
            return ritorno;
        }
        /// <summary>
        /// Save Table(DataRecords) to file xml AfterTableSelect(add new)
        /// </summary>
        /// <param name="rec">tableRecords[]: 0.Table Name(tag) 1.list fields separated by commas(fields) 2.richtext(cdata);</param>
        /// <returns></returns>
        public bool RecordTable(string[] rec)
        {
            try
            {
                XmlNode newNodo;
                XmlNode currNodo;
                XmlCDataSection cdata;
                //recovers root
                XmlNodeList nodoTable = fileCfg.GetElementsByTagName(this.nomeTabella);
                //numbers of nodes(tables)
                int last = nodoTable.Count;
                //build node
                newNodo = fileCfg.CreateNode(XmlNodeType.Element, rec[0], "");//
                //append new node
                currNodo = nodoTable[0].AppendChild(newNodo);
                //cdata element
                cdata = fileCfg.CreateCDataSection(rec[2]);
                newNodo.AppendChild(cdata);
                //add attributes
                //1.
                XmlAttribute genere = fileCfg.CreateAttribute("field");
                genere.Value = rec[1];
                newNodo.Attributes.Append(genere);

                //save
                fileCfg.Save(strNomeFileHelp);
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Save Table(DataRecords) to file xml BeforeTableSelect(update)
        /// </summary>
        /// <param name="posTable">Position of parent-node</param>
        /// <returns></returns>
        public bool SalvaTable(int posTable)
        {
            try
            {
                XmlNode newNodo;
                XmlNode currNodo;
                XmlNode oldNodo;
                XmlCDataSection cdata;
                //delete the node and create another
                XmlNodeList nodoItem = fileCfg.GetElementsByTagName(this.nomeTabella);
                //formed the node to be added
                newNodo = fileCfg.CreateNode(XmlNodeType.Element, this.dataRecord[posTable, 0], "");
                //recovery node to replace old
                oldNodo = fileCfg.GetElementsByTagName(this.dataRecord[posTable, 0])[0];
                //***************** Replacement node ******************************//
                currNodo = nodoItem[0].ReplaceChild(newNodo, oldNodo);
                cdata = fileCfg.CreateCDataSection(this.dataRecord[posTable, 2]);
                newNodo.AppendChild(cdata);
                //add attributes
                //1.
                XmlAttribute attr1 = fileCfg.CreateAttribute("field");
                attr1.Value = this.dataRecord[posTable, 1];
                newNodo.Attributes.Append(attr1);
                ////2.
                //XmlAttribute attr2 = fileCfg.CreateAttribute("xxx");
                //attr2.Value = this.dataRecord[posTable, 1];
                //newNodo.Attributes.Append(attr2);

                //Save xml file
                fileCfg.Save(strNomeFileHelp);
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Update Datarecord
        /// </summary>
        /// <param name="record"></param>
        /// <param name="helps"></param>
        /// <returns></returns>
        public bool AggiornaTable(int record, string[] helps)
        {
            try
            {
                this.dataRecord[record, 0] = helps[0];
                this.dataRecord[record, 1] = helps[1];
                this.dataRecord[record, 2] = helps[2];
                return true;
            }
            catch { return false; }
        }
    }
}
