using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipes;

namespace GitDataMiningTool.Pipelines.Data
{
    public class AuthorChurnDataPipeline : DataPipelineBase
    {
        private AuthorChurnDataPipeline(
            RepositoryDestination repositoryDestination) 
            : base(
                  "author-churn.csv", 
                  DataAnalysisResultType.AuthorChurn, 
                  repositoryDestination)
        {
        }

        public static CompositePipe<CommandResults> CreatePipeline(
            RepositoryDestination repositoryDestination)
                => new AuthorChurnDataPipeline(
                    repositoryDestination);
    }
}
