using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace Utilities.Xml
{
    public static class XDocumentExtensions
    {
        public static IList<XElement> FindElements(this XDocument p_document, string p_name)
        {
            return p_document.Elements(p_name).ToList();
        }
        
        public static IList<XElement> FindElements(this XDocument p_document, string p_name, string p_attribute)
        {
            return p_document.Elements(p_name)
                .Where(p_element => p_element.HasAttributes && p_element.Attribute(p_attribute) != null).ToList();
        }
        
        public static IList<XElement> FindElements(this XDocument p_document, string p_name,  string p_attribute, string p_value)
        {
            return p_document.Elements(p_name)
                .Where(p_element => p_element.HasAttributes && p_element.Attribute(p_attribute) != null &&
                                    p_element.Attribute(p_attribute).Value.Equals(p_value)).ToList();
        }
        
        public static IList<XElement> FindElementsByAttribute(this XDocument p_document, string p_attribute)
        {
            return p_document.Descendants()
                .Where(p_element => p_element.HasAttributes && p_element.Attribute(p_attribute) != null).ToList();
        }
        
        public static IList<XElement> FindElementsByAttribute(this XDocument p_document, string p_attribute, string p_value)
        {
                return p_document.Descendants()
                    .Where(p_element => p_element.HasAttributes && p_element.Attribute(p_attribute) != null &&
                                        p_element.Attribute(p_attribute).Value.Equals(p_value)).ToList();
        }
    }

    public static class XElementExtensions
    {
        public static bool HasName(this XElement p_element, string p_name)
        {
            return p_element.Name.LocalName.Equals(p_name);
        }

        public static bool HasAttribute(this XElement p_element, string p_name)
        {
            return p_element.HasAttributes && p_element.Attribute(p_name) != null;
        }

        public static bool HasAttributeWithValue(this XElement p_element, string p_name, string p_value)
        {
            return p_element.HasAttributes && p_element.Attribute(p_name) != null && p_element.Attribute(p_name).Value.Equals(p_value);
        }

    }
}