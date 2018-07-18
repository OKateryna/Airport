using System;
using System.Threading.Tasks;
using Airport.DAL;
using Airport.DAL.Abstractions;
using Airport.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Airport.BL.Tests
{
    public static class MoqSetup
    {
        public static IUnitOfWork GetFakeUnitOfWork()
        {
            var mockedPilotRepository = new Mock<IRepository<Pilot>>();
            mockedPilotRepository.Setup(m => m.Get(It.IsAny<int>())).Returns((int i) => Task.Run(() => new Pilot
            {
                BirthDate = new DateTime(2000, 2, 2),
                Expierence = 5,
                FirstName = "Johh",
                SecondName = "Pilot",
                Id = i
            }));
            var mockedDataContext = new Mock<DbContext>();
            return new UnitOfWork(null, null, null, null, mockedPilotRepository.Object, null, null, null, mockedDataContext.Object);
        }
    }
}