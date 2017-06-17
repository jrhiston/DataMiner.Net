using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipes;

namespace GitDataMiningTool.Pipelines.Data
{
    public sealed class SummaryDataPipeline : DataPipelineBase
    {
        private SummaryDataPipeline(
            RepositoryDestination repositoryDestination)
            : base(
                  "summary.csv",
                  DataAnalysisResultType.Summary,
                  repositoryDestination)
        {
        }

        public static CompositePipe<CommandResults> CreatePipeline(
            RepositoryDestination repositoryDestination)
                => new SummaryDataPipeline(
                    repositoryDestination);
    }
}
