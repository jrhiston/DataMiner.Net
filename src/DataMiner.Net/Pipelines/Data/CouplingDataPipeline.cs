using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipes;

namespace GitDataMiningTool.Pipelines.Data
{
    public sealed class CouplingDataPipeline : DataPipelineBase
    {
        private CouplingDataPipeline(
            RepositoryDestination repositoryDestination)
            : base(
                  "coupling.csv",
                  DataAnalysisResultType.Coupling,
                  repositoryDestination)
        {
        }

        public static CompositePipe<CommandResults> CreatePipeline(
            RepositoryDestination repositoryDestination)
                => new CouplingDataPipeline(
                    repositoryDestination);
    }
}
