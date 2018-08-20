using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task6
{
    class PatentCreator : LibraryResourceCreator
    {
        public override LibraryResource CreateFromXElementIfMatch(XElement item)
        {
            Patent patent = null;
            if (item.Name == typeof(Patent).Name)
            {
                patent = new Patent();
                Int32Setter(patent, item);
                Int64Setter(patent, item);
                StringsSetter(patent, item);
                DateSetter(patent, item);
            }
            return patent;
        }
    }
}
