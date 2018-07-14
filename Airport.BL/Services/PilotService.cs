using System.Collections.Generic;
using System.Linq;
using Airport.BL.Abstractions;
using Airport.BL.Dto.Pilot;
using Airport.DAL.Abstractions;
using Airport.DAL.Models;
using AutoMapper;

namespace Airport.BL.Services
{
    public class PilotService : IPilotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PilotService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public PilotDto GetById(int id)
        {
            var pilot = _unitOfWork.PilotRepository.Get(id);
            return _mapper.Map<PilotDto>(pilot);
        }

        public IEnumerable<PilotDto> GetAll()
        {
            var results = _unitOfWork.PilotRepository.GetAll();
            return results.Select(pilot => _mapper.Map<PilotDto>(pilot));
        }

        public int Insert(EditablePilotFields createPilotRequest)
        {
            var entityToUpdate = _mapper.Map<Pilot>(createPilotRequest);
            _unitOfWork.PilotRepository.Insert(entityToUpdate);
            _unitOfWork.PilotRepository.Save();

            return entityToUpdate.Id;
        }

        public bool Update(int id, EditablePilotFields updatePilotRequest)
        {
            var pilotToUpdate = _mapper.Map<Pilot>(updatePilotRequest);
            pilotToUpdate.Id = id;
            return _unitOfWork.PilotRepository.Update(pilotToUpdate);
        }

        public bool Delete(int id)
        {
            return _unitOfWork.PilotRepository.Delete(id);
        }
    }
}