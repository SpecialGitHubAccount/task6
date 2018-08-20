using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Task6;

namespace Task6
{
    public class LibResRW : LibraryInfrastructure<LibraryResource>, IDisposable
    {
        
        public LibResRW(Stream stream)
        {
            this.stream = stream;
            // Если корневого элемента не будет найдено - возникнет исключение.
            try
            {
                doc = XDocument.Load(stream);
            }
            catch
            {
                doc = new XDocument(new XElement("catalog"));
                doc.Save(stream);
            }
            // Если не будет найдено тега "Каталог",
            if (doc.Element("catalog") == null)
            {
                doc = new XDocument(new XElement("catalog"));
                doc.Save(stream);
            }

            stream.Position = 0;
            reader = XmlReader.Create(stream);
            writer = XmlWriter.Create(stream);
        }

        public IEnumerable<LibraryResource> Read()
        {
            stream.Position = 0;
            reader = XmlReader.Create(stream);
            reader.ReadToFollowing(XmlRootElelement);
            XElement catalog = XElement.ReadFrom(reader) as XElement;

            List<LibraryResource> libResources = new List<LibraryResource>();
            libResCreators = new LibraryResourceCreator[3] 
            {
                new BookCreator(),
                new NewspaperCreator(),
                new PatentCreator()
            };

            catalog.Elements().ToList().ForEach((libResourceXElement) =>
            {
                foreach (var libResourceCreator in libResCreators)
                {
                    LibraryResource libraryResource = libResourceCreator.CreateFromXElementIfMatch(libResourceXElement);
                    if(libraryResource !=null)
                    {
                        libResources.Add(libraryResource);
                    }
                }
            });
            return libResources;
        }

        public void Write(LibraryResource item)
        {
            XElement data = item.SerializeToXEelement();
            doc.Element(XmlRootElelement).Add(data);
        }

        public void Flush()
        {
            stream.Position = 0;
            doc.Save(stream);
            stream.Position = 0;
            doc = XDocument.Load(stream);
        }

        public void Dispose()
        {
            stream.Position = 0;
            doc.Save(stream);
            reader.Close();
            writer.Close();
            stream.Close();
        }

        private XmlReader reader;
        private XmlWriter writer;
        private XDocument doc;
        private readonly Stream stream;

        private LibraryResourceCreator[] libResCreators = null;
        private static string XmlRootElelement = "catalog";
    }
}
