﻿using CF.Common.Authorization.Policies;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CF.WebBootstrap.Authorization.Requirements
{
    internal class PolicyRequirement : IAuthorizationRequirement
    {
        public Type PolicyType { get; }

        public PolicyRequirement(Type policyType)
        {
            this.PolicyType = policyType ?? throw new ArgumentNullException(nameof(policyType));

            if (!this.PolicyType.GetInterfaces().Any(x => x == typeof(IStandalonePolicy)))
            {
                throw new ArgumentException($"The policy type [{policyType.FullName}] does not inherit from [{typeof(IStandalonePolicy).FullName}].");
            }
        }
    }
}
