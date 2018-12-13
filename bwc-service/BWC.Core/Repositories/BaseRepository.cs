using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using T.Core.Infrastructure;
using System.Web;

namespace BWC.Core.Repositories
{
    public class BaseRepository : BaseFactory
    {
        public BaseRepository()
        {
            if (base.ConnectionString == null)
            {
                XmlDocument xdoc = new XmlDocument();
                string path = HttpContext.Current.Server.MapPath("\\") + "App_Data\\connectionstring.xml";
                if (System.IO.File.Exists(path))
                {
                    XmlNode node = default(XmlNode);
                    xdoc.Load(path);
                    node = xdoc.SelectSingleNode("connections/connection");
                    string connection = node.SelectSingleNode("connectionString[@name='DB']").Attributes["connectionString"].Value;
                    base.ConnectionString = connection;
                }
            }
        }
    }
}
