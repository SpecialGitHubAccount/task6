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
    public abstract class LibraryResourceCreator
    {
        public abstract LibraryResource CreateFromXElementIfMatch(XElement item);

        protected LibraryResource Int64Setter( LibraryResource libResource, XElement item)
        {
            foreach (PropertyInfo property in libResource.GetType().GetProperties().Where(pi => pi.PropertyType == typeof(Nullable<Int64>)))
            {
                long? value = null;
                try
                {
                    value = XmlConvert.ToInt64(item.Attribute(property.Name).Value);
                }
                catch
                {
                    value = null;
                }
                if (Attribute.IsDefined(property, typeof(RequiredAttribute), true))
                {
                    if (!value.HasValue)
                    {
                        throw new Exception("this is mandatory field");
                    }
                }
                property.SetValue(libResource, value);
            }
            return libResource;
        }

        protected LibraryResource Int32Setter(LibraryResource libResource, XElement item)
        {
            foreach (PropertyInfo property in libResource.GetType().GetProperties().Where(pi => pi.PropertyType == typeof(Nullable<Int32>)))
            {
                int? value = null;
                try
                {
                    value = XmlConvert.ToInt32(item.Attribute(property.Name).Value);
                }
                catch
                {
                    value = null;
                }
                if (Attribute.IsDefined(property, typeof(RequiredAttribute), true))
                {
                    if (!value.HasValue)
                    {
                        throw new Exception("this is mandatory field");
                    }
                }
                property.SetValue(libResource, value);
            }
            return libResource;
        }

        protected LibraryResource StringsSetter(LibraryResource libResource, XElement item)
        {
            foreach (PropertyInfo property in libResource.GetType().GetProperties().Where(pi => pi.PropertyType == typeof(string)))
            {
                var value = item.Attribute(property.Name);
                if (Attribute.IsDefined(property, typeof(RequiredAttribute), true))
                {
                    if (string.IsNullOrEmpty(value.ToString()))
                    {
                        throw new Exception("this is mandatory field");
                    }
                }
                // а если не равен null - можно будет безопасно возпользоваться
                // свойством Value, объекта value.
                if (value != null)
                {
                    property.SetValue(libResource, value.Value);
                }
            }
            return libResource;
        }

        protected LibraryResource DateSetter( LibraryResource libResource, XElement item)
        {
            foreach (PropertyInfo property in libResource.GetType().GetProperties().Where(pi => pi.PropertyType == typeof(Nullable<DateTime>)))
            {
                DateTime? value = null;
                try
                {
                    value = XmlConvert.ToDateTime(item.Attribute(property.Name).Value, XmlDateTimeSerializationMode.Local);
                }
                catch
                {
                    value = null;
                }
                if (Attribute.IsDefined(property, typeof(RequiredAttribute), true))
                {
                    if (!value.HasValue)
                    {
                        throw new Exception("this is mandatory field");
                    }
                }
                property.SetValue(libResource, value);
            }
            return libResource;
        }
    }
}
