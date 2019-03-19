using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHw04_Forums.Services
{
    public class NotBannedRequirement : IAuthorizationRequirement
    {
        public string role { get; private set; }
        public NotBannedRequirement(string r)
        {
            role = r;
        }
    }
}
