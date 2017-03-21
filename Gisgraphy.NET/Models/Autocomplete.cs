using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gisgraphy.NET.Models
{
    public class Autocomplete : Object
    {
        /// <summary>
        /// The name of the feature
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }

        /// <summary>
        /// A number that indicates the relevance of the result
        /// </summary>
        [JsonProperty("score")]
        public double? score { get; set; }

        /// <summary>
        /// The alternate names of the feature that without specific language code
        /// </summary>
        [JsonProperty("name_alternates")]
        public IEnumerable<string> nameAlternates { get; set; }

        /// <summary>
        /// The feature class
        /// </summary>
        [JsonProperty("feature_class")]
        public string featureClass { get; set; }

        /// <summary>
        /// The feature code
        /// </summary>
        [JsonProperty("feature_code")]
        public string featureCode { get; set; }

        /// <summary>
        /// A unique id that identify the feature
        /// </summary>
        [JsonProperty("feature_id")]
        public int? featureId { get; set; }

        /// <summary>
        /// The ASCII name of the feature
        /// </summary>
        [JsonProperty("name_ascii")]
        public string asciiName { get; set; }

        /// <summary>
        /// The elevation in meters
        /// </summary>
        [JsonProperty("elevation")]
        public int? elevation { get; set; }

        /// <summary>
        /// Average elevation of 30'x30' (ca 900mx900m) area in meters
        /// </summary>
        [JsonProperty("gtopo30")]
        public int? gtopo30 { get; set; }

        /// <summary>
        /// The time zone
        /// </summary>
        [JsonProperty("timezone")]
        public string timezone { get; set; }

        /// <summary>
        /// A name of the form : (adm1Name et adm2Name are printed) Paris, Département de Ville-De-Paris, Ile-De-France, (FR)
        /// </summary>
        [JsonProperty("fully_qualified_name")]
        public string fullyQualifiedName { get; set; }

        /// <summary>
        /// The type of feature
        /// </summary>
        [JsonProperty("placetype")]
        public string placeType { get; set; }

        /// <summary>
        /// How many people live in this feature
        /// </summary>
        [JsonProperty("population")]
        public int? population { get; set; }

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
        /// The internal code for the administrative division of level 1
        /// </summary>
        [JsonProperty("adm1_code")]
        public string adm1Code { get; set; }

        /// <summary>
        /// The internal code for the administrative division of level 2
        /// </summary>
        [JsonProperty("adm2_code")]
        public string adm2Code { get; set; }

        /// <summary>
        /// The internal code for the administrative division of level 3
        /// </summary>
        [JsonProperty("adm3_code")]
        public string adm3Code { get; set; }

        /// <summary>
        /// The internal code for the administrative division of level 4
        /// </summary>
        [JsonProperty("adm4_code")]
        public string adm4Code { get; set; }

        /// <summary>
        /// The continent the country belongs to
        /// </summary>
        [JsonProperty("continent")]
        public string continent { get; set; }

        /// <summary>
        /// The Currency code (ISO_4217) of the country
        /// </summary>
        [JsonProperty("currency_code")]
        public string currencyCode { get; set; }

        /// <summary>
        ///  The Currency name of the country
        /// </summary>
        [JsonProperty("currency_name")]
        public string currencyName { get; set; }

        /// <summary>
        /// The fips Code of the country
        /// </summary>
        [JsonProperty("fips_code")]
        public string fipsCode { get; set; }

        /// <summary>
        /// The iso 3166 Alpha 2 code of the country
        /// </summary>
        [JsonProperty("isoalpha2_country_code")]
        public string iso3166Alpha2Code { get; set; }

        /// <summary>
        /// The iso 3166 Alpha 3 code of the country
        /// </summary>
        [JsonProperty("isoalpha3_country_code")]
        public string iso3166Alpha3Code { get; set; }

        /// <summary>
        /// The mask that all postal code of the country matches
        /// </summary>
        [JsonProperty("postal_code_mask")]
        public string postalCodeMask { get; set; }

        /// <summary>
        /// The regexp that all zipcode/postalcode of the country matches
        /// </summary>
        [JsonProperty("postal_code_regex")]
        public string postalCodeRegex { get; set; }

        /// <summary>
        /// The phone prefix of the country
        /// </summary>
        [JsonProperty("phone_prefix")]
        public string phonePrefix { get; set; }

        /// <summary>
        /// List of languages spoken in the country (only for country placetype)
        /// </summary>
        [JsonProperty("spoken_languages")]
        public IEnumerable<string> spokenLanguages { get; set; }

        /// <summary>
        /// Top-level domain name, (last part of an Internet domain name) of the country
        /// </summary>
        [JsonProperty("tld")]
        public string topLevelDomain { get; set; }

        /// <summary>
        /// The Capital of the country
        /// </summary>
        [JsonProperty("capital_name")]
        public string capitalName { get; set; }

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
        /// The name of the administrative division of level 1
        /// </summary>
        [JsonProperty("adm1_name")]
        public string adm1Name { get; set; }

        /// <summary>
        /// The alternate names of the administrative division of level 1 without specific language code
        /// </summary>
        [JsonProperty("adm1_names_alternate")]
        public IEnumerable<string> adm1NameAlternate { get; set; }

        /// <summary>
        /// The name of the administrative division of level 2
        /// </summary>
        [JsonProperty("adm2_name")]
        public string adm2Name { get; set; }

        /// <summary>
        /// The alternate names of the administrative division of level 2 without specific language code
        /// </summary>
        [JsonProperty("adm2_names_alternate")]
        public IEnumerable<string> adm2NameAlternate { get; set; }

        /// <summary>
        /// The name of the administrative division of level 3
        /// </summary>
        [JsonProperty("adm3_name")]
        public string adm3Name { get; set; }

        /// <summary>
        /// The alternate names of the administrative division of level 3 without specific language code
        /// </summary>
        [JsonProperty("adm3_names_alternate")]
        public IEnumerable<string> adm3NameAlternate { get; set; }

        /// <summary>
        /// The name of the administrative division of level 4
        /// </summary>
        [JsonProperty("adm4_name")]
        public string adm4Name { get; set; }

        /// <summary>
        /// The alternate names of the administrative division of level 4 without specific language code
        /// </summary>
        [JsonProperty("adm4_names_alternate")]
        public IEnumerable<string> adm4NameAlternate { get; set; }

        /// <summary>
        /// The zipcodes (only for city and city subdivision), one node by zipcode,
        /// </summary>
        [JsonProperty("zipcodes")]
        public IEnumerable<string> zipCodes { get; set; }

        /// <summary>
        /// The ISO 3166 country code
        /// </summary>
        [JsonProperty("country_code")]
        public string countryCode { get; set; }

        /// <summary>
        /// The name of the country the feature belongs to
        /// </summary>
        [JsonProperty("country_name")]
        public string countryName { get; set; }

        /// <summary>
        /// The alternate names of the country without specific language code
        /// </summary>
        [JsonProperty("country_name_alternate")]
        public IEnumerable<string> countryNamesAlternate { get; set; }

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
        /// Whether the street is one-way or not
        /// </summary>
        [JsonProperty("one_way")]
        public bool? oneWay { get; set; }

        /// <summary>
        /// The length of the street
        /// </summary>
        [JsonProperty("length")]
        public double? length { get; set; }

        /// <summary>
        /// The type of the street 
        /// </summary>
        [JsonProperty("street_type")]
        public string streetType { get; set; }

        /// <summary>
        /// The id of the openstreetmap element
        /// </summary>
        [JsonProperty("openstreetmap_id")]
        public int? openStreetMapId { get; set; }

        /// <summary>
        /// Information of the city where the street / POI is (depends on openstreetmap 'is_in' field), the city in general (only for placetype street)
        /// </summary>
        [JsonProperty("is_in")]
        public string isIn { get; set; }

        /// <summary>
        /// Information of the place where the street / POI is (quarter, common place). Generally a place at a lower level than city
        /// </summary>
        [JsonProperty("is_in_place")]
        public string isInPlace { get; set; }

        /// <summary>
        /// Information of the administration division where the street / POI is
        /// </summary>
        [JsonProperty("is_in_adm")]
        public string isInAdm { get; set; }

        /// <summary>
        /// Information of the zipcode where the street / POI is
        /// </summary>
        [JsonProperty("is_in_zip")]
        public IEnumerable<string> isInZip { get; set; }

        /// <summary>
        /// NOT USED YET
        /// </summary>
        [JsonProperty("fully_qualified_address")]
        public string fullyQualifiedAddress { get; set; }

        /// <summary>
        /// A list of all the house numbers sorted and their coordinates (only for placetype street)
        /// </summary>
        [JsonProperty("house_numbers")]
        public IEnumerable<HouseNumber> houseAddress { get; set; }

        /// <summary>
        /// If the place is a municipality. it is usefull for geonames feature that don't have concept of 'city' but a populated place (that can be a quarter),
        /// </summary>
        [JsonProperty("mucipality")]
        public bool? municipality { get; set; }

        /// <summary>
        /// Informations on category of OpenStreetMap POIs
        /// </summary>
        [JsonProperty("amenity")]
        public string amenity { get; set; }

        #region Overrides
        /// <summary>
        /// Compare two <see cref="Autocomplete"/>  objects
        /// </summary>
        /// <param name="obj"><see cref="Autocomplete"/> to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            Autocomplete auto = (Autocomplete)obj;
            return (featureId == auto.featureId);
        }

        /// <summary>
        /// Get <see cref="Autocomplete"/> Hash Code
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
        /// Get <see cref="Autocomplete"/> as a string 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }
}
