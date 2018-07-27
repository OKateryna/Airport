using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport.BL.Abstractions;
using Airport.BL.Dto.PlaneType;
using Airport.DAL.Abstractions;
using Airport.DAL.Models;
using AutoMapper;

namespace Airport.BL.Services
{
    public class PlaneTypeService : IPlaneTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlaneTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PlaneTypeDto> GetById(int id)
        {
            var planeType = await _unitOfWork.PlaneTypeRepository.Get(id);
            return _mapper.Map<PlaneTypeDto>(planeType);
        }

        public async Task<IEnumerable<PlaneTypeDto>> GetAll()
        {
            var results = await _unitOfWork.PlaneTypeRepository.GetAll();
            return results.Select(planeType => _mapper.Map<PlaneTypeDto>(planeType));
        }

        public async Task<int> Insert(EditablePlaneTypeFields createPlaneTypeRequest)
        {
            var entityToUpdate = _mapper.Map<PlaneType>(createPlaneTypeRequest);
            await _unitOfWork.PlaneTypeRepository.Insert(entityToUpdate);
            await _unitOfWork.SaveChangesAsync();

            return entityToUpdate.Id;
        }

        public async Task<bool> Update(int id, EditablePlaneTypeFields updatePlaneTypeRequest)
        {
            var planeTypeToUpdate = _mapper.Map<PlaneType>(updatePlaneTypeRequest);
            planeTypeToUpdate.Id = id;
            var result = await _unitOfWork.PlaneTypeRepository.Update(planeTypeToUpdate);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _unitOfWork.PlaneTypeRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}