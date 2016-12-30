using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipes;

namespace GitDataMiningTool
{
    internal interface IPipelineFactory
    {
        IPipeline<CommandResults> CreateDataAnalysisPipeline(
            RepositoryUrl repositoryUrl,
            RepositoryDestination repositoryDestination);
    }
}