using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Data;

namespace YemenSchoolsV1.Persistence.Repositories
{
    public class SubjectGradeRepository : GenericRepositoryAsync<SubjectGrade>, ISubjectGradeRepositry
    {
        private readonly YemenShoolsDbContext dbContext;

        public SubjectGradeRepository(YemenShoolsDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get SubjectGrades by SubjectId
        public async Task<List<SubjectGrade>> GetBySubjectIdAsync(Guid subjectId)
        {
            return await dbContext.SubjectGrades
                .Include(sg => sg.Subject)   
                .Include(sg => sg.Grade)    
                .Where(sg => sg.SubjectId == subjectId)   
                .ToListAsync();  
        }

        // Get SubjectGrades by GradeId
        public async Task<List<SubjectGrade>> GetByGradeIdAsync(Guid gradeId)
        {
            return await dbContext.SubjectGrades
                .Include(sg => sg.Subject)   
                .Include(sg => sg.Grade)    
                .Where(sg => sg.GradeId == gradeId)   
                .ToListAsync();  
        }
    }

}
