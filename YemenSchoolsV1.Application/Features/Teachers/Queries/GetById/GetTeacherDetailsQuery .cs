﻿using FinalProject.Application.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YemenSchoolsV1.Application.Features.Teachers.Queries.GetById
{
    public class GetTeacherDetailsQuery : IRequest<Response<GetTeacherDetailsResponse>>
    {
        public GetTeacherDetailsQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }

}
