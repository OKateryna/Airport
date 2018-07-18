using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport.BL.Abstractions;
using Airport.BL.Dto.Crew;
using Airport.BL.Dto.Departure;
using Airport.BL.Dto.Flight;
using Airport.BL.Dto.Pilot;
using Airport.BL.Dto.Plane;
using Airport.BL.Dto.PlaneType;
using Airport.BL.Dto.Stewardess;
using Airport.BL.Dto.Ticket;
using Airport.DAL.Abstractions;
using Airport.DAL.Models;
using AutoMapper;

namespace Airport.BL.Services
{
    public class DepartureService : IDepartureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartureService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DepartureDto> GetById(int id)
        {
            var departure = await _unitOfWork.DepartureRepository.Get(id);
            return await GetDepartureDto(departure);
        }

        public async Task<IEnumerable<DepartureDto>> GetAll()
        {
            var departures = await _unitOfWork.DepartureRepository.GetAll();
            var departuresDtos = await Task.WhenAll(departures.Select(GetDepartureDto));
            return departuresDtos.Where(x => x != null);
        }

        public async Task<int> Insert(EditableDepartureFields createDepartureRequest)
        {
            var entityToUpdate = _mapper.Map<Departure>(createDepartureRequest);
            await _unitOfWork.DepartureRepository.Insert(entityToUpdate);
            await _unitOfWork.SaveChangesAsync();

            return entityToUpdate.Id;
        }

        public async Task<bool> Update(int id, EditableDepartureFields updateDepartureRequest)
        {
            var departureToUpdate = _mapper.Map<Departure>(updateDepartureRequest);
            departureToUpdate.Id = id;
            return await _unitOfWork.DepartureRepository.Update(departureToUpdate);
        }

        public async Task<bool> Delete(int id)
        {
            return await _unitOfWork.DepartureRepository.Delete(id);
        }

        private async Task<DepartureDto> GetDepartureDto(Departure departure)
        {
            var flight = await _unitOfWork.FlightRepository.Get(departure.FlightId);
            var crew = await _unitOfWork.CrewRepository.Get(departure.CrewId);
            var plane = await _unitOfWork.PlaneRepository.Get(departure.PlaneId);
            var result = _mapper.Map<DepartureDto>(departure);
            result.Flight = await GetFlightDto(flight);
            result.Plane = await GetPlaneDto(plane);
            result.Crew = _mapper.Map<CrewDto>(crew);
            
            return result;
        }

        private async Task<PlaneDto> GetPlaneDto(Plane plane)
        {
            var planeType = await _unitOfWork.PlaneTypeRepository.Get(plane.PlaneTypeId);
            var result = _mapper.Map<PlaneDto>(plane);
            result.PlaneType = _mapper.Map<PlaneTypeDto>(planeType);
            return result;
        }

        private async Task<FlightDto> GetFlightDto(Flight flight)
        {
            var tickets = (await _unitOfWork.TicketRepository.GetAll()).Where(x => x.FlightId == flight.Id);
            var result = _mapper.Map<FlightDto>(flight);
            result.Tickets = tickets.Select(ticket => _mapper.Map<TicketDto>(ticket)).ToList();
            return result;
        }
    }
}