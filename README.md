# Gisgraphy.NET
[![Build Status](https://travis-ci.org/nwestfall/Gisgraphy.NET.svg?branch=master)](https://travis-ci.org/nwestfall/Gisgraphy.NET)

A wrapper for [Gisgraphy](http://www.gisgraphy.com) written in .NET

Visit http://www.gisgraphy.com/documentation/api/ for more information on any of the methods below

## Dependencies
* Newtonsoft.JSON 9.0.01
* Microsoft.Bcl 1.1.10
* Microsoft.Bcl.Build 1.0.14
* Microsoft.Net.Http 2.2.29

## Installation
Available on [nuget](https://www.nuget.org/packages/Gisgraphy.NET/)
[![NuGet](https://img.shields.io/nuget/v/Gisgraphy.NET.svg?label=NuGet)](https://www.nuget.org/packages/Gisgraphy.NET/)
```
Install-Package Gisgraphy.NET
```

## Usage
Create an instance with the Gisgraphy Server URL and API Key (if needed)
```
  Gisgraphy gisgraphy = new Gisgraphy("http://free.gisgraphy.com/", null);
```
* Will throw `ArgumentNullException` if `Server URL` is null or blank when calling method
* Will throw `InvalidCastException` if `Server URL` cannot be converted to `Uri`

### Methods

#### geocode(string address, [string country = null], [string postal = null])
Geocode an address
* `address` - A postal address, structured or not, a street, a city, a postal code, a country, or a combination (required)
  * Will throw `ArgumentNullException` if blank or null
* `country` - The country where the place/address is. It is used to determine the postal address format and to improve performance.
  * Will throw `ArgumentOutOfRangeException` if length of string is not 2
* `postal` - Whether the given address is a postal address. default to false. In other words, if the address follow the specification or if it is a well-formed address as it was written on an envelope. This parameter will enable the parsing of the address by the address parser before geocoding, this way, the relevance will be better because because if parsing is successful, we will know the meaning of each word. Note that you can also specify each field if you already know them. (optional)

Returns `Gisgraphy.NET.Models.AddressResult` object

#### geocodeAsync(string address, [string country = null], [string postal = null])
Geocode an address async
* `address` - A postal address, structured or not, a street, a city, a postal code, a country, or a combination (required)
  * Will throw `ArgumentNullException` if blank or null
* `country` - The country where the place/address is. It is used to determine the postal address format and to improve performance.
  * Will throw `ArgumentOutOfRangeException` if length of string is not 2
* `postal` - Whether the given address is a postal address. default to false. In other words, if the address follow the specification or if it is a well-formed address as it was written on an envelope. This parameter will enable the parsing of the address by the address parser before geocoding, this way, the relevance will be better because because if parsing is successful, we will know the meaning of each word. Note that you can also specify each field if you already know them. (optional)
Returns `Task<Gisgraphy.NET.Models.AddressResult>` object

#### reverseGeocode(double lat, double lng)
Reverse geocode a latitude/longitude position
* `lat` - The latitude(north-south) for the location point to search around.The value is a floating number, between -90 and +90. It uses GPS coordinates.
  * Will throw `ArgumentOutOfRangeException` if less than -90 or greater than 90
* `lng` - The longitude (east-West) for the location point to search around. The value is a floating number between -180 and +180. It uses GPS coordinates.
  * Will throw `ArgumentOutOfRangeException` if less than -180 or greater than 180

Returns `Gisgraphy.NET.Models.AddressResult` object

#### reverseGeocodeAsync(double lat, double lng)
Reverse geocode a latitude/longitude position async
* `lat` - The latitude(north-south) for the location point to search around.The value is a floating number, between -90 and +90. It uses GPS coordinates.
  * Will throw `ArgumentOutOfRangeException` if less than -90 or greater than 90
* `lng` - The longitude (east-West) for the location point to search around. The value is a floating number between -180 and +180. It uses GPS coordinates.
  * Will throw `ArgumentOutOfRangeException` if less than -180 or greater than 180

Returns `Task<Gisgraphy.NET.Models.AddressResult>` object

#### findStreet(double lat, double lng, [double radius = 1000], [bool oneWay = false], [bool distance = true], [string streetType = null])
The street service allows you to search for street by GPS position. You can : Specify GPS position, Give the beginning or a part of the name of the street (useful for autocompletion), Limit search to a specific type (e.g : Pedestrian, highway, residential, ... 25 types available), Limit search to a specified radius, Limit search to one way streets.
* `lat` - The latitude(north-south) for the location point to search around.The value is a floating number, between -90 and +90. It uses GPS coordinates.
  * Will throw `ArgumentOutOfRangeException` if less than -90 or greater than 90
* `lng` - The longitude (east-West) for the location point to search around. The value is a floating number between -180 and +180. It uses GPS coordinates.
  * Will throw `ArgumentOutOfRangeException` if less than -180 or greater than 180
* `radius` - Distance from the location point in meters we'd like to search around. The value is a number > 0 if it is not specify or incorrect.
  * Will throw `ArgumentOutOfRangeException` is less than or equal to 0
* `oneWay` - Whether the street should be a oneWay street (optional) : limit the search to the street that are one way street. If you use a checkbox in a form to indent the results, the value will be 'on' or 'off', so to simplify the use : the value for the web service can be 'true' or 'on'
* `distance` - Whether(or not) we want the distance field to be output.This option is useful to improve the performance if we don't care about the distance (e.g : we search for name). Of course, the results won't be sorted by distance.If you use a checkbox in a form to indent the results, the value will be 'on' or 'off', so to simplify the use : the value for the web service can be 'true' or 'on'
* `streetType` - Filter search with a street type

Returns `Gisgraphy.NET.Models.StreetDistanceResult` object

#### findStreetAsync(double lat, double lng, [double radius = 1000], [bool oneWay = false], [bool distance = true], [string streetType = null])
The street service allows you to search for street by GPS position. You can : Specify GPS position, Give the beginning or a part of the name of the street (useful for autocompletion), Limit search to a specific type (e.g : Pedestrian, highway, residential, ... 25 types available), Limit search to a specified radius, Limit search to one way streets.
* `lat` - The latitude(north-south) for the location point to search around.The value is a floating number, between -90 and +90. It uses GPS coordinates.
  * Will throw `ArgumentOutOfRangeException` if less than -90 or greater than 90
* `lng` - The longitude (east-West) for the location point to search around. The value is a floating number between -180 and +180. It uses GPS coordinates.
  * Will throw `ArgumentOutOfRangeException` if less than -180 or greater than 180
* `radius` - Distance from the location point in meters we'd like to search around. The value is a number > 0 if it is not specify or incorrect.
  * Will throw `ArgumentOutOfRangeException` is less than or equal to 0
* `oneWay` - Whether the street should be a oneWay street (optional) : limit the search to the street that are one way street. If you use a checkbox in a form to indent the results, the value will be 'on' or 'off', so to simplify the use : the value for the web service can be 'true' or 'on'
* `distance` - Whether(or not) we want the distance field to be output.This option is useful to improve the performance if we don't care about the distance (e.g : we search for name). Of course, the results won't be sorted by distance.If you use a checkbox in a form to indent the results, the value will be 'on' or 'off', so to simplify the use : the value for the web service can be 'true' or 'on'
* `streetType` - Filter search with a street type

Returns `Task<Gisgraphy.NET.Models.StreetDistanceResult>` object

#### geolocalization(double lat, double lng, [double radius = 10000], [bool distance = true], [string placeType = null])
The geolocalisation service allows to search for features around a earth location
* `lat` - The latitude(north-south) for the location point to search around.The value is a floating number, between -90 and +90. It uses GPS coordinates.
  * Will throw `ArgumentOutOfRangeException` if less than -90 or greater than 90
* `lng` - The longitude (east-West) for the location point to search around. The value is a floating number between -180 and +180. It uses GPS coordinates.
  * Will throw `ArgumentOutOfRangeException` if less than -180 or greater than 180
* `radius` - Distance from the location point in meters we'd like to search around. The value is a number > 0 if it is not specify or incorrect.
  * Will throw `ArgumentOutOfRangeException` is less than or equal to 0
* `oneWay` - Whether the street should be a oneWay street (optional) : limit the search to the street that are one way street. If you use a checkbox in a form to indent the results, the value will be 'on' or 'off', so to simplify the use : the value for the web service can be 'true' or 'on'
* `distance` - Whether(or not) we want the distance field to be output.This option is useful to improve the performance if we don't care about the distance (e.g : we search for name). Of course, the results won't be sorted by distance.If you use a checkbox in a form to indent the results, the value will be 'on' or 'off', so to simplify the use : the value for the web service can be 'true' or 'on'
* `placeType` - Filter search for a given placetype

Returns `Gisgraphy.NET.Models.GeolocalizationFeatureResult` object

#### geolocalizationAsync(double lat, double lng, [double radius = 10000], [bool distance = true], [string placeType = null])
The geolocalisation service allows to search for features around a earth location async
* `lat` - The latitude(north-south) for the location point to search around.The value is a floating number, between -90 and +90. It uses GPS coordinates.
  * Will throw `ArgumentOutOfRangeException` if less than -90 or greater than 90
* `lng` - The longitude (east-West) for the location point to search around. The value is a floating number between -180 and +180. It uses GPS coordinates.
  * Will throw `ArgumentOutOfRangeException` if less than -180 or greater than 180
* `radius` - Distance from the location point in meters we'd like to search around. The value is a number > 0 if it is not specify or incorrect.
  * Will throw `ArgumentOutOfRangeException` is less than or equal to 0
* `oneWay` - Whether the street should be a oneWay street (optional) : limit the search to the street that are one way street. If you use a checkbox in a form to indent the results, the value will be 'on' or 'off', so to simplify the use : the value for the web service can be 'true' or 'on'
* `distance` - Whether(or not) we want the distance field to be output.This option is useful to improve the performance if we don't care about the distance (e.g : we search for name). Of course, the results won't be sorted by distance.If you use a checkbox in a form to indent the results, the value will be 'on' or 'off', so to simplify the use : the value for the web service can be 'true' or 'on'
* `placeType` - Filter search for a given placetype

Returns `Task<Gisgraphy.NET.Models.GeolocalizationFeatureResult>` object

#### autocomplete(string text, [bool allwordsrequired = false], [string spellchecking = null], [double? lat = null], [double? lng = null], [double radius = 10000], [bool suggest = false], [Styles style = Styles.MEDIUM], [string country = null], [string lang = null])
Implementation Notes - The full text service allows you to search for features / places / street and do autocompletion.you can : Specify one or more words search on part of the name(auto completion / suggestion) Search for text or zip code Specify a GPS restriction(promote nearest, not sorted but has an impact on the score), Limit the results to a specific Language, Country, place type, Paginate the results, Specify the output verbosity, Tells if you want the output to be indented, Tells that all words are required or not, The search is case insensitive, use synonyms(Saint/st, ..), separator characters stripping, ...
* `text` - The searched text : The text for the query can be a zip code, a string or one or more strings
  * Will throw `ArgumentNullException` if blank or null
* `allwordsrequired` - Whether the fulltext engine should considers all the words specified as required. Defaults to false (since v 4.0). possible values are true|false (or 'on' when used with the rest service)
* `spellchecking` - The spellchecking (optional) : whether some suggestions should be provided if no results are found
* `lat` - The latitude(north-south) for the location point to search around.The value is a floating number, between -90 and +90. It uses GPS coordinates.
  * Will throw `ArgumentOutOfRangeException` if less than -90 or greater than 90
  * Will throw `ArgumentNullException` if `lng` is not passed as well
* `lng` - The longitude (east-West) for the location point to search around. The value is a floating number between -180 and +180. It uses GPS coordinates.
  * Will throw `ArgumentOutOfRangeException` if less than -180 or greater than 180
  * Will throw `ArgumentNullException` if `lat` is not passed as well
* `radius` - Distance from the location point in meters we'd like to search around. The value is a number > 0 if it is not specify or incorrect.
  * Will throw `ArgumentOutOfRangeException` is less than or equal to 0
* `suggest` - If this parameter is set then it will search in part of the names of the street, place,.... It allow you to do auto completion auto suggestion. See the Gisgraphy leaflet plugin for more details. The JSON format will be forced if this parameter is true. See auto completion / suggestions engine for more details
* `style` - The output style verbosity (optional) : Determines the output verbosity. 4 styles are available
* `country` - The country where the place/address is. It is used to determine the postal address format and to improve performance.
  * Will throw `ArgumentOutOfRangeException` if length of string is not 2
* `lang` - The language code (optional) : The iso 639 Alpha2 or alpha3 Language Code. Some properties such as the AlternateName AdmNames and countryname belong to a certain language code. The language parameter can limit the output of those fields to a certain language (it only apply when style parameter='style') : If the language code does not exists or is not specified, properties with all the languages are retrieved If it exists, the properties with the specified language code, are retrieved
  * Will throw `ArgumentOutOfRangeException` if length of string is not 2 or 3
  
Returns `Gisgraphy.NET.Models.AutocompleteResult` object

#### autocompleteAsync(string text, [bool allwordsrequired = false], [string spellchecking = null], [double? lat = null], [double? lng = null], [double radius = 10000], [bool suggest = false], [Styles style = Styles.MEDIUM], [string country = null], [string lang = null])
Implementation Notes - The full text service allows you to search for features / places / street and do autocompletion.you can : Specify one or more words search on part of the name(auto completion / suggestion) Search for text or zip code Specify a GPS restriction(promote nearest, not sorted but has an impact on the score), Limit the results to a specific Language, Country, place type, Paginate the results, Specify the output verbosity, Tells if you want the output to be indented, Tells that all words are required or not, The search is case insensitive, use synonyms(Saint/st, ..), separator characters stripping, ...
* `text` - The searched text : The text for the query can be a zip code, a string or one or more strings
  * Will throw `ArgumentNullException` if blank or null
* `allwordsrequired` - Whether the fulltext engine should considers all the words specified as required. Defaults to false (since v 4.0). possible values are true|false (or 'on' when used with the rest service)
* `spellchecking` - The spellchecking (optional) : whether some suggestions should be provided if no results are found
* `lat` - The latitude(north-south) for the location point to search around.The value is a floating number, between -90 and +90. It uses GPS coordinates.
  * Will throw `ArgumentOutOfRangeException` if less than -90 or greater than 90
  * Will throw `ArgumentNullException` if `lng` is not passed as well
* `lng` - The longitude (east-West) for the location point to search around. The value is a floating number between -180 and +180. It uses GPS coordinates.
  * Will throw `ArgumentOutOfRangeException` if less than -180 or greater than 180
  * Will throw `ArgumentNullException` if `lat` is not passed as well
* `radius` - Distance from the location point in meters we'd like to search around. The value is a number > 0 if it is not specify or incorrect.
  * Will throw `ArgumentOutOfRangeException` is less than or equal to 0
* `suggest` - If this parameter is set then it will search in part of the names of the street, place,.... It allow you to do auto completion auto suggestion. See the Gisgraphy leaflet plugin for more details. The JSON format will be forced if this parameter is true. See auto completion / suggestions engine for more details
* `style` - The output style verbosity (optional) : Determines the output verbosity. 4 styles are available
* `country` - The country where the place/address is. It is used to determine the postal address format and to improve performance.
  * Will throw `ArgumentOutOfRangeException` if length of string is not 2
* `lang` - The language code (optional) : The iso 639 Alpha2 or alpha3 Language Code. Some properties such as the AlternateName AdmNames and countryname belong to a certain language code. The language parameter can limit the output of those fields to a certain language (it only apply when style parameter='style') : If the language code does not exists or is not specified, properties with all the languages are retrieved If it exists, the properties with the specified language code, are retrieved
  * Will throw `ArgumentOutOfRangeException` if length of string is not 2 or 3
  
Returns `Task<Gisgraphy.NET.Models.AutocompleteResult>` object

#### parseAddress(string address, [string country = null])
The address parser and address standardizer, are part of the Gisgraphy project (free open source worldwide geocoder). Address parsing is the process of dividing a single address string into its individual component parts. Please visit http://www.address-parser.net for more details
* `address` - A postal address.
  * Will throw `ArgumentNullException` if blank or null
* `country` - The ISO 3166 Alpha 2 code of the country.
  * Will throw `ArgumentOutOfRangeException` if length of string is not 2
  
Returns `Gisgraphy.NET.Models.AddressResult` object

#### parseAddressAsync(string address, [string country = null])
The address parser and address standardizer, are part of the Gisgraphy project (free open source worldwide geocoder). Address parsing is the process of dividing a single address string into its individual component parts. Please visit http://www.address-parser.net for more details
* `address` - A postal address.
  * Will throw `ArgumentNullException` if blank or null
* `country` - The ISO 3166 Alpha 2 code of the country.
  * Will throw `ArgumentOutOfRangeException` if length of string is not 2
  
Returns `Task<Gisgraphy.NET.Models.AddressResult>` object

## Author
Nathan Westfall
