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
    class BookCreator : LibraryResourceCreator
    {
        public override LibraryResource CreateFromXElementIfMatch(XElement item)
        {
            Book book = null;
            
            if (item.Name == typeof(Book).Name)
            {
                book = new Book();
                Int32Setter(book, item);
                StringsSetter(book, item);
            }
            return book;
        }
    }
}
