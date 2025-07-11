﻿using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Features.Schools.Queries.GetSchoolDetails;

namespace YemenSchoolsV1.Application.Features.SchoolsNews.Queries.GetSchoolNewsDetails
{
    public class GetSchoolNewsDetailsQueary : IRequest<Response<GetSchoolNewsDetailsRseponse>>
    {
        public GetSchoolNewsDetailsQueary(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }

    }
}
