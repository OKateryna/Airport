using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Airport.BL.Dto.Pilot;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Airport.API.FunctionalTests
{
    [TestFixture]
    public class PilotControllerApiFunctionalTests
    {
        private readonly HttpClient _client;

        public PilotControllerApiFunctionalTests()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Test]
        public async Task PostPilotRequestValidation()
        {
            var pilot = new EditablePilotFields
            {
                SecondName = "Test",
                FirstName = "Pilot",
                BirthDate = DateTime.Now.AddYears(-100)
            };

            var content = new StringContent(JsonConvert.SerializeObject(pilot), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/pilots", content);

            Assert.IsNotNull(response);

            var contentResponse = await response.Content.ReadAsStringAsync();
            Assert.IsNotNull(contentResponse);
        }

        [Test]
        public async Task PostPilotRequestCreatesPilot()
        {
            var pilot = new EditablePilotFields
            {
                SecondName = "Test",
                FirstName = "Pilot",
                BirthDate = DateTime.Now.AddYears(-25),
                Experience = 5
            };
            var content = new StringContent(JsonConvert.SerializeObject(pilot), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/pilots", content);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var createdObjectString = await response.Content.ReadAsStringAsync();

            var insertedPilot = JsonConvert.DeserializeObject<PilotDto>(createdObjectString);

            Assert.IsNotNull(insertedPilot);
            Assert.Greater(insertedPilot.Id, 0);
            Assert.AreEqual(pilot.FirstName, insertedPilot.FirstName);
            Assert.AreEqual(pilot.SecondName, insertedPilot.SecondName);
            Assert.AreEqual(pilot.Experience, insertedPilot.Experience);
            Assert.AreEqual(pilot.BirthDate, insertedPilot.BirthDate);
        }

        [Test]
        [TestCase(1, "Ihor", "Melnyk", "1997/09/30", 3)]
        [TestCase(2, "Oleh", "Verbytskyi", "1992/02/3", 3)]
        [TestCase(3, "Maksym", "Kvashckuk", "1993/01/12", 3)]
        [TestCase(4, "Olha", "Herasymyuk", "1977/03/12", 3)]
        public async Task PutPilotRequestUpdatesPilot(int id, string firstName, string secondName, DateTime birthDate, int experience)
        {
            var pilot = new EditablePilotFields
            {
                SecondName = firstName,
                FirstName = secondName,
                BirthDate = birthDate,
                Experience = experience
            };

            var content = new StringContent(JsonConvert.SerializeObject(pilot), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/api/pilots/{id}", content);

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [Test]
        public async Task GetPilotRequestReturnsNotEmptyPilotDtoWithCorrectId(int id)
        {
            var response = await _client.GetAsync($"/api/pilots/{id}");

            Assert.IsNotNull(response);
            var retrievedPilotDtoString = await response.Content.ReadAsStringAsync();

            var insertedPilot = JsonConvert.DeserializeObject<PilotDto>(retrievedPilotDtoString);
            Assert.IsNotNull(insertedPilot);
            Assert.AreEqual(id, insertedPilot.Id);
        }

        [TestCase("Ihor", "Melnyk", "1997/09/30", 3)]
        [TestCase("Oleh", "Verbytskyi", "1992/02/3", 3)]
        [TestCase("Maksym", "Kvashckuk", "1993/01/12", 3)]
        [TestCase("Olha", "Herasymyuk", "1977/03/12", 3)]
        public async Task DeletePilotRequestReturnsSuccess(string firstName, string secondName, DateTime birthDate,
            int experience)
        {
            var pilot = new EditablePilotFields
            {
                SecondName = firstName,
                FirstName = secondName,
                BirthDate = birthDate,
                Experience = experience
            };
            var insertedId = await InsertPilot(pilot);

            var response = await _client.DeleteAsync($"/api/pilots/{insertedId}");
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task DeletePilotRequestReturnsBadResponse()
        {
            var response = await _client.DeleteAsync("/api/pilots/999999");

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        private async Task<int> InsertPilot(EditablePilotFields pilotFields)
        {
            var content = new StringContent(JsonConvert.SerializeObject(pilotFields), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/pilots", content);
            var createdObjectString = await response.Content.ReadAsStringAsync();

            var insertedPilot = JsonConvert.DeserializeObject<PilotDto>(createdObjectString);
            return insertedPilot.Id;
        }
    }
}