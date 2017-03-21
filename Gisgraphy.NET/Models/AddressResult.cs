using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gisgraphy.NET.Models
{
    public class AddressResult : BaseResult
    {
        [JsonProperty("parsedAddress")]
        public Address parsedAddress { get; set; }

        [JsonProperty("result")]
        public IEnumerable<Address> results { get; set; }

        #region Overrides
        /// <summary>
        /// Get <see cref="AddressResult"/> as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
