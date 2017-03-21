using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gisgraphy.NET.Models
{
    public class AutocompleteResult
    {
        [JsonProperty("responseHeader")]
        public ResponseHeader responseHeader { get; set; }
        
        [JsonProperty("response")]
        public AutocompleteResponse response { get; set; }       

        [JsonIgnore]
        public string message { get; set; }

        public AutocompleteResult()
        {
            responseHeader = new ResponseHeader();
            response = new AutocompleteResponse();
        }

        #region Overrides
        /// <summary>
        /// Get <see cref="AutocompleteResult"/> as a string 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
