using System;
using Newtonsoft.Json;

namespace Gisgraphy.NET.Models
{
    public class StreetDistance : Object
    {
        /// <summary>
        /// The name of the street
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }

        /// <summary>
        /// The distance between the point and the nearest point to the street in meters
        /// </summary>
        [JsonProperty("distance")]
        public double? distance { get; set; }

        /// <summary>
        /// Unique id of the street, it is unique between GeoNames and OpenStreetMap
        /// </summary>
        [JsonProperty("gid")]
        public int? id { get; set; }

        /// <summary>
        /// OpenStreetMap unique id of the street
        /// </summary>
        [JsonProperty("openstreetmapId")]
        public int? openStreetMapId { get; set; }

        /// <summary>
        /// The type of the street (see street type list)
        /// </summary>
        [JsonProperty("streetType")]
        public string streetType { get; set; }

        /// <summary>
        /// Whether the street is a one way street or not
        /// </summary>
        [JsonProperty("oneWay")]
        public bool oneWay { get; set; }

        /// <summary>
        /// The ISO 3166 country code
        /// </summary>
        [JsonProperty("countryCode")]
        public string countryCode { get; set; }

        /// <summary>
        /// Length of the street in meters
        /// </summary>
        [JsonProperty("length")]
        public double? length { get; set; }

        /// <summary>
        /// The latitude of the middle of the street(north-south)
        /// </summary>
        [JsonProperty("lat")]
        public double? latitude { get; set; }

        /// <summary>
        /// The longitude of the middle of the street(east-west)
        /// </summary>
        [JsonProperty("lng")]
        public double? longitude { get; set; }

        /// <summary>
        /// Information on the city where the street is (depends on OpenStreetMap 'is_in' field), the city in general
        /// </summary>
        [JsonProperty("isIn")]
        public string isIn { get; set; }

        /// <summary>
        /// Information on the place where the street is (quater, common place). Generally a place at a lower level than city
        /// </summary>
        [JsonProperty("isInPlace")]
        public string isInPlace { get; set; }

        /// <summary>
        /// Information of the administration division where the street is
        /// </summary>
        [JsonProperty("isInAdm")]
        public string isInAdm { get; set; }

        /// <summary>
        /// Information of the zipcode where the street / POI is (often fill for placetype street)
        /// </summary>
        [JsonProperty("isInZip")]
        public string isInZip { get; set; }

        /// <summary>
        /// NOT USED YET
        /// </summary>
        [JsonProperty("fullyQualifiedAddress")]
        public string fullyQualifiedAddress { get; set; }

        #region Overrides
        /// <summary>
        /// Compare two <see cref="StreetDistance"/>   objects
        /// </summary>
        /// <param name="obj"><see cref="StreetDistance"/> to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            StreetDistance streetDist = (StreetDistance)obj;
            return (id == streetDist.id);
        }

        /// <summary>
        /// Get <see cref="StreetDistance"/> Hash Code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (id.HasValue)
                return id.Value;
            else
                return base.GetHashCode();
        }

        /// <summary>
        /// Get <see cref="StreetDistance"/> as a string 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
