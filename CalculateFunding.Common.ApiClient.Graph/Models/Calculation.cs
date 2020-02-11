﻿using Newtonsoft.Json;
using System;

namespace CalculateFunding.Common.ApiClient.Graph.Models
{
    [Serializable]
    public class Calculation
    {
        [JsonProperty("calculationid")]
        public string CalculationId { get; set; }

        [JsonProperty("specificationid")]
        public string SpecificationId { get; set; }

        [JsonProperty("calculationname")]
        public string CalculationName { get; set; }

        [JsonProperty("calculationtype")]
        public CalculationType CalculationType { get; set; }

        [JsonProperty("fundingstream")]
        public string FundingStream { get; set; }

        [JsonProperty("templatecalculationid", NullValueHandling = NullValueHandling.Ignore)]
        public string TemplateCalculationId { get; set; }
    }
}
