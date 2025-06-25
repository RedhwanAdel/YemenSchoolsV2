using Microsoft.AspNetCore.Mvc;
using YemenSchoolsV1.API.Bases;
using YemenSchoolsV1.Application.Features.Cities.Commands.CreateCity;
using YemenSchoolsV1.Application.Features.Cities.Commands.DeleteCity;
using YemenSchoolsV1.Application.Features.Cities.Commands.UpdateCity;
using YemenSchoolsV1.Application.Features.Cities.Queries.GetCities;
using YemenSchoolsV1.Application.Features.Cities.Queries.GetCityDetails;

namespace YemenSchoolsV1.API.Controllers
{
	public class CitiesController : AppControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var response = await Mediator.Send(new GetCitiesListQueary());
			return NewResult(response);
		}
		[HttpGet]
		[Route("{id:guid}")]
		public async Task<IActionResult> GetSingle([FromRoute] Guid id)
		{
			var response = await Mediator.Send(new GetCityDetailsQueary(id));
			return NewResult(response);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateCityCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);

		}
		[HttpPut]
		public async Task<IActionResult> Edit([FromBody] EditCityCommand command)
		{
			var response = await Mediator.Send(command);
			return NewResult(response);

		}
		[HttpDelete]
		[Route("{id:guid}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			var response = await Mediator.Send(new DeleteCityCommand(id));
			return NewResult(response);
		}
	}
}
