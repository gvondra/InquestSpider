﻿using Microsoft.AspNetCore.Authorization;
using System;

namespace ResourceRPC
{
    public class AuthorizationRequirement : IAuthorizationRequirement
    {
        public AuthorizationRequirement(string policyName, string issuer)
        {
            PolicyName = policyName;
            Issuer = issuer;
            Roles = Array.Empty<string>();
        }

        public AuthorizationRequirement(string policyName, string issuer, params string[] roles)
        {
            PolicyName = policyName;
            Issuer = issuer;
            Roles = roles;
        }

        public string PolicyName { get; set; }
        public string Issuer { get; set; }
        public string[] Roles { get; set; }
    }
}
