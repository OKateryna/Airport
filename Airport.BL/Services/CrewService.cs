using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<CrewDto> GetById(int id)
        {
            var crew = await _unitOfWork.CrewRepository.Get(id);
            return await GetCrewDto(crew);
        }

        public async Task<IEnumerable<CrewDto>> GetAll()
        {
            var crews = await _unitOfWork.CrewRepository.GetAll();
            var crewDto = await Task.WhenAll(crews.Select(GetCrewDto));

            return crewDto.Where(x => x != null);
        }

        private async Task<CrewDto> GetCrewDto(Crew crew)
        {
            var crewDto = _mapper.Map<CrewDto>(crew);
            var pilot = crew.CrewPilot.Pilot ?? await _unitOfWork.PilotRepository.Get(crew.CrewPilot.PilotId);
            crewDto.Pilot = _mapper.Map<PilotDto>(pilot);
            crewDto.Stewardesses = await GetStewardessesDtoFromCrewStewardesses(crew.CrewStewardesses);
            return crewDto;
        }

        private async Task<IEnumerable<StewardessDto>> GetStewardessesDtoFromCrewStewardesses(ICollection<CrewStewardess> crewCrewStewardesses)
        {
            var result = crewCrewStewardesses.Select(async x =>
            {
                var stewardess = x.Stewardess ?? await _unitOfWork.StewardessRepository.Get(x.StewardessId);
                return _mapper.Map<StewardessDto>(stewardess);
            });

            return await Task.WhenAll(result);
        }

        public async Task<int> Insert(EditableCrewFields createCrewRequest)
        {
            var entityToInsert = _mapper.Map<Crew>(createCrewRequest);
            await _unitOfWork.CrewRepository.Insert(entityToInsert);
            await _unitOfWork.SaveChangesAsync();
            await UpdateCrew(entityToInsert, createCrewRequest);

            return entityToInsert.Id;
        }

        public async Task<bool> Update(int id, EditableCrewFields updateCrewRequest)
        {
            var crewToUpdate = _mapper.Map<Crew>(updateCrewRequest);
            crewToUpdate.Id = id;

            return await UpdateCrew(crewToUpdate, updateCrewRequest);
        }

        private async Task<bool> UpdateCrew(Crew crew, EditableCrewFields editableCrewFields)
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

            var updateResult = await _unitOfWork.CrewRepository.Update(crew);
            await _unitOfWork.SaveChangesAsync();

            return updateResult;
        }

        public async Task<bool> Delete(int id)
        {
            return await _unitOfWork.CrewRepository.Delete(id);
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