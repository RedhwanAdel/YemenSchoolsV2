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
        private readonly IStageRepository stageRepository;

        public StageService(IStageRepository stageRepository)
        {
            this.stageRepository = stageRepository;
        }
        public async Task<Stage?> CreateStageAsync(Stage stage)
        {
            if (stage == null)
            {
                throw new ArgumentNullException(nameof(stage));
            }
            return await stageRepository.AddAsync(stage);
        }

        public async Task<bool> DeleteStageAsync(Guid id)
        {
            var stage = await stageRepository.GetByIdAsync(id);
            if (stage == null)
                return false;
            return await stageRepository.DeleteAsync(id);
        }

        public async Task<Stage?> EditStageAsync(Guid id, Stage stage)
        {
            if (stage == null)
            {
                throw new ArgumentNullException(nameof(stage));
            }
            var existingStage = await stageRepository.GetByIdAsync(id);
            if (existingStage == null) { return null; }
            return await stageRepository.UpdateAsync(id, stage);
        }

        public async Task<List<Stage>> GetAllStagesAsync()
        {
            return await stageRepository.GetAllAsync();
        }

        public async Task<Stage?> GetStageDetailsAsync(Guid id)
        {
            return await stageRepository.GetByIdAsync(id);
        }
    }
}
