using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Airport.BL.Abstractions;
using Airport.BL.Dto.Crew;
using Airport.BL.Dto.Pilot;
using Airport.BL.Dto.Stewardess;
using Airport.DAL.Abstractions;
using Airport.DAL.Models;
using AutoMapper;
using Newtonsoft.Json;
using ServiceStack.Text;

namespace Airport.BL.Services
{
    public class CrewService : ICrewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private const string CsvFolder = @"CsvData\";
        private const string CsvFileTemplate = "CrewsCsv_{0}.csv";

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
            return await UpdateCrew(crew, editableCrewFields.PilotId, editableCrewFields.StewardessIds);
        }

        private async Task<bool> UpdateCrew(Crew crewToUpdate, int pilotId, IEnumerable<int> stewardessesIds)
        {
            crewToUpdate.CrewPilot = new CrewPilot
            {
                CrewId = crewToUpdate.Id,
                PilotId = pilotId
            };

            crewToUpdate.CrewStewardesses = stewardessesIds.Select(x => new CrewStewardess
            {
                CrewId = crewToUpdate.Id,
                StewardessId = x
            }).ToList();

            var updateResult = await _unitOfWork.CrewRepository.Update(crewToUpdate);
            await _unitOfWork.SaveChangesAsync();

            return updateResult;
        }

        public async Task<bool> Delete(int id)
        {
            return await _unitOfWork.CrewRepository.Delete(id);
        }

        public async Task SaveFromExternalApi()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://5b128555d50a5c0014ef1204.mockapi.io/crew");
            List<CrewExternalDto> crewsFromApi = null;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var json = await responseContent.ReadAsStringAsync();
                crewsFromApi = JsonConvert.DeserializeObject<List<CrewExternalDto>>(json).Take(10).ToList();
            }
            else
            {
                throw new AggregateException("Could not get a correct response from the server.");
            }

            await Task.WhenAll(SaveInDatabase(crewsFromApi), WriteToCsv(crewsFromApi));
        }

        private async Task SaveInDatabase(List<CrewExternalDto> externalCrewDtos)
        {
            foreach (var externalCrewDto in externalCrewDtos)
            {
                // Insert pilot
                var pilot = externalCrewDto.Pilot.FirstOrDefault();
                var pilotToInsert = _mapper.Map<Pilot>(pilot);
                await _unitOfWork.PilotRepository.Insert(pilotToInsert);
                
                // Insert stewardesses
                var stewardesses = externalCrewDto.Stewardess;
                var stewardessesToInsert = stewardesses.Select(x =>
                {
                    var stewardess = _mapper.Map<Stewardess>(x);
                    return stewardess;
                }).ToList();

                await Task.WhenAll(stewardessesToInsert.Select(stewardessToInsert =>
                    _unitOfWork.StewardessRepository.Insert(stewardessToInsert)));

                // Save to generate Id's
                await _unitOfWork.SaveChangesAsync();

                // Insert crew
                var crewToInsert = _mapper.Map<Crew>(externalCrewDto);

                await _unitOfWork.CrewRepository.Insert(crewToInsert);
                await _unitOfWork.SaveChangesAsync();
                // Update crew
                await UpdateCrew(crewToInsert, pilotToInsert.Id, stewardessesToInsert.Select(x => x.Id));
            }
        }

        private async Task WriteToCsv(List<CrewExternalDto> externalCrewDtos)
        {
            string path = Path.Combine(Environment.CurrentDirectory, CsvFolder, string.Format(CsvFileTemplate, DateTime.Now.ToString("dd-MM-yy___H-mm")));
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            using (StreamWriter wrt = new StreamWriter(path))
            {
                var csv = CsvSerializer.SerializeToCsv(externalCrewDtos);
                await wrt.WriteLineAsync(csv);
            }
        }
    }
}