using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ResourceRPC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            string googleIdIssuer = configuration["GoogleIdIssuer"];
            string idIssuer = configuration["IdIssuer"];
            List<string> authenticationSchemes = new List<string>
            {
                Constants.AUTH_SCHEMA_INQUESTSPIDER
            };
            if (!string.IsNullOrEmpty(googleIdIssuer))
                authenticationSchemes.Add(Constants.AUTH_SCHEME_GOOGLE);
            services.AddAuthorization(o =>
            {
                o.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddAuthenticationSchemes(authenticationSchemes.ToArray())
                .Build();
                Console.WriteLine($"GoogleIdIssuer={googleIdIssuer}");
                if (!string.IsNullOrEmpty(googleIdIssuer))
                {
                    o.AddPolicy(
                        Constants.POLICY_TOKEN_CREATE,
                        configure =>
                        {
                            configure.AddRequirements(new AuthorizationRequirement(Constants.POLICY_TOKEN_CREATE, googleIdIssuer))
                            .AddAuthenticationSchemes(Constants.AUTH_SCHEME_GOOGLE)
                            .Build();
                        });
                }
                Console.WriteLine($"IdIssuer={idIssuer}");
                if (!string.IsNullOrEmpty(idIssuer))
                {
                    o.AddPolicy(
                        Constants.POLICY_BL_AUTH,
                        configure =>
                        {
                            configure.AddRequirements(new AuthorizationRequirement(Constants.POLICY_BL_AUTH, idIssuer))
                            .AddAuthenticationSchemes(Constants.AUTH_SCHEMA_INQUESTSPIDER)
                            .Build();
                        });
                    AddPolicy(o, Constants.POLICY_RESOURCE_EDIT, Constants.AUTH_SCHEMA_INQUESTSPIDER, idIssuer);
                    AddPolicy(o, Constants.POLICY_RESOURCE_READ, Constants.AUTH_SCHEMA_INQUESTSPIDER, idIssuer);
                }
            });
            return services;
        }

        public static AuthenticationBuilder AddAuthentication(this AuthenticationBuilder builder, IConfiguration configuration)
        {
            HttpDocumentRetriever documentRetriever = new HttpDocumentRetriever() { RequireHttps = false };
            JsonWebKeySet keySet = JsonWebKeySet.Create(
                documentRetriever.GetDocumentAsync(configuration["JwkAddress"], System.Threading.CancellationToken.None).Result);
            builder.AddJwtBearer(Constants.AUTH_SCHEMA_INQUESTSPIDER, o =>
            {
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateActor = false,
                    ValidateTokenReplay = false,
                    RequireAudience = false,
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,
                    ValidAudience = configuration["IdIssuer"],
                    ValidIssuer = configuration["IdIssuer"],
                    IssuerSigningKeys = keySet.GetSigningKeys(),
                    TryAllIssuerSigningKeys = true
                };
                o.IncludeErrorDetails = true;
            })
            ;
            return builder;
        }

        public static AuthenticationBuilder AddGoogleAuthentication(this AuthenticationBuilder builder, IConfiguration configuration)
        {
            List<string> audiences = configuration.GetSection("GoogleIdAudiences")
                .GetChildren()
                .Select(c => c.Value)
                .ToList();
            HttpDocumentRetriever documentRetriever = new HttpDocumentRetriever() { RequireHttps = false };
            JsonWebKeySet keySet = JsonWebKeySet.Create(
                documentRetriever.GetDocumentAsync(configuration["GoogleJwksUrl"], System.Threading.CancellationToken.None).Result);
            builder.AddJwtBearer(Constants.AUTH_SCHEME_GOOGLE, o =>
            {
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateActor = false,
                    ValidateTokenReplay = false,
                    RequireAudience = false,
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,
                    ValidAudiences = audiences,
                    ValidIssuer = configuration["GoogleIdIssuer"],
                    IssuerSigningKeys = keySet.GetSigningKeys(),
                    TryAllIssuerSigningKeys = true
                };
                o.IncludeErrorDetails = true;
            })
            ;
            return builder;
        }

        private static void AddPolicy(AuthorizationOptions authorizationOptions, string policyName, string schema, string issuer, IEnumerable<string> additinalPolicies = null)
        {
            if (additinalPolicies == null)
            {
                additinalPolicies = new List<string> { policyName };
            }
            else if (!additinalPolicies.Contains(policyName))
            {
                additinalPolicies = additinalPolicies.Concat(new List<string> { policyName });
            }
            authorizationOptions.AddPolicy(
                policyName,
                configure =>
                {
                    configure.AddRequirements(new AuthorizationRequirement(policyName, issuer, additinalPolicies.ToArray()))
                    .AddAuthenticationSchemes(schema)
                    .Build();
                });
        }
    }
}
