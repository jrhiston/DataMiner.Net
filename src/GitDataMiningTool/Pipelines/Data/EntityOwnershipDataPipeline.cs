using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipes;

namespace GitDataMiningTool.Pipelines.Data
{
    public sealed class EntityOwnershipDataPipeline : DataPipelineBase
    {
        private EntityOwnershipDataPipeline(
            RepositoryDestination repositoryDestination)
            : base(
                  "entity-ownership.csv",
                  DataAnalysisResultType.EntityOwnership,
                  repositoryDestination)
        {
        }

        public static CompositePipe<CommandResults> CreatePipeline(
            RepositoryDestination repositoryDestination)
                => new EntityOwnershipDataPipeline(
                    repositoryDestination);
    }
}
