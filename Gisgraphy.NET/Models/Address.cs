using System;
using Newtonsoft.Json;

namespace Gisgraphy.NET.Models
{
    public class Address : Object
    {
        [JsonProperty("nameAlternatesLocalized")]
        public dynamic nameAlternatesLocalized { get; set; }

        [JsonProperty("adm1NameAlternatesLocalized")]
        public dynamic adm1NameAlternatesLocalized { get; set; }

        [JsonProperty("adm2NameAlternatesLocalized")]
        public dynamic adm2NameAlternatesLocalized { get; set; }

        [JsonProperty("countryNameAlternatesLocalized")]
        public dynamic countryNameAlternatesLocalized { get; set; }

        /// <summary>
        /// Possible values
        /// NONE
        /// HOUSE_NUMBER
        /// STREET
        /// CITY
        /// STATE
        /// COUNTRY
        /// </summary>
        [JsonProperty("geocodingLevel")]
        public string geocodingLevel { get; set; }

        /// <summary>
        /// An internal ID to identify the address
        /// </summary>
        [JsonProperty("id")]
        public int? id { get; set; }

        /// <summary>
        /// The longitude of the address
        /// </summary>
        [JsonProperty("lng")]
        public double? longitude { get; set; }

        /// <summary>
        /// The latitude of the address
        /// </summary>
        [JsonProperty("lat")]
        public double? latitude { get; set; }

        /// <summary>
        /// An indicator that measures how the parser is confident for the result
        /// </summary>
        [JsonProperty("confidence")]
        public string confidence { get; set; }

        /// <summary>
        /// The distance of the address for the given parameter location in the query
        /// </summary>
        [JsonProperty("distance")]
        public double? distance { get; set; }

        /// <summary>
        /// The name of the place, it is null in case of address but filled if common place.
        /// Name is different than recipient name
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }

        /// <summary>
        /// The name of the organization or person at the given address
        /// </summary>
        [JsonProperty("recipientName")]
        public string recipientName { get; set; }

        /// <summary>
        /// Official number assigned to an address by the municipality, serveral languages supported
        /// </summary>
        [JsonProperty("houseNumber")]
        public string houseNumber { get; set; }

        /// <summary>
        /// All information that gives extra information on the <see cref="houseNumber"/>
        /// </summary>
        [JsonProperty("houseNumberInfo")]
        public string houseNumberInfo { get; set; }

        /// <summary>
        /// The official name of the street or the ordinal number
        /// </summary>
        [JsonProperty("streetName")]
        public string streetName { get; set; }

        /// <summary>
        /// The type of the <see cref="streetName"/>
        /// </summary>
        [JsonProperty("streetType")]
        public string streetType { get; set; }

        /// <summary>
        /// The city or locality
        /// A small town or village name sometimes is included in an address when the
        /// Delivery Point is outside the boundary of the main Post Town that serves it
        /// </summary>
        [JsonProperty("city")]
        public string city { get; set; }

        /// <summary>
        /// A sub division of a <see cref="city"/>
        /// </summary>
        [JsonProperty("citySubdivision")]
        public string citySubdivision { get; set; }

        /// <summary>
        /// 'Sub' city attached to a big city
        /// </summary>
        [JsonProperty("dependentLocality")]
        public string dependentLocality { get; set; }

        /// <summary>
        /// A city is a required part of all postal addresses in the United Kingdom
        /// </summary>
        [JsonProperty("postTown")]
        public string postTown { get; set; }

        /// <summary>
        /// The state or county (when applicable)
        /// Can be fullname or abbreviation
        /// </summary>
        [JsonProperty("state")]
        public string state { get; set; }

        /// <summary>
        /// The district
        /// Mainly use for Russia
        /// </summary>
        [JsonProperty("district")]
        public string district { get; set; }

        /// <summary>
        /// A section of an urban settlement
        /// </summary>
        [JsonProperty("quarter")]
        public string quarter { get; set; }

        /// <summary>
        /// The zip or postal code
        /// </summary>
        [JsonProperty("zipCode")]
        public string zipCode { get; set; }

        /// <summary>
        /// Information on floor, unit, and sometimes <see cref="POBox"/>
        /// </summary>
        [JsonProperty("extraInfo")]
        public string extraInfo { get; set; }

        /// <summary>
        /// Information on the unit, mainly used and filled by standardizer
        /// </summary>
        [JsonProperty("suiteType")]
        public string suiteType { get; set; }

        /// <summary>
        /// Information on the unit, maily used and filled by standardizer
        /// </summary>
        [JsonProperty("suiteNumber")]
        public string suiteNumber { get; set; }

        /// <summary>
        /// Post office box
        /// Biote postale
        /// Casilla de Correo
        /// And more...
        /// </summary>
        [JsonProperty("POBox")]
        public string POBox { get; set; }

        /// <summary>
        /// Extra info on the <see cref="POBox"/>
        /// </summary>
        [JsonProperty("POBoxInfo")]
        public string POBoxInfo { get; set; }

        /// <summary>
        /// Agency where the <see cref="POBox"/> is
        /// </summary>
        [JsonProperty("POBoxAgency")]
        public string POBoxAgency { get; set; }

        /// <summary>
        /// The cardinal direction before the name of the <see cref="streetName"/>
        /// </summary>
        [JsonProperty("preDirection")]
        public string preDirection { get; set; }

        /// <summary>
        /// The cardinal direction after the name of the <see cref="streetName"/>
        /// </summary>
        [JsonProperty("postDirection")]
        public string postDirection { get; set; }

        /// <summary>
        /// The official name of the intersection street
        /// </summary>
        [JsonProperty("streetNameIntersection")]
        public string streetNameIntersection { get; set; }

        /// <summary>
        /// The type of the <see cref="streetNameIntersection"/>
        /// </summary>
        [JsonProperty("streetTypeIntersection")]
        public string streetTypeIntersection { get; set; }

        /// <summary>
        /// The cardinal direction before the name of the <see cref="streetNameIntersection"/>
        /// </summary>
        [JsonProperty("preDirectionIntersection")]
        public string preDirectionIntersection { get; set; }

        /// <summary>
        /// The cardinal direction after the name of the <see cref="streetNameIntersection"/>
        /// </summary>
        [JsonProperty("postDirectionIntersection")]
        public string postDirectionIntersection { get; set; }

        /// <summary>
        /// The number that follows the <see cref="houseNumber"/>
        /// Canada only
        /// </summary>
        [JsonProperty("civicNumberSuffix")]
        public string civicNumberSuffix { get; set; }

        /// <summary>
        /// The floor in an address, not a floor number in a unit (Brasilia only)
        /// </summary>
        [JsonProperty("floor")]
        public string floor { get; set; }

        /// <summary>
        /// The sector in an address (Brasilia only)
        /// </summary>
        [JsonProperty("sector")]
        public string sector { get; set; }

        /// <summary>
        /// The quadrant in an address (Brasilia only)
        /// </summary>
        [JsonProperty("quadrant")]
        public string quadrant { get; set; }

        /// <summary>
        /// The block in an address (Brasilia only)
        /// </summary>
        [JsonProperty("block")]
        public string block { get; set; }

        /// <summary>
        /// Lote in Brazilian address
        /// </summary>
        [JsonProperty("lote")]
        public string lote { get; set; }

        /// <summary>
        /// The country name
        /// </summary>
        [JsonProperty("country")]
        public string country { get; set; }

        /// <summary>
        /// The <see cref="country"/> code
        /// </summary>
        [JsonProperty("countrycode")]
        public string countryCode { get; set; }

        /// <summary>
        /// Ward in Japanese Address
        /// </summary>
        [JsonProperty("ward")]
        public string ward { get; set; }
        
        /// <summary>
        /// Full formatted address
        /// </summary>
        [JsonProperty("formatedFull")]
        public string formattedFull { get; set; }

        #region Overrides
        /// <summary>
        /// Compare two <see cref="Address"/>  objects
        /// </summary>
        /// <param name="obj"><see cref="Address"/> to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            Address addr = (Address)obj;
            return (id == addr.id);
        }
        
        /// <summary>
        /// Get <see cref="Address"/> Hash Code
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
        /// Get <see cref="Address"/> as a string 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
