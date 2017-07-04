using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using XMLValidator.Black.Utilities;

namespace XMLValidator.Black
{
    class Program
    {
        static string filePath = Env.GetFilePath(@"Files\Data.xml");

        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XMLField field = new XMLField(doc.FirstChild.NextSibling, "");
            string val = XMLField.SelectStringValue(doc.FirstChild.NextSibling, "/extensions/add");
            Console.Read();
        }
    }
}
