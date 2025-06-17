using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Domain.Entities;

namespace YemenSchoolsV1.Application.Contracts.Services
{
    public interface IStageService
    {
        public Task<List<Stage>> GetAllStagesAsync();
        public Task<Stage?> GetStageDetailsAsync(Guid id);
        public Task<Stage?> CreateStageAsync(Stage stage);
        public Task<Stage?> EditStageAsync(Guid id, Stage stage);
        public Task<bool> DeleteStageAsync(Guid id);
    }
}
