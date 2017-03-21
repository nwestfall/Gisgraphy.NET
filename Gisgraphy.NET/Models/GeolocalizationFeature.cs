using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gisgraphy.NET.Models
{
    public class GeolocalizationFeature : Object
    {
        /// <summary>
        /// The distance beetween the point and the gisFeature in meters
        /// </summary>
        [JsonProperty("distance")]
        public double? distance { get; set; }

        /// <summary>
        /// The name of the feature
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }

        /// <summary>
        /// The internal code for the administrative division of level 1
        /// </summary>
        [JsonProperty("adm1Code")]
        public string adm1Code { get; set; }

        /// <summary>
        /// The internal code for the administrative division of level 2
        /// </summary>
        [JsonProperty("adm2Code")]
        public string adm2Code { get; set; }

        /// <summary>
        /// The internal code for the administrative division of level 3
        /// </summary>
        [JsonProperty("adm3Code")]
        public string adm3Code { get; set; }

        /// <summary>
        /// The internal code for the administrative division of level 4
        /// </summary>
        [JsonProperty("adm4Code")]
        public string adm4Code { get; set; }

        /// <summary>
        /// The internal code for the administrative division of level 5
        /// </summary>
        [JsonProperty("adm5Code")]
        public string adm5Code { get; set; }

        /// <summary>
        /// The name of the administrative division of level 1
        /// </summary>
        [JsonProperty("adm1Name")]
        public string adm1Name { get; set; }

        /// <summary>
        /// The name of the administrative division of level 2
        /// </summary>
        [JsonProperty("adm2Name")]
        public string adm2Name { get; set; }

        /// <summary>
        /// The name of the administrative division of level 3
        /// </summary>
        [JsonProperty("adm3Name")]
        public string adm3Name { get; set; }

        /// <summary>
        /// The name of the administrative division of level 4
        /// </summary>
        [JsonProperty("adm4Name")]
        public string adm4Name { get; set; }

        /// <summary>
        /// The name of the administrative division of level 5
        /// </summary>
        [JsonProperty("adm5Name")]
        public string adm5Name { get; set; }

        /// <summary>
        /// The ASCII name of the feature
        /// </summary>
        [JsonProperty("asciiName")]
        public string asciiName { get; set; }

        /// <summary>
        /// The ISO 3166 country code
        /// </summary>
        [JsonProperty("countryCode")]
        public string countryCode { get; set; }

        /// <summary>
        /// The elevation in meters
        /// </summary>
        [JsonProperty("elevation")]
        public int? elevation { get; set; }

        /// <summary>
        /// The feature class
        /// </summary>
        [JsonProperty("featureClass")]
        public string featureClass { get; set; }

        /// <summary>
        /// The feature code
        /// </summary>
        [JsonProperty("featureCode")]
        public string featureCode { get; set; }

        /// <summary>
        /// A unique id that identify the feature
        /// </summary>
        [JsonProperty("featureId")]
        public int? featureId { get; set; }

        /// <summary>
        /// Average elevation of 30'x30' (ca 900mx900m) area in meters
        /// </summary>
        [JsonProperty("gtopo30")]
        public int? gtopo30 { get; set; }

        /// <summary>
        /// How many people live in this feature
        /// </summary>
        [JsonProperty("population")]
        public int? population { get; set; }

        /// <summary>
        /// The time zone
        /// </summary>
        [JsonProperty("timezone")]
        public string timezone { get; set; }

        /// <summary>
        /// The latitude (north-south)
        /// </summary>
        [JsonProperty("lat")]
        public double? latitude { get; set; }

        /// <summary>
        /// The longitude (east-west)
        /// </summary>
        [JsonProperty("lng")]
        public double? longitude { get; set; }

        /// <summary>
        /// The type of feature
        /// </summary>
        [JsonProperty("placeType")]
        public string placeType { get; set; }

        /// <summary>
        /// Whether the street is one-way or not
        /// </summary>
        [JsonProperty("oneWay")]
        public bool? oneWay { get; set; }

        /// <summary>
        /// The type of the street 
        /// </summary>
        [JsonProperty("streetType")]
        public string streetType { get; set; }

        /// <summary>
        /// The id of the openstreetmap element
        /// </summary>
        [JsonProperty("openstreetmapId")]
        public int? openStreetMapId { get; set; }
        
        /// <summary>
        /// The length of the street
        /// </summary>
        [JsonProperty("length")]
        public double? length { get; set; }

        /// <summary>
        /// The zipcodes (only for city and city subdivision), one node by zipcode,
        /// </summary>
        [JsonProperty("zipCodes")]
        public IEnumerable<string> zipCodes { get; set; }

        /// <summary>
        /// The URL to get the location on Google maps
        /// </summary>
        [JsonProperty("google_map_url")]
        public string googleMapUrl { get; set; }

        /// <summary>
        /// The URL to get the location on Yahoo maps
        /// </summary>
        [JsonProperty("yahoo_map_url")]
        public string yahooMapUrl { get; set; }

        /// <summary>
        /// The URL to get the location on OpenStreet Map
        /// </summary>
        [JsonProperty("openstreetmap_map_url")]
        public string openStreetMapUrl { get; set; }

        /// <summary>
        /// The relative URL to get the country flag image
        /// </summary>
        [JsonProperty("country_flag_url")]
        public string countryFlagUrl { get; set; }

        /// <summary>
        /// The level of the administrative division (1-5)
        /// </summary>
        [JsonProperty("level")]
        public int? level { get; set; }

        /// <summary>
        /// The area of the feature 
        /// </summary>
        [JsonProperty("area")]
        public double? area { get; set; }

        /// <summary>
        /// Top-level domain name, (last part of an Internet domain name) of the country
        /// </summary>
        [JsonProperty("tld")]
        public string topLevelDomain { get; set; }

        /// <summary>
        /// The Capital of the country
        /// </summary>
        [JsonProperty("capitalName")]
        public string capitalName { get; set; }

        /// <summary>
        /// The continent the country belongs to
        /// </summary>
        [JsonProperty("continent")]
        public string continent { get; set; }

        /// <summary>
        /// The regexp that all zipcode/postalcode of the country matches
        /// </summary>
        [JsonProperty("postalCodeRegex")]
        public string postalCodeRegex { get; set; }

        /// <summary>
        /// The Currency code (ISO_4217) of the country
        /// </summary>
        [JsonProperty("currencyCode")]
        public string currencyCode { get; set; }

        /// <summary>
        ///  The Currency name of the country
        /// </summary>
        [JsonProperty("currencyName")]
        public string currencyName { get; set; }

        /// <summary>
        /// The fips Code of the country when no code are available
        /// </summary>
        [JsonProperty("equivalentFipsCode")]
        public string equivalentFipsCode { get; set; }

        /// <summary>
        /// The fips Code of the country
        /// </summary>
        [JsonProperty("fipsCode")]
        public string fipsCode { get; set; }

        /// <summary>
        /// The iso 3166 Alpha 2 code of the country
        /// </summary>
        [JsonProperty("iso3166Alpha2Code")]
        public string iso3166Alpha2Code { get; set; }

        /// <summary>
        /// The iso 3166 Alpha 3 code of the country
        /// </summary>
        [JsonProperty("iso3166Alpha3Code")]
        public string iso3166Alpha3Code { get; set; }

        /// <summary>
        /// The iso 3166 numeric code of the country
        /// </summary>
        [JsonProperty("iso3166NumericCode")]
        public string iso3166NumericCode { get; set; }

        /// <summary>
        /// The phone prefix of the country
        /// </summary>
        [JsonProperty("phonePrefix")]
        public string phonePrefix { get; set; }

        /// <summary>
        /// The mask that all postal code of the country matches
        /// </summary>
        [JsonProperty("postalCodeMask")]
        public string postalCodeMask { get; set; }

        /// <summary>
        /// Information of the city where the street / POI is (depends on openstreetmap 'is_in' field), the city in general (only for placetype street)
        /// </summary>
        [JsonProperty("isIn")]
        public string isIn { get; set; }

        /// <summary>
        /// Information of the place where the street / POI is (quarter, common place). Generally a place at a lower level than city
        /// </summary>
        [JsonProperty("isInPlace")]
        public string isInPlace { get; set; }

        /// <summary>
        /// Information of the administration division where the street / POI is
        /// </summary>
        [JsonProperty("isInAdm")]
        public string isInAdm { get; set; }

        /// <summary>
        /// Information of the zipcode where the street / POI is
        /// </summary>
        [JsonProperty("isInZip")]
        public string isInZip { get; set; }

        /// <summary>
        /// Informations on category of OpenStreetMap POIs
        /// </summary>
        [JsonProperty("amenity")]
        public string amenity { get; set; }

        /// <summary>
        /// NOT USED YET
        /// </summary>
        [JsonProperty("fullyQualifiedAddress")]
        public string fullyQualifiedAddress { get; set; }

        #region Overrides
        /// <summary>
        /// Compare two <see cref="GeolocalizationFeature"/>   objects
        /// </summary>
        /// <param name="obj"><see cref="GeolocalizationFeature"/> to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            GeolocalizationFeature gisFeat = (GeolocalizationFeature)obj;
            return (featureId == gisFeat.featureId);
        }

        /// <summary>
        /// Get <see cref="GeolocalizationFeature"/> Hash Code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (featureId.HasValue)
                return featureId.Value;
            else
                return base.GetHashCode();
        }

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
