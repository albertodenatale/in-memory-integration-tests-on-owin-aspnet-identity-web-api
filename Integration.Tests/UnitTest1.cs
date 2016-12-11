using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Web.Http;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Integration.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            using (var server = TestServer.Create<Startup>())
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/token"))
            {
                var formData = new List<KeyValuePair<string, string>>();
                formData.Add(new KeyValuePair<string, string>("grant_type", "password"));
                formData.Add(new KeyValuePair<string, string>("username", "alberto"));
                formData.Add(new KeyValuePair<string, string>("password", "alberto"));

                request.Content = new FormUrlEncodedContent(formData);

                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                server.HttpClient
                      .DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                using (HttpResponseMessage response = server.HttpClient.SendAsync(request, CancellationToken.None).Result)
                {
                    response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
