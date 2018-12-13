using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace BWC.Core.Common
{
    public static class GeneralFuncs
    {
        public static string ReadFile(string path)
        {
            string content = string.Empty;
            string LocalPath = HttpContext.Current.Server.MapPath("\\") + path;
            if (System.IO.File.Exists(LocalPath))
            {
                content = System.IO.File.ReadAllText(LocalPath);
            }
            return content;
        }
        public static XmlDocument ReadXML(string path)
        {
            XmlDocument xdoc = new XmlDocument();
            string xmlPath = HttpContext.Current.Server.MapPath("\\") + path;
            if (System.IO.File.Exists(xmlPath))
            {
                xdoc.Load(xmlPath);
            }
            return xdoc;
        }
        public static string SerializeObject<T>(this List<T> toSerialize)
        {
            if (toSerialize == null)
                return string.Empty;
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString().Replace("encoding=\"utf-16\"", "");
            }

        }
    }
}
