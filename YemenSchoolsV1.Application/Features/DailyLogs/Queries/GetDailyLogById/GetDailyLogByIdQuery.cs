using MediatR;
using YemenSchoolsV1.Application.Bases;
using YemenSchoolsV1.Application.Features.DailyLogs.Dto;

namespace YemenSchoolsV1.Application.Features.DailyLogs.Queries.GetDailyLogById
{
    public class GetDailyLogByIdQuery : IRequest<Response<DailyLogDto>>
    {
        public Guid Id { get; set; }

        public GetDailyLogByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
