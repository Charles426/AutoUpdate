using AutoUpdate.Field;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AutoUpdate.Helper
{
    public class XmlHelper
    {
        public static string LocalUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Local.xml");
        public static LocalField LocalField { get; set; } = new LocalField();
        public static ServerField ServerField { get; set; } = new ServerField();

        private static XmlDocument LoadXml(string url)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(url);
            return xmlDocument;
        }


        public static void LoadXml()
        {
            LoadLocalXml();
            LoadServerXml();
        }

        public static void LoadLocalXml()
        {
            XmlDocument xmlDocument = LoadXml(LocalUrl);
            XmlElement root = xmlDocument.DocumentElement;
            XmlNodeList xmlNodeList = root.SelectNodes("/LocalUpdate");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                foreach (XmlNode xmlNode2 in xmlNode.ChildNodes)
                {
                    LocalField.GetType().GetProperty(xmlNode2.Name).SetValue(LocalField, xmlNode2.InnerText);
                }
            }
        }


        public static void SaveLocalXml()
        {
            XmlDocument xmlDocument = LoadXml(LocalUrl);
            XmlElement root = xmlDocument.DocumentElement;
            XmlNodeList xmlNodeList = root.SelectNodes("/LocalUpdate");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                foreach (XmlNode xmlNode2 in xmlNode.ChildNodes)
                {
                    xmlNode2.InnerText = LocalField.GetType().GetProperty(xmlNode2.Name).GetValue(LocalField).ToString();
                }
            }

            xmlDocument.Save(LocalUrl);
        }

        public static void LoadServerXml()
        {
            if (string.IsNullOrEmpty(LocalField.ServerUpdateUrl))
                return;

            XmlDocument xmlDocument = LoadXml(LocalField.ServerUpdateUrl);
            XmlElement root = xmlDocument.DocumentElement;
            XmlNodeList xmlNodeList = root.SelectNodes("/ServerUpdate");
            foreach (XmlNode xmlNode in xmlNodeList)
            {
                foreach (XmlNode xmlNode2 in xmlNode.ChildNodes)
                {
                    ServerField.GetType().GetProperty(xmlNode2.Name).SetValue(ServerField, xmlNode2.InnerText);
                }
            }
        }


    }
}
