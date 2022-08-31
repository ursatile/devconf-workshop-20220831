using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using Autobarn.Data.Entities;

namespace Autobarn.Website.Controllers {
    public static class HypermediaExtensions {
        public static dynamic ToDynamic(this object value) {
            IDictionary<string, object> expando = new ExpandoObject();
            var properties = TypeDescriptor.GetProperties(value.GetType());
            foreach (PropertyDescriptor property in properties) {
                if (Ignore(property)) continue;
                expando.Add(property.Name, property.GetValue(value));
            }
            return (ExpandoObject)expando;
        }

        private static bool Ignore(PropertyDescriptor property) {
            return property.Attributes.OfType<Newtonsoft.Json.JsonIgnoreAttribute>().Any();
        }

        public static dynamic ToResource(this Model model)
        {
            var result = model.ToDynamic();
            result._links = new {
                self = new {
                    href = $"/api/models/{model.Code}"
                }
            };
            return result;
        }
    }
}
