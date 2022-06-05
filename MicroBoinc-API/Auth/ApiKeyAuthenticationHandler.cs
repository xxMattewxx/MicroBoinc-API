using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using MicroBoincAPI.Data.Accounts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using MicroBoincAPI.Models.Accounts;

namespace MicroBoincAPI.Authentication
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private static readonly AccountKey INVALID_KEY = new();
        private const string ApiKeyHeaderName = "Authorization";
        private readonly IAccountsRepo _accountsRepo;
        private readonly IMemoryCache _memoryCache;

        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<ApiKeyAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock, IAccountsRepo accountsRepo, IMemoryCache memoryCache) : base(options, logger, encoder, clock)
        {
            _accountsRepo = accountsRepo;
            _memoryCache = memoryCache;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKeyHeaderValues))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var providedApiKey = apiKeyHeaderValues.FirstOrDefault();

            if (apiKeyHeaderValues.Count == 0 || string.IsNullOrWhiteSpace(providedApiKey))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            if(!_memoryCache.TryGetValue(providedApiKey, out AccountKey key))
            {
                key = _accountsRepo.GetKey(providedApiKey) == null ? INVALID_KEY : null;
                _memoryCache.Set(providedApiKey, key, TimeSpan.FromSeconds(30));
            }
            
            if (key == INVALID_KEY)
                return Task.FromResult(AuthenticateResult.NoResult());

            Context.Items["LoggedInUser"] = key;

            var claims = new List<Claim>();
            var identity = new ClaimsIdentity(claims, Options.AuthenticationType);
            var identities = new List<ClaimsIdentity> { identity };
            var principal = new ClaimsPrincipal(identities);
            var ticket = new AuthenticationTicket(principal, Options.Scheme);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
