using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SysML_Sync
{
    public class Cleaner
    {
        public Dictionary<string, string> Content { get; set; }

        public Cleaner(Dictionary<string, string> content)
        {
            Content = content;
        }
    }
}
