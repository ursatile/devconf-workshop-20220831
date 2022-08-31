using Autobarn.Data.Entities;
using GraphQL.Types;

namespace Autobarn.Website.GraphQL.GraphTypes
{
    public sealed class ManufacturerGraphType : ObjectGraphType<Manufacturer> {
        public ManufacturerGraphType() {
            Name = "manufacturer";
            Field(m => m.Name).Description("The manufacturer name, eg. Volkswagen, Fiat, BMW");
            Field(m => m.Code).Description("The database code identifying this model");
        }
    }
}