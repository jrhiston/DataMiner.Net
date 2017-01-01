using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipes;

namespace GitDataMiningTool.Pipelines.Data
{
    public class EntityChurnDataPipeline : DataPipelineBase 
    {
        private EntityChurnDataPipeline(
            RepositoryDestination repositoryDestination) 
            : base(
                  "entity-churn.csv", 
                  DataAnalysisResultType.EntityChurn, 
                  repositoryDestination)
        {
        }

        public static CompositePipe<CommandResults> CreatePipeline(
            RepositoryDestination repositoryDestination)
                => new EntityChurnDataPipeline(
                    repositoryDestination);
    }
}
