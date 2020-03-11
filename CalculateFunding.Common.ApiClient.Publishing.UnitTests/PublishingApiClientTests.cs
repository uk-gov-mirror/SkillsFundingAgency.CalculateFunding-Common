using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CalculateFunding.Common.ApiClient.Models;
using CalculateFunding.Common.ApiClient.Publishing.Models;
using CalculateFunding.Common.Models.Search;
using CalculateFunding.Common.Testing;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Serilog;
// ReSharper disable HeapView.CanAvoidClosure

namespace CalculateFunding.Common.ApiClient.Publishing.UnitTests
{
    [TestClass]
    public class PublishingApiClientTests : ApiClientTestBase
    {
        private PublishingApiClient _client;

        [TestInitialize]
        public void SetUp()
        {
            _client = new PublishingApiClient(ClientFactory,
                Substitute.For<ILogger>());
        }

        [TestMethod]
        public async Task SavePaymentDates()
        {
            string csv = NewRandomString();
            string fundingStreamId = NewRandomString();
            string fundingPeriodId = NewRandomString();

            HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
            
            GivenTheStatusCode($"fundingstreams/{fundingStreamId}/fundingperiods/{fundingPeriodId}/paymentdates",
                expectedStatusCode, HttpMethod.Post);

            HttpStatusCode actualStatusCode = await _client.SavePaymentDates(csv, fundingStreamId, fundingPeriodId);

            actualStatusCode
                .Should()
                .Be(expectedStatusCode);
            
            AndTheRequestContentsShouldHaveBeen(csv);
        }

        [TestMethod]
        public async Task GetPaymentDates()
        {
            string fundingStreamId = NewRandomString();
            string fundingPeriodId = NewRandomString();

            await AssertGetRequest($"fundingstreams/{fundingStreamId}/fundingperiods/{fundingPeriodId}/paymentdates",
                new FundingStreamPaymentDates(),
                () => _client.GetPaymentDates(fundingStreamId, fundingPeriodId));
        }

        [TestMethod]
        public async Task GetLatestProfileTotalsGetsProfileTotals()
        {
            string fundingStreamId = NewRandomString();
            string fundingPeriodId = NewRandomString();
            string providerId = NewRandomString();
            
            string expectedUri = $"publishedproviders/{fundingStreamId}/{fundingPeriodId}/{providerId}/profileTotals";
            
            IEnumerable<ProfileTotal> expectedTotals = new[]
            {
                new ProfileTotal(),
            };
            
            GivenTheResponse(expectedUri, expectedTotals, HttpMethod.Get);

            ApiResponse<IEnumerable<ProfileTotal>> response = await _client.GetLatestProfileTotals(fundingStreamId,
                fundingPeriodId,
                providerId);

            response?.Content
                .Should()
                .BeEquivalentTo(expectedTotals);
        }

        [TestMethod]
        public async Task GetAllReleasedProfileTotalsGetsAllReleasedProfileTotals()
        {
            string fundingStreamId = NewRandomString();
            string fundingPeriodId = NewRandomString();
            string providerId = NewRandomString();

            string expectedUri = $"publishedproviders/{fundingStreamId}/{fundingPeriodId}/{providerId}/allProfileTotals";

            PublishedProviderVersion publishedProviderVersion = new PublishedProviderVersion { Version = 1, Date = DateTimeOffset.MinValue };

            IEnumerable<ProfileTotal> expectedTotals = new[]
            {
                new ProfileTotal(),
            };

            IDictionary<int, ProfilingVersion> expectedProfiling = new Dictionary<int, ProfilingVersion>(new[] { new KeyValuePair<int, 
                ProfilingVersion>(publishedProviderVersion.Version, 
                new ProfilingVersion { Date = publishedProviderVersion.Date, 
                    Version = publishedProviderVersion.Version, 
                    ProfileTotals = expectedTotals }) });

            GivenTheResponse(expectedUri, expectedProfiling, HttpMethod.Get);

            ApiResponse<IDictionary<int, ProfilingVersion>> response = await _client.GetAllReleasedProfileTotals(fundingStreamId,
                fundingPeriodId,
                providerId);

            response?.Content
                .Should()
                .BeEquivalentTo(expectedProfiling);

            AndTheUrisShouldHaveBeenRequested(expectedUri);
        }

        [TestMethod]
        [DataRow(HttpStatusCode.OK)]
        [DataRow(HttpStatusCode.Unauthorized)]
        [DataRow(HttpStatusCode.InsufficientStorage)]
        public async Task RefreshFundingForSpecificationPostsToPublishingService(HttpStatusCode expectedStatusCode)
        {
            string specificationId = NewRandomString();
            string expectedUri = $"specifications/{specificationId}/refresh";

            GivenTheStatusCode(expectedUri, expectedStatusCode, HttpMethod.Post);

            ValidatedApiResponse<JobCreationResponse> response = await _client.RefreshFundingForSpecification(specificationId);

            response
                .Should()
                .NotBeNull();

            response
                .StatusCode
                .Should()
                .Be(expectedStatusCode);
        }

        [TestMethod]
        [DataRow(HttpStatusCode.OK)]
        [DataRow(HttpStatusCode.BadGateway)]
        [DataRow(HttpStatusCode.InternalServerError)]
        public async Task ApproveFundingForSpecificationPostsToPublishingService(HttpStatusCode expectedStatusCode)
        {
            string specificationId = NewRandomString();
            string expectedUri = $"specifications/{specificationId}/approve";

            GivenTheStatusCode(expectedUri, expectedStatusCode, HttpMethod.Post);

            ValidatedApiResponse<JobCreationResponse> response = await _client.ApproveFundingForSpecification(specificationId);

            response
                 .Should()
                 .NotBeNull();

            response
                .StatusCode
                .Should()
                .Be(expectedStatusCode);
        }

        [TestMethod]
        public async Task GetPublishedProviderVersion()
        {
            string fundingStreamId = NewRandomString();
            string fundingPeriodId = NewRandomString();
            string providerId = NewRandomString();
            string version = NewRandomString();

            await AssertGetRequest($"publishedproviderversions/{fundingStreamId}/{fundingPeriodId}/{providerId}/{version}",
                new PublishedProviderVersion(),
                () => _client.GetPublishedProviderVersion(fundingStreamId, fundingPeriodId, providerId, version));
        }

        [TestMethod]
        public async Task GetPublishedProviderTransactions()
        {
            string specificationId = NewRandomString();
            string providerId = NewRandomString();

            await AssertGetRequest($"publishedprovidertransactions/{specificationId}/{providerId}",
                Enumerable.Empty<PublishedProviderTransaction>(),
                () => _client.GetPublishedProviderTransactions(specificationId, providerId));
        }

        [TestMethod]
        public async Task GetPublishedProviderVersionBody()
        {
            string id = NewRandomString();

            await AssertGetRequest($"publishedproviderversion/{id}/body",
                id,
                NewRandomString(),
                _client.GetPublishedProviderVersionBody);
        }

        [TestMethod]
        public async Task CanChooseForFunding()
        {
            string id = NewRandomString();

            await AssertGetRequest($"specifications/{id}/funding/canChoose",
                id,
                new SpecificationCheckChooseForFundingResult(),
                _client.CanChooseForFunding);    
        }

        [TestMethod]
        public async Task SearchPublishedProvider()
        {
            await AssertPostRequest("publishedprovider/publishedprovider-search",
                new SearchModel(),
                new SearchResults<PublishedProviderSearchItem>(),
                _client.SearchPublishedProvider);
        }

        [TestMethod]
        public async Task RefreshFundingForSpecification()
        {
            string id = NewRandomString();

            await AssertPostRequest($"specifications/{id}/refresh",
                id,
                new JobCreationResponse(),
                _client.RefreshFundingForSpecification);        
        }
     
        [TestMethod]
        public async Task ApproveFundingForSpecification()
        {
            string id = NewRandomString();

            await AssertPostRequest($"specifications/{id}/approve",
                id,
                new JobCreationResponse(),
                _client.ApproveFundingForSpecification);        
        }
        
        [TestMethod]
        public async Task PublishFundingForSpecification()
        {
            string id = NewRandomString();

            await AssertPostRequest($"specifications/{id}/publish",
                id,
                new JobCreationResponse(),
                _client.PublishFundingForSpecification);        
        }

        [TestMethod]
        public async Task GetProviderStatusCounts()
        {
            string specificationId = NewRandomString();
            string providerType = NewRandomString();
            string localAuthority = NewRandomString();
            string status = NewRandomString();

            await AssertGetRequest(
                $"specifications/{specificationId}/publishedproviders/publishingstatus?providerType={providerType}&localAuthority={localAuthority}&status={status}",
                Enumerable.Empty<ProviderFundingStreamStatusResponse>(),
                () => _client.GetProviderStatusCounts(specificationId, providerType, localAuthority, status));
        }

        [TestMethod]
        public async Task SearchPublishedProviderLocalAuthorities()
        {
            string searchText = NewRandomString();
            string fundingStreamId = NewRandomString();
            string fundingPeriodId = NewRandomString();

            await AssertGetRequest($"publishedproviders/{fundingStreamId}/{fundingPeriodId}/localauthorities?searchText={searchText}",
                new[]
                {
                    NewRandomString(),
                    NewRandomString()
                }.AsEnumerable(),
                () => _client.SearchPublishedProviderLocalAuthorities(searchText, fundingStreamId, fundingPeriodId));
        }
    }
}