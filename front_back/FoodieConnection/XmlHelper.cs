using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;

namespace Foodie.DataBase
{
    public class XmlHelper
    {
        public string GetXmlConfigValue(string key)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath("/XmlConfig/Config.xml"));
            XmlNode xNode = doc.SelectSingleNode("//appSettings");
            XmlElement xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key='" + key + "']");
            return xElem1.GetAttribute("value");
        }
    }
}
