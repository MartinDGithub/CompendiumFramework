﻿using CF.Common.Config;
using CF.Web.AspNetCore.Config.Options;
using Microsoft.Extensions.Options;
using System;

namespace CF.Web.AspNetCore.Config
{
    class DomainConfig : IDomainConfig
    {
        public string Name { get; private set; }

        public DomainConfig(IOptionsMonitor<Root> options)
        {
            var domainOptions = options.CurrentValue?.Domain ?? throw new Exception($"Options for [{nameof(Options.Domain)}] were not loaded.");

            if (string.IsNullOrWhiteSpace(domainOptions.Name))
            {
                throw new Exception($"The [{nameof(Options.Domain.Name)}] property of [{nameof(Options.Domain)}] options was null or whitespace.");
            }
            this.Name = domainOptions.Name;
        }
    }
}
