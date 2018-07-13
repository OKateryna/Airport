﻿using System.Collections.Generic;
using System.Linq;
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

        public DepartureDto GetById(int id)
        {
            var departure = _unitOfWork.DepartureRepository.Get(id);
            return GetDepartureDto(departure);
        }

        public IEnumerable<DepartureDto> GetAll()
        {
            var results = _unitOfWork.DepartureRepository.GetAll();
            return results.Select(GetDepartureDto);
        }

        public int Insert(EditableDepartureFields createDepartureRequest)
        {
            return _unitOfWork.DepartureRepository.Insert(_mapper.Map<Departure>(createDepartureRequest));
        }

        public bool Update(int id, EditableDepartureFields updateDepartureRequest)
        {
            var departureToUpdate = _mapper.Map<Departure>(updateDepartureRequest);
            departureToUpdate.Id = id;
            return _unitOfWork.DepartureRepository.Update(departureToUpdate);
        }

        public bool Delete(int id)
        {
            return _unitOfWork.DepartureRepository.Delete(id);
        }

        private DepartureDto GetDepartureDto(Departure departure)
        {
            var flight = _unitOfWork.FlightRepository.Get(departure.FlightId);
            var crew = _unitOfWork.CrewRepository.Get(departure.CrewId);
            var plane = _unitOfWork.PlaneRepository.Get(departure.PlaneId);
            var result = _mapper.Map<DepartureDto>(departure);
            result.Flight = GetFlightDto(flight);
            result.Plane = GetPlaneDto(plane);
            result.Crew = GetCrewDto(crew);
            
            return result;
        }

        private CrewDto GetCrewDto(Crew crew)
        {
            var pilot = _unitOfWork.PilotRepository.Get(crew.PilotId);
            var stewardesses = _unitOfWork.StewardessRepository.GetAll().Where(x => crew.StewardessIds.Contains(x.Id));
            var result = _mapper.Map<CrewDto>(crew);
            result.Pilot = _mapper.Map<PilotDto>(pilot);
            result.Stewardesses = stewardesses.Select(stewardess => _mapper.Map<StewardessDto>(stewardess));

            return result;
        }

        private PlaneDto GetPlaneDto(Plane plane)
        {
            var planeType = _unitOfWork.PlaneTypeRepository.Get(plane.PlaneTypeId);
            var result = _mapper.Map<PlaneDto>(plane);
            result.PlaneType = _mapper.Map<PlaneTypeDto>(planeType);
            return result;
        }

        private FlightDto GetFlightDto(Flight flight)
        {
            var ticket = _unitOfWork.TicketRepository.Get(flight.TicketId);
            var result = _mapper.Map<FlightDto>(flight);
            result.Ticket = _mapper.Map<TicketDto>(ticket);
            return result;
        }
    }
}