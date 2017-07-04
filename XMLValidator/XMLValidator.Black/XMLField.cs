using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace XMLValidator.Black
{
    public class XMLFieldAttribute
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public XMLFieldAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    public class XMLField
    {
        private XmlNode nextSibling;
        private int v1;
        private string v2;

        public string Name { get; private set; }
        public string Value { get; private set; }
        public int Level { get; private set; }
        public string Path { get; private set; }
        public List<XMLField> Children { get; private set; }
        public List<XMLFieldAttribute> Attributes { get; private set; }


        public XMLField(XmlNode xmlNode, string path, int? level = 1)
        {
            Initialize(xmlNode, path, level);

            if (xmlNode.Attributes != null)
            {
                foreach (XmlAttribute attribute in xmlNode.Attributes)
                {
                    Attributes.Add(new XMLFieldAttribute(attribute.Name, attribute.Value));
                }
            }

            if (xmlNode.ChildNodes != null)
            {
                foreach (XmlNode node in xmlNode.ChildNodes)
                {
                    if (!node.Name.StartsWith("#"))
                    {
                        var tmpPath = path == "" ? xmlNode.Name : path + "/" + xmlNode.Name;
                        Children.Add(new XMLField(node, tmpPath, level + 1));
                    }      
                }
            }
        }

        void Initialize(XmlNode xmlNode, string path, int? level)
        {
            if (xmlNode.Name.StartsWith("#"))
                throw new Exception("Comment Nodes are Not Allowed");
            Name = xmlNode.Name;
            Value = xmlNode.Value;
            Level = (int)level;
            Children = new List<XMLField>();
            Attributes = new List<XMLFieldAttribute>();
            Path = path == "" ? xmlNode.Name : path + "/" + xmlNode.Name;
        }

        public bool IsEmptyElement
        {
            get
            {
                return HasNoChildren && string.IsNullOrEmpty(Value);
            }
        }

        public bool HasNoChildren
        {
            get
            {
                return Children.Count == 0;
            }
        }

        public List<XMLField> GetAllNonEmptyFields()
        {
            var fields = new List<XMLField>();
            CollectNonEmptyFields(fields, this);
            return fields;
        }

        void CollectNonEmptyFields(List<XMLField> fieldList, XMLField field)
        {
            if (!field.HasNoChildren)
            {
                fieldList.Add(field);
            }
            foreach (var child in field.Children)
            {
                CollectNonEmptyFields(fieldList, child);
            }
        }


        /// <summary>
        /// Return a string by applying an XPath query to an XmlNode. 
        /// </summary>
        internal static string SelectStringValue(XmlNode node, string query)
        {
            return SelectStringValue(node, query, null);
        }

        /// <summary>
        /// Return a string by applying an XPath query to an XmlNode. 
        /// </summary>
        internal static string SelectStringValue(XmlNode node, string query, XmlNamespaceManager namespaceManager)
        {
            string strValue;
            XmlNode result;

            result = node.SelectSingleNode(query, namespaceManager);

            if (result != null)
            {
                strValue = ExtractString(result);
            }
            else
            {
                strValue = String.Empty;
            }

            return strValue;
        }


        /// <summary> 
        /// Get a string from an XmlNode (of any kind:  element, attribute, etc.)
        /// </summary> 
        internal static string ExtractString(XmlNode node)
        {
            string value = "";

            if (node.NodeType == XmlNodeType.Element)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    if (node.ChildNodes[i].NodeType == XmlNodeType.Text)
                    {
                        value += node.ChildNodes[i].Value;
                    }
                }
            }
            else
            {
                value = node.Value;
            }
            return value;
        }
    }
}
