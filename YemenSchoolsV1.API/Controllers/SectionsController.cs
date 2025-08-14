using AutoMapper;
using FinalProject.Application.Bases;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Dto;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.API.Controllers
{

    public class SectionsController(ISectionRepositry _sectionRepositry, IMapper mapper) : AppControllerBase
    {
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetSectionById(Guid id)
        {
            if (id == Guid.Empty)
                return NewResult(new Response<SectionDto>("Invalid section ID.", false) { StatusCode = HttpStatusCode.BadRequest });

            var section = await _sectionRepositry.GetSectionByIdAsync(id);
            if (section == null)
                return NewResult(new Response<SectionDto>("Section not found.", false) { StatusCode = HttpStatusCode.NotFound });

            var sectionDto = mapper.Map<SectionDto>(section);
            return NewResult(new Response<SectionDto>(sectionDto) { StatusCode = HttpStatusCode.OK, Succeeded = true });

        }

        [HttpGet("by-academic-year-and-grade")]
        public async Task<IActionResult> GetByAcademicYearAndSchoolGrade([FromQuery] Guid academicYearId, [FromQuery] Guid schoolGradeId)
        {
            if (academicYearId == Guid.Empty || schoolGradeId == Guid.Empty)
                return NewResult(new Response<IEnumerable<SectionByGradeAndYearDto>>("Invalid academic year or school grade ID.", false) { StatusCode = HttpStatusCode.BadRequest });

            var sections = await _sectionRepositry.GetSectionsByAcademicYearAndSchoolGradeAsync(academicYearId, schoolGradeId);
            var sectionDtos = mapper.Map<IEnumerable<SectionByGradeAndYearDto>>(sections);
            return NewResult(new Response<IEnumerable<SectionByGradeAndYearDto>>(sectionDtos) { StatusCode = HttpStatusCode.OK, Succeeded = true });
        }

        [HttpGet("by-teacherId/{teacherId:guid}")]
        public async Task<IActionResult> GetByTeacherId(Guid teacherId)
        {
            if (teacherId == Guid.Empty)
                return NewResult(new Response<IEnumerable<SectionByGradeAndYearDto>>("Invalid teacher ID.", false) { StatusCode = HttpStatusCode.BadRequest });

            var sections = await _sectionRepositry.GetSectionsByTeacherIdAsync(teacherId);
            var sectionDtos = mapper.Map<IEnumerable<SectionByGradeAndYearDto>>(sections);
            return NewResult(new Response<IEnumerable<SectionByGradeAndYearDto>>(sectionDtos) { StatusCode = HttpStatusCode.OK, Succeeded = true });
        }

        /// <summary>
        /// Create a new section.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSectionDto dto)
        {
            if (dto == null)
                return NewResult(new Response<string>("Section data is required.", false) { StatusCode = HttpStatusCode.BadRequest });

            // Manual mapping, replace with AutoMapper if available
            var section = mapper.Map<Section>(dto);

            var created = await _sectionRepositry.AddAsync(section);
            if (created == null)
                return NewResult(new Response<string>("Failed to create section.", false) { StatusCode = HttpStatusCode.InternalServerError });
            return NewResult(new Response<string>("Section created successfully.") { StatusCode = HttpStatusCode.Created, Succeeded = true });
        }

        /// <summary>
        /// Update an existing section.
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSectionDto dto)
        {
            if (dto == null)
                return NewResult(new Response<Section>("Section data is required.", false) { StatusCode = HttpStatusCode.BadRequest });

            var section = new Section
            {
                Id = id,
                Name = dto.Name,
                AcademicYearId = dto.AcademicYearId,
                SchoolGradeId = dto.SchoolGradeId,
                Capacity = dto.Capacity,
                ClassTeacherId = dto.ClassTeacherId

            };

            var updated = await _sectionRepositry.UpdateAsync(id, section);
            if (updated == null)
                return NewResult(new Response<string>("Section not found.", false) { StatusCode = HttpStatusCode.NotFound });

            return NewResult(new Response<string>("Section updated successfully.") { StatusCode = HttpStatusCode.OK, Succeeded = true });
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _sectionRepositry.DeleteAsync(id);
            if (!deleted)
                return NewResult(new Response<bool>("Section not found.", false) { StatusCode = HttpStatusCode.NotFound });

            return NewResult(new Response<bool>(true, "Section deleted successfully.") { StatusCode = HttpStatusCode.OK, Succeeded = true });
        }


        [HttpGet("by-academic-year")]
        public async Task<IActionResult> GetSectionSummariesByAcademicYear([FromQuery] Guid academicYearId)
        {
            if (academicYearId == Guid.Empty)
                return NewResult(new Response<List<SectionSummaryDto>>("Invalid academic year ID.", false) { StatusCode = HttpStatusCode.BadRequest });

            var summaries = await _sectionRepositry.GetSectionSummariesByAcademicYearAsync(academicYearId);
            return NewResult(new Response<List<SectionSummaryDto>>(summaries) { StatusCode = HttpStatusCode.OK, Succeeded = true });
        }


    }
}
