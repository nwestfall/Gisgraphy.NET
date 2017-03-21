using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gisgraphy.NET.Models
{
    public class GeolocalizationFeatureResult : BaseResult
    {
        /// <summary>
        /// A String only present if an error occured (e.g : empty Latitude or longitude)
        /// </summary>
        [JsonProperty("error")]
        public string error { get; set; }

        [JsonProperty("result")]
        public IEnumerable<GeolocalizationFeature> results { get; set; }

        #region Overrides
        /// <summary>
        /// Get <see cref="GeolocalizationFeature"/> as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
