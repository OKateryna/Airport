using Airport.DAL.Abstractions;
using Airport.DAL.Models;

namespace Airport.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IRepository<Flight> flightRepository, IRepository<Ticket> ticketRepository, IRepository<Departure> departureRepository, IRepository<Stewardess> stewardessRepository, IRepository<Pilot> pilotRepository, IRepository<Crew> crewRepository, IRepository<Plane> planeRepository, IRepository<PlaneType> planeTypeRepository)
        {
            FlightRepository = flightRepository;
            TicketRepository = ticketRepository;
            DepartureRepository = departureRepository;
            StewardessRepository = stewardessRepository;
            PilotRepository = pilotRepository;
            CrewRepository = crewRepository;
            PlaneRepository = planeRepository;
            PlaneTypeRepository = planeTypeRepository;
        }

        public IRepository<Flight> FlightRepository { get; }
        public IRepository<Ticket> TicketRepository { get; }
        public IRepository<Departure> DepartureRepository { get; }
        public IRepository<Stewardess> StewardessRepository { get; }
        public IRepository<Pilot> PilotRepository { get; }
        public IRepository<Crew> CrewRepository { get; }
        public IRepository<Plane> PlaneRepository { get; }
        public IRepository<PlaneType> PlaneTypeRepository { get; }
    }
}