using System;
using System.Collections.Generic;
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
        private static int pilotIdCounter = 1;
        public static IUnitOfWork GetFakeUnitOfWork()
        {
            var mockedPilotRepository = new Mock<IRepository<Pilot>>();
            mockedPilotRepository.Setup(p => p.Get(It.Is<int>(x => x != 99))).ReturnsAsync((int i) =>  new Pilot
            {
                BirthDate = new DateTime(2000, 2, 2),
                Experience = 5,
                FirstName = "Johh",
                SecondName = "Pilot",
                Id = i
            });

            mockedPilotRepository.Setup(p => p.GetAll()).ReturnsAsync(() => new List<Pilot>
                {
                    new Pilot
                    {
                        BirthDate = new DateTime(2001, 3, 3),
                        Experience = 2,
                        FirstName = "Abdul",
                        SecondName = "Pilot",
                        Id = 1
                    },
                    new Pilot
                    {
                        BirthDate = new DateTime(1995, 2, 2),
                        Experience = 5,
                        FirstName = "Johh",
                        SecondName = "Pilot",
                        Id = 2
                    }
                }
            );

            mockedPilotRepository.Setup(p => p.Delete(It.Is<int>(x => x != 99))).ReturnsAsync(true);
            mockedPilotRepository.Setup(p => p.Update(It.Is<Pilot>(pilot => pilot.Id != 99))).ReturnsAsync(true);

            mockedPilotRepository.Setup(p => p.Update(It.Is<Pilot>(pilot => pilot.Id == 99))).ReturnsAsync(false);
            mockedPilotRepository.Setup(p => p.Delete(It.Is<int>(x => x == 99))).ReturnsAsync(false);

            mockedPilotRepository.Setup(p => p.Insert(It.IsAny<Pilot>())).Returns((Pilot pilot) => Task.Run(() =>
                {
                    pilot.Id = pilotIdCounter++;
                }));
            var mockedDataContext = new Mock<DbContext>();
            return new UnitOfWork(null, null, null, null, mockedPilotRepository.Object, null, null, null, mockedDataContext.Object);
        }
    }
}