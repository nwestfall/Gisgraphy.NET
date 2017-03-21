using System;
using Newtonsoft.Json;

namespace Gisgraphy.NET.Models
{
    public class BaseResult : Object
    {
        [JsonProperty("message")]
        public string message { get; set; }

        [JsonProperty("numFound")]
        public int resultsFound { get; set; } = 0;

        [JsonProperty("QTime")]
        public int executionTime { get; set; } = 0;

        [JsonProperty("executionTimeSpan")]
        public TimeSpan executionTimeSpan
        {
            get
            {
                return new TimeSpan(0, 0, executionTime);
            }
        }

        #region Overrides
        /// <summary>
        /// Get <see cref="BaseResult"/>  as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
