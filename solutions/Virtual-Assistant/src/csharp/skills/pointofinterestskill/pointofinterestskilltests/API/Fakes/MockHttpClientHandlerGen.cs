﻿using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PointOfInterestSkillTests.API.Fakes
{
    public class MockHttpClientHandlerGen
    {
        private readonly HttpClientHandler httpClientHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockHttpClientHandlerGen"/> class.
        /// </summary>
        public MockHttpClientHandlerGen()
        {
            this.httpClientHandler = this.GenerateMockHttpClientHandler();
        }

        public HttpClientHandler GetMockHttpClientHandler()
        {
            return this.httpClientHandler;
        }

        private HttpClientHandler GenerateMockHttpClientHandler()
        {
            var mockClient = new Mock<HttpClientHandler>(MockBehavior.Strict);
            this.SetHttpMockBehavior(ref mockClient);
            return mockClient.Object;
        }

        private void SetHttpMockBehavior(ref Mock<HttpClientHandler> mockClient)
        {
            mockClient
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
               MockData.SendAsync,
               ItExpr.Is<HttpRequestMessage>(r => r.RequestUri.ToString().StartsWith("https://atlas.microsoft.com/search/nearby/json")),
               ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(() => new HttpResponseMessage()
               {
                   Content = new StringContent(this.GetAzureMapsPointOfInterest()),
               });

            mockClient
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
               MockData.SendAsync,
               ItExpr.Is<HttpRequestMessage>(r => r.RequestUri.ToString().StartsWith("https://atlas.microsoft.com/search/fuzzy/json")),
               ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(() => new HttpResponseMessage()
               {
                   Content = new StringContent(this.GetAzureMapsPointOfInterest()),
               });

            mockClient
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
               MockData.SendAsync,
               ItExpr.Is<HttpRequestMessage>(r => r.RequestUri.ToString().StartsWith("https://atlas.microsoft.com/route/directions/json")),
               ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(() => new HttpResponseMessage()
               {
                   Content = new StringContent(this.GetRouteDirections()),
               });

            mockClient
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
               MockData.SendAsync,
               ItExpr.Is<HttpRequestMessage>(r => r.RequestUri.ToString().StartsWith("https://api.foursquare.com/v2/venues/search")),
               ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(() => new HttpResponseMessage()
               {
                   Content = new StringContent(this.GetFoursquarePointOfInterest()),
               });

            mockClient
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
               MockData.SendAsync,
               ItExpr.Is<HttpRequestMessage>(r => r.RequestUri.ToString().StartsWith("https://api.foursquare.com/v2/search/explore")),
               ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(() => new HttpResponseMessage()
               {
                   Content = new StringContent(this.GetFoursquarePointOfInterest()),
               });

            mockClient
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
               MockData.SendAsync,
               ItExpr.Is<HttpRequestMessage>(r => r.RequestUri.ToString().StartsWith("https://api.foursquare.com/v2/venues/")),
               ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(() => new HttpResponseMessage()
               {
                   Content = new StringContent(this.GetFoursquarePointOfInterest()),
               });
        }

        private string GetAzureMapsPointOfInterest()
        {
            return "{\"summary\":{\"queryType\":\"NEARBY\",\"queryTime\":35,\"numResults\":3,\"offset\":0,\"totalResults\":29711,\"fuzzyLevel\":1,\"geoBias\":{\"lat\":47.63962,\"lon\":-122.13061}},\"results\":[{\"type\":\"POI\",\"id\":\"US/POI/p1/101761\",\"score\":-0.011,\"dist\":11.162404707265612,\"info\":\"search:ta:840539001321263-US\",\"poi\":{\"name\":\"Microsoft Way\",\"categories\":[\"bus stop\",\"public transport stop\"],\"classifications\":[{\"code\":\"PUBLIC_TRANSPORT_STOP\",\"names\":[{\"nameLocale\":\"en-US\",\"name\":\"bus stop\"},{\"nameLocale\":\"en-US\",\"name\":\"public transport stop\"}]}]},\"address\":{\"streetName\":\"157th Ave NE\",\"municipality\":\"Redmond\",\"countrySecondarySubdivision\":\"King\",\"countryTertiarySubdivision\":\"Seattle East\",\"countrySubdivision\":\"WA\",\"postalCode\":\"98052\",\"extendedPostalCode\":\"980525396\",\"countryCode\":\"US\",\"country\":\"United States Of America\",\"countryCodeISO3\":\"USA\",\"freeformAddress\":\"157th Ave NE, Redmond, WA 98052\",\"countrySubdivisionName\":\"Washington\"},\"position\":{\"lat\":47.63954,\"lon\":-122.1307},\"viewport\":{\"topLeftPoint\":{\"lat\":47.64044,\"lon\":-122.13203},\"btmRightPoint\":{\"lat\":47.63864,\"lon\":-122.12937}},\"entryPoints\":[{\"type\":\"main\",\"position\":{\"lat\":47.63954,\"lon\":-122.1306}}]},{\"type\":\"POI\",\"id\":\"US/POI/p0/5994875\",\"score\":-0.025,\"dist\":24.611388831355395,\"info\":\"search:ta:840539001904749-US\",\"poi\":{\"name\":\"Intentional Software Corporation\",\"phone\":\"+(1)-(425)-8220700\",\"url\":\"www.intentsoft.com\",\"categories\":[\"company\",\"computer data services\"],\"classifications\":[{\"code\":\"COMPANY\",\"names\":[{\"nameLocale\":\"en-US\",\"name\":\"company\"},{\"nameLocale\":\"en-US\",\"name\":\"computer data services\"}]}]},\"address\":{\"streetNumber\":\"1\",\"streetName\":\"Microsoft Way\",\"municipality\":\"Redmond\",\"countrySecondarySubdivision\":\"King\",\"countryTertiarySubdivision\":\"Seattle East\",\"countrySubdivision\":\"WA\",\"postalCode\":\"98052\",\"extendedPostalCode\":\"980526399\",\"countryCode\":\"US\",\"country\":\"United States Of America\",\"countryCodeISO3\":\"USA\",\"freeformAddress\":\"1 Microsoft Way, Redmond, WA 98052\",\"countrySubdivisionName\":\"Washington\"},\"position\":{\"lat\":47.63967,\"lon\":-122.13029},\"viewport\":{\"topLeftPoint\":{\"lat\":47.64057,\"lon\":-122.13162},\"btmRightPoint\":{\"lat\":47.63877,\"lon\":-122.12896}},\"entryPoints\":[{\"type\":\"main\",\"position\":{\"lat\":47.63962,\"lon\":-122.13029}}]},{\"type\":\"POI\",\"id\":\"US/POI/p0/4207658\",\"score\":-0.058,\"dist\":57.86137366890676,\"info\":\"search:ta:840539001255513-US\",\"poi\":{\"name\":\"Microsoft Corporation\",\"phone\":\"+(1)-(425)-4217900\",\"categories\":[\"electrical, office it: computer computer supplies\",\"shop\"],\"classifications\":[{\"code\":\"SHOP\",\"names\":[{\"nameLocale\":\"en-US\",\"name\":\"electrical, office it: computer computer supplies\"},{\"nameLocale\":\"en-US\",\"name\":\"shop\"}]}]},\"address\":{\"streetNumber\":\"3635\",\"streetName\":\"157th Ave NE\",\"municipality\":\"Redmond\",\"countrySecondarySubdivision\":\"King\",\"countryTertiarySubdivision\":\"Seattle East\",\"countrySubdivision\":\"WA\",\"postalCode\":\"98052\",\"extendedPostalCode\":\"980525449\",\"countryCode\":\"US\",\"country\":\"United States Of America\",\"countryCodeISO3\":\"USA\",\"freeformAddress\":\"3635 157th Ave NE, Redmond, WA 98052\",\"countrySubdivisionName\":\"Washington\"},\"position\":{\"lat\":47.63966,\"lon\":-122.13138},\"viewport\":{\"topLeftPoint\":{\"lat\":47.64056,\"lon\":-122.13271},\"btmRightPoint\":{\"lat\":47.63876,\"lon\":-122.13005}},\"entryPoints\":[{\"type\":\"main\",\"position\":{\"lat\":47.63966,\"lon\":-122.1306}}]}]}";
        }

        private string GetRouteDirections()
        {
            return "{  \"formatVersion\": \"0.0.12\",  \"copyright\": \"Copyright 2017 TomTom International BV. All rights reserved. This navigation data is the proprietary copyright of TomTom International BV and may be used only in accordance with the terms of a fully executed license agreement entered into between TomTom International BV, or an authorised reseller and yourself. If you have not entered into such a license agreement you are not authorised to use this data in any manner and should immediately return it to TomTom International BV.\",  \"privacy\": \"TomTom keeps information that tells us how and when you use our services. This includes information about the device you are using and the information we receive while you use the service, such as locations, routes, destinations and search queries. TomTom is unable to identify you based on the information it collects, and will not try to. TomTom uses the information for technical diagnostics, to detect fraud and abuse, to create usage reports, and to improve its services. The information is kept only for these purposes and for a limited period of time, after which it is destroyed. TomTom applies security methods based on industry standards to protect the information against unauthorised access. TomTom will not give anyone else access to the information or use it for any other purpose, unless explicitly and lawfully ordered to do so following due legal process. You can find out more at http://tomtom.com/privacy. You can contact TomTom by going to http://tomtom.com/support.\",  \"routes\": [    {      \"summary\": {        \"lengthInMeters\": 1147,        \"travelTimeInSeconds\": 162,        \"trafficDelayInSeconds\": 0,        \"departureTime\": \"2017-09-07T16:56:58+00:00\",        \"arrivalTime\": \"2017-09-07T16:59:40+00:00\"      },      \"legs\": [        {          \"summary\": {            \"lengthInMeters\": 1147,            \"travelTimeInSeconds\": 162,            \"trafficDelayInSeconds\": 0,            \"departureTime\": \"2017-09-07T16:56:58+00:00\",            \"arrivalTime\": \"2017-09-07T16:59:40+00:00\"        },          \"points\": [            {              \"latitude\": 52.50931,              \"logitude\": 13.42937            },            {              \"latitude\": 52.50904,              \"longitude\": 13.42912            },            {              \"latitude\": 52.50894,              \"longitude\": 13.42904            },            {              \"latitude\": 52.50867,              \"longitude\": 13.42879            },            {              \"latitude\": 52.5084,              \"longitude\": 13.42857            },            {              \"latitude\": 52.50791,              \"longitude\": 13.42824            },            {              \"latitude\": 52.50757,              \"longitude\": 13.42772            },            {              \"latitude\": 52.50735,              \"longitude\": 13.42823            },            {              \"latitude\": 52.5073,              \"longitude\": 13.42836            },            {              \"latitude\": 52.50573,              \"longitude\": 13.43194            },            {              \"latitude\": 52.50512,              \"longitude\": 13.43336            },            {              \"latitude\": 52.50464,              \"longitude\": 13.43451            },            {              \"latitude\": 52.5045,              \"longitude\": 13.43481            },            {              \"latitude\": 52.50443,              \"longitude\": 13.43498            },            {              \"latitude\": 52.50343,              \"longitude\": 13.43737            },            {              \"latitude\": 52.50274,              \"longitude\": 13.43872            }          ]        }      ],      \"sections\": [        {          \"startPointIndex\": 0,          \"endPointIndex\": 15,          \"sectionType\": \"TRAVEL_MODE\",          \"travelMode\": \"car\"        }      ]    }  ]}";
        }

        private string GetFoursquarePointOfInterest()
        {
            return "{  \"meta\": {    \"code\": 200,    \"requestId\": \"59a45921351e3d43b07028b5\"  }, \"response\": {    \"venue\": { \"id\": \"412d2800f964a520df0c1fe3\", \"name\": \"Central Park\",      \"contact\": {        \"phone\": \"2123106600\",        \"formattedPhone\": \"(212) 310-6600\", \"twitter\": \"centralparknyc\", \"instagram\": \"centralparknyc\", \"facebook\": \"37965424481\", \"facebookUsername\": \"centralparknyc\", \"facebookName\": \"Central Park\" }, \"location\": { \"address\": \"59th St to 110th St\", \"crossStreet\": \"5th Ave to Central Park West\", \"lat\": 40.78408342593807, \"lng\": -73.96485328674316, \"postalCode\": \"10028\", \"cc\": \"US\", \"city\": \"New York\", \"state\": \"NY\", \"country\": \"United States\", \"formattedAddress\": [ \"59th St to 110th St (5th Ave to Central Park West)\", \"New York, NY 10028\", \"United States\" ] }, \"canonicalUrl\": \"https://foursquare.com/v/central-park/412d2800f964a520df0c1fe3\", \"categories\": [ { \"id\": \"4bf58dd8d48988d163941735\", \"name\": \"Park\", \"pluralName\": \"Parks\", \"shortName\": \"Park\", \"icon\": { \"prefix\": \"https://ss3.4sqi.net/img/categories_v2/parks_outdoors/park_\", \"suffix\": \".png\" }, \"primary\": true } ], \"verified\": true, \"stats\": { \"checkinsCount\": 364591, \"usersCount\": 311634, \"tipCount\": 1583, \"visitsCount\": 854553 }, \"url\": \"http://www.centralparknyc.org\", \"likes\": { \"count\": 17370, \"summary\": \"17370 Likes\" }, \"rating\": 9.8, \"ratingColor\": \"00B551\", \"ratingSignals\": 18854, \"beenHere\": { \"count\": 0, \"unconfirmedCount\": 0, \"marked\": false, \"lastCheckinExpiredAt\": 0 }, \"photos\": { \"count\": 26681, \"groups\": [ { \"type\": \"venue\", \"name\": \"Venue photos\", \"count\": 26681, \"items\": [ { \"id\": \"513bd223e4b0e8ef8292ee54\", \"createdAt\": 1362874915, \"source\": { \"name\": \"Instagram\", \"url\": \"http://instagram.com\" }, \"prefix\": \"https://igx.4sqi.net/img/general/\", \"suffix\": \"/655018_Zp3vA90Sy4IIDApvfAo5KnDItoV0uEDZeST7bWT-qzk.jpg\", \"width\": 612, \"height\": 612, \"user\": { \"id\": \"123456\", \"firstName\": \"John\", \"lastName\": \"Doe\", \"gender\": \"male\" }, \"visibility\": \"public\" } ] } ] }, \"description\": \"Central Park is the 843-acre green heart of Manhattan and is maintained by the Central Park Conservancy. It was designed in the 19th century by Frederick Law Olmsted and Calvert Vaux as an urban escape for New Yorkers, and now receives over 40 million visits per year.\", \"storeId\": \"\", \"page\": { \"pageInfo\": { \"description\": \"The mission of the Central Park Conservancy, a private non-profit, is to restore, manage, and enhance Central Park, in partnership with the public.\", \"banner\": \"https://is1.4sqi.net/userpix/HS2JAA2IAAAR2WZO.jpg\", \"links\": { \"count\": 1, \"items\": [ { \"url\": \"http://www.centralparknyc.org\" } ] } }, \"user\": { \"id\": \"29060351\", \"firstName\": \"Central Park\", \"gender\": \"none\", \"photo\": { \"prefix\": \"https://igx.4sqi.net/img/user/\", \"suffix\": \"/PCPGGJ2N3ULA5O05.jpg\" }, \"type\": \"chain\", \"tips\": { \"count\": 37 }, \"lists\": { \"groups\": [ { \"type\": \"created\", \"count\": 2, \"items\": [] } ] }, \"homeCity\": \"New York, NY\", \"bio\": \"\", \"contact\": { \"twitter\": \"centralparknyc\", \"facebook\": \"37965424481\" } } }, \"hereNow\": { \"count\": 16, \"summary\": \"16 people are here\", \"groups\": [ { \"type\": \"others\", \"name\": \"Other people here\", \"count\": 16, \"items\": [] } ] }, \"createdAt\": 1093478400, \"tips\": { \"count\": 1583, \"groups\": [ { \"type\": \"others\", \"name\": \"All tips\", \"count\": 1583, \"items\": [ { \"id\": \"5150464ee4b02f70eb28eee4\", \"createdAt\": 1364215374, \"text\": \"Did you know? To create that feeling of being in the countryside, and not in the middle of a city, the four Transverse Roads were sunken down eight feet below the park’s surface.\", \"type\": \"user\", \"canonicalUrl\": \"https://foursquare.com/item/5150464ee4b02f70eb28eee4\", \"photo\": { \"id\": \"5150464f52625adbe29d04c2\", \"createdAt\": 1364215375, \"source\": { \"name\": \"Foursquare Web\", \"url\": \"https://foursquare.com\" }, \"prefix\": \"https://igx.4sqi.net/img/general/\", \"suffix\": \"/13764780_Ao02DfJpgG1ar2PfgP51hOKWsn38iai8bsSpzKd0GcM.jpg\", \"width\": 800, \"height\": 542, \"visibility\": \"public\" }, \"photourl\": \"https://igx.4sqi.net/img/general/original/13764780_Ao02DfJpgG1ar2PfgP51hOKWsn38iai8bsSpzKd0GcM.jpg\", \"lang\": \"en\", \"likes\": { \"count\": 247, \"groups\": [ { \"type\": \"others\", \"count\": 247, \"items\": [] } ], \"summary\": \"247 likes\" }, \"logView\": true, \"agreeCount\": 246, \"disagreeCount\": 0, \"todo\": { \"count\": 30 }, \"user\": { \"id\": \"13764780\", \"firstName\": \"City of New York\", \"gender\": \"none\", \"photo\": { \"prefix\": \"https://igx.4sqi.net/img/user/\", \"suffix\": \"/2X1FKJPUY3DGRRK3.png\" }, \"type\": \"page\" } }, { \"id\": \"522afa5b11d2740e9aeeb336\", \"createdAt\": 1378548315, \"text\": \"Lots of squirrels in the park! パーク内にはリスがたくさんいます！しかも思ったよりデカイです。\", \"type\": \"user\", \"logView\": true, \"editedAt\": 1399418942, \"agreeCount\": 61, \"disagreeCount\": 0, \"todo\": { \"count\": 1 }, \"user\": { \"id\": \"5053872\", \"firstName\": \"Nnkoji\", \"gender\": \"male\", \"photo\": { \"prefix\": \"https://igx.4sqi.net/img/user/\", \"suffix\": \"/5053872-DUZ51RAOUVH3GU33.jpg\" } }, \"authorInteractionType\": \"liked\" }, { \"id\": \"4cd5bda1b6962c0fd19c2e96\", \"createdAt\": 1289076129, \"text\": \"PHOTO: 1975 was the last year the New York City marathon was raced entirely inside Central Park. In this photo, runners at the marathon starting line.\", \"type\": \"user\", \"url\": \"http://www.nydailynewspix.com/sales/largeview.php?name=87g0km0g.jpg&id=152059&lbx=-1&return_page=searchResults.php&page=2\", \"canonicalUrl\": \"https://foursquare.com/item/4cd5bda1b6962c0fd19c2e96\", \"lang\": \"en\", \"likes\": { \"count\": 26, \"groups\": [ { \"type\": \"others\", \"count\": 26, \"items\": [] } ], \"summary\": \"26 likes\" }, \"logView\": true, \"agreeCount\": 25, \"disagreeCount\": 0, \"todo\": { \"count\": 16 }, \"user\": { \"id\": \"1241858\", \"firstName\": \"The New York Daily News\", \"gender\": \"none\", \"photo\": { \"prefix\": \"https://igx.4sqi.net/img/user/\", \"suffix\": \"/3EV01452MGIUWBAQ.jpg\" }, \"type\": \"page\" } } ] } ] }, \"shortUrl\": \"http://4sq.com/2UsPUp\", \"timeZone\": \"America/New_York\", \"listed\": { \"count\": 5731, \"groups\": [ { \"type\": \"others\", \"name\": \"Lists from other people\", \"count\": 5731, \"items\": [ { \"id\": \"4fad24a2e4b0bcc0c18be03c\", \"name\": \"101 places to see in Manhattan before you die\", \"description\": \"Best spots to see in Manhattan (New York City) as restaurants, monuments and public spaces. Enjoy!\", \"type\": \"others\", \"user\": { \"id\": \"356747\", \"firstName\": \"John\", \"lastName\": \"Doe\", \"gender\": \"male\", \"photo\": { \"prefix\": \"https://igx.4sqi.net/img/user/\", \"suffix\": \"/356747-WQOTM2ASOIERONL3.jpg\" } }, \"editable\": false, \"public\": true, \"collaborative\": false, \"url\": \"/boke/list/101-places-to-see-in-manhattan-before-you-die\", \"canonicalUrl\": \"https://foursquare.com/boke/list/101-places-to-see-in-manhattan-before-you-die\", \"createdAt\": 1336747170, \"updatedAt\": 1406242886, \"photo\": { \"id\": \"4fa97b0c121d8a3faef6f2df\", \"createdAt\": 1336507148, \"prefix\": \"https://igx.4sqi.net/img/general/\", \"suffix\": \"/IcmBihQCVr4Zt0Vxt9l237NHv--nxg1Z5_8QIMjeD8E.jpg\", \"width\": 325, \"height\": 487, \"user\": { \"id\": \"13125997\", \"firstName\": \"IWalked Audio Tours\", \"gender\": \"none\", \"photo\": { \"prefix\": \"https://igx.4sqi.net/img/user/\", \"suffix\": \"/KZCTVBJ0FXUHSQA5.jpg\" }, \"type\": \"page\" }, \"visibility\": \"public\" }, \"followers\": { \"count\": 944 }, \"listItems\": { \"count\": 101, \"items\": [ { \"id\": \"t4b67904a70c603bb845291b4\", \"createdAt\": 1336747293, \"photo\": { \"id\": \"4faa9dd9e4b01bd5523d1de8\", \"createdAt\": 1336581593, \"prefix\": \"https://igx.4sqi.net/img/general/\", \"suffix\": \"/KaAuGPKMZev1Te0uucRYHk92RiULGj3-GYWkX_zXbjM.jpg\", \"width\": 720, \"height\": 532, \"visibility\": \"public\" } } ] } } ] } ] }, \"phrases\": [ { \"phrase\": \"parque todo\", \"sample\": { \"entities\": [ { \"indices\": [ 22, 33 ], \"type\": \"keyPhrase\" } ], \"text\": \"... a ponta, curtir o parque todo, sem pressa, admirando cada lugar. Se puder...\" }, \"count\": 4 } ], \"hours\": { \"status\": \"Open until 1:00 AM\", \"isOpen\": true, \"isLocalHoliday\": false, \"timeframes\": [ { \"days\": \"Mon–Sun\", \"includesToday\": true, \"open\": [ { \"renderedTime\": \"6:00 AM–1:00 AM\" } ], \"segments\": [] } ] }, \"popular\": { \"status\": \"Likely open\", \"isOpen\": true, \"isLocalHoliday\": false, \"timeframes\": [ { \"days\": \"Tue–Thu\", \"open\": [ { \"renderedTime\": \"Noon–8:00 PM\" } ], \"segments\": [] }, { \"days\": \"Fri\", \"open\": [ { \"renderedTime\": \"11:00 AM–7:00 PM\" } ], \"segments\": [] }, { \"days\": \"Sat\", \"open\": [ { \"renderedTime\": \"8:00 AM–8:00 PM\" } ], \"segments\": [] }, { \"days\": \"Sun\", \"open\": [ { \"renderedTime\": \"8:00 AM–7:00 PM\" } ], \"segments\": [] } ] }, \"pageUpdates\": { \"count\": 12, \"items\": [] }, \"inbox\": { \"count\": 0, \"items\": [] }, \"venueChains\": [], \"attributes\": { \"groups\": [ { \"type\": \"payments\", \"name\": \"Credit Cards\", \"summary\": \"No Credit Cards\", \"count\": 7, \"items\": [ { \"displayName\": \"Credit Cards\", \"displayValue\": \"No\" } ] } ] }, \"bestPhoto\": { \"id\": \"513bd223e4b0e8ef8292ee54\", \"createdAt\": 1362874915, \"source\": { \"name\": \"Instagram\", \"url\": \"http://instagram.com\" }, \"prefix\": \"https://igx.4sqi.net/img/general/\", \"suffix\": \"/655018_Zp3vA90Sy4IIDApvfAo5KnDItoV0uEDZeST7bWT-qzk.jpg\", \"width\": 612, \"height\": 612, \"visibility\": \"public\" } } } } ";
        }

    }
}