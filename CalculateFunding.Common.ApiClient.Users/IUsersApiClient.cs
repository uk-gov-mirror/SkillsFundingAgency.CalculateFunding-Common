﻿using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CalculateFunding.Common.ApiClient.Models;
using CalculateFunding.Common.ApiClient.Users.Models;

namespace CalculateFunding.Common.ApiClient.Users
{
    public interface IUsersApiClient
    {
        Task<ApiResponse<User>> GetUserByUserId(string userId);

        Task<ValidatedApiResponse<User>> ConfirmSkills(string userId, UserConfirmModel userConfirmModel);

        Task<ApiResponse<IEnumerable<FundingStreamPermission>>> GetFundingStreamPermissionsForUser(string userId);

        Task<ApiResponse<EffectiveSpecificationPermission>> GetEffectivePermissionsForUser(string userId, string specificationId);

        Task<ValidatedApiResponse<FundingStreamPermission>> UpdateFundingStreamPermission(string userId, string fundingStreamId, FundingStreamPermissionUpdateModel permissions);
        
        Task<HttpStatusCode> ReIndex();
        Task<ApiResponse<FundingStreamPermissionCurrentDownloadModel>> DownloadEffectivePermissionsForFundingStream(string fundingStreamId);

    }
}
