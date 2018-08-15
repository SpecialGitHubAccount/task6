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

        public static Newspaper CreateNewspaper(XElement item)
        {
            string title = string.Empty;
            string note = string.Empty;
            string author = string.Empty;
            int? sheetsQuantity = 0;
            int? publishingYear = 0;
            string publisherName = string.Empty;
            string publicationPlace = string.Empty;
            string issn = string.Empty;
            long? id = 0;
            DateTime? date = null;


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
            if (item.Attribute("id") != null)
            {
                try
                {
                    id = XmlConvert.ToInt64(item.Attribute("id").Value);
                }
                catch
                {
                    id = null;
                }
            }
            if (item.Attribute("issn") != null)
                issn = item.Attribute("issn").Value;
            if (item.Attribute("date") != null)
            {
                try
                {
                    date = XmlConvert.ToDateTime(item.Attribute("date").Value, "yyyy-MM-dd HH:mm:ss");
                }
                catch
                {
                    date = null;
                }
            }
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

            return new Newspaper
            {
                Id = id,
                ISSN = issn,
                Note = note,
                Title = title,
                SheetsQuantity = sheetsQuantity,
                PublisherName = publisherName,
                PublicationPlace = publicationPlace,
                PublishingYear = publishingYear,
                Date = date
            };
        }

        public override string ToString()
        {
            return base.ToString() + string.Format("ISSN: {0}\nId: {1}\nDate {2}",
                !string.IsNullOrEmpty(ISSN) ? ISSN : "Unknown", 
                Id.HasValue ? Id.Value.ToString() : "Unknown", 
                Date.HasValue ? Date.Value.ToString("yyyy-MM-dd") : "Unknown");
        }

        protected override IEnumerable<XAttribute> GetAttributes()
        {
            if (string.IsNullOrEmpty(ISSN))
            {
                throw new Exception("This is mandatory property");
            }
            yield return new XAttribute("issn", this.ISSN);

            if (Id.HasValue)
                yield return new XAttribute("id", this.Id.Value);
            if (Date.HasValue)
                yield return new XAttribute("date", this.Date.Value.ToString("yyyy-MM-dd HH:mm:ss"));


            foreach (var item in base.GetAttributes())
            {
                yield return item;
            }
        }

        public override XElement SerializeToXEelement()
        {
            return new XElement("newspaper", GetAttributes().ToArray());
        }
    }
}
