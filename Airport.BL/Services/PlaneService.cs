using System.Collections.Generic;
using System.Linq;
using Airport.BL.Abstractions;
using Airport.BL.Dto.Plane;
using Airport.BL.Dto.PlaneType;
using Airport.DAL.Abstractions;
using Airport.DAL.Models;
using AutoMapper;

namespace Airport.BL.Services
{
    public class PlaneService : IPlaneService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlaneService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public PlaneDto GetById(int id)
        {
            var plane = _unitOfWork.PlaneRepository.Get(id);
            return GetPlaneDto(plane);
        }

        public IEnumerable<PlaneDto> GetAll()
        {
            var results = _unitOfWork.PlaneRepository.GetAll();
            return results.Select(GetPlaneDto);
        }

        public int Insert(EditablePlaneFields createPlaneRequest)
        {
            return _unitOfWork.PlaneRepository.Insert(_mapper.Map<Plane>(createPlaneRequest));
        }

        public bool Update(int id, EditablePlaneFields updatePlaneRequest)
        {
            var planeToUpdate = _mapper.Map<Plane>(updatePlaneRequest);
            planeToUpdate.Id = id;
            return _unitOfWork.PlaneRepository.Update(planeToUpdate);
        }

        public bool Delete(int id)
        {
            return _unitOfWork.PlaneRepository.Delete(id);
        }

        private PlaneDto GetPlaneDto(Plane plane)
        {
            var planeType = _unitOfWork.PlaneTypeRepository.Get(plane.PlaneTypeId);
            var result = _mapper.Map<PlaneDto>(plane);
            result.PlaneType = _mapper.Map<PlaneTypeDto>(planeType);
            return result;
        }
    }
}