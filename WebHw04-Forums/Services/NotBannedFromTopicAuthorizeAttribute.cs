using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHw04_Forums.Services
{
    public class NotBannedFromTopicAuthorizeAtribute : AuthorizeAttribute
    {

        public NotBannedFromTopicAuthorizeAtribute(string name) : base()
        {
            topicName = name;
        }
        

        // Get or set the Category property by manipulating the underlying Policy property
        public string topicName
        {
            get
            {
                return Policy;
            }
            set
            {
                Policy = $"{MyIdentityData.Policy_NotBanned}{value.ToString()}";
            }
        }
        
    }
}
