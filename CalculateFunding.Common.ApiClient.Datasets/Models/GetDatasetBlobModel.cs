namespace CalculateFunding.Common.ApiClient.DataSets.Models
{
    public class GetDatasetBlobModel
    {
        public string DatasetId { get; set; }

        public string Filename { get; set; }

        public int Version { get; set; }

        public string DefinitionId { get; set; }

        public string Comment { get; set; }

        public string Description { get; set; }

        public string LastUpdatedById { get; set; }

        public string LastUpdatedByName { get; set; }

        public string FundingStreamId { get; set; }

        public bool MergeExistingVersion { get; set; }

        public bool ConverterWizard { get; set; }

        public DatasetEmptyFieldEvaluationOptions EmptyFieldEvaluationOption { get; set; }

        public override string ToString()
        {
            return $"{DatasetId}/v{Version}/{Filename}";
        }
    }
}