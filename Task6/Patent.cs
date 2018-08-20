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
        
        public override string ToString()
        {
            return base.ToString() + string.Format("RegistrationNumber: {0}\nPublicationDate: {1}\nInventor: {2},\nCountry: {3}\nApplicationSubmissionDate: {4}",
                RegistrationNumber.HasValue? RegistrationNumber.Value.ToString(): "Unknown", 
                PublicationDate.HasValue ? PublicationDate.Value.ToString("yyyy-MM-dd") : "Unknown", 
                !string.IsNullOrEmpty( Inventor)? Inventor : "Unknown", 
                !string.IsNullOrEmpty(Country)? Country : "Unknown", 
                ApplicationSubmissionDate.HasValue ? ApplicationSubmissionDate.Value.ToString("yyyy-MM-dd") : "Unknown");
        }
        
        public override XElement SerializeToXEelement()
        {
            return new XElement(GetType().Name, GetAttributes().ToArray());
        }
    }
}
