using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Task6
{
    public abstract class LibraryResource
    {
        public string Title { get; set; }
        public int? SheetsQuantity { get; set; }
        public string Note { get; set; }

        public abstract XElement SerializeToXEelement();

        public override string ToString()
        {
            return string.Format("Title: {0}\nNote: {1}\nSheets: {2}\n",
                !string.IsNullOrEmpty(Title) ? Title : "Unknown",
                !string.IsNullOrEmpty(Note) ? Note : "Unknown",
                SheetsQuantity.HasValue ? SheetsQuantity.Value.ToString() : "Unknown");
        }

        protected virtual IEnumerable<XAttribute> GetAttributes()
        {
            foreach (var item in GetType().GetProperties())
            {
                var value = item.GetValue(this);
                if (value != null)
                {
                    yield return new XAttribute(item.Name, item.GetValue(this));
                }
            }
        }

    }
}
