using System;
using System.Collections.Generic;
using System.Linq;
using Airport.BL.Abstractions;
using Airport.BL.Dto.Crew;
using Airport.BL.Dto.Pilot;
using Airport.BL.Dto.Stewardess;
using Airport.DAL.Abstractions;
using Airport.DAL.Models;
using AutoMapper;

namespace Airport.BL.Services
{
    public class CrewService : ICrewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CrewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public CrewDto GetById(int id)
        {
            var crew = _unitOfWork.CrewRepository.Get(id);
            return GetCrewDto(crew);
        }

        public IEnumerable<CrewDto> GetAll()
        {
            var results = _unitOfWork.CrewRepository.GetAll();

            return results.Select(GetCrewDto);
        }

        private CrewDto GetCrewDto(Crew crew)
        {
            var crewDto = _mapper.Map<CrewDto>(crew);
            crewDto.Pilot = _mapper.Map<PilotDto>(crew.CrewPilot.Pilot);
            crewDto.Stewardesses = crew.CrewStewardesses.Select(cs => _mapper.Map<StewardessDto>(cs.Stewardess));
            return crewDto;
        }

        public int Insert(EditableCrewFields createCrewRequest)
        {
            var entityToInsert = _mapper.Map<Crew>(createCrewRequest);
            _unitOfWork.CrewRepository.Insert(entityToInsert);
            _unitOfWork.CrewRepository.Save();

            UpdateCrew(entityToInsert, createCrewRequest);

            return entityToInsert.Id;
        }

        public bool Update(int id, EditableCrewFields updateCrewRequest)
        {
            var crewToUpdate = _mapper.Map<Crew>(updateCrewRequest);
            crewToUpdate.Id = id;

            return UpdateCrew(crewToUpdate, updateCrewRequest);
        }

        private bool UpdateCrew(Crew crew, EditableCrewFields editableCrewFields)
        {
            crew.CrewPilot = new CrewPilot
            {
                CrewId = crew.Id,
                PilotId = editableCrewFields.PilotId
            };

            crew.CrewStewardesses = editableCrewFields.StewardessIds.Select(x => new CrewStewardess
            {
                CrewId = crew.Id,
                StewardessId = x
            }).ToList();

            _unitOfWork.CrewRepository.Update(crew);
            _unitOfWork.CrewRepository.Save();

            return true;
        }

        public bool Delete(int id)
        {
            return _unitOfWork.CrewRepository.Delete(id);
        }

        //private CrewDto GetCrewDto(Crew crew)
        //{
        //    var pilot = _unitOfWork.PilotRepository.Get(crew.PilotId);
        //    var stewardesses = _unitOfWork.StewardessRepository.GetAll().Where(x => crew.StewardessIds.Contains(x.Id));
        //    var result = _mapper.Map<CrewDto>(crew);
        //    result.Pilot = _mapper.Map<PilotDto>(pilot);
        //    result.Stewardesses = stewardesses.Select(stewardess => _mapper.Map<StewardessDto>(stewardess));

        //    return result;
        //}
    }
}