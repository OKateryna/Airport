using System.Collections.Generic;
using System.Linq;
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

        public PlaneTypeDto GetById(int id)
        {
            var planeType = _unitOfWork.PlaneTypeRepository.Get(id);
            return _mapper.Map<PlaneTypeDto>(planeType);
        }

        public IEnumerable<PlaneTypeDto> GetAll()
        {
            var results = _unitOfWork.PlaneTypeRepository.GetAll();
            return results.Select(planeType => _mapper.Map<PlaneTypeDto>(planeType));
        }

        public int Insert(EditablePlaneTypeFields createPlaneTypeRequest)
        {
            return _unitOfWork.PlaneTypeRepository.Insert(_mapper.Map<PlaneType>(createPlaneTypeRequest));
        }

        public bool Update(int id, EditablePlaneTypeFields updatePlaneTypeRequest)
        {
            var planeTypeToUpdate = _mapper.Map<PlaneType>(updatePlaneTypeRequest);
            planeTypeToUpdate.Id = id;
            return _unitOfWork.PlaneTypeRepository.Update(planeTypeToUpdate);
        }

        public bool Delete(int id)
        {
            return _unitOfWork.PlaneTypeRepository.Delete(id);
        }
    }
}