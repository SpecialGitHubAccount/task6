using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations;

namespace Task6
{
    public class Book : Publication
    {
        public string ISBN { get; set; }
        public string Author { get; set; }

        public override string ToString()
        {
            string baseStr = base.ToString();
            return baseStr + string.Format("ISBN: {0}\nAuthor: {1}\n",
                !string.IsNullOrEmpty(ISBN)?ISBN : "Unknown", 
                !string.IsNullOrEmpty(Author)? Author: "Unknown");
        }        

        public override XElement SerializeToXEelement()
        {
            return new XElement(GetType().Name, GetAttributes().ToArray());
        }
    }
}