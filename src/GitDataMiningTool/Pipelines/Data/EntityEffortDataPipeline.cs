using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipes;

namespace GitDataMiningTool.Pipelines.Data
{
    public sealed class EntityEffortDataPipeline : DataPipelineBase
    {
        private EntityEffortDataPipeline(
            RepositoryDestination repositoryDestination)
            : base(
                  "entity-effort.csv",
                  DataAnalysisResultType.EntityEffort,
                  repositoryDestination)
        {
        }

        public static CompositePipe<CommandResults> CreatePipeline(
            RepositoryDestination repositoryDestination)
                => new EntityEffortDataPipeline(
                    repositoryDestination);
    }
}
