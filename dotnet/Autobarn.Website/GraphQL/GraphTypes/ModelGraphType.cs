using Autobarn.Data.Entities;
using GraphQL.Types;

namespace Autobarn.Website.GraphQL.GraphTypes
{
    public sealed class ModelGraphType : ObjectGraphType<Model> {
        public ModelGraphType() {
            Name = "model";
            Field(m => m.Name).Description("What model is this, eg. DMC Delorean");
            Field(m => m.Code).Description("the vehicle model code identifying this model");
            Field(m => m.Manufacturer, nullable: false, type: typeof(ManufacturerGraphType));
        }
    }
}