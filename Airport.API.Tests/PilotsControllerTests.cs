using System;
using System.Linq;
using System.Threading.Tasks;
using Airport.API.Controllers;
using Airport.BL.Abstractions;
using Airport.BL.Dto.Pilot;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Airport.API.Tests
{
    [TestFixture]
    public class PilotsControllerTests
    {
        private IPilotService _pilotService;

        [SetUp]
        public void Initialize()
        {
            var pilotServiceMock = new Mock<IPilotService>();
            pilotServiceMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) => Task.Run(() => new PilotDto
            {
                BirthDate = DateTime.Now,
                Expierence = 5,
                FirstName = "Pilot",
                SecondName = "Tester",
                Id = id
            }));
            _pilotService = pilotServiceMock.Object;
        }

        [Test]
        public async Task PilotControllerReturnsCorrectId()
        {
            var pilotController = new PilotsController(_pilotService);
            var result = (await pilotController.Get(5) as OkObjectResult)?.Value as PilotDto;
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Id);
        }
    }
}