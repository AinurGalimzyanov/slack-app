using System.Net;
using System.Security.Authentication;
using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using RestSharp;
using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm;
using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm.Dto;
using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm.Options;
using Saritasa.Tools.Domain.Exceptions;
using Saritasa.SlackApp.Infrastructure.Abstractions.Interfaces.Crm.Dto.Planner;
using Saritasa.SlackApp.Infrastructure.Dtos.Planner;

namespace Saritasa.SlackApp.Infrastructure;

/// <summary>
/// Crm client.
/// </summary>
public class CrmClient : ICrmClient
{
    private readonly RestClient client;
    private readonly CrmSecretsOptions crmSecretsOptions;
    private readonly AppSettings appSettings;
    private string? accessToken;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="client">Client.</param>
    /// <param name="secretOptions">Secret options.</param>
    /// <param name="appSettings">App settings.</param>
    /// <param name="mapper">Mapper.</param>
    public CrmClient(RestClient client, IOptions<CrmSecretsOptions> secretOptions, IOptions<AppSettings> appSettings, IMapper mapper)
    {
        this.appSettings = appSettings.Value;
        this.client = client;
        this.mapper = mapper;
        crmSecretsOptions = secretOptions.Value;
    }

    private async Task<T> ExecuteWithRetryAsync<T>(RestRequest request, CancellationToken cancellationToken)
        where T : new()
    {
        if (string.IsNullOrEmpty(accessToken))
        {
            await AuthUserAsync(cancellationToken);
        }

        var authorisationEnsuringPolicy = Policy
            .HandleResult<RestResponse>(restResponse => restResponse.StatusCode == HttpStatusCode.Unauthorized)
            .RetryAsync(
                retryCount: 1,
                onRetryAsync: async (outcome, retryNumber) =>
                {
                    await AuthUserAsync(cancellationToken);
                });

        var response = await authorisationEnsuringPolicy.ExecuteAsync(
            async token =>
            {
                request.AddOrUpdateHeader("Authorization", $"Bearer {accessToken}");
                return await client.ExecuteAsync<T>(request, token);
            }, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var exception = response.ErrorException;
            throw new InfrastructureException(exception!.Message, exception);
        }

        if (response.Content == null)
        {
            throw new InvalidOperationException("Response content is null.");
        }

        return JsonConvert.DeserializeObject<T>(response.Content)!;
    }

    private async Task AuthUserAsync(CancellationToken cancellationToken)
    {
        var request = new RestRequest($"{appSettings.CrmUrl}/oauth/Token")
            .AddParameter("password", crmSecretsOptions.Password)
            .AddParameter("username", crmSecretsOptions.Username);

        var tokenDto = await client.PostAsync<TokenDto>(request, cancellationToken);

        if (tokenDto == null)
        {
            throw new AuthenticationException("No access token was received.");
        }

        accessToken = tokenDto.AccessToken;
    }

    /// <inheritdoc/>
    public async Task<CrmUserDto> GetUserAsync(string email, CancellationToken cancellationToken)
    {
        var request = new RestRequest($"{appSettings.CrmUrl}/api/users")
            .AddParameter("email", email);

        return await ExecuteWithRetryAsync<CrmUserDto>(request, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<CrmDailyStatusDto> GetDailyStatsAsync(int userId, DateOnly dateFrom, DateOnly dateTo, CancellationToken cancellationToken)
    {
        var request = new RestRequest($"{appSettings.CrmUrl}/api/users/DailyStats")
            .AddParameter("userId", userId)
            .AddParameter("dateFrom", $"{dateFrom:O}")
            .AddParameter("dateTo", $"{dateTo:O}");

        return await ExecuteWithRetryAsync<CrmDailyStatusDto>(request, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<PlannerDto> GetPlannerAsync(int userId, DateOnly dateFrom, DateOnly dateTo, CancellationToken cancellationToken)
    {
        var plannerUrl = $"{appSettings.CrmUrl}/api/projectassigned/GetByWeeks";
        var request = new RestRequest(plannerUrl)
            .AddQueryParameter("userId", userId)
            .AddQueryParameter("dateFrom", $"{dateFrom:O}")
            .AddQueryParameter("dateTo", $"{dateTo:O}");

        try
        {
            var result = await ExecuteWithRetryAsync<CrmPlannerDto>(request, cancellationToken);
            return mapper.Map<PlannerDto>(result);
        }
        catch (HttpRequestException httpRequestException)
        {
            throw new InfrastructureException("Something went wrong taking request to CRM", httpRequestException);
        }
    }
}
