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

        public int Insert(EditableCrewFields createCrewRequest)
        {
            var entityToUpdate = _mapper.Map<Crew>(createCrewRequest);
            _unitOfWork.CrewRepository.Insert(entityToUpdate);
            _unitOfWork.CrewRepository.Save();

            return entityToUpdate.Id;
        }

        public bool Update(int id, EditableCrewFields updateCrewRequest)
        {
            var crewToUpdate = _mapper.Map<Crew>(updateCrewRequest);
            crewToUpdate.Id = id;
            return _unitOfWork.CrewRepository.Update(crewToUpdate);
        }

        public bool Delete(int id)
        {
            return _unitOfWork.CrewRepository.Delete(id);
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
    }
}