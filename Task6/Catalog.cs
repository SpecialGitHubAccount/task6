using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    public class Catalog
    {
        public DateTime UploadDateTime { get; set; }
        public string LibraryName { get; set; }
        public List<Book> Books { get; private set; }
        public List<Newspaper> Newspapers { get; private set; }
        public List<Patent> Patents { get; private set; }
    }
}
