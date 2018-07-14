using Airport.BL.Dto.Crew;
using Airport.BL.Dto.Departure;
using Airport.BL.Dto.Flight;
using Airport.BL.Dto.Pilot;
using Airport.BL.Dto.Plane;
using Airport.BL.Dto.PlaneType;
using Airport.BL.Dto.Stewardess;
using Airport.BL.Dto.Ticket;
using Airport.DAL.Models;
using AutoMapper;

namespace Airport.BL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Flight, EditableFlightFields>();
            CreateMap<EditableFlightFields, Flight>();
            CreateMap<Flight, FlightDto>();
            CreateMap<FlightDto, Flight>();

            CreateMap<Ticket, EditableTicketFields>();
            CreateMap<EditableTicketFields, Ticket>();
            CreateMap<Ticket, TicketDto>().ForMember(x => x.Flight, opt => opt.Ignore());
            CreateMap<TicketDto, Ticket>().ForMember(x => x.FlightId, opt => opt.Ignore());

            CreateMap<Stewardess, EditableStewardessFields>();
            CreateMap<EditableStewardessFields, Stewardess>();
            CreateMap<Stewardess, StewardessDto>();
            CreateMap<StewardessDto, Stewardess>();

            CreateMap<Pilot, EditablePilotFields>();
            CreateMap<EditablePilotFields, Pilot>();
            CreateMap<Pilot, PilotDto>();
            CreateMap<PilotDto, Pilot>();

            CreateMap<PlaneType, EditablePlaneTypeFields>();
            CreateMap<EditablePlaneTypeFields, PlaneType>();
            CreateMap<PlaneType, PlaneTypeDto>();
            CreateMap<PlaneTypeDto, PlaneType>();

            CreateMap<Plane, EditablePlaneFields>();
            CreateMap<EditablePlaneFields, Plane>();
            CreateMap<Plane, PlaneDto>().ForMember(x => x.PlaneType, opt => opt.Ignore());
            CreateMap<PlaneDto, Plane>().ForMember(x => x.PlaneTypeId, opt => opt.Ignore());

            CreateMap<Crew, EditableCrewFields>();
            CreateMap<EditableCrewFields, Crew>();
            CreateMap<Crew, CrewDto>();
            CreateMap<CrewDto, Crew>();

            CreateMap<Departure, EditableDepartureFields>();
            CreateMap<EditableDepartureFields, Departure>();
            CreateMap<Departure, DepartureDto>().ForMember(x => x.Crew, opt => opt.Ignore()).ForMember(x => x.Flight, opt => opt.Ignore()).ForMember(x => x.Plane, opt =>opt.Ignore());
            CreateMap<DepartureDto, Departure>().ForMember(x => x.CrewId, opt => opt.Ignore()).ForMember(x => x.FlightId, opt => opt.Ignore()).ForMember(x => x.PlaneId, opt => opt.Ignore());
        }
    }
}