using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Task6
{
    class NewspaperCreator : LibraryResourceCreator
    {
        public override LibraryResource CreateFromXElementIfMatch(XElement item)
        {
            Newspaper newspaper = null;
            if (item.Name == typeof(Newspaper).Name)
            {
                newspaper = new Newspaper();
                Int32Setter(newspaper, item);
                Int64Setter(newspaper, item);
                StringsSetter(newspaper, item);
                DateSetter(newspaper, item);
            }
            return newspaper;
        }
    }
}
