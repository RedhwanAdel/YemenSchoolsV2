using AutoMapper;
using FinalProject.Application.Bases;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Application.Dto.Students;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.API.Controllers
{
    public class SectionSubjectsController(ISectionSubjectRepository _sectionSubjectRepository, IMapper _mapper) : AppControllerBase
    {


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _sectionSubjectRepository.GetAllAsync();
            var dtos = _mapper.Map<List<SectionSubjectInfoDto>>(entities);
            return NewResult(new Response<List<SectionSubjectInfoDto>>(dtos) { StatusCode = HttpStatusCode.OK, Succeeded = true });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var entity = await _sectionSubjectRepository.GetByIdAsync(id);
            if (entity == null)
                return NewResult(new Response<SectionSubjectInfoDto>("SectionSubject not found.", false) { StatusCode = HttpStatusCode.NotFound });

            var dto = _mapper.Map<SectionSubjectInfoDto>(entity);
            return NewResult(new Response<SectionSubjectInfoDto>(dto) { StatusCode = HttpStatusCode.OK, Succeeded = true });
        }


        [HttpGet("by-section/{sectionId:guid}")]
        public async Task<IActionResult> GetBySectionId(Guid sectionId)
        {
            var result = await _sectionSubjectRepository.GetSectionSubjectsInfoBySectionIdAsync(sectionId);
            return NewResult(new Response<List<SectionSubjectInfoDto>>(result) { StatusCode = HttpStatusCode.OK, Succeeded = true });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSectionSubjectDto dto)
        {
            if (dto == null)
                return NewResult(new Response<string>("SectionSubject data is required.", false) { StatusCode = HttpStatusCode.BadRequest });

            var entity = _mapper.Map<SectionSubject>(dto);
            var created = await _sectionSubjectRepository.AddAsync(entity);
            if (created == null)
                return NewResult(new Response<string>("Failed to create SectionSubject.", false) { StatusCode = HttpStatusCode.InternalServerError });

            return NewResult(new Response<string>("SectionSubject created successfully.") { StatusCode = HttpStatusCode.Created, Succeeded = true });
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SectionSubjecUpdateDto dto)
        {
            if (dto == null)
                return NewResult(new Response<string>("SectionSubject data is required.", false) { StatusCode = HttpStatusCode.BadRequest });

            var entity = _mapper.Map<SectionSubject>(dto);
            entity.Id = id;
            var updated = await _sectionSubjectRepository.UpdateAsync(id, entity);
            if (updated == null)
                return NewResult(new Response<string>("SectionSubject not found.", false) { StatusCode = HttpStatusCode.NotFound });

            return NewResult(new Response<string>("SectionSubject updated successfully.") { StatusCode = HttpStatusCode.OK, Succeeded = true });
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _sectionSubjectRepository.DeleteAsync(id);
            if (!deleted)
                return NewResult(new Response<bool>("SectionSubject not found.", false) { StatusCode = HttpStatusCode.NotFound });

            return NewResult(new Response<bool>(true, "SectionSubject deleted successfully.") { StatusCode = HttpStatusCode.OK, Succeeded = true });
        }
    }
}
