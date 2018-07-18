using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport.BL.Abstractions;
using Airport.BL.Dto.Stewardess;
using Airport.DAL.Abstractions;
using Airport.DAL.Models;
using AutoMapper;

namespace Airport.BL.Services
{
    public class StewardessService : IStewardessService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StewardessService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<StewardessDto> GetById(int id)
        {
            var stewardess = await _unitOfWork.StewardessRepository.Get(id);
            return _mapper.Map<StewardessDto>(stewardess);
        }

        public async Task<IEnumerable<StewardessDto>> GetAll()
        {
            var stewardesses = await _unitOfWork.StewardessRepository.GetAll();
            var stewardessesDtos = stewardesses.Select(stewardess => _mapper.Map<StewardessDto>(stewardess));
            return stewardessesDtos;
        }

        public async Task<int> Insert(EditableStewardessFields createStewardessRequest)
        {
            var entityToUpdate = _mapper.Map<Stewardess>(createStewardessRequest);
            await _unitOfWork.StewardessRepository.Insert(entityToUpdate);
            await _unitOfWork.SaveChangesAsync();

            return entityToUpdate.Id;
        }

        public async Task<bool> Update(int id, EditableStewardessFields updateStewardessRequest)
        {
            var stewardessToUpdate = _mapper.Map<Stewardess>(updateStewardessRequest);
            stewardessToUpdate.Id = id;
            return await _unitOfWork.StewardessRepository.Update(stewardessToUpdate);
        }

        public async Task<bool> Delete(int id)
        {
            return await _unitOfWork.StewardessRepository.Delete(id);
        }
    }
}