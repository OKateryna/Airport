using System;
using System.Linq;
using Airport.DAL.EntityFramework;
using Airport.DAL.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Airport.DAL.IntegrationTests
{
    [TestFixture]
    public class DalTests
    {
        private DataContext _dataContext;

        private const string DbConnectionString =
            "Data Source=.\\SQLEXPRESS;Initial Catalog=AirportDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [SetUp]
        public void Initialize()
        {
            var builder = new DbContextOptionsBuilder<DataContext>().UseSqlServer(DbConnectionString);
            _dataContext = new DataContext(builder.Options);
        }

        [TearDown]
        public void Destroy()
        {
            _dataContext = null;
        }

        [Test]
        public void AddPilotIncreaseByOne()
        {
            Pilot testPilot = new Pilot
            {
                BirthDate = new DateTime(2000, 11, 1),
                Experience = 2,
                FirstName = "Jonh",
                SecondName = "Smith"
            };

            var existingPilotCount = _dataContext.Pilots.Count();

            _dataContext.Pilots.Add(testPilot);
            _dataContext.SaveChanges();
            var changedPilotCont = _dataContext.Pilots.Count();

            Assert.AreEqual(existingPilotCount + 1, changedPilotCont);
        }

        [Test]
        public void AddedPilotIsSame()
        {
            var birthDay = new DateTime(1999, 1, 1);
            var exp = 10;
            var firstName = "Abdula";
            var secondName = "Ichma";

            Pilot pilotToAdd = new Pilot
            {
                BirthDate = birthDay,
                Experience = exp,
                FirstName = firstName,
                SecondName = secondName
            };

            _dataContext.Pilots.Add(pilotToAdd);
            _dataContext.SaveChanges();

            var pilotFromDb = _dataContext.Pilots.Find(pilotToAdd.Id);
            Assert.AreEqual(birthDay, pilotFromDb.BirthDate);
            Assert.AreEqual(exp, pilotFromDb.Experience);
            Assert.AreEqual(firstName, pilotFromDb.FirstName);
            Assert.AreEqual(secondName, pilotFromDb.SecondName);
        }

        [Test]
        public void RemovePilotIsRemoved()
        {
            Pilot testPilot = new Pilot
            {
                BirthDate = new DateTime(2000, 11, 1),
                Experience = 2,
                FirstName = "Jonh",
                SecondName = "Smith"
            };

            var existingPilotCount = _dataContext.Pilots.Count();

            _dataContext.Pilots.Add(testPilot);
            _dataContext.SaveChanges();

            var pilotId = testPilot.Id;
            
            var afterAddPilotCount = _dataContext.Pilots.Count();

            var addedPilot = _dataContext.Pilots.Find(pilotId);

            _dataContext.Pilots.Remove(addedPilot);
            _dataContext.SaveChanges();
            var afterDeletePilotCount = _dataContext.Pilots.Count();
            Assert.Greater(afterAddPilotCount, existingPilotCount);
            Assert.AreEqual(existingPilotCount, afterDeletePilotCount);

            var deletedPilot = _dataContext.Pilots.Find(pilotId);
            Assert.IsNull(deletedPilot);
        }

        [Test]
        public void RemovePilotFromCrewRemovesCrewPilot()
        {
            Pilot testPilot = new Pilot
            {
                BirthDate = new DateTime(2000, 11, 1),
                Experience = 2,
                FirstName = "Jonh",
                SecondName = "Smith"
            };

            _dataContext.Pilots.Add(testPilot);
            _dataContext.SaveChanges();

            Crew crew = new Crew()
            {
                Name = "Pilot John's crew"
            };

            _dataContext.Crews.Add(crew);
            _dataContext.SaveChanges();

            var crewPilotToAdd = new CrewPilot
            {
                Crew = crew,
                CrewId = crew.Id,
                Pilot = testPilot,
                PilotId = testPilot.Id
            };

            crew.CrewPilot = crewPilotToAdd;
            _dataContext.Crews.Update(crew);
            _dataContext.SaveChanges();

            var crewFromDatabase = _dataContext.Crews.Find(crew.Id);

            Assert.IsNotNull(crewFromDatabase.CrewPilot);
            
            _dataContext.Pilots.Remove(testPilot);
            _dataContext.SaveChanges();

            crewFromDatabase = _dataContext.Crews.Find(crew.Id);

            Assert.IsNull(crewFromDatabase.CrewPilot);
        }

        [Test]
        public void RemoveFlightFromTicketCascadeDelete()
        {
            var testFlight = new Flight
            {
                Destination = "Lviv",
                Number = "GCK208",
                PlaceDeparture = "Kyiv",
                TimeDeparture = new DateTime(2018, 7, 5, 9, 30, 0),
                TimeDestination = new DateTime(2018, 7, 5, 11, 30, 0)
            };

            _dataContext.Flights.Add(testFlight);
            _dataContext.SaveChanges();

            var testTicket = new Ticket
            {
                FlightId = testFlight.Id,
                Price = 650
            };

            _dataContext.Tickets.Add(testTicket);
            _dataContext.SaveChanges();

            var tickedId = testTicket.Id;

            var flightFromDb = _dataContext.Flights.Find(testFlight.Id);

            _dataContext.Flights.Remove(flightFromDb);
            _dataContext.SaveChanges();

            var ticketFromDb = _dataContext.Tickets.Find(tickedId);
            Assert.IsNull(ticketFromDb);
        }

        [Test]
        public void AssertThrowsIfCannotDeleteDependency()
        {
            Assert.Throws(typeof(DbUpdateException), CannotDeleteFlightIfDepartureExists);
        }

        [Test]
        public void AddStewardessAssignId()
        {
            var stewardess = new Stewardess
            {
                BirthDate = DateTime.Now,
                FirstName = "Olena",
                SecondName = "Zvarych"
            };

            var initialId = stewardess.Id;

            _dataContext.Stewardesses.Add(stewardess);
            _dataContext.SaveChanges();

            Assert.AreNotEqual(initialId, stewardess.Id);
        }

        [Test]
        public void DbDoesNotContainIfNotSaved()
        {
            var planeType = new PlaneType()
            {
                CarryingCapacity = 10,
                PlaneModel = "Passanger",
                PlacesAmount = 100
            };

            _dataContext.PlaneTypes.Add(planeType);

            var addedButNotSavedId = planeType.Id;

            _dataContext.SaveChanges();

            Assert.AreNotEqual(addedButNotSavedId, planeType.Id);
        }

        [Test]
        public void StewardessIsUpdatedExceptId()
        {
            var initialFirstName = "Kate";
            var initialSecondName = "Winston";
            var initialBirthDay = new DateTime(1950, 11, 11);
            var stewardess = new Stewardess
            {
                BirthDate = initialBirthDay,
                FirstName = initialFirstName,
                SecondName = initialSecondName
            };

            _dataContext.Stewardesses.Add(stewardess);
            _dataContext.SaveChanges();

            var initialId = stewardess.Id;
            var stewardessFromDb = _dataContext.Stewardesses.Find(initialId);

            var changedFirstName = "Kateryna";
            var changedSecondName = "Kamin";
            var changedBirthDay = new DateTime(1992, 12, 12);

            stewardessFromDb.FirstName = changedFirstName;
            stewardessFromDb.SecondName = changedSecondName;
            stewardessFromDb.BirthDate = changedBirthDay;

            _dataContext.Stewardesses.Update(stewardessFromDb);
            _dataContext.SaveChanges();

            var idAfterChage = stewardessFromDb.Id;

            var changedEntity = _dataContext.Stewardesses.Find(stewardessFromDb.Id);

            Assert.AreNotEqual(initialFirstName, changedEntity.FirstName);
            Assert.AreNotEqual(initialSecondName, changedEntity.SecondName);
            Assert.AreNotEqual(initialBirthDay, changedEntity.BirthDate);

            Assert.AreEqual(changedFirstName, changedEntity.FirstName);
            Assert.AreEqual(changedSecondName, changedEntity.SecondName);
            Assert.AreEqual(changedBirthDay, changedEntity.BirthDate);

            Assert.AreEqual(initialId, idAfterChage);
        }

        [Test]
        public void AssertThrowsInvalidOperationExceptionIfChangedKey()
        {
            Assert.Throws(typeof(InvalidOperationException), KayCannotBeChangedToExisting);
        }


        public void KayCannotBeChangedToExisting()
        {
            Pilot testPilot = new Pilot
            {
                BirthDate = new DateTime(2000, 11, 1),
                Experience = 2,
                FirstName = "Jonh",
                SecondName = "Smith"
            };

            Pilot testPilot2 = new Pilot
            {
                BirthDate = new DateTime(1999, 1, 1),
                Experience = 10,
                FirstName = "Abdula",
                SecondName = "Ichma"
            };

            _dataContext.Pilots.Add(testPilot);
            _dataContext.Pilots.Add(testPilot2);
            _dataContext.SaveChanges();

            testPilot.Id = testPilot2.Id;

            _dataContext.Update(testPilot);
            _dataContext.SaveChanges();
        }


        public void CannotDeleteFlightIfDepartureExists()
        {
            var testFlight = new Flight
            {
                Destination = "Lviv",
                Number = "GCK208",
                PlaceDeparture = "Kyiv",
                TimeDeparture = new DateTime(2018, 7, 5, 9, 30, 0),
                TimeDestination = new DateTime(2018, 7, 5, 11, 30, 0)
            };

            _dataContext.Flights.Add(testFlight);
            _dataContext.SaveChanges();

            Crew crew = new Crew()
            {
                Name = "Pilot John's crew"
            };

            _dataContext.Crews.Add(crew);
            _dataContext.SaveChanges();

            Pilot testPilot = new Pilot
            {
                BirthDate = new DateTime(2000, 11, 1),
                Experience = 2,
                FirstName = "Jonh",
                SecondName = "Smith"
            };

            _dataContext.Pilots.Add(testPilot);
            _dataContext.SaveChanges();

            var crewPilotToAdd = new CrewPilot
            {
                Crew = crew,
                CrewId = crew.Id,
                Pilot = testPilot,
                PilotId = testPilot.Id
            };

            crew.CrewPilot = crewPilotToAdd;

            _dataContext.Crews.Update(crew);
            _dataContext.SaveChanges();

            var planeType = new PlaneType()
            {
                CarryingCapacity = 10,
                PlaneModel = "Passanger",
                PlacesAmount = 100
            };

            _dataContext.PlaneTypes.Add(planeType);
            _dataContext.SaveChanges();

            var plane = new Plane
            {
                LifeSpan = 10,
                ManufectureDate = new DateTime(1995, 12, 1),
                PlaneName = "Litachok",
                PlaneTypeId = planeType.Id
            };

            _dataContext.Planes.Add(plane);
            _dataContext.SaveChanges();

            var departure = new Departure()
            {
                CrewId = crew.Id,
                DepartureDate = DateTime.Now,
                FlightId = testFlight.Id,
                PlaneId = plane.Id
            };

            _dataContext.Departures.Add(departure);
            _dataContext.SaveChanges();

            var flightFromDb = _dataContext.Flights.Find(testFlight.Id);

            _dataContext.Flights.Remove(flightFromDb);
            _dataContext.SaveChanges();
        }
    }
}