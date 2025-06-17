using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YemenSchoolsV1.Application.Contracts.Persistence;
using YemenSchoolsV1.Application.Contracts.Services;
using YemenSchoolsV1.Domain.Entities;
using YemenSchoolsV1.Persistence.Repositories;

namespace YemenSchoolsV1.Services.Implementations
{
    public class StageService : IStageService
    {
        private readonly IStageRepositry stageRepositry;

        public StageService(IStageRepositry stageRepositry)
        {
            this.stageRepositry = stageRepositry;
        }
        public async Task<Stage?> CreateStageAsync(Stage stage)
        {
            if (stage == null)
            {
                throw new ArgumentNullException(nameof(stage));
            }
            return await stageRepositry.AddAsync(stage);
        }

        public async Task<bool> DeleteStageAsync(Guid id)
        {
            var stage = await stageRepositry.GetByIdAsync(id);
            if (stage == null)
                return false;
            return await stageRepositry.DeleteAsync(id);
        }

        public async Task<Stage?> EditStageAsync(Guid id, Stage stage)
        {
            if (stage == null)
            {
                throw new ArgumentNullException(nameof(stage));
            }
            var existingStage = await stageRepositry.GetByIdAsync(id);
            if (existingStage == null) { return null; }
            return await stageRepositry.UpdateAsync(id, stage);
        }

        public async Task<List<Stage>> GetAllStagesAsync()
        {
            return await stageRepositry.GetAllAsync();
        }

        public async Task<Stage?> GetStageDetailsAsync(Guid id)
        {
            return await stageRepositry.GetByIdAsync(id);
        }
    }
}
