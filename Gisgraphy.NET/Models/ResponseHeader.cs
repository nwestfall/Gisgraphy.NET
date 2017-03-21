using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gisgraphy.NET.Models
{
    public class ResponseHeader : Object
    {
        [JsonProperty("status")]
        public int? status { get; set; }

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
        /// Get <see cref="ResponseHeader"/>  as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
