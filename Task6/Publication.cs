using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task6
{
    public abstract class Publication : LibraryResource
    {
        public string PublisherName { get; set; }
        public string PublicationPlace { get; set; }
        public int? PublishingYear { get; set; }

        public override string ToString()
        {
            string baseStr = base.ToString();
            return baseStr + string.Format("PublisherName: {0}\nPublicationPlace: {1}\nPublishingYear: {2}\n",
                !string.IsNullOrEmpty(PublisherName)?PublisherName: "Unknown", 
                !string.IsNullOrEmpty(PublicationPlace) ? PublicationPlace.ToString() : "Unknown", 
                PublishingYear.HasValue ? PublishingYear.Value.ToString() : "Unknown");
        }
    }
}