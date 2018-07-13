using Airport.DAL.Models;

namespace Airport.DAL.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<Flight> FlightRepository { get; }
        IRepository<Ticket> TicketRepository { get; }
        IRepository<Departure> DepartureRepository { get; }
        IRepository<Stewardess> StewardessRepository { get; }
        IRepository<Pilot> PilotRepository { get; }
        IRepository<Crew> CrewRepository { get; }
        IRepository<Plane> PlaneRepository { get; }
        IRepository<PlaneType> PlaneTypeRepository { get; }
    }
}