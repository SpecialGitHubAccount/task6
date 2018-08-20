using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Task6
{
    public class Newspaper : Publication
    {
        public long? Id { get; set; }
        public DateTime? Date { get; set; }
        public string ISSN { get; set; }        

        public override string ToString()
        {
            return base.ToString() + string.Format("ISSN: {0}\nId: {1}\nDate {2}",
                !string.IsNullOrEmpty(ISSN) ? ISSN : "Unknown", 
                Id.HasValue ? Id.Value.ToString() : "Unknown", 
                Date.HasValue ? Date.Value.ToString("yyyy-MM-dd") : "Unknown");
        }        

        public override XElement SerializeToXEelement()
        {
            return new XElement(GetType().Name, GetAttributes().ToArray());
        }
    }
}
