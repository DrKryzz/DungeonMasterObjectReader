using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DMObjectReader.Helpers
{
    public class MyUt
    {
    }

    public static class MyXUt {
        public static string GetElementText(XmlElement el, string nz) 
        {
            string ans_GetElementText = nz;
            if (el != null)
            {
                foreach (XmlNode o in el.ChildNodes)
                {
                    if (o.NodeType == XmlNodeType.Text) 
                    {
                        ans_GetElementText = o.Value;
                    }
                }
            }

            return ans_GetElementText;
        }
    }
}
