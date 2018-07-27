using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<PilotDto> GetById(int id)
        {
            var pilot = await _unitOfWork.PilotRepository.Get(id);
            return _mapper.Map<PilotDto>(pilot);
        }

        public async Task<IEnumerable<PilotDto>> GetAll()
        {
            var results = await _unitOfWork.PilotRepository.GetAll();
            return results.Select(pilot => _mapper.Map<PilotDto>(pilot));
        }

        public async Task<int> Insert(EditablePilotFields createPilotRequest)
        {
            var entityToUpdate = _mapper.Map<Pilot>(createPilotRequest);
            await _unitOfWork.PilotRepository.Insert(entityToUpdate);
            await _unitOfWork.SaveChangesAsync();

            return entityToUpdate.Id;
        }

        public async Task<bool> Update(int id, EditablePilotFields updatePilotRequest)
        {
            var pilotToUpdate = _mapper.Map<Pilot>(updatePilotRequest);
            pilotToUpdate.Id = id;
            var result = await _unitOfWork.PilotRepository.Update(pilotToUpdate);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _unitOfWork.PilotRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }
    }
}