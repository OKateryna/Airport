using System.Threading.Tasks;
using Airport.BL.Dto.Pilot;
using Airport.BL.Services;
using Airport.DAL.Abstractions;
using Airport.DAL.Models;
using AutoMapper;
using Moq;
using NUnit.Framework;

namespace Airport.BL.Tests
{
    public class PilotServiceTests
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;
        private const int NotExistingPilotId = 99;
        
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
                Experience = any.Experience
            });
            mockedMapper.Setup(x => x.Map<Pilot>(It.IsAny<EditablePilotFields>())).Returns((EditablePilotFields any) => new Pilot
            {
                SecondName = any.SecondName,
                FirstName = any.FirstName,
                BirthDate = any.BirthDate,
                Experience = any.Experience
            });

            _mapper = mockedMapper.Object;
            _unitOfWork = MoqSetup.GetFakeUnitOfWork();
        }

        [Test]
        public async Task ReturnsDtoWithCorrectId()
        {
            var pilotService = new PilotService(_unitOfWork, _mapper);

            var pilot = await pilotService.GetById(5);
            Assert.AreEqual(5, pilot.Id);
        }

        [Test]
        public async Task GetAllRetrunsResults()
        {
            var pilotService = new PilotService(_unitOfWork, _mapper);

            var pilots = await pilotService.GetAll();
            Assert.IsNotNull(pilots);
            Assert.IsNotEmpty(pilots);
        }

        [Test]
        public async Task UpdateReturnsTrue()
        {
            var pilotService = new PilotService(_unitOfWork, _mapper);

            var result = await pilotService.Update(1, new EditablePilotFields());
            Assert.IsTrue(result);
        }

        public async Task UpdateUnExistingEntityReturnsFalse()
        {
            var pilotService = new PilotService(_unitOfWork, _mapper);

            var result = await pilotService.Update(NotExistingPilotId, new EditablePilotFields());
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DeleteReturnsTrue()
        {
            var pilotService = new PilotService(_unitOfWork, _mapper);

            var result = await pilotService.Delete(1);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteUnExistingEntityReturnsFalse()
        {
            var pilotService = new PilotService(_unitOfWork, _mapper);

            var result = await pilotService.Delete(NotExistingPilotId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task InsertReturnsId()
        {
            var pilotService = new PilotService(_unitOfWork, _mapper);

            var result = await pilotService.Insert(new EditablePilotFields());
            Assert.AreNotEqual(0, result);
            Assert.Greater(result, 0);
        }
    }
}