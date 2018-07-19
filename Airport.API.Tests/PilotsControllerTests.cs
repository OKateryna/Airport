using System;
using System.Linq;
using System.Threading.Tasks;
using Airport.API.Controllers;
using Airport.BL.Abstractions;
using Airport.BL.Dto.Pilot;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using NUnit.Framework;

namespace Airport.API.Tests
{
    [TestFixture]
    public class PilotsControllerTests
    {
        private PilotsController _pilotsController;

        [SetUp]
        public void Initialize()
        {
            var pilotServiceMock = new Mock<IPilotService>();
            pilotServiceMock.Setup(x => x.GetById(It.Is<int>(i => i != 1 && i != 99)))
                .ReturnsAsync((int id) => new PilotDto
            {
                BirthDate = new DateTime(2001, 12, 12),
                Experience = 5,
                FirstName = "Pilot",
                SecondName = "Tester",
                Id = id
            });

            pilotServiceMock.Setup(x => x.GetById(99)).ReturnsAsync(() => null);

            pilotServiceMock.SetupSequence(x => x.GetById(1))
                .Returns(() => Task.Run(() => new PilotDto
                {
                    BirthDate = new DateTime(2001, 12, 12),
                    Experience = 5,
                    FirstName = "Pilot",
                    SecondName = "Tester",
                    Id = 5
                }))
                .Returns(() => Task.Run(() => new PilotDto
                {
                    BirthDate = new DateTime(2018, 12, 12),
                    Experience = 1,
                    FirstName = "PilotUpdated",
                    SecondName = "TesterUpdated",
                    Id = 5
                }));

            pilotServiceMock.Setup(x => x.Insert(It.IsAny<EditablePilotFields>()))
                .ReturnsAsync(() => 1);

            pilotServiceMock.Setup(x => x.Update(1, It.IsAny<EditablePilotFields>())).ReturnsAsync(
                (int id, EditablePilotFields pilotEditableFields) => { return true; });

            _pilotsController =
                new PilotsController(pilotServiceMock.Object)
                {
                    ControllerContext = {HttpContext = new DefaultHttpContext()}
                };

            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper
                .Setup(
                    x => x.Action(
                        It.IsAny<UrlActionContext>()
                    )
                )
                .Returns("callbackUrl")
                .Verifiable();
            mockUrlHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns((string url, object obj) =>
                {
                    return url;
                });

            _pilotsController.Url = mockUrlHelper.Object;
        }

        [Test]
        public async Task PilotControllerReturnsCorrectId()
        {
            var result = (await _pilotsController.Get(5) as OkObjectResult)?.Value as PilotDto;
            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Id);
        }

        [Test]
        public async Task PilotControllerPostReturnCreatedResult()
        {
            var pilot = new EditablePilotFields
            {
                SecondName = "Test",
                FirstName = "Tester",
                BirthDate = new DateTime(2001, 12, 12),
                Experience = 5
            };

            var createdPilot = await _pilotsController.Post(pilot) as CreatedResult;

            Assert.IsNotNull(createdPilot);
            Assert.AreEqual(201, createdPilot.StatusCode);
        }

        [Test]
        public async Task PilotControllerReturns201Post()
        {
            var pilot = new EditablePilotFields
            {
                SecondName = "Test",
                FirstName = "Tester",
                BirthDate = new DateTime(2001, 12, 12),
                Experience = 5
            };

            var createdPilot = await _pilotsController.Post(pilot) as CreatedResult;

            Assert.IsNotNull(createdPilot);
            Assert.AreEqual(201, createdPilot.StatusCode);
        }

        [Test]
        public async Task PilotControllerReturnsNoContent()
        {
            var result = await _pilotsController.Get(99) as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }

        [Test]
        public async Task PilotControllerPutUpdatesPilot()
        {
            var initialPilot = (await _pilotsController.Get(1) as OkObjectResult)?.Value as PilotDto;

            Assert.IsNotNull(initialPilot);

            var updateSecondName = "TesterUpdated";
            var updateFirstName = "PilotUpdated";
            var updateBirthDate = new DateTime(2018, 12, 12);
            var updateExperience = 1;

            var pilotUpdateRequest = new EditablePilotFields
            {
                SecondName = updateSecondName,
                FirstName = updateFirstName,
                BirthDate = updateBirthDate,
                Experience = updateExperience
            };

            var updateResult = await _pilotsController.Put(1, pilotUpdateRequest) as OkResult;
            Assert.IsNotNull(updateResult);
            Assert.AreEqual(200, updateResult.StatusCode);

            var updatedPilot = (await _pilotsController.Get(1) as OkObjectResult)?.Value as PilotDto;
            Assert.IsNotNull(updatedPilot);

            Assert.AreEqual(updateSecondName, updatedPilot.SecondName);
            Assert.AreEqual(updateFirstName, updatedPilot.FirstName);
            Assert.AreEqual(updateBirthDate, updatedPilot.BirthDate);
            Assert.AreEqual(updateExperience, updatedPilot.Experience);

            Assert.AreEqual(initialPilot.Id, updatedPilot.Id);
            Assert.AreNotEqual(initialPilot.FirstName, updatedPilot.FirstName);
            Assert.AreNotEqual(initialPilot.SecondName, updatedPilot.SecondName);
            Assert.AreNotEqual(initialPilot.BirthDate, updatedPilot.BirthDate);
            Assert.AreNotEqual(initialPilot.Experience, updatedPilot.Experience);
        }

        [Test]
        public async Task PilotControllerPutInvalidIdBadRequest()
        {
            var pilotUpdateRequest = new EditablePilotFields
            {
                BirthDate = new DateTime(2018, 12, 12),
                Experience = 1,
                FirstName = "PilotUpdated",
                SecondName = "TesterUpdated",
            };

            var updateBadRequestResult = await _pilotsController.Put(99, pilotUpdateRequest) as BadRequestResult;
            
            Assert.IsNotNull(updateBadRequestResult);
            Assert.AreEqual(400, updateBadRequestResult.StatusCode);
        }
    }
}