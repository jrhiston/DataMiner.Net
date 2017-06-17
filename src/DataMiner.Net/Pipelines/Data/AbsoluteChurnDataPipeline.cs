using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipes;

namespace GitDataMiningTool.Pipelines.Data
{
    public sealed class AbsoluteChurnDataPipeline : DataPipelineBase
    {
        public AbsoluteChurnDataPipeline(
            RepositoryDestination repositoryDestination)
            : base(
                  "abs-churn",
                  DataAnalysisResultType.AbsoluteChurn,
                  repositoryDestination)
        {
        }

        public static CompositePipe<CommandResults> CreatePipeline(
            RepositoryDestination repositoryDestination)
                => new AbsoluteChurnDataPipeline(
                    repositoryDestination);
    }
}
