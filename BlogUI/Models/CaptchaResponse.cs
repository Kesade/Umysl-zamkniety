using System.Collections.Generic;
using BlogUI.Controllers;
using Common.DomainEntities;
using Newtonsoft.Json;

namespace BlogUI.Models
{
        public class CaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("error-codes")]
            public List<string> ErrorCodes { get; set; }
        }
}