using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiTests.Models;

namespace WebApiTests.Tests
{
    public class StudentQueryTests : IDisposable
    {
        private readonly HttpServer _server;
        private string _url = "http://just-a-dummy-host/";

        public StudentQueryTests()
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            _server = new HttpServer(config);
        }

        [TestClass]
        public class When_Query_For_All_Students : StudentQueryTests
        {
            private HttpClient client;
            private HttpRequestMessage request;

            [TestInitialize]
            public void Init()
            {
                client = new HttpClient(_server);
                request = createRequest("api/v1.0/catalogue/students", "application/json", HttpMethod.Get);
            }

            [TestMethod]
            public void Then_Should_Get_All_Available_Students()
            {
                using (HttpResponseMessage response = client.SendAsync(request).Result)
                {
                    Assert.IsNotNull(response.Content);
                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                    Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
                    Assert.AreEqual(100, response.Content.ReadAsAsync<IEnumerable<Student>>().Result.Count()); 
                }
            }

            [TestCleanup]
            public void TearDown()
            {
                request.Dispose();
            }
        }

        [TestClass]
        public class When_Query_For_An_Existing_Student_By_Name : StudentQueryTests
        {
            private HttpClient client;
            private HttpRequestMessage request;

            [TestInitialize]
            public void Init()
            {
                client = new HttpClient(_server);
                request = createRequest("api/v1.0/catalogue/students/M", "application/json", HttpMethod.Get);
            }

            [TestMethod]
            public void Then_Should_Get_The_Student()
            {
                using (HttpResponseMessage response = client.SendAsync(request).Result)
                {
                    Assert.IsNotNull(response.Content);
                    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                    Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
                    Assert.IsTrue(response.Content.ReadAsAsync<IEnumerable<Student>>().Result.Any());
                }
            }
        }

        private HttpRequestMessage createRequest(string url, string mthv, HttpMethod method)
        {
            var request = new HttpRequestMessage { RequestUri = new Uri(_url + url) };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mthv));
            request.Method = method;

            return request;
        }

        private HttpRequestMessage createRequest<T>(string url, string mthv, HttpMethod method, T content, MediaTypeFormatter formatter) where T : class
        {
            HttpRequestMessage request = createRequest(url, mthv, method);
            request.Content = new ObjectContent<T>(content, formatter);

            return request;
        }

        public void Dispose()
        {
            _server?.Dispose();
        }
    }
}
