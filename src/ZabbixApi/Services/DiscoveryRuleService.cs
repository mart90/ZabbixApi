﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZabbixApi.Entities;
using ZabbixApi.Helper;
using ZabbixApi;

namespace ZabbixApi.Services
{
    public interface IDiscoveryRuleService : ICRUDService<DiscoveryRule, DiscoveryRuleInclude>
    {

    }
    public class DiscoveryRuleService : CRUDService<DiscoveryRule, DiscoveryRuleService.DiscoveryRulesidsResult, DiscoveryRuleInclude>, IDiscoveryRuleService
    {
        public DiscoveryRuleService(IContext context) : base(context, "drule") { }

        public override IEnumerable<DiscoveryRule> Get(object filter = null, IEnumerable<DiscoveryRuleInclude> include = null)
        {
            var includeHelper = new IncludeHelper(include == null ? 1 : include.Sum(x => (int)x));
            var @params = new
            {
                output = "extend",
                selectDHosts = includeHelper.WhatShouldInclude(DiscoveryRuleInclude.DiscoveredHosts),
                selectDChecks = includeHelper.WhatShouldInclude(DiscoveryRuleInclude.DiscoveryChecks),

                filter = filter
            };
            return BaseGet(@params);
        }

        public class DiscoveryRulesidsResult : EntityResultBase
        {
            [JsonProperty("druleids")]
            public override string[] ids { get; set; }
        }


    }

    public enum DiscoveryRuleInclude
    {
        All = 1,
        None = 2,
        DiscoveryChecks  = 4,
        DiscoveredHosts = 8
    }
}
