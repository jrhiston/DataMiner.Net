using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipes;
using System;

namespace GitDataMiningTool.Pipelines.Data
{
    public class OrganisationalMetricsDataPipeline : DataPipelineBase
    {
        private OrganisationalMetricsDataPipeline(
            RepositoryDestination repositoryDestination) 
            : base(
                  "org-metrics.csv",
                  DataAnalysisResultType.OrganisationMetrics,
                  repositoryDestination)
        {
        }

        public static CompositePipe<CommandResults> CreatePipeline(
            RepositoryDestination repositoryDestination)
                => new OrganisationalMetricsDataPipeline(
                    repositoryDestination);
    }
}
