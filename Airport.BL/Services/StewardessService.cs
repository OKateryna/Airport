using System.Collections.Generic;
using System.Linq;
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

        public StewardessDto GetById(int id)
        {
            var stewardess = _unitOfWork.StewardessRepository.Get(id);
            return _mapper.Map<StewardessDto>(stewardess);
        }

        public IEnumerable<StewardessDto> GetAll()
        {
            var results = _unitOfWork.StewardessRepository.GetAll();
            return results.Select(stewardess => _mapper.Map<StewardessDto>(stewardess));
        }

        public int Insert(EditableStewardessFields createStewardessRequest)
        {
            return _unitOfWork.StewardessRepository.Insert(_mapper.Map<Stewardess>(createStewardessRequest));
        }

        public bool Update(int id, EditableStewardessFields updateStewardessRequest)
        {
            var stewardessToUpdate = _mapper.Map<Stewardess>(updateStewardessRequest);
            stewardessToUpdate.Id = id;
            return _unitOfWork.StewardessRepository.Update(stewardessToUpdate);
        }

        public bool Delete(int id)
        {
            return _unitOfWork.StewardessRepository.Delete(id);
        }
    }
}