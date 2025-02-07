using CalculateFunding.Common.Models;

namespace CalculateFunding.Common.ApiClient.DataSets.Models
{
    public class DefinitionSpecificationRelationship : Reference
    {
        public Reference DatasetDefinition { get; set; }

        public Reference Specification { get; set; }

        public string Description { get; set; }

        public DatasetRelationshipVersion DatasetVersion { get; set; }

        public bool IsSetAsProviderData { get; set; }

        public bool UsedInDataAggregations { get; set; }
    }
}