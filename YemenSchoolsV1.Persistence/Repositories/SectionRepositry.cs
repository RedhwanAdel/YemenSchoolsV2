﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class SectionRepositry : GenericRepositoryAsync<Section>, ISectionRepositry
    {
        public SectionRepositry(YemenShoolsDbContext dbContext) : base(dbContext)
        {

        }
    }
}
