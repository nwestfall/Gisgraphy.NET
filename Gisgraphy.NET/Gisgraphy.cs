using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Gisgraphy.NET.Models;

namespace Gisgraphy.NET
{
    /// <summary>
    /// Main class to handle Gisgraphy connections and results
    /// </summary>
    public class Gisgraphy
    {
        #region Endpoints
        private const string GEOCODING = "/geocoding/geocode";
        private const string REVERSE_GEOCODING = "/reversegeocoding/reversegeocode";
        private const string STREET_SERVICE = "/street/find";
        private const string GEO_LOCALISATION = "/geoloc/search";
        private const string FULLTEXT_AUTOCOMPLETE = "/fulltext/search";
        private const string ADDRESS_PARSER = "/addressparser/parse";
        #endregion

        #region Properties
        /// <summary>
        /// Get/set the gisgrpahy server URL
        /// </summary>
        public string serverUrl { get; set; }

        /// <summary>
        /// Get/set the API Key for the <see cref="serverUrl"/> 
        /// </summary>
        public string apiKey { get; set; }
        #endregion

        #region ENUMS
        /// <summary>
        /// Determines the output verbosity. 4 styles are available
        /// </summary>
        public enum Styles
        {
            SHORT,
            MEDIUM,
            LONG,
            FULL
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Basic ctor with properties
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <param name="apiKey"></param>
        public Gisgraphy(string serverUrl, string apiKey)
        {
            this.serverUrl = serverUrl;
            this.apiKey = apiKey;
        }

        /// <summary>
        /// Basic ctor
        /// </summary>
        public Gisgraphy() { }
        #endregion

        #region HTTP
        private HttpClient DefaultClient
        {
            get
            {
                if (string.IsNullOrEmpty(serverUrl))
                    throw new ArgumentNullException("Server URL is not set");

                Uri baseServerUrl;
                if (!Uri.TryCreate(serverUrl, UriKind.Absolute, out baseServerUrl))
                    throw new InvalidCastException("Server URL is not a valid URI");

                HttpClient client = new HttpClient();
                client.BaseAddress = baseServerUrl;
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                return client;
            }
        }

        private Dictionary<string, string> DefaultParams
        {
            get
            {
                Dictionary<string, string> param = new Dictionary<string, string>();
                param.Add("format", "JSON");
                param.Add("indent", "false");
                if (!string.IsNullOrEmpty(apiKey))
                    param.Add("api_key", apiKey);

                return param;
            }
        }
        #endregion

        #region Geocode Services
        /// <summary>
        /// Geocode an address returning <see cref="AddressResult"/>  
        /// </summary>
        /// <param name="address">A postal address, structured or not, a street, a city, a postal code, a country, or a combination (required)</param>
        /// <param name="country">The country where the place/address is. It is used to determine the postal address format and to improve performance.
        /// It will probably be optional in next version to ease the usability.The value must be the ISO 3166 Alpha 2 code of the country. (optional)</param>
        /// <param name="postal">Whether the given address is a postal address. default to false. In other words, if the address follow the specification
        /// or if it is a well-formed address as it was written on an envelope. This parameter will enable the parsing of the address by the address parser
        /// before geocoding, this way, the relevance will be better because because if parsing is successful, we will know the meaning of each word. Note 
        /// that you can also specify each field if you already know them. (optional)</param>
        /// <returns><see cref="AddressResult"/> </returns>
        public AddressResult geocode(string address, string country = null, string postal = null)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException("Address is a required parameter");
            if (!string.IsNullOrEmpty(country) && country.Length != 2)
                throw new ArgumentOutOfRangeException("Country needs to be the ISO 3166 Alpha 2 code");

            AddressResult result = new AddressResult();
            using (var client = DefaultClient)
            {
                Dictionary<string, string> param = DefaultParams;
                param.Add("address", address);
                if (!string.IsNullOrEmpty(country))
                    param.Add("country", country);
                if (!string.IsNullOrEmpty(postal))
                    param.Add("postal", postal);

                var response = client.GetAsync(string.Format("{0}{1}", Gisgraphy.GEOCODING, param.ToQueryString())).Result;
                if(!response.IsSuccessStatusCode)
                {
                    result.resultsFound = 0;
                    result.message = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    try
                    {
                        result = JsonConvert.DeserializeObject<AddressResult>(response.Content.ReadAsStringAsync().Result);
                    }
                    catch(JsonSerializationException ex)
                    {
                        result.resultsFound = 0;
                        result.message = string.Format("{0}\n{1}", ex.Message, ex.StackTrace);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Geocode an address returning <see cref="AddressResult"/> async
        /// </summary>
        /// <param name="address">A postal address, structured or not, a street, a city, a postal code, a country, or a combination (required)</param>
        /// <param name="country">The country where the place/address is. It is used to determine the postal address format and to improve performance.
        /// It will probably be optional in next version to ease the usability.The value must be the ISO 3166 Alpha 2 code of the country. (optional)</param>
        /// <param name="postal">Whether the given address is a postal address. default to false. In other words, if the address follow the specification
        /// or if it is a well-formed address as it was written on an envelope. This parameter will enable the parsing of the address by the address parser
        /// before geocoding, this way, the relevance will be better because because if parsing is successful, we will know the meaning of each word. Note 
        /// that you can also specify each field if you already know them. (optional)</param>
        /// <returns><see cref="AddressResult"/> </returns>
        public async Task<AddressResult> geocodeAsync(string address, string country = null, string postal = null)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException("Address is a required parameter");
            if (!string.IsNullOrEmpty(country) && country.Length != 2)
                throw new ArgumentOutOfRangeException("Country needs to be the ISO 3166 Alpha 2 code");

            AddressResult result = new AddressResult();
            using (var client = DefaultClient)
            {
                Dictionary<string, string> param = DefaultParams;
                param.Add("address", address);
                if (!string.IsNullOrEmpty(country))
                    param.Add("country", country);
                if (!string.IsNullOrEmpty(postal))
                    param.Add("postal", postal);

                var response = await client.GetAsync(string.Format("{0}{1}", Gisgraphy.GEOCODING, param.ToQueryString()));
                if (!response.IsSuccessStatusCode)
                {
                    result.resultsFound = 0;
                    result.message = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    try
                    {
                        result = JsonConvert.DeserializeObject<AddressResult>(await response.Content.ReadAsStringAsync());
                    }
                    catch (JsonSerializationException ex)
                    {
                        result.resultsFound = 0;
                        result.message = string.Format("{0}\n{1}", ex.Message, ex.StackTrace);
                    }
                }
            }

            return result;
        }
        #endregion

        #region Reverse Geocode Services
        /// <summary>
        /// Reverse geocode a latitude/longitude position returning <see cref="AddressResult"/> 
        /// </summary>
        /// <param name="lat">The latitude(north-south) for the location point to search around.The value is a floating number, between -90 and +90. It uses GPS coordinates.</param>
        /// <param name="lng">The longitude (east-West) for the location point to search around. The value is a floating number between -180 and +180. It uses GPS coordinates.</param>
        /// <returns><see cref="AddressResult"/> </returns>
        public AddressResult reverseGeocode(double lat, double lng)
        {
            if (lat > 90 || lat < -90)
                throw new ArgumentOutOfRangeException("Latitude must be between -90 an 90");
            if (lng > 180 || lng < -180)
                throw new ArgumentOutOfRangeException("Longitude must be between -180 and 180");

            AddressResult result = new AddressResult();
            using (var client = DefaultClient)
            {
                Dictionary<string, string> param = DefaultParams;
                param.Add("lat", lat.ToString());
                param.Add("lng", lng.ToString());

                var response = client.GetAsync(string.Format("{0}{1}", Gisgraphy.REVERSE_GEOCODING, param.ToQueryString())).Result;
                if (!response.IsSuccessStatusCode)
                {
                    result.resultsFound = 0;
                    result.message = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    try
                    {
                        result = JsonConvert.DeserializeObject<AddressResult>(response.Content.ReadAsStringAsync().Result);
                    }
                    catch (JsonSerializationException ex)
                    {
                        result.resultsFound = 0;
                        result.message = string.Format("{0}\n{1}", ex.Message, ex.StackTrace);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Reverse geocode a latitude/longitude position returning <see cref="AddressResult"/> async
        /// </summary>
        /// <param name="lat">The latitude(north-south) for the location point to search around.The value is a floating number, between -90 and +90. It uses GPS coordinates.</param>
        /// <param name="lng">The longitude (east-West) for the location point to search around. The value is a floating number between -180 and +180. It uses GPS coordinates.</param>
        /// <returns><see cref="AddressResult"/> </returns>
        public async Task<AddressResult> reverseGeocodeAsync(double lat, double lng)
        {
            if (lat > 90 || lat < -90)
                throw new ArgumentOutOfRangeException("Latitude must be between -90 an 90");
            if (lng > 180 || lng < -180)
                throw new ArgumentOutOfRangeException("Longitude must be between -180 and 180");

            AddressResult result = new AddressResult();
            using (var client = DefaultClient)
            {
                Dictionary<string, string> param = DefaultParams;
                param.Add("lat", lat.ToString());
                param.Add("lng", lng.ToString());

                var response = await client.GetAsync(string.Format("{0}{1}", Gisgraphy.REVERSE_GEOCODING, param.ToQueryString()));
                if (!response.IsSuccessStatusCode)
                {
                    result.resultsFound = 0;
                    result.message = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    try
                    {
                        result = JsonConvert.DeserializeObject<AddressResult>(await response.Content.ReadAsStringAsync());
                    }
                    catch (JsonSerializationException ex)
                    {
                        result.resultsFound = 0;
                        result.message = string.Format("{0}\n{1}", ex.Message, ex.StackTrace);
                    }
                }
            }

            return result;
        }
        #endregion

        #region Street Service
        /// <summary>
        /// The street service allows you to search for street by GPS position. 
        /// You can : Specify GPS position, Give the beginning or a part of the name of the street (useful for autocompletion), 
        /// Limit search to a specific type (e.g : Pedestrian, highway, residential, ... 25 types available), 
        /// Limit search to a specified radius, Limit search to one way streets.
        /// </summary>
        /// <param name="lat">The latitude(north-south) for the location point to search around.The value is a floating number, between -90 and +90. It uses GPS coordinates.</param>
        /// <param name="lng">The longitude (east-West) for the location point to search around. The value is a floating number between -180 and +180. It uses GPS coordinates.</param>
        /// <param name="radius">Distance from the location point in meters we'd like to search around. The value is a number > 0 if it is not specify or incorrect.</param>
        /// <param name="oneWay">Whether the street should be a oneWay street (optional) : limit the search to the street that are one way street. If you use a checkbox in a form to indent the results, the value will be 'on' or 'off', so to simplify the use : the value for the web service can be 'true' or 'on'</param>
        /// <param name="distance">Whether(or not) we want the distance field to be output.This option is useful to improve the performance if we don't care about the distance (e.g : we search for name). Of course, the results won't be sorted by distance.If you use a checkbox in a form to indent the results, the value will be 'on' or 'off', so to simplify the use : the value for the web service can be 'true' or 'on'</param>
        /// <param name="streetType">Filter search with a stret type</param>
        /// <returns><see cref="StreetDistanceResult"/> </returns>
        public StreetDistanceResult findStreet(double lat, double lng, double radius = 1000, bool oneWay = false, bool distance = true, string streetType = null)
        {
            if (lat > 90 || lat < -90)
                throw new ArgumentOutOfRangeException("Latitude must be between -90 an 90");
            if (lng > 180 || lng < -180)
                throw new ArgumentOutOfRangeException("Longitude must be between -180 and 180");
            if (radius <= 0)
                throw new ArgumentOutOfRangeException("Radius must be greater than 0");

            StreetDistanceResult result = new StreetDistanceResult();
            using (var client = DefaultClient)
            {
                Dictionary<string, string> param = DefaultParams;
                param.Add("lat", lat.ToString());
                param.Add("lng", lng.ToString());
                param.Add("radius", radius.ToString());
                param.Add("oneway", oneWay.ToString().ToLower());
                param.Add("distance", distance.ToString().ToLower());
                if (!string.IsNullOrEmpty(streetType))
                    param.Add("streettype", streetType);

                var response = client.GetAsync(string.Format("{0}{1}", Gisgraphy.STREET_SERVICE, param.ToQueryString())).Result;
                if (!response.IsSuccessStatusCode)
                {
                    result.resultsFound = 0;
                    result.error = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    try
                    {
                        result = JsonConvert.DeserializeObject<StreetDistanceResult>(response.Content.ReadAsStringAsync().Result);
                    }
                    catch (JsonSerializationException ex)
                    {
                        result.resultsFound = 0;
                        result.error = string.Format("{0}\n{1}", ex.Message, ex.StackTrace);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// The street service allows you to search for street by GPS position async.
        /// You can : Specify GPS position, Give the beginning or a part of the name of the street (useful for autocompletion), 
        /// Limit search to a specific type (e.g : Pedestrian, highway, residential, ... 25 types available), 
        /// Limit search to a specified radius, Limit search to one way streets.
        /// </summary>
        /// <param name="lat">The latitude(north-south) for the location point to search around.The value is a floating number, between -90 and +90. It uses GPS coordinates.</param>
        /// <param name="lng">The longitude (east-West) for the location point to search around. The value is a floating number between -180 and +180. It uses GPS coordinates.</param>
        /// <param name="radius">Distance from the location point in meters we'd like to search around. The value is a number > 0 if it is not specify or incorrect.</param>
        /// <param name="oneWay">Whether the street should be a oneWay street (optional) : limit the search to the street that are one way street. If you use a checkbox in a form to indent the results, the value will be 'on' or 'off', so to simplify the use : the value for the web service can be 'true' or 'on'</param>
        /// <param name="distance">Whether(or not) we want the distance field to be output.This option is useful to improve the performance if we don't care about the distance (e.g : we search for name). Of course, the results won't be sorted by distance.If you use a checkbox in a form to indent the results, the value will be 'on' or 'off', so to simplify the use : the value for the web service can be 'true' or 'on'</param>
        /// <param name="streetType">Filter search with a stret type</param>
        /// <returns><see cref="StreetDistanceResult"/> </returns>
        public async Task<StreetDistanceResult> findStreetAsync(double lat, double lng, double radius = 1000, bool oneWay = false, bool distance = true, string streetType = null)
        {
            if (lat > 90 || lat < -90)
                throw new ArgumentOutOfRangeException("Latitude must be between -90 an 90");
            if (lng > 180 || lng < -180)
                throw new ArgumentOutOfRangeException("Longitude must be between -180 and 180");
            if (radius <= 0)
                throw new ArgumentOutOfRangeException("Radius must be greater than 0");

            StreetDistanceResult result = new StreetDistanceResult();
            using (var client = DefaultClient)
            {
                Dictionary<string, string> param = DefaultParams;
                param.Add("lat", lat.ToString());
                param.Add("lng", lng.ToString());
                param.Add("radius", radius.ToString());
                param.Add("oneway", oneWay.ToString().ToLower());
                param.Add("distance", distance.ToString().ToLower());
                if (!string.IsNullOrEmpty(streetType))
                    param.Add("streettype", streetType);

                var response = await client.GetAsync(string.Format("{0}{1}", Gisgraphy.STREET_SERVICE, param.ToQueryString()));
                if (!response.IsSuccessStatusCode)
                {
                    result.resultsFound = 0;
                    result.error = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    try
                    {
                        result = JsonConvert.DeserializeObject<StreetDistanceResult>(await response.Content.ReadAsStringAsync());
                    }
                    catch (JsonSerializationException ex)
                    {
                        result.resultsFound = 0;
                        result.error = string.Format("{0}\n{1}", ex.Message, ex.StackTrace);
                    }
                }
            }

            return result;
        }
        #endregion

        #region Geolocalization Services
        /// <summary>
        /// The geolocalisation service allows to search for features around a earth location
        /// </summary>
        /// <param name="lat">The latitude (north-south) for the location point to search around. The value is a floating number, between -90 and +90. It uses GPS coordinates</param>
        /// <param name="lng">The longitude (east-West) for the location point to search around. The value is a floating number between -180 and +180. It uses GPS coordinates.</param>
        /// <param name="radius">Distance from the location point in meters we'd like to search around. The value is a number > 0 if it is not specify or incorrect.</param>
        /// <param name="distance">Whether (or not) we want the distance field to be output. This option is useful to improve the performance if we don't care about the distance (e.g : we search for name). Of course, the results won't be sorted by distance. If you use a checkbox in a form to indent the results, the value will be 'on' or 'off', so to simplify the use : the value for the web service can be 'true' or 'on'</param>
        /// <param name="placeType">Filter search for a given placetype</param>
        /// <returns><see cref="GeolocalizationFeatureResult"/> </returns>
        public GeolocalizationFeatureResult geolocalization(double lat, double lng, double radius = 10000, bool distance = true, string placeType = null)
        {
            if (lat > 90 || lat < -90)
                throw new ArgumentOutOfRangeException("Latitude must be between -90 an 90");
            if (lng > 180 || lng < -180)
                throw new ArgumentOutOfRangeException("Longitude must be between -180 and 180");
            if (radius <= 0)
                throw new ArgumentOutOfRangeException("Radius must be greater than 0");

            GeolocalizationFeatureResult result = new GeolocalizationFeatureResult();
            using (var client = DefaultClient)
            {
                Dictionary<string, string> param = DefaultParams;
                param.Add("lat", lat.ToString());
                param.Add("lng", lng.ToString());
                param.Add("radius", radius.ToString());
                param.Add("distance", distance.ToString().ToLower());
                if (!string.IsNullOrEmpty(placeType))
                    param.Add("placetype", placeType);

                var response = client.GetAsync(string.Format("{0}{1}", Gisgraphy.GEO_LOCALISATION, param.ToQueryString())).Result;
                if (!response.IsSuccessStatusCode)
                {
                    result.resultsFound = 0;
                    result.error = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    try
                    {
                        result = JsonConvert.DeserializeObject<GeolocalizationFeatureResult>(response.Content.ReadAsStringAsync().Result);
                    }
                    catch (JsonSerializationException ex)
                    {
                        result.resultsFound = 0;
                        result.error = string.Format("{0}\n{1}", ex.Message, ex.StackTrace);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// The geolocalisation service allows to search for features around a earth location async
        /// </summary>
        /// <param name="lat">The latitude (north-south) for the location point to search around. The value is a floating number, between -90 and +90. It uses GPS coordinates</param>
        /// <param name="lng">The longitude (east-West) for the location point to search around. The value is a floating number between -180 and +180. It uses GPS coordinates.</param>
        /// <param name="radius">Distance from the location point in meters we'd like to search around. The value is a number > 0 if it is not specify or incorrect.</param>
        /// <param name="distance">Whether (or not) we want the distance field to be output. This option is useful to improve the performance if we don't care about the distance (e.g : we search for name). Of course, the results won't be sorted by distance. If you use a checkbox in a form to indent the results, the value will be 'on' or 'off', so to simplify the use : the value for the web service can be 'true' or 'on'</param>
        /// <param name="placeType">Filter search for a given placetype</param>
        /// <returns><see cref="GeolocalizationFeatureResult"/> </returns>
        public async Task<GeolocalizationFeatureResult> geolocalizationAsync(double lat, double lng, double radius = 10000, bool distance = true, string placeType = null)
        {
            if (lat > 90 || lat < -90)
                throw new ArgumentOutOfRangeException("Latitude must be between -90 an 90");
            if (lng > 180 || lng < -180)
                throw new ArgumentOutOfRangeException("Longitude must be between -180 and 180");
            if (radius <= 0)
                throw new ArgumentOutOfRangeException("Radius must be greater than 0");

            GeolocalizationFeatureResult result = new GeolocalizationFeatureResult();
            using (var client = DefaultClient)
            {
                Dictionary<string, string> param = DefaultParams;
                param.Add("lat", lat.ToString());
                param.Add("lng", lng.ToString());
                param.Add("radius", radius.ToString());
                param.Add("distance", distance.ToString().ToLower());
                if (!string.IsNullOrEmpty(placeType))
                    param.Add("placetype", placeType);

                var response = await client.GetAsync(string.Format("{0}{1}", Gisgraphy.GEO_LOCALISATION, param.ToQueryString()));
                if (!response.IsSuccessStatusCode)
                {
                    result.resultsFound = 0;
                    result.error = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    try
                    {
                        result = JsonConvert.DeserializeObject<GeolocalizationFeatureResult>(await response.Content.ReadAsStringAsync());
                    }
                    catch (JsonSerializationException ex)
                    {
                        result.resultsFound = 0;
                        result.error = string.Format("{0}\n{1}", ex.Message, ex.StackTrace);
                    }
                }
            }

            return result;
        }
        #endregion

        #region Autocomplete Services
        /// <summary>
        /// Implementation Notes - The full text service allows you to search for features / places / street 
        /// and do autocompletion.you can : Specify one or more words search on part of the name(auto completion / suggestion) 
        /// Search for text or zip code Specify a GPS restriction(promote nearest, not sorted but has an impact on the score),
        /// Limit the results to a specific Language, Country, place type, Paginate the results, Specify the output verbosity, 
        /// Tells if you want the output to be indented, Tells that all words are required or not, The search is case 
        /// insensitive, use synonyms(Saint/st, ..), separator characters stripping, ...
        /// </summary>
        /// <param name="text">The searched text : The text for the query can be a zip code, a string or one or more strings</param>
        /// <param name="allwordsrequired">Whether the fulltext engine should considers all the words specified as required. Defaults to false (since v 4.0). possible values are true|false (or 'on' when used with the rest service)</param>
        /// <param name="spellchecking">The spellchecking (optional) : whether some suggestions should be provided if no results are found</param>
        /// <param name="lat">The latitude (north-south) for the location point to search around. The value is a floating number, between -90 and +90. It uses GPS coordinates</param>
        /// <param name="lng">The longitude (east-West) for the location point to search around. The value is a floating number between -180 and +180. It uses GPS coordinates.</param>
        /// <param name="radius">Distance from the location point in meters we'd like to search around. The value is a number > 0 if it is not specify or incorrect.</param>
        /// <param name="suggest">If this parameter is set then it will search in part of the names of the street, place,.... It allow you to do auto completion auto suggestion. See the Gisgraphy leaflet plugin for more details. The JSON format will be forced if this parameter is true. See auto completion / suggestions engine for more details</param>
        /// <param name="style" cref="Styles">The output style verbosity (optional) : Determines the output verbosity. 4 styles are available</param>
        /// <param name="country">Limit the search to the specified ISO 3166 country code. Default : search in all countries</param>
        /// <param name="lang">The language code (optional) : The iso 639 Alpha2 or alpha3 Language Code. Some properties such as the AlternateName AdmNames and countryname belong to a certain language code. The language parameter can limit the output of those fields to a certain language (it only apply when style parameter='style') : If the language code does not exists or is not specified, properties with all the languages are retrieved If it exists, the properties with the specified language code, are retrieved</param>
        /// <returns><see cref="AutocompleteResult"/> </returns>
        public AutocompleteResult autocomplete(string text, bool allwordsrequired = false, string spellchecking = null, double? lat = null, double? lng = null, double radius = 10000, bool suggest = false, Styles style = Styles.MEDIUM, string country = null, string lang = null)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("Search text cannot be blank");
            if (lat.HasValue && (lat.Value > 90 || lat.Value < -90))
                throw new ArgumentOutOfRangeException("Latitude must be between -90 an 90");
            if (lng.HasValue && (lng.Value > 180 || lng.Value < -180))
                throw new ArgumentOutOfRangeException("Longitude must be between -180 and 180");
            if ((lat.HasValue && !lng.HasValue) || (!lat.HasValue && lng.HasValue))
                throw new ArgumentNullException("If using latitude and longitude, both values must be provided");
            if (radius <= 0)
                throw new ArgumentOutOfRangeException("Radius must be greater than 0");
            if (!string.IsNullOrEmpty(country) && country.Length != 2)
                throw new ArgumentOutOfRangeException("Country needs to be the ISO 3166 Alpha 2 code");
            if (!string.IsNullOrEmpty(lang) && lang.Length != 2 && lang.Length != 3)
                throw new ArgumentOutOfRangeException("Language needs to be the ISO 639 Alpha 2 or Alpha 3 code");

            AutocompleteResult result = new AutocompleteResult();
            using (var client = DefaultClient)
            {
                Dictionary<string, string> param = DefaultParams;
                param.Add("q", text);
                param.Add("allwordsrequired", allwordsrequired.ToString().ToLower());
                if (!string.IsNullOrEmpty(spellchecking))
                    param.Add("spellchecking", spellchecking);
                if (lat.HasValue && lng.HasValue)
                {
                    param.Add("lat", lat.ToString());
                    param.Add("lng", lng.ToString());
                }
                param.Add("radius", radius.ToString());
                param.Add("suggest", suggest.ToString().ToLower());
                switch(style)
                {
                    case Styles.FULL:
                        param.Add("style", "FULL");
                        break;
                    case Styles.LONG:
                        param.Add("style", "LONG");
                        break;
                    case Styles.MEDIUM:
                        param.Add("style", "MEDIUM");
                        break;
                    case Styles.SHORT:
                        param.Add("style", "SHORT");
                        break;
                }
                if (!string.IsNullOrEmpty(country))
                    param.Add("country", country);
                if (!string.IsNullOrEmpty(lang))
                    param.Add("lang", lang);

                var response = client.GetAsync(string.Format("{0}{1}", Gisgraphy.FULLTEXT_AUTOCOMPLETE, param.ToQueryString())).Result;
                if (!response.IsSuccessStatusCode)
                {
                    result.response.resultsFound = 0;
                    result.message = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    try
                    {
                        result = JsonConvert.DeserializeObject<AutocompleteResult>(response.Content.ReadAsStringAsync().Result);
                    }
                    catch (JsonSerializationException ex)
                    {
                        result.response.resultsFound = 0;
                        result.message = string.Format("{0}\n{1}", ex.Message, ex.StackTrace);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Implementation Notes - The full text service allows you to search for features / places / street 
        /// and do autocompletion.you can : Specify one or more words search on part of the name(auto completion / suggestion) 
        /// Search for text or zip code Specify a GPS restriction(promote nearest, not sorted but has an impact on the score),
        /// Limit the results to a specific Language, Country, place type, Paginate the results, Specify the output verbosity, 
        /// Tells if you want the output to be indented, Tells that all words are required or not, The search is case 
        /// insensitive, use synonyms(Saint/st, ..), separator characters stripping, ...
        /// </summary>
        /// <param name="text">The searched text : The text for the query can be a zip code, a string or one or more strings</param>
        /// <param name="allwordsrequired">Whether the fulltext engine should considers all the words specified as required. Defaults to false (since v 4.0). possible values are true|false (or 'on' when used with the rest service)</param>
        /// <param name="spellchecking">The spellchecking (optional) : whether some suggestions should be provided if no results are found</param>
        /// <param name="lat">The latitude (north-south) for the location point to search around. The value is a floating number, between -90 and +90. It uses GPS coordinates</param>
        /// <param name="lng">The longitude (east-West) for the location point to search around. The value is a floating number between -180 and +180. It uses GPS coordinates.</param>
        /// <param name="radius">Distance from the location point in meters we'd like to search around. The value is a number > 0 if it is not specify or incorrect.</param>
        /// <param name="suggest">If this parameter is set then it will search in part of the names of the street, place,.... It allow you to do auto completion auto suggestion. See the Gisgraphy leaflet plugin for more details. The JSON format will be forced if this parameter is true. See auto completion / suggestions engine for more details</param>
        /// <param name="style" cref="Styles">The output style verbosity (optional) : Determines the output verbosity. 4 styles are available</param>
        /// <param name="country">Limit the search to the specified ISO 3166 country code. Default : search in all countries</param>
        /// <param name="lang">The language code (optional) : The iso 639 Alpha2 or alpha3 Language Code. Some properties such as the AlternateName AdmNames and countryname belong to a certain language code. The language parameter can limit the output of those fields to a certain language (it only apply when style parameter='style') : If the language code does not exists or is not specified, properties with all the languages are retrieved If it exists, the properties with the specified language code, are retrieved</param>
        /// <returns><see cref="AutocompleteResult"/> </returns>
        public async Task<AutocompleteResult> autocompleteAsync(string text, bool allwordsrequired = false, string spellchecking = null, double? lat = null, double? lng = null, double radius = 10000, bool suggest = false, Styles style = Styles.MEDIUM, string country = null, string lang = null)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException("Search text cannot be blank");
            if (lat.HasValue && (lat.Value > 90 || lat.Value < -90))
                throw new ArgumentOutOfRangeException("Latitude must be between -90 an 90");
            if (lng.HasValue && (lng.Value > 180 || lng.Value < -180))
                throw new ArgumentOutOfRangeException("Longitude must be between -180 and 180");
            if ((lat.HasValue && !lng.HasValue) || (!lat.HasValue && lng.HasValue))
                throw new ArgumentNullException("If using latitude and longitude, both values must be provided");
            if (radius <= 0)
                throw new ArgumentOutOfRangeException("Radius must be greater than 0");
            if (!string.IsNullOrEmpty(country) && country.Length != 2)
                throw new ArgumentOutOfRangeException("Country needs to be the ISO 3166 Alpha 2 code");
            if (!string.IsNullOrEmpty(lang) && lang.Length != 2 && lang.Length != 3)
                throw new ArgumentOutOfRangeException("Language needs to be the ISO 639 Alpha 2 or Alpha 3 code");

            AutocompleteResult result = new AutocompleteResult();
            using (var client = DefaultClient)
            {
                Dictionary<string, string> param = DefaultParams;
                param.Add("q", text);
                param.Add("allwordsrequired", allwordsrequired.ToString().ToLower());
                if (!string.IsNullOrEmpty(spellchecking))
                    param.Add("spellchecking", spellchecking);
                if (lat.HasValue && lng.HasValue)
                {
                    param.Add("lat", lat.ToString());
                    param.Add("lng", lng.ToString());
                }
                param.Add("radius", radius.ToString());
                param.Add("suggest", suggest.ToString().ToLower());
                switch (style)
                {
                    case Styles.FULL:
                        param.Add("style", "FULL");
                        break;
                    case Styles.LONG:
                        param.Add("style", "LONG");
                        break;
                    case Styles.MEDIUM:
                        param.Add("style", "MEDIUM");
                        break;
                    case Styles.SHORT:
                        param.Add("style", "SHORT");
                        break;
                }
                if (!string.IsNullOrEmpty(country))
                    param.Add("country", country);
                if (!string.IsNullOrEmpty(lang))
                    param.Add("lang", lang);

                var response = await client.GetAsync(string.Format("{0}{1}", Gisgraphy.FULLTEXT_AUTOCOMPLETE, param.ToQueryString()));
                if (!response.IsSuccessStatusCode)
                {
                    result.response.resultsFound = 0;
                    result.message = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    try
                    {
                        result = JsonConvert.DeserializeObject<AutocompleteResult>(await response.Content.ReadAsStringAsync());
                    }
                    catch (JsonSerializationException ex)
                    {
                        result.response.resultsFound = 0;
                        result.message = string.Format("{0}\n{1}", ex.Message, ex.StackTrace);
                    }
                }
            }
            return result;
        }
        #endregion

        #region Address Parser Services
        /// <summary>
        /// The address parser and address standardizer, are part of the Gisgraphy project 
        /// (free open source worldwide geocoder). Address parsing is the process of dividing 
        /// a single address string into its individual component parts. 
        /// Please visit http://www.address-parser.net for more details
        /// </summary>
        /// <param name="address">A postal address.</param>
        /// <param name="country"></param>The ISO 3166 Alpha 2 code of the country.</param>
        /// <returns><see cref="AddressResult"/> </returns>
        public AddressResult parseAddress(string address, string country = null)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException("Address is a required parameter");
            if (!string.IsNullOrEmpty(country) && country.Length != 2)
                throw new ArgumentOutOfRangeException("Country needs to be the ISO 3166 Alpha 2 code");

            AddressResult result = new AddressResult();
            using (var client = DefaultClient)
            {
                Dictionary<string, string> param = DefaultParams;
                param.Add("address", address);
                if (!string.IsNullOrEmpty(country))
                    param.Add("country", country);

                var response = client.GetAsync(string.Format("{0}{1}", Gisgraphy.ADDRESS_PARSER, param.ToQueryString())).Result;
                if (!response.IsSuccessStatusCode)
                {
                    result.resultsFound = 0;
                    result.message = response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    try
                    {
                        result = JsonConvert.DeserializeObject<AddressResult>(response.Content.ReadAsStringAsync().Result);
                    }
                    catch (JsonSerializationException ex)
                    {
                        result.resultsFound = 0;
                        result.message = string.Format("{0}\n{1}", ex.Message, ex.StackTrace);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// The address parser and address standardizer, are part of the Gisgraphy project 
        /// (free open source worldwide geocoder). Address parsing is the process of dividing 
        /// a single address string into its individual component parts. 
        /// Please visit http://www.address-parser.net for more details
        /// </summary>
        /// <param name="address">A postal address.</param>
        /// <param name="country"></param>The ISO 3166 Alpha 2 code of the country.</param>
        /// <returns><see cref="AddressResult"/> </returns>
        public async Task<AddressResult> parseAddressAsync(string address, string country = null)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException("Address is a required parameter");
            if (!string.IsNullOrEmpty(country) && country.Length != 2)
                throw new ArgumentOutOfRangeException("Country needs to be the ISO 3166 Alpha 2 code");

            AddressResult result = new AddressResult();
            using (var client = DefaultClient)
            {
                Dictionary<string, string> param = DefaultParams;
                param.Add("address", address);
                if (!string.IsNullOrEmpty(country))
                    param.Add("country", country);

                var response = await client.GetAsync(string.Format("{0}{1}", Gisgraphy.ADDRESS_PARSER, param.ToQueryString()));
                if (!response.IsSuccessStatusCode)
                {
                    result.resultsFound = 0;
                    result.message = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    try
                    {
                        result = JsonConvert.DeserializeObject<AddressResult>(await response.Content.ReadAsStringAsync());
                    }
                    catch (JsonSerializationException ex)
                    {
                        result.resultsFound = 0;
                        result.message = string.Format("{0}\n{1}", ex.Message, ex.StackTrace);
                    }
                }
            }

            return result;
        }
        #endregion
    }
}
