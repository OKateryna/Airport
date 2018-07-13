using Airport.BL.Dto;
using Airport.BL.Dto.Flight;
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
            CreateMap<Flight, FlightDto>().ForMember(x => x.Ticket, opt => opt.Ignore());
            CreateMap<FlightDto, Flight>().ForMember(x => x.TicketId, opt => opt.Ignore());

            CreateMap<Ticket, TicketDto>();
            CreateMap<TicketDto, Ticket>();
        }
    }
}