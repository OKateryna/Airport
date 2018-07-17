using Airport.BL.Dto.Pilot;
using Airport.BL.Services;
using Airport.DAL.Models;
using AutoMapper;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Airport.BL.Tests
{
    public class PilotServiceTests
    {
        private IMapper _mapper;

        [SetUp]
        public void Initialize()
        {
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(x => x.Map<PilotDto>(It.IsAny<Pilot>())).Returns((Pilot any) => new PilotDto
            {
                Id = any.Id,
                SecondName = any.SecondName,
                FirstName = any.FirstName,
                BirthDate = any.BirthDate,
                Expierence = any.Expierence
            });
            _mapper = mockedMapper.Object;
        }

        [Test]
        public void ReturnsDtoWithCorrectId()
        {
            var uow = MoqSetup.GetFakeUnitOfWork();
            var pilotService = new PilotService(uow, _mapper);

            var pilot = pilotService.GetById(5);
            Assert.AreEqual(5, pilot.Id);
        }
    }
}