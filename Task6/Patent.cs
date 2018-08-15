using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Task6
{
    public class Patent : LibraryResource
    {
        public string Inventor { get; set; }
        public string Country { get; set; }
        public int? RegistrationNumber { get; set; }
        public DateTime? ApplicationSubmissionDate { get; set; }
        public DateTime? PublicationDate { get; set; }

        public static Patent CreatePatent(XElement item)
        {
            string title = string.Empty;
            string note = string.Empty;
            string inventor = string.Empty;
            int? sheetsQuantity = null;
            int? registrationNumber = null;
            string country = string.Empty;
            DateTime? applicationSubmissionDate = null;
            DateTime? publicationDate = null;

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
            if (item.Attribute("inventor") != null)
                inventor = item.Attribute("inventor").Value;
            if (item.Attribute("country") != null)
                country = item.Attribute("country").Value;
            if (item.Attribute("registrationNumber") != null)
            {
                try
                {
                    registrationNumber = XmlConvert.ToInt32(item.Attribute("registrationNumber").Value);
                }
                catch
                {
                    registrationNumber = null;
                }
            }
            if (item.Attribute("applicationSubmissionDate") != null)
            {
                try
                {
                    applicationSubmissionDate = XmlConvert.ToDateTime(item.Attribute("applicationSubmissionDate").Value, "yyyy-MM-dd HH:mm:ss");
                }
                catch
                {
                    applicationSubmissionDate = null;
                }
            }
            if (item.Attribute("publicationDate") != null)
            {
                try
                {
                    publicationDate = XmlConvert.ToDateTime(item.Attribute("publicationDate").Value, "yyyy-MM-dd HH:mm:ss");
                }
                catch
                {
                    publicationDate = null;
                }
            }

            return new Patent
            {
                Title = title,
                Note = note,
                SheetsQuantity = sheetsQuantity,
                ApplicationSubmissionDate = applicationSubmissionDate,
                Country = country,
                Inventor = inventor,
                PublicationDate = publicationDate,
                RegistrationNumber = registrationNumber
            };
        }

        public override string ToString()
        {
            return string.Format("RegistrationNumber: {0}\nPublicationDate: {1}\nInventor: {5},\nCountry: {6}\nApplicationSubmissionDate: {7}",
                RegistrationNumber.HasValue? RegistrationNumber.Value.ToString(): "Unknown", 
                PublicationDate.HasValue ? PublicationDate.Value.ToString("yyyy-MM-dd") : "Unknown", 
                !string.IsNullOrEmpty( Inventor)? Inventor : "Unknown", 
                !string.IsNullOrEmpty(Country)? Country : "Unknown", 
                ApplicationSubmissionDate.HasValue ? ApplicationSubmissionDate.Value.ToString("yyyy-MM-dd") : "Unknown");
        }

        protected override IEnumerable<XAttribute> GetAttributes()
        {
            if (!RegistrationNumber.HasValue)
            {
                throw new Exception("This is mandatory property");
            }
            yield return new XAttribute("registrationNumber", this.RegistrationNumber);

            if (!string.IsNullOrEmpty(Inventor))
                yield return new XAttribute("inventor", this.Inventor);
            if (string.IsNullOrEmpty(Country))
                yield return new XAttribute("country", this.Country);
            if (ApplicationSubmissionDate.HasValue)
                yield return new XAttribute("applicationSubmissionDate", this.ApplicationSubmissionDate.Value);
            if (PublicationDate.HasValue)
                yield return new XAttribute("publicationDate", this.PublicationDate.Value);

            foreach (var item in base.GetAttributes())
            {
                yield return item;
            }
        }

        public override XElement SerializeToXEelement()
        {
            return new XElement("patent", GetAttributes().ToArray());
        }
    }
}
