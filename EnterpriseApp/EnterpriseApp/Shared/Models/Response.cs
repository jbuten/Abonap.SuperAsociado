using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseApp.Shared.Models
{
using System.Text.Json.Serialization;
    public class Response<T>
    {
        //[JsonProperty("isSuccess", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsSuccess { get; set; }

        //[JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; } = string.Empty;

        //[JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; } 
    }
}
