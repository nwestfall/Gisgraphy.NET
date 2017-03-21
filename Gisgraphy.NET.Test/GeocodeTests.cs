using System;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

using Gisgraphy.NET;
using Gisgraphy.NET.Models;

namespace Gisgraphy.NET.Test
{
    [TestFixture(Author = "Nathan Westfall", Description = "Tests against Gisgraphy.NET library")]
    public class GeocodeTests
    {
        //Proobably move these
        private DateTime lastApiCall = DateTime.MinValue;
        private const string SERVER_URL = "http://free.gisgraphy.com/geocoding/geocode";
        private const string API_KEY = "none";
        private const string ALBANY_AIRPORT_ADDRESS = "Airport Terminal Road, 12205 Colonie";
        private const string ALBANY_AIRPORT_POSTAL_ADDRESS = "Airport Terminal Road";
        private const double ALBANY_AIRPORT_LAT = 42.74511469046002;
        private const double ALBANY_AIRPORT_LNG = -73.80948451774645;
        private const string ALBANY_AUTOCOMPLETE = "Albany International Airport";

        private void WaitForApiLimit()
        {
            var difference = (int)(DateTime.Now - lastApiCall).TotalSeconds;
            if (difference > 10 || difference < 0)
                return;
            else
                Thread.Sleep((10 - difference) * 1000);
        }

        #region Default Tests
        [Test]
        public void VerifyConstructor()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.AreEqual(SERVER_URL, gis.serverUrl);
            Assert.AreEqual(API_KEY, gis.apiKey);
        }

        [Test]
        public void VerifyNoServerURL()
        {
            Gisgraphy gis = new Gisgraphy();

            Assert.Throws<ArgumentNullException>(() => gis.geocode(ALBANY_AIRPORT_ADDRESS), "Server URL is not set");
        }

        [Test]
        public void VerifyInvalidServerURL()
        {
            Gisgraphy gis = new Gisgraphy("INVALID URL", null);

            Assert.Throws<InvalidCastException>(() => gis.geocode(ALBANY_AIRPORT_ADDRESS), "Server URL is not a valid URI");
        }
        #endregion

        #region Geocode Tests
        [Test]
        public void VerifyGeocodeNoAddress()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentNullException>(() => gis.geocode(null), "Address is a required parameter");
        }

        [Test]
        public void VerifyGeocodeIncorrectCountryCodeFormat()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.geocode(ALBANY_AIRPORT_ADDRESS, "United States"), "Country needs to be the ISO 3166 Alpha 2 code");
        }

        [Test]
        public void VerifyGeocodeAddressOnly()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            WaitForApiLimit();

            //Check Albany International Airport
            var result = gis.geocode(ALBANY_AIRPORT_ADDRESS);
            var firstResult = result.results.FirstOrDefault();

            lastApiCall = DateTime.Now;

            Assert.Greater(result.resultsFound, 0);
            Assert.AreEqual(result.executionTime, result.executionTimeSpan.TotalSeconds);
            Assert.AreEqual(result.resultsFound, result.results.Count());
            Assert.AreEqual(167344877, firstResult.id);
            Assert.AreEqual(-73.80948451774645, firstResult.longitude);
            Assert.AreEqual(42.74511469046002, firstResult.latitude);
            Assert.AreEqual("Airport Terminal Road", firstResult.name);
            Assert.AreEqual("Airport Terminal Road", firstResult.streetName);
            Assert.AreEqual("UNCLASSIFIED", firstResult.streetType);
            Assert.AreEqual("12205", firstResult.zipCode);
            Assert.AreEqual("Shakers", firstResult.dependentLocality);
            Assert.AreEqual("Colonie", firstResult.city);
            Assert.AreEqual("Albany County", firstResult.state);
            Assert.AreEqual("US", firstResult.countryCode);
            Assert.AreEqual("STREET", firstResult.geocodingLevel);

            Console.WriteLine(result.ToString());
        }

        [Test]
        public void VerifyGeocodeAddressAndCountry()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            WaitForApiLimit();

            //Check Albany International Airport
            var result = gis.geocode(ALBANY_AIRPORT_ADDRESS, "US");
            var firstResult = result.results.FirstOrDefault();

            lastApiCall = DateTime.Now;

            Assert.Greater(result.resultsFound, 0);
            Assert.AreEqual(result.executionTime, result.executionTimeSpan.TotalSeconds);
            Assert.AreEqual(result.resultsFound, result.results.Count());
            Assert.AreEqual(167344877, firstResult.id);
            Assert.AreEqual(-73.80948451774645, firstResult.longitude);
            Assert.AreEqual(42.74511469046002, firstResult.latitude);
            Assert.AreEqual("Airport Terminal Road", firstResult.name);
            Assert.AreEqual("Airport Terminal Road", firstResult.streetName);
            Assert.AreEqual("UNCLASSIFIED", firstResult.streetType);
            Assert.AreEqual("12205", firstResult.zipCode);
            Assert.AreEqual("Shakers", firstResult.dependentLocality);
            Assert.AreEqual("Colonie", firstResult.city);
            Assert.AreEqual("Albany County", firstResult.state);
            Assert.AreEqual("US", firstResult.countryCode);
            Assert.AreEqual("STREET", firstResult.geocodingLevel);

            Console.WriteLine(result.ToString());
        }
        
        [Test]
        public void VerifyGeocodeAddressCountryAndPostal()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            WaitForApiLimit();

            //Check Albany International Airport
            var result = gis.geocode(ALBANY_AIRPORT_ADDRESS, "US", "12205");
            var firstResult = result.results.FirstOrDefault();

            lastApiCall = DateTime.Now;

            Assert.Greater(result.resultsFound, 0);
            Assert.AreEqual(result.executionTime, result.executionTimeSpan.TotalSeconds);
            Assert.AreEqual(result.resultsFound, result.results.Count());
            Assert.AreEqual(167344877, firstResult.id);
            Assert.AreEqual(-73.80948451774645, firstResult.longitude);
            Assert.AreEqual(42.74511469046002, firstResult.latitude);
            Assert.AreEqual("Airport Terminal Road", firstResult.name);
            Assert.AreEqual("Airport Terminal Road", firstResult.streetName);
            Assert.AreEqual("UNCLASSIFIED", firstResult.streetType);
            Assert.AreEqual("12205", firstResult.zipCode);
            Assert.AreEqual("Shakers", firstResult.dependentLocality);
            Assert.AreEqual("Colonie", firstResult.city);
            Assert.AreEqual("Albany County", firstResult.state);
            Assert.AreEqual("US", firstResult.countryCode);
            Assert.AreEqual("STREET", firstResult.geocodingLevel);

            Console.WriteLine(result.ToString());
        }

        [Test]
        public void VerifyGeocodeNoAddressAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await gis.geocodeAsync(null), "Address is a required parameter");
        }

        [Test]
        public void VerifyGeocodeIncorrectCountryCodeFormatAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.geocodeAsync(ALBANY_AIRPORT_ADDRESS, "United States"), "Country needs to be the ISO 3166 Alpha 2 code");
        }

        [Test]
        public async Task VerifyGeocodeAddressOnlyAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            WaitForApiLimit();

            //Check Albany International Airport
            var result = await gis.geocodeAsync(ALBANY_AIRPORT_ADDRESS);
            var firstResult = result.results.FirstOrDefault();

            lastApiCall = DateTime.Now;

            Assert.Greater(result.resultsFound, 0);
            Assert.AreEqual(result.executionTime, result.executionTimeSpan.TotalSeconds);
            Assert.AreEqual(result.resultsFound, result.results.Count());
            Assert.AreEqual(167344877, firstResult.id);
            Assert.AreEqual(-73.80948451774645, firstResult.longitude);
            Assert.AreEqual(42.74511469046002, firstResult.latitude);
            Assert.AreEqual("Airport Terminal Road", firstResult.name);
            Assert.AreEqual("Airport Terminal Road", firstResult.streetName);
            Assert.AreEqual("UNCLASSIFIED", firstResult.streetType);
            Assert.AreEqual("12205", firstResult.zipCode);
            Assert.AreEqual("Shakers", firstResult.dependentLocality);
            Assert.AreEqual("Colonie", firstResult.city);
            Assert.AreEqual("Albany County", firstResult.state);
            Assert.AreEqual("US", firstResult.countryCode);
            Assert.AreEqual("STREET", firstResult.geocodingLevel);

            Console.WriteLine(result.ToString());
        }

        [Test]
        public async Task VerifyGeocodeAddressAndCountryAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            WaitForApiLimit();

            //Check Albany International Airport
            var result = await gis.geocodeAsync(ALBANY_AIRPORT_ADDRESS, "US", "12205");
            var firstResult = result.results.FirstOrDefault();

            lastApiCall = DateTime.Now;

            Assert.Greater(result.resultsFound, 0);
            Assert.AreEqual(result.executionTime, result.executionTimeSpan.TotalSeconds);
            Assert.AreEqual(result.resultsFound, result.results.Count());
            Assert.AreEqual(167344877, firstResult.id);
            Assert.AreEqual(-73.80948451774645, firstResult.longitude);
            Assert.AreEqual(42.74511469046002, firstResult.latitude);
            Assert.AreEqual("Airport Terminal Road", firstResult.name);
            Assert.AreEqual("Airport Terminal Road", firstResult.streetName);
            Assert.AreEqual("UNCLASSIFIED", firstResult.streetType);
            Assert.AreEqual("12205", firstResult.zipCode);
            Assert.AreEqual("Shakers", firstResult.dependentLocality);
            Assert.AreEqual("Colonie", firstResult.city);
            Assert.AreEqual("Albany County", firstResult.state);
            Assert.AreEqual("US", firstResult.countryCode);
            Assert.AreEqual("STREET", firstResult.geocodingLevel);

            Console.WriteLine(result.ToString());
        }

        [Test]
        public async Task VerifyGeocodeAddressCountryAndPostalAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            WaitForApiLimit();

            //Check Albany International Airport
            var result = await gis.geocodeAsync(ALBANY_AIRPORT_ADDRESS, "US", "12205");
            var firstResult = result.results.FirstOrDefault();

            lastApiCall = DateTime.Now;

            Assert.Greater(result.resultsFound, 0);
            Assert.AreEqual(result.executionTime, result.executionTimeSpan.TotalSeconds);
            Assert.AreEqual(result.resultsFound, result.results.Count());
            Assert.AreEqual(167344877, firstResult.id);
            Assert.AreEqual(-73.80948451774645, firstResult.longitude);
            Assert.AreEqual(42.74511469046002, firstResult.latitude);
            Assert.AreEqual("Airport Terminal Road", firstResult.name);
            Assert.AreEqual("Airport Terminal Road", firstResult.streetName);
            Assert.AreEqual("UNCLASSIFIED", firstResult.streetType);
            Assert.AreEqual("12205", firstResult.zipCode);
            Assert.AreEqual("Shakers", firstResult.dependentLocality);
            Assert.AreEqual("Colonie", firstResult.city);
            Assert.AreEqual("Albany County", firstResult.state);
            Assert.AreEqual("US", firstResult.countryCode);
            Assert.AreEqual("STREET", firstResult.geocodingLevel);

            Console.WriteLine(result.ToString());
        }
        #endregion

        #region Reverse Geocode Tests
        [Test]
        public void VerifyReverseGeocodeInvalidLatHigh()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.reverseGeocode(100, 0), "Latitude must be between -90 an 90");
        }

        [Test]
        public void VerifyReverseGeocodeInvalidLatLow()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.reverseGeocode(-100, 0), "Latitude must be between -90 an 90");
        }

        [Test]
        public void VerifyReverseGeocodeInvalidLngHigh()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.reverseGeocode(0, 200), "Longitude must be between -180 and 180");
        }

        [Test]
        public void VerifyReverseGeocodeInvalidLngLow()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.reverseGeocode(0, -200), "Longitude must be between -180 and 180");
        }

        [Test]
        public void VerifyReverseGeocode()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            WaitForApiLimit();

            //Check Albany International Airport
            var result = gis.reverseGeocode(ALBANY_AIRPORT_LAT, ALBANY_AIRPORT_LNG);
            var firstResult = result.results.FirstOrDefault();

            lastApiCall = DateTime.Now;

            Assert.Greater(result.resultsFound, 0);
            Assert.AreEqual(result.executionTime, result.executionTimeSpan.TotalSeconds);
            Assert.AreEqual(result.resultsFound, result.results.Count());
            Assert.AreEqual(ALBANY_AIRPORT_LAT, firstResult.latitude);
            Assert.AreEqual(ALBANY_AIRPORT_LNG, firstResult.longitude);
            Assert.AreEqual("Airport Terminal Road", firstResult.streetName);
            Assert.AreEqual("Colonie", firstResult.city);
            Assert.AreEqual("Shakers", firstResult.citySubdivision);
            Assert.AreEqual("New York", firstResult.state);
            Assert.AreEqual("US", firstResult.countryCode);
            Assert.AreEqual("STREET", firstResult.geocodingLevel);
            Assert.AreEqual(0.21190578937061122, firstResult.distance);
            Assert.AreEqual("Airport Terminal Road, Shakers, Colonie, New York, United States, US", firstResult.formattedFull);

            Console.WriteLine(result.ToString());
        }

        [Test]
        public void VerifyReverseGeocodeInvalidLatHighAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.reverseGeocodeAsync(100, 0), "Latitude must be between -90 an 90");
        }

        [Test]
        public void VerifyReverseGeocodeInvalidLatLowAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.reverseGeocodeAsync(-100, 0), "Latitude must be between -90 an 90");
        }

        [Test]
        public void VerifyReverseGeocodeInvalidLngHighAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.reverseGeocodeAsync(0, 200), "Longitude must be between -180 and 180");
        }

        [Test]
        public void VerifyReverseGeocodeInvalidLngLowAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.reverseGeocodeAsync(0, -200), "Longitude must be between -180 and 180");
        }

        [Test]
        public async Task VerifyReverseGeocodeAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            WaitForApiLimit();

            //Check Albany International Airport
            var result = await gis.reverseGeocodeAsync(ALBANY_AIRPORT_LAT, ALBANY_AIRPORT_LNG);
            var firstResult = result.results.FirstOrDefault();

            lastApiCall = DateTime.Now;

            Assert.Greater(result.resultsFound, 0);
            Assert.AreEqual(result.executionTime, result.executionTimeSpan.TotalSeconds);
            Assert.AreEqual(result.resultsFound, result.results.Count());
            Assert.AreEqual(ALBANY_AIRPORT_LAT, firstResult.latitude);
            Assert.AreEqual(ALBANY_AIRPORT_LNG, firstResult.longitude);
            Assert.AreEqual("Airport Terminal Road", firstResult.streetName);
            Assert.AreEqual("Colonie", firstResult.city);
            Assert.AreEqual("Shakers", firstResult.citySubdivision);
            Assert.AreEqual("New York", firstResult.state);
            Assert.AreEqual("US", firstResult.countryCode);
            Assert.AreEqual("STREET", firstResult.geocodingLevel);
            Assert.AreEqual(0.21190578937061122, firstResult.distance);
            Assert.AreEqual("Airport Terminal Road, Shakers, Colonie, New York, United States, US", firstResult.formattedFull);

            Console.WriteLine(result.ToString());
        }
        #endregion

        #region Street Service Tests
        [Test]
        public void VerifyStreetServiceInvalidLatHigh()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.findStreet(100, 0), "Latitude must be between -90 an 90");
        }

        [Test]
        public void VerifyStreetServiceInvalidLatLow()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.findStreet(-100, 0), "Latitude must be between -90 an 90");
        }

        [Test]
        public void VerifyStreetServiceInvalidLngHigh()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.findStreet(0, 200), "Longitude must be between -180 and 180");
        }

        [Test]
        public void VerifyStreetServiceInvalidLngLow()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.findStreet(0, -200), "Longitude must be between -180 and 180");
        }

        [Test]
        public void VerifyStreetAddressInvalidRadius()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.findStreet(ALBANY_AIRPORT_LAT, ALBANY_AIRPORT_LNG, -100), "Radius must be greater than 0");
        }

        [Test]
        public void VerifyStreetServiceLatLngOnly()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            WaitForApiLimit();

            var result = gis.findStreet(ALBANY_AIRPORT_LAT, ALBANY_AIRPORT_LNG);
            var firstResult = result.results.FirstOrDefault();

            lastApiCall = DateTime.Now;

            Assert.Greater(result.resultsFound, 0);
            Assert.AreEqual(result.executionTime, result.executionTimeSpan.TotalSeconds);
            Assert.AreEqual(result.resultsFound, result.results.Count());
            Assert.AreEqual(167344877, firstResult.id);
            Assert.AreEqual(-73.80948451774645, firstResult.longitude);
            Assert.AreEqual(42.74511469046002, firstResult.latitude);
            Assert.AreEqual("Airport Terminal Road", firstResult.name);
            Assert.AreEqual("UNCLASSIFIED", firstResult.streetType);
            Assert.AreEqual("US", firstResult.countryCode);
            Assert.AreEqual(0.217368757236126, firstResult.distance);
            Assert.AreEqual(5583598, firstResult.openStreetMapId);
            Assert.AreEqual(false, firstResult.oneWay);
            Assert.AreEqual(738.207938454055, firstResult.length);
            Assert.AreEqual("Colonie", firstResult.isIn);
            Assert.AreEqual("Shakers", firstResult.isInPlace);
            Assert.AreEqual("Albany County", firstResult.isInAdm);

            Console.WriteLine(result.ToString());
        }

        [Test]
        public void VerifyStreetServiceInvalidLatHighAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.findStreetAsync(100, 0), "Latitude must be between -90 an 90");
        }

        [Test]
        public void VerifyStreetServiceInvalidLatLowAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.findStreetAsync(-100, 0), "Latitude must be between -90 an 90");
        }

        [Test]
        public void VerifyStreetServiceInvalidLngHighAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.findStreetAsync(0, 200), "Longitude must be between -180 and 180");
        }

        [Test]
        public void VerifyStreetServiceInvalidLngLowAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.findStreetAsync(0, -200), "Longitude must be between -180 and 180");
        }

        [Test]
        public void VerifyStreetAddressInvalidRadiusAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.findStreetAsync(ALBANY_AIRPORT_LAT, ALBANY_AIRPORT_LNG, -100), "Radius must be greater than 0");
        }

        [Test]
        public async Task VerifyStreetServiceLatLngOnlyAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            WaitForApiLimit();

            var result = await gis.findStreetAsync(ALBANY_AIRPORT_LAT, ALBANY_AIRPORT_LNG);
            var firstResult = result.results.FirstOrDefault();

            lastApiCall = DateTime.Now;

            Assert.Greater(result.resultsFound, 0);
            Assert.AreEqual(result.executionTime, result.executionTimeSpan.TotalSeconds);
            Assert.AreEqual(result.resultsFound, result.results.Count());
            Assert.AreEqual(167344877, firstResult.id);
            Assert.AreEqual(-73.80948451774645, firstResult.longitude);
            Assert.AreEqual(42.74511469046002, firstResult.latitude);
            Assert.AreEqual("Airport Terminal Road", firstResult.name);
            Assert.AreEqual("UNCLASSIFIED", firstResult.streetType);
            Assert.AreEqual("US", firstResult.countryCode);
            Assert.AreEqual(0.217368757236126, firstResult.distance);
            Assert.AreEqual(5583598, firstResult.openStreetMapId);
            Assert.AreEqual(false, firstResult.oneWay);
            Assert.AreEqual(738.207938454055, firstResult.length);
            Assert.AreEqual("Colonie", firstResult.isIn);
            Assert.AreEqual("Shakers", firstResult.isInPlace);
            Assert.AreEqual("Albany County", firstResult.isInAdm);

            Console.WriteLine(result.ToString());
        }
        #endregion

        #region Geolocalization Service Tests
        [Test]
        public void VerifyGeolocalizationServiceInvalidLatHigh()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.geolocalization(100, 0), "Latitude must be between -90 an 90");
        }

        [Test]
        public void VerifyGeocalizationServiceInvalidLatLow()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.geolocalization(-100, 0), "Latitude must be between -90 an 90");
        }

        [Test]
        public void VerifyGeolocalizationServiceInvalidLngHigh()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.geolocalization(0, 200), "Longitude must be between -180 and 180");
        }

        [Test]
        public void VerifyGeolocalizationServiceInvalidLngLow()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.geolocalization(0, -200), "Longitude must be between -180 and 180");
        }

        [Test]
        public void VerifyGeolocalizationServiceInvalidRadius()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.geolocalization(ALBANY_AIRPORT_LAT, ALBANY_AIRPORT_LNG, -100), "Radius must be greater than 0");
        }

        [Test]
        public void VerifyGeolocalizationService()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            WaitForApiLimit();

            var result = gis.geolocalization(ALBANY_AIRPORT_LAT, ALBANY_AIRPORT_LNG);
            var firstResult = result.results.FirstOrDefault();

            lastApiCall = DateTime.Now;

            Assert.Greater(result.resultsFound, 0);
            Assert.AreEqual(result.executionTime, result.executionTimeSpan.TotalSeconds);
            Assert.AreEqual(result.resultsFound, result.results.Count());
            Assert.AreEqual(637.999392531314, firstResult.distance);
            Assert.AreEqual("Shakers", firstResult.name);
            Assert.AreEqual("NY", firstResult.adm1Code);
            Assert.AreEqual("001", firstResult.adm2Code);
            Assert.AreEqual("New York", firstResult.adm1Name);
            Assert.AreEqual("Albany County", firstResult.adm2Name);
            Assert.AreEqual("Shakers", firstResult.asciiName);
            Assert.AreEqual("US", firstResult.countryCode);
            Assert.AreEqual(83, firstResult.elevation);
            Assert.AreEqual("P", firstResult.featureClass);
            Assert.AreEqual("PPL", firstResult.featureCode);
            Assert.AreEqual(5137753, firstResult.featureId);
            Assert.AreEqual(85, firstResult.gtopo30);
            Assert.AreEqual(0, firstResult.population);
            Assert.AreEqual("America/New_York", firstResult.timezone);
            Assert.AreEqual(42.73952102661133, firstResult.latitude);
            Assert.AreEqual(-73.81123352050781, firstResult.longitude);
            Assert.AreEqual("city", firstResult.placeType);
            Assert.AreEqual(158912974, firstResult.openStreetMapId);
            Assert.AreEqual("https://www.google.fr/maps/preview#!q=42.76952102661133+-73.81123352050781", firstResult.googleMapUrl);
            Assert.AreEqual("http://maps.yahoo.com/place/?lat=42.73952102661133&amp;lon=-73.81123352050781", firstResult.yahooMapUrl);
            Assert.AreEqual("http://www.openstreetmap.org/#map=14/42.73952102661133/-73.81123352050781", firstResult.openStreetMapUrl);
            Assert.AreEqual("/images/flags/US.png", firstResult.countryFlagUrl);
            Assert.AreEqual("hamlet", firstResult.amenity);

            Console.WriteLine(result.ToString());
        }

        [Test]
        public void VerifyGeolocalizationServiceInvalidLatHighAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.geolocalizationAsync(100, 0), "Latitude must be between -90 an 90");
        }

        [Test]
        public void VerifyGeocalizationServiceInvalidLatLowAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.geolocalizationAsync(-100, 0), "Latitude must be between -90 an 90");
        }

        [Test]
        public void VerifyGeolocalizationServiceInvalidLngHighAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.geolocalizationAsync(0, 200), "Longitude must be between -180 and 180");
        }

        [Test]
        public void VerifyGeolocalizationServiceInvalidLngLowAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.geolocalizationAsync(0, -200), "Longitude must be between -180 and 180");
        }

        [Test]
        public void VerifyGeolocalizationServiceInvalidRadiusAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.geolocalizationAsync(ALBANY_AIRPORT_LAT, ALBANY_AIRPORT_LNG, -100), "Radius must be greater than 0");
        }

        [Test]
        public async Task VerifyGeolocalizationServiceAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            WaitForApiLimit();

            var result = await gis.geolocalizationAsync(ALBANY_AIRPORT_LAT, ALBANY_AIRPORT_LNG);
            var firstResult = result.results.FirstOrDefault();

            lastApiCall = DateTime.Now;

            Assert.Greater(result.resultsFound, 0);
            Assert.AreEqual(result.executionTime, result.executionTimeSpan.TotalSeconds);
            Assert.AreEqual(result.resultsFound, result.results.Count());
            Assert.AreEqual(637.999392531314, firstResult.distance);
            Assert.AreEqual("Shakers", firstResult.name);
            Assert.AreEqual("NY", firstResult.adm1Code);
            Assert.AreEqual("001", firstResult.adm2Code);
            Assert.AreEqual("New York", firstResult.adm1Name);
            Assert.AreEqual("Albany County", firstResult.adm2Name);
            Assert.AreEqual("Shakers", firstResult.asciiName);
            Assert.AreEqual("US", firstResult.countryCode);
            Assert.AreEqual(83, firstResult.elevation);
            Assert.AreEqual("P", firstResult.featureClass);
            Assert.AreEqual("PPL", firstResult.featureCode);
            Assert.AreEqual(5137753, firstResult.featureId);
            Assert.AreEqual(85, firstResult.gtopo30);
            Assert.AreEqual(0, firstResult.population);
            Assert.AreEqual("America/New_York", firstResult.timezone);
            Assert.AreEqual(42.73952102661133, firstResult.latitude);
            Assert.AreEqual(-73.81123352050781, firstResult.longitude);
            Assert.AreEqual("city", firstResult.placeType);
            Assert.AreEqual(158912974, firstResult.openStreetMapId);
            Assert.AreEqual("https://www.google.fr/maps/preview#!q=42.76952102661133+-73.81123352050781", firstResult.googleMapUrl);
            Assert.AreEqual("http://maps.yahoo.com/place/?lat=42.73952102661133&amp;lon=-73.81123352050781", firstResult.yahooMapUrl);
            Assert.AreEqual("http://www.openstreetmap.org/#map=14/42.73952102661133/-73.81123352050781", firstResult.openStreetMapUrl);
            Assert.AreEqual("/images/flags/US.png", firstResult.countryFlagUrl);
            Assert.AreEqual("hamlet", firstResult.amenity);

            Console.WriteLine(result.ToString());
        }
        #endregion

        #region Autocomplete Tests
        [Test]
        public void VerifyAutocompleteNoTest()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentNullException>(() => gis.autocomplete(null), "Search text cannot be blank");
        }

        [Test]
        public void VerifyAutocompleteInvalidLatHigh()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.autocomplete(ALBANY_AUTOCOMPLETE, false, null, 100, 0), "Latitude must be between -90 an 90");
        }

        [Test]
        public void VerifyAutocompleteInvalidLatLow()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.autocomplete(ALBANY_AUTOCOMPLETE, false, null, -100, 0), "Latitude must be between -90 an 90");
        }

        [Test]
        public void VerifyAutocompleteInvalidLngHigh()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.autocomplete(ALBANY_AUTOCOMPLETE, false, null, 0, 200), "Longitude must be between -180 and 180");
        }

        [Test]
        public void VerifyAutocompleteInvalidLngLow()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.autocomplete(ALBANY_AUTOCOMPLETE, false, null, 0, -200), "Longitude must be between -180 and 180");
        }

        [Test]
        public void VerifyAutocompleteLatNoLng()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentNullException>(() => gis.autocomplete(ALBANY_AUTOCOMPLETE, false, null, 0), "If using latitude and longitude, both values must be provided");
        }

        [Test]
        public void VerifyAutocompleteLngNoLat()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentNullException>(() => gis.autocomplete(ALBANY_AUTOCOMPLETE, false, null, null, 0), "If using latitude and longitude, both values must be provided");
        }

        [Test]
        public void VerifyAutocompleteInvalidRadius()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.autocomplete(ALBANY_AUTOCOMPLETE, false, null, 0, 0, -100), "Radius must be greater than 0");
        }

        [Test]
        public void VerifyAutocompleteInvalidCountry()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.autocomplete(ALBANY_AUTOCOMPLETE, false, null, 0, 0, 1000, false, Gisgraphy.Styles.MEDIUM, "United States"), "Country needs to be the ISO 3166 Alpha 2 code");
        }

        [Test]
        public void VerifyAutocompleteInvalidLang()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.autocomplete(ALBANY_AUTOCOMPLETE, false, null, 0, 0, 1000, false, Gisgraphy.Styles.MEDIUM, "US", "English"), "Language needs to be the ISO 639 Alpha 2 or Alpha 3 code");
        }

        [Test]
        public void VerifyAutcompleteTextOnly()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            WaitForApiLimit();

            var result = gis.autocomplete(ALBANY_AUTOCOMPLETE);
            var firstResult = result.response.results.FirstOrDefault();

            lastApiCall = DateTime.Now;

            Assert.Greater(result.response.resultsFound, 0);
            Assert.AreEqual(result.responseHeader.executionTime, result.responseHeader.executionTimeSpan.TotalSeconds);
            Assert.AreEqual(0, result.response.start);
            Assert.AreEqual(result.response.maxScore, firstResult.score);
            Assert.AreEqual(8494005, firstResult.featureId);
            Assert.AreEqual("Albany International Airport Fire Department", firstResult.name);
            Assert.AreEqual(42.74192810058594, firstResult.latitude);
            Assert.AreEqual(-73.8081283569336, firstResult.longitude);
            Assert.AreEqual("Pond", firstResult.placeType);
            Assert.AreEqual("US", firstResult.countryCode);
            Assert.AreEqual("S", firstResult.featureClass);
            Assert.AreEqual("BLDG", firstResult.featureCode);
            Assert.AreEqual("Albany International Airport Fire Department", firstResult.asciiName);
            Assert.AreEqual(83, firstResult.elevation);
            Assert.AreEqual(86, firstResult.gtopo30);
            Assert.AreEqual("America/New_York", firstResult.timezone);
            Assert.AreEqual(0, firstResult.population);
            Assert.AreEqual("United States", firstResult.countryName);
            Assert.AreEqual(55.05506, firstResult.score);

            Console.WriteLine(result.ToString());
        }

        [Test]
        public void VerifyAutocompleteNoTestAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await gis.autocompleteAsync(null), "Search text cannot be blank");
        }

        [Test]
        public void VerifyAutocompleteInvalidLatHighAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.autocompleteAsync(ALBANY_AUTOCOMPLETE, false, null, 100, 0), "Latitude must be between -90 an 90");
        }

        [Test]
        public void VerifyAutocompleteInvalidLatLowAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.autocompleteAsync(ALBANY_AUTOCOMPLETE, false, null, -100, 0), "Latitude must be between -90 an 90");
        }

        [Test]
        public void VerifyAutocompleteInvalidLngHighAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.autocompleteAsync(ALBANY_AUTOCOMPLETE, false, null, 0, 200), "Longitude must be between -180 and 180");
        }

        [Test]
        public void VerifyAutocompleteInvalidLngLowAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.autocompleteAsync(ALBANY_AUTOCOMPLETE, false, null, 0, -200), "Longitude must be between -180 and 180");
        }

        [Test]
        public void VerifyAutocompleteLatNoLngAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await gis.autocompleteAsync(ALBANY_AUTOCOMPLETE, false, null, 0), "If using latitude and longitude, both values must be provided");
        }

        [Test]
        public void VerifyAutocompleteLngNoLatAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await gis.autocompleteAsync(ALBANY_AUTOCOMPLETE, false, null, null, 0), "If using latitude and longitude, both values must be provided");
        }

        [Test]
        public void VerifyAutocompleteInvalidRadiusAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.autocompleteAsync(ALBANY_AUTOCOMPLETE, false, null, 0, 0, -100), "Radius must be greater than 0");
        }

        [Test]
        public void VerifyAutocompleteInvalidCountryAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.autocompleteAsync(ALBANY_AUTOCOMPLETE, false, null, 0, 0, 1000, false, Gisgraphy.Styles.MEDIUM, "United States"), "Country needs to be the ISO 3166 Alpha 2 code");
        }

        [Test]
        public void VerifyAutocompleteInvalidLangAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.autocompleteAsync(ALBANY_AUTOCOMPLETE, false, null, 0, 0, 1000, false, Gisgraphy.Styles.MEDIUM, "US", "English"), "Language needs to be the ISO 639 Alpha 2 or Alpha 3 code");
        }

        [Test]
        public async Task VerifyAutcompleteTextOnlyAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            WaitForApiLimit();

            var result = await gis.autocompleteAsync(ALBANY_AUTOCOMPLETE);
            var firstResult = result.response.results.FirstOrDefault();

            lastApiCall = DateTime.Now;

            Assert.Greater(result.response.resultsFound, 0);
            Assert.AreEqual(result.responseHeader.executionTime, result.responseHeader.executionTimeSpan.TotalSeconds);
            Assert.AreEqual(0, result.response.start);
            Assert.AreEqual(result.response.maxScore, firstResult.score);
            Assert.AreEqual(8494005, firstResult.featureId);
            Assert.AreEqual("Albany International Airport Fire Department", firstResult.name);
            Assert.AreEqual(42.74192810058594, firstResult.latitude);
            Assert.AreEqual(-73.8081283569336, firstResult.longitude);
            Assert.AreEqual("Pond", firstResult.placeType);
            Assert.AreEqual("US", firstResult.countryCode);
            Assert.AreEqual("S", firstResult.featureClass);
            Assert.AreEqual("BLDG", firstResult.featureCode);
            Assert.AreEqual("Albany International Airport Fire Department", firstResult.asciiName);
            Assert.AreEqual(83, firstResult.elevation);
            Assert.AreEqual(86, firstResult.gtopo30);
            Assert.AreEqual("America/New_York", firstResult.timezone);
            Assert.AreEqual(0, firstResult.population);
            Assert.AreEqual("United States", firstResult.countryName);
            Assert.AreEqual(55.05506, firstResult.score);

            Console.WriteLine(result.ToString());
        }
        #endregion

        #region Address Parser Tests
        [Test]
        public void VerifyAddressParserNoAddress()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentNullException>(() => gis.parseAddress(null), "Address is a required parameter");
        }

        [Test]
        public void VerifyAddressParserIncorrectCountryCodeFormat()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.Throws<ArgumentOutOfRangeException>(() => gis.parseAddress(ALBANY_AIRPORT_ADDRESS, "United States"), "Country needs to be the ISO 3166 Alpha 2 code");
        }

        /// <summary>
        /// Might enable this, this feature is pretty bad
        /// </summary>
        public void VerifyAddressParserAddressOnly()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            WaitForApiLimit();

            //Check Albany International Airport
            var result = gis.parseAddress(ALBANY_AIRPORT_POSTAL_ADDRESS);
            var firstResult = result.results.FirstOrDefault();

            lastApiCall = DateTime.Now;

            Assert.Greater(result.resultsFound, 0);
            Assert.AreEqual(result.executionTime, result.executionTimeSpan.TotalSeconds);
            Assert.AreEqual(result.resultsFound, result.results.Count());
            Assert.AreEqual(firstResult.geocodingLevel, "CITY");
            Assert.AreEqual(firstResult.city, "Terminal");
            Assert.AreEqual(firstResult.countryCode, "US");
            Assert.AreEqual(firstResult.confidence, "LOW");

            Console.WriteLine(result.ToString());
        }

        /// <summary>
        /// Might enable this, this feature is pretty bad
        /// </summary>
        public void VerifyAddressParserAddressAndCountry()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            WaitForApiLimit();

            //Check Albany International Airport
            var result = gis.parseAddress(ALBANY_AIRPORT_POSTAL_ADDRESS, "US");
            var firstResult = result.results.FirstOrDefault();

            lastApiCall = DateTime.Now;

            Assert.Greater(result.resultsFound, 0);
            Assert.AreEqual(result.executionTime, result.executionTimeSpan.TotalSeconds);
            Assert.AreEqual(result.resultsFound, result.results.Count());
            Assert.AreEqual(firstResult.geocodingLevel, "CITY");
            Assert.AreEqual(firstResult.city, "Terminal");
            Assert.AreEqual(firstResult.countryCode, "US");
            Assert.AreEqual(firstResult.confidence, "LOW");

            Console.WriteLine(result.ToString());
        }

        [Test]
        public void VerifyAddressParserNoAddressAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await gis.parseAddressAsync(null), "Address is a required parameter");
        }

        [Test]
        public void VerifyAddressParserIncorrectCountryCodeFormatAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await gis.parseAddressAsync(ALBANY_AIRPORT_ADDRESS, "United States"), "Country needs to be the ISO 3166 Alpha 2 code");
        }

        /// <summary>
        /// Might enable this, this feature is pretty bad
        /// </summary>
        /// <returns></returns>
        public async Task VerifyAddressParserAddressOnlyAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            WaitForApiLimit();

            //Check Albany International Airport
            var result = await gis.parseAddressAsync(ALBANY_AIRPORT_POSTAL_ADDRESS);
            var firstResult = result.results.FirstOrDefault();

            lastApiCall = DateTime.Now;

            Assert.Greater(result.resultsFound, 0);
            Assert.AreEqual(result.executionTime, result.executionTimeSpan.TotalSeconds);
            Assert.AreEqual(result.resultsFound, result.results.Count());
            Assert.AreEqual(firstResult.geocodingLevel, "CITY");
            Assert.AreEqual(firstResult.city, "Terminal");
            Assert.AreEqual(firstResult.countryCode, "US");
            Assert.AreEqual(firstResult.confidence, "LOW");

            Console.WriteLine(result.ToString());
        }

        /// <summary>
        /// Might enable this, this feature is pretty bad
        /// </summary>
        /// <returns></returns>
        public async Task VerifyAddressParserAddressAndCountryAsync()
        {
            Gisgraphy gis = new Gisgraphy(SERVER_URL, API_KEY);

            WaitForApiLimit();

            //Check Albany International Airport
            var result = await gis.parseAddressAsync(ALBANY_AIRPORT_POSTAL_ADDRESS, "US");
            var firstResult = result.results.FirstOrDefault();

            lastApiCall = DateTime.Now;

            Assert.Greater(result.resultsFound, 0);
            Assert.AreEqual(result.executionTime, result.executionTimeSpan.TotalSeconds);
            Assert.AreEqual(result.resultsFound, result.results.Count());
            Assert.AreEqual(firstResult.geocodingLevel, "CITY");
            Assert.AreEqual(firstResult.city, "Terminal");
            Assert.AreEqual(firstResult.countryCode, "US");
            Assert.AreEqual(firstResult.confidence, "LOW");

            Console.WriteLine(result.ToString());
        }
        #endregion
    }
}
