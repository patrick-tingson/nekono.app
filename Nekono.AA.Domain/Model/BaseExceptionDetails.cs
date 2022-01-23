using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nekono.AA.Domain.Model
{
    public class BaseExceptionDetails
    {
        [JsonProperty("statusCode", Order = 1)]
        public int StatusCode { get; set; }

        [JsonProperty("errorMessage", Order = 2)]
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class DevExceptionDetails : BaseExceptionDetails
    {
        [JsonProperty("stackTrace", Order = 3)]
        public string Stacktrace { get; set; }
    }

    public class ExceptionDetails
    {
        [JsonProperty("statusCode", Order = 1)]
        public int StatusCode { get; set; }

        [JsonProperty("errorMessage", Order = 2)]
        public string ErrorMessage { get; set; }

        [JsonProperty("stackTrace", Order = 3)]
        public string Stacktrace { get; set; }
    }
}
