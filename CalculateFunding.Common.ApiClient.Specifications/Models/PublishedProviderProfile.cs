﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace CalculateFunding.Common.ApiClient.Specifications.Models
{
    public class PublishedProviderProfile
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("profilePeriods")]
        public IEnumerable<ProfilingPeriod> ProfilingPeriods { get; set; }

        [JsonProperty("financialEnvelopes")]
        public IEnumerable<FinancialEnvelope> FinancialEnvelopes { get; set; }
    }
}
