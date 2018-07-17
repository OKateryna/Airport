using System;
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
            pilotServiceMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((int id) => new PilotDto
            {
                BirthDate = DateTime.Now,
                Expierence = 5,
                FirstName = "Pilot",
                SecondName = "Tester",
                Id = id
            });
            _pilotService = pilotServiceMock.Object;
        }

        [Test]
        public void PilotControllerReturnsCorrectId()
        {
            var pilotController = new PilotsController(_pilotService);
            var result = (pilotController.Get(5) as OkObjectResult)?.Value as PilotDto;
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Id);
        }
    }
}