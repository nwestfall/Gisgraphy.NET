using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gisgraphy.NET.Models
{
    public class AutocompleteResponse : Object
    {
        [JsonProperty("numFound")]
        public int resultsFound { get; set; } = 0;

        [JsonProperty("start")]
        public int start { get; set; } = 0;

        /// <summary>
        /// The max score value accross all the results found
        /// </summary>
        [JsonProperty("maxScore")]
        public double? maxScore { get; set; }

        [JsonProperty("docs")]
        public IEnumerable<Autocomplete> results { get; set; }

        #region Overrides
        /// <summary>
        /// Get <see cref="AutocompleteResponse"/> as a string 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
