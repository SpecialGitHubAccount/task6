using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Task6
{
    public class Book : Publication
    {
        public string ISBN { get; set; }
        public string Author { get; set; }

        public static Book CreateBook(XElement item)
        {
            string title = string.Empty;
            string note = string.Empty;
            string author = string.Empty;
            int? sheetsQuantity = null;
            int? publishingYear = null;
            string publisherName = string.Empty;
            string publicationPlace = string.Empty;
            string isbn = string.Empty;


            if (item.Attribute("title") != null)
                title = item.Attribute("title").Value;
            if (item.Attribute("sheetsQuantity") != null)
            {
                try
                {
                    sheetsQuantity = XmlConvert.ToInt32(item.Attribute("sheetsQuantity").Value);
                }
                catch
                {
                    sheetsQuantity = null;
                }
            }
            if (item.Attribute("note") != null)
                note = item.Attribute("note").Value;
            if (item.Attribute("author") != null)
                author = item.Attribute("author").Value;
            if (item.Attribute("isbn") != null)
                isbn = item.Attribute("isbn").Value;
            if (item.Attribute("publisherName") != null)
                publisherName = item.Attribute("publisherName").Value;
            if (item.Attribute("publicationPlace") != null)
                publicationPlace = item.Attribute("publicationPlace").Value;
            if (item.Attribute("publishingYear") != null)
            {
                try
                {
                    publishingYear = Convert.ToInt32(item.Attribute("publishingYear").Value);
                }
                catch
                {
                    publishingYear = null;
                }
            }

            return new Book
            {
                Author = author,
                ISBN = isbn,
                Note = note,
                Title = title,
                SheetsQuantity = sheetsQuantity,
                PublisherName = publisherName,
                PublicationPlace = publicationPlace,
                PublishingYear = publishingYear
            };
        }

        public override string ToString()
        {
            string baseStr = base.ToString();
            return baseStr + string.Format("ISBN: {0}\nAuthor: {1}\n",
                !string.IsNullOrEmpty(ISBN)?ISBN : "Unknown", 
                !string.IsNullOrEmpty(Author)? Author: "Unknown");
        }

        protected override IEnumerable<XAttribute> GetAttributes()
        {
            if (string.IsNullOrEmpty(ISBN))
            {
                throw new Exception("This is mandatory property");
            }
            yield return new XAttribute("isbn", this.ISBN);

            if (!string.IsNullOrEmpty(Author))
                yield return new XAttribute("author", this.Author);

            foreach (var item in base.GetAttributes())
            {
                yield return item;
            }
        }

        public override XElement SerializeToXEelement()
        {
            return new XElement("book", GetAttributes().ToArray());
        }
    }
}