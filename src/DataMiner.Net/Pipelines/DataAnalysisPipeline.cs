using GitDataMiningTool.Pipes;
using GitDataMiningTool.Commands;
using System.IO;
using GitDataMiningTool.Pipelines.Data;

namespace GitDataMiningTool.Pipelines
{
    public sealed class DataAnalysisPipeline
    {
        public IFileCopier FileCopier { get; }

        public RepositoryUrl RepositoryUrl { get; }

        public RepositoryDestination RepositoryDestination { get; }

        private DataAnalysisPipeline(
            IFileCopier fileCopier,
            RepositoryUrl repositoryUrl,
            RepositoryDestination repositoryDestination)
        {
            FileCopier = fileCopier;
            RepositoryUrl = repositoryUrl;
            RepositoryDestination = repositoryDestination;
        }

        private CompositePipe<CommandResults> Create()
            => new CompositePipe<CommandResults>(
                new ConditionalPipe<CommandResults>(
                    r => Directory.Exists(RepositoryDestination.ToString()),
                    new CompositePipe<CommandResults>(
                        SummaryDataPipeline.CreatePipeline(RepositoryDestination),
                        OrganisationalMetricsDataPipeline.CreatePipeline(RepositoryDestination),
                        CouplingDataPipeline.CreatePipeline(RepositoryDestination),
                        AgeDataPipeline.CreatePipeline(RepositoryDestination),
                        AbsoluteChurnDataPipeline.CreatePipeline(RepositoryDestination),
                        AuthorChurnDataPipeline.CreatePipeline(RepositoryDestination),
                        EntityChurnDataPipeline.CreatePipeline(RepositoryDestination),
                        EntityEffortDataPipeline.CreatePipeline(RepositoryDestination),
                        EntityOwnershipDataPipeline.CreatePipeline(RepositoryDestination)),
                    new CompositePipe<CommandResults>(
                        CloneRepositoryPipeline.CreatePipeline(RepositoryUrl, RepositoryDestination),
                        CopyFilesToDestinationPipeline.CreatePipeline(FileCopier, RepositoryDestination),
                        GenerateDataPipeline.CreatePipeline(RepositoryDestination, BenchmarkingFileNames.GitLogFileName),
                        GenerateDataPipeline.CreatePipeline(RepositoryDestination, BenchmarkingFileNames.GitAnalysisFileName),
                        SummaryDataPipeline.CreatePipeline(RepositoryDestination),
                        OrganisationalMetricsDataPipeline.CreatePipeline(RepositoryDestination),
                        CouplingDataPipeline.CreatePipeline(RepositoryDestination),
                        AgeDataPipeline.CreatePipeline(RepositoryDestination),
                        AbsoluteChurnDataPipeline.CreatePipeline(RepositoryDestination),
                        AuthorChurnDataPipeline.CreatePipeline(RepositoryDestination),
                        EntityChurnDataPipeline.CreatePipeline(RepositoryDestination),
                        EntityEffortDataPipeline.CreatePipeline(RepositoryDestination),
                        EntityOwnershipDataPipeline.CreatePipeline(RepositoryDestination))));

        public static CompositePipe<CommandResults> CreatePipeline(
            IFileCopier fileCopier,
            RepositoryUrl repositoryUrl,
            RepositoryDestination repositoryDestination)
                => new DataAnalysisPipeline(
                    fileCopier,
                    repositoryUrl,
                    repositoryDestination);

        public static implicit operator CompositePipe<CommandResults>(
            DataAnalysisPipeline pipeline)
            => pipeline.Create();
    }
}
