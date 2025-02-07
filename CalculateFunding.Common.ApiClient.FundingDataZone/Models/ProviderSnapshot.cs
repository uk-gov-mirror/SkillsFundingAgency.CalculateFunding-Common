﻿using System;

namespace CalculateFunding.Common.ApiClient.FundingDataZone.Models
{
    public class ProviderSnapshot
    {
        public int ProviderSnapshotId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Version { get; set; }

        public DateTime TargetDate { get; set; }

        public DateTime Created { get; set; }

        public string FundingStreamCode { get; set; }

        public string FundingStreamName { get; set; }
    }
}
