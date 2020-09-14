using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Net;

namespace BrewdogBeerTests
{
    [TestClass]
    public class BeerChallangeTests
    {
        private const string url = "https://api.punkapi.com/v2/";
        private string requestHeader = "beers";
        RestClient client;
        RestRequest request;
        RestResponse response;

        [TestMethod]
        public void TheBeerChallangeQuest()
        {
            // arrange
            client = new RestClient(url);
            Helper.SetSerialization(client);
            BeerScale bs = new BeerScale(4.0);
            GetBeer gb = new GetBeer("beer_name", "hopped-up");
            BrewTime bt = new BrewTime("12-2015");
            Int32.TryParse(Helper.RemoveUnnecessaryString(bt.When), out int date);

            // act
            request = new RestRequest(requestHeader, Method.GET);
            request.RequestFormat = DataFormat.Json;

            ///summary Information about comma replacement
            /// For some reason when assigning a value to a double variable (ex. 2.0) and putting this into the AddParameter it was switching from 2.0 to 2,0 and it returned an error.
            /// I have created a small method to do a replace for the dot. When I do replace the dot with a comma it suddenly works. 
            ///summary
            request.AddParameter(bs.Abv_gt, Helper.ReplaceCommaWithDot(bs.Value.ToString()));
            request.AddParameter(bt.Brewed_after, bt.When);
            request.AddParameter(gb.ParameterName, gb.BeverageName);

            response = (RestResponse)client.Execute(request);
            string errorMsg = string.Format("{0} !contains {1}, {2}", response.Content, gb.BeverageName, bs.Value.ToString());

            // assert
            if (!string.IsNullOrWhiteSpace(response.Content))
            {
                if (!response.Content.Equals("[]"))
                {
                    Assert.IsTrue(response.Content.ToLower().Contains(bs.Value.ToString()));
                    Assert.IsTrue(response.Content.ToLower().Contains(gb.BeverageName));
                    Assert.IsTrue(response.Content.ToLower().Contains((date + 1).ToString()));
                }
                else
                {
                    Assert.Fail(errorMsg);
                }
            }
            else
            {
                Assert.Fail(errorMsg);
            }
        }

        [TestMethod]
        public void GetBeerUsingBrewTime()
        {
            //arrange
            client = new RestClient(url);
            Helper.SetSerialization(client);
            BrewTime bt = new BrewTime("12-2015");
            Int32.TryParse(Helper.RemoveUnnecessaryString(bt.When), out int date);   // To be removed. This is silly.

            request = new RestRequest(requestHeader, Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter(bt.Brewed_after, bt.When);

            // act
            response = (RestResponse)client.Execute(request);
            string errorMsg = string.Format("{0} !contains {1}", response.Content, bt.When);

            // assert
            if (!string.IsNullOrWhiteSpace(response.Content))
            {
                if (!response.Content.Equals("[]"))
                {
                    Assert.IsTrue(response.Content.Contains((date + 1).ToString()));
                }
            }
            else
            {
                Assert.Fail(errorMsg);
            }
        }

        [TestMethod]
        public void GetBeerUsingBeerScale()
        {
            //arrange
            client = new RestClient(url);
            Helper.SetSerialization(client);
            BeerScale bs = new BeerScale(4.0);

            ///summary Information about comma replacement
            /// For some reason when assigning a value to a double variable (ex. 2.0) and putting this into the AddParameter it was switching from 2.0 to 2,0 and it returned an error.
            /// I have created a small method to do a replace for the dot. When I do replace the dot with a comma it suddenly works. 
            ///summary
            string scale = Helper.ReplaceCommaWithDot(bs.Value.ToString());

            request = new RestRequest(requestHeader, Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter(bs.Ibu_gt, scale);

            // act
            response = (RestResponse)client.Execute(request);
            string errorMsg = string.Format("{0} !contains {1}: {2}", response.Content, bs.Ibu_gt, scale);

            // assert
            if (string.IsNullOrWhiteSpace(response.Content) || response.Content.Contains(scale) || response.Content.Contains(bs.Ibu_gt))
            {
                if (!response.Content.Equals("[]"))
                {
                    Assert.IsTrue(response.Content.Contains(bs.Ibu_gt) || response.Content.Contains(scale));
                }
            }
            else
            {
                Assert.Fail(errorMsg);
            }
        }

        [TestMethod]
        public void GetBeerUsingId()
        {
            //arrange
            client = new RestClient(url);
            Helper.SetSerialization(client);
            GetBeer gb = new GetBeer();
            gb.Id = 50;

            request = new RestRequest(GetBeer.GetBeerById(requestHeader, gb.Id), Method.GET);
            request.RequestFormat = DataFormat.Json;

            // act
            response = (RestResponse)client.Execute(request);
            string errorMsg = string.Format("{0} != {1}", response.Content, gb.Id);
            // assert
            if (response.Content != null && response.Content.Contains(gb.Id.ToString()))
            {
                if (!response.Content.Equals("[]"))
                {
                    Assert.IsTrue(response.Content.Contains(gb.Id.ToString()));
                }
            }
            else
            {
                Assert.Fail(errorMsg);
            }
        }

        [TestMethod]
        public void GetBeerUsingName()
        {
            //arrange
            client = new RestClient(url);
            Helper.SetSerialization(client);
            GetBeer gb = new GetBeer("beer_name", "Wine");

            request = new RestRequest(requestHeader, Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter(gb.ParameterName, gb.BeverageName);

            // act
            response = (RestResponse)client.Execute(request);
            string errorMsg = string.Format("{0} !contains {1}", response.Content, gb.BeverageName);

            // assert
            if (response.Content != null && response.Content.Contains(gb.BeverageName))
            {
                if (!response.Content.Equals("[]"))
                {
                    Assert.IsTrue(response.Content.Contains(gb.BeverageName));
                }
            }
            else
            {
                Assert.Fail(errorMsg);
            }
        }

        [TestMethod]
        public void GetBeerUsingFood()
        {
            //arrange
            client = new RestClient(url);
            Helper.SetSerialization(client);
            GetBeer gb = new GetBeer("food", "fries");

            request = new RestRequest(requestHeader, Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter(gb.ParameterName, gb.BeverageName);

            // act
            response = (RestResponse)client.Execute(request);
            string errorMsg = string.Format("{0} !contains {1}", response.Content, gb.BeverageName);

            // assert
            if (response.Content != null && response.Content.Contains(gb.BeverageName))
            {
                if (!response.Content.Equals("[]"))
                {
                    Assert.IsTrue(response.Content.Contains(gb.BeverageName));
                }
            }
            else
            {
                Assert.Fail(errorMsg);
            }
        }

        [TestMethod]
        public void GetBeerUsingYeast()
        {
            //arrange
            client = new RestClient(url);
            Helper.SetSerialization(client);
            GetBeer gb = new GetBeer("yeast_name", "hop");

            request = new RestRequest(requestHeader, Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter(gb.ParameterName, gb.BeverageName);

            // act
            response = (RestResponse)client.Execute(request);
            string errorMsg = string.Format("{0} !contains {1}", response.Content, gb.BeverageName);

            // assert
            if (response.Content != null && response.Content.Contains(gb.BeverageName))
            {
                if (!response.Content.Equals("[]"))
                {
                    Assert.IsTrue(response.Content.Contains(gb.BeverageName));
                }
            }
            else
            {
                Assert.Fail(errorMsg);
            }
        }

    }

    [TestClass]
    public class NegativeTests
    {
        private const string url = "https://api.punkapi.com/v2/";
        private string requestHeader = "beers";
        RestClient client;
        RestRequest request;
        RestResponse response;

        [TestMethod]
        public void EmptyValueTest()
        {
            //arrange
            client = new RestClient(url);
            Helper.SetSerialization(client);
            GetBeer gb = new GetBeer("beer_name", string.Empty);

            request = new RestRequest(requestHeader, Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter(gb.ParameterName, gb.BeverageName);
            request.AddParameter(gb.ParameterName, gb.BeverageName);
            request.AddParameter(gb.ParameterName, gb.BeverageName);

            Debug.DebugPars(request);

            // act
            response = (RestResponse)client.Execute(request);
            int statusCode = Helper.ReturnHttpStatusCode(response);
            string errorMsg = Helper.ReturnMessage(statusCode, response);

            // assert
            if (!string.IsNullOrWhiteSpace(response.Content))
            {
                if (!response.Content.Equals("[]"))
                {
                    Assert.AreNotEqual(response.Content, gb.BeverageName);
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
                } 
                else
                {
                    Assert.Fail("Error. The response.Content should return [] not {0}", response.Content);
                }
            }
            else
            {
                Assert.Fail(errorMsg);
            }
        }

        [TestMethod]
        public void NullValueTest()
        {
            //arrange
            client = new RestClient(url);
            Helper.SetSerialization(client);
            GetBeer b = new GetBeer("yeast", null);

            request = new RestRequest(requestHeader, Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter(b.ParameterName, b.BeverageName);
            Debug.DebugPars(request);

            // act
            response = (RestResponse)client.Execute(request);
            int statusCode = Helper.ReturnHttpStatusCode(response);
            string errorMsg = Helper.ReturnMessage(statusCode, response);
            Debug.ShowOutput(response.Content);

            // assert
            if (!string.IsNullOrWhiteSpace(response.Content))
            {
                if (!response.Content.Equals("[]"))
                {
                    Assert.AreNotEqual(response.Content, b.BeverageName);
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
                }
                else
                {
                    Assert.Fail("Error. The response.Content should return [] not {0}", response.Content);
                }
            }
            else
            {
                Assert.Fail(errorMsg);
            }
        }

    }

    [TestClass]
    public class ResponseCodeTests
    {
        private const string url = "https://api.punkapi.com/v2/";
        private string requestHeader = "beers";
        RestClient client;
        RestRequest request;
        RestResponse response;

        [TestMethod]
        public void GetResponseFromServer()
        {
            // arrange
            client = new RestClient(url);
            Helper.SetSerialization(client);
            request = new RestRequest(requestHeader, Method.GET);
            request.RequestFormat = DataFormat.Json;

            // act
            response = (RestResponse)client.Execute(request);
            int statusCode = Helper.ReturnHttpStatusCode(response);

            if (!response.IsSuccessful)
            {
                Helper.ReturnMessage(statusCode, response, 1);
            }
        }

        [TestMethod]
        public void CheckResponseOk()
        {
            // arrange
            client = new RestClient(url);
            Helper.SetSerialization(client);
            request = new RestRequest(requestHeader, Method.GET);
            request.RequestFormat = DataFormat.Json;

            // act
            response = (RestResponse)client.Execute(request);
            int statusCode = Helper.ReturnHttpStatusCode(response);
            string errorMsg = Helper.ReturnMessage(statusCode, response, 1);

            // assert
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (!response.Content.Equals("[]"))
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                }
            }
            else
            {
                Assert.Fail(errorMsg);
            }
        }

        public void CheckResponseBadRequest()
        {
            // arrange
            client = new RestClient(url);
            Helper.SetSerialization(client);
            BeerScale bc = new BeerScale(4.0);

            request = new RestRequest(requestHeader, Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter(bc.ScaleType, "x");

            Debug.DebugPars(request);

            // act
            response = (RestResponse)client.Execute(request);
            int statusCode = Helper.ReturnHttpStatusCode(response);
            string errorMsg = Helper.ReturnMessage(statusCode, response, 1);

            // assert
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                if (!response.Content.Equals("[]"))
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.BadRequest);
                }
            }
            else
            {
                Assert.Fail(errorMsg);
            }
        }

        [TestMethod]
        public void CheckResponseInternalError()
        {
            //arrange
            client = new RestClient(url);
            Helper.SetSerialization(client);
            GetBeer gb = new GetBeer("beer_name", "American*");

            request = new RestRequest(requestHeader, Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter(gb.ParameterName, gb.BeverageName);

            // act
            response = (RestResponse)client.Execute(request);
            int statusCode = Helper.ReturnHttpStatusCode(response);
            string errorMsg = Helper.ReturnMessage(statusCode, response, 1);

            // assert
            if (response.Content != null && response.StatusCode == HttpStatusCode.InternalServerError)
            {
                if (!response.Content.Equals("[]"))
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.InternalServerError);
                }
            }
            else
            {
                Assert.Fail(errorMsg);
            }
        }

        public void CheckResponseNotFound()
        {
            // arrange
            client = new RestClient(url);
            Helper.SetSerialization(client);
            requestHeader = "beerz";

            request = new RestRequest(requestHeader, Method.GET);
            request.RequestFormat = DataFormat.Json;

            // act
            response = (RestResponse)client.Execute(request);
            int statusCode = Helper.ReturnHttpStatusCode(response);
            string errorMsg = Helper.ReturnMessage(statusCode, response, 1);

            // assert
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                if (!response.Content.Equals("[]"))
                {
                    Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
                }
            }
            else
            {
                Assert.Fail(errorMsg);
            }
        }
    }

    [TestClass]
    public class ContentTests
    {
        private const string url = "https://api.punkapi.com/v2/";

        [TestMethod]
        public void CheckContentType()
        {
            // arrange
            RestClient client = new RestClient("http://api.zippopotam.us");
            Helper.SetSerialization(client, 1);
            RestRequest request = new RestRequest("beers", Method.GET);

            // act
            RestResponse response = (RestResponse)client.Execute(request);

            // assert
            Assert.AreEqual(response.ContentType, "text/html; charset=UTF-8");
        }
    }

}
