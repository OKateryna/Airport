using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airport.BL.Abstractions;
using Airport.BL.Dto.Plane;
using Airport.BL.Dto.PlaneType;
using Airport.DAL.Abstractions;
using Airport.DAL.Models;
using AutoMapper;
using Remotion.Linq.Clauses;

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

        public async Task<PlaneDto> GetById(int id)
        {
            var plane = await _unitOfWork.PlaneRepository.Get(id);

            return await GetPlaneDto(plane);
        }

        public async Task<IEnumerable<PlaneDto>> GetAll()
        {
            var planes = await _unitOfWork.PlaneRepository.GetAll();
            var planeDtos = await Task.WhenAll(planes.Select(GetPlaneDto));
            return planeDtos.Where(x => x != null);
        }

        public async Task<int> Insert(EditablePlaneFields createPlaneRequest)
        {
            var entityToUpdate = _mapper.Map<Plane>(createPlaneRequest);
            await _unitOfWork.PlaneRepository.Insert(entityToUpdate);
            await _unitOfWork.SaveChangesAsync();

            return entityToUpdate.Id;
        }

        public async Task<bool> Update(int id, EditablePlaneFields updatePlaneRequest)
        {
            var planeToUpdate = _mapper.Map<Plane>(updatePlaneRequest);
            planeToUpdate.Id = id;
            var result = await _unitOfWork.PlaneRepository.Update(planeToUpdate);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _unitOfWork.PlaneRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }

        private async Task<PlaneDto> GetPlaneDto(Plane plane)
        {
            var planeType = await _unitOfWork.PlaneTypeRepository.Get(plane.PlaneTypeId);
            var result = _mapper.Map<PlaneDto>(plane);
            result.PlaneType = _mapper.Map<PlaneTypeDto>(planeType);
            return result;
        }
    }
}