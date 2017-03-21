using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Gisgraphy.NET.Models
{
    public class HouseNumber : Object
    {
        /// <summary>
        /// The lat / long coordinate
        /// </summary>
        [JsonProperty("location")]
        public string location { get; set; }

        /// <summary>
        /// the house number value
        /// </summary>
        [JsonProperty("number")]
        public string number { get; set; }

        #region Overrides
        /// <summary>
        /// Compare two <see cref="HouseNumber"/>  objects
        /// </summary>
        /// <param name="obj"><see cref="HouseNumber"/> to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            HouseNumber houseNum = (HouseNumber)obj;
            return (location == houseNum.location);
        }

        /// <summary>
        /// Get <see cref="HouseNumber"/> Hash Code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Get <see cref="HouseNumber"/> as a string 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
