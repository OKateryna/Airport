using System;
using System.Net.Http;
using System.Text;
using Airport.BL.Dto.Pilot;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Airport.API.FunctionalTests
{
    [TestFixture]
    public class AirportApiFunctionalTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public AirportApiFunctionalTests()
        {
            
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Test]
        public void PostPilotRequestValidation()
        {
            var pilot = new EditablePilotFields
            {
                SecondName = "Test",
                FirstName = "Pilot",
                BirthDate = DateTime.Now.AddYears(-100)
            };

            var content = new StringContent(JsonConvert.SerializeObject(pilot), Encoding.UTF8, "application/json");
            var response = _client.PostAsync("/api/pilots", content).Result;

            Assert.IsNotNull(response);
        }


    }
}