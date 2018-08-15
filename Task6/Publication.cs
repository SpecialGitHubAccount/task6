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

        protected override IEnumerable<XAttribute> GetAttributes()
        {
            if (!string.IsNullOrEmpty(PublisherName))
                yield return new XAttribute("publisherName", PublisherName);
            if (!string.IsNullOrEmpty(PublicationPlace))
                yield return new XAttribute("publicationPlace", this.PublicationPlace);
            if (PublishingYear.HasValue)
                yield return new XAttribute("publishingYear", this.PublishingYear.Value);

            foreach (var item in base.GetAttributes())
            {
                yield return item;
            }
        }
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