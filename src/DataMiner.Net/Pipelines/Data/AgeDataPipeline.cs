using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipes;

namespace GitDataMiningTool.Pipelines.Data
{
    public sealed class AgeDataPipeline : DataPipelineBase
    {
        private AgeDataPipeline(
            RepositoryDestination repositoryDestination)
            : base(
                  "age.csv",
                  DataAnalysisResultType.Age,
                  repositoryDestination)
        {
        }

        public static CompositePipe<CommandResults> CreatePipeline(
            RepositoryDestination repositoryDestination)
                => new AgeDataPipeline(
                    repositoryDestination);
    }
}
