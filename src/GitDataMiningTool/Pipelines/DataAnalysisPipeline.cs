using GitDataMiningTool.Pipes;
using GitDataMiningTool.Commands;
using System.IO;
using GitDataMiningTool.Pipelines.Data;

namespace GitDataMiningTool.Pipelines
{
    public class DataAnalysisPipeline
    {
        private readonly IFileCopier _fileCopier;
        private readonly RepositoryUrl _repositoryUrl;
        private readonly RepositoryDestination _repositoryDestination;

        public IFileCopier FileCopier => _fileCopier;
        public RepositoryUrl RepositoryUrl => _repositoryUrl;
        public RepositoryDestination RepositoryDestination => _repositoryDestination;

        private DataAnalysisPipeline(
            IFileCopier fileCopier,
            RepositoryUrl repositoryUrl,
            RepositoryDestination repositoryDestination)
        {
            _fileCopier = fileCopier;
            _repositoryUrl = repositoryUrl;
            _repositoryDestination = repositoryDestination;
        }

        private CompositePipe<CommandResults> Create()
            => new CompositePipe<CommandResults>(
                new ConditionalPipe<CommandResults>(
                    r => Directory.Exists(_repositoryDestination.ToString()),
                    new CompositePipe<CommandResults>(
                        SummaryDataPipeline.CreatePipeline(_repositoryDestination),
                        OrganisationalMetricsDataPipeline.CreatePipeline(_repositoryDestination),
                        CouplingDataPipeline.CreatePipeline(_repositoryDestination),
                        AgeDataPipeline.CreatePipeline(_repositoryDestination),
                        AbsoluteChurnDataPipeline.CreatePipeline(_repositoryDestination),
                        AuthorChurnDataPipeline.CreatePipeline(_repositoryDestination),
                        EntityChurnDataPipeline.CreatePipeline(_repositoryDestination),
                        EntityEffortDataPipeline.CreatePipeline(_repositoryDestination),
                        EntityOwnershipDataPipeline.CreatePipeline(_repositoryDestination)),
                    new CompositePipe<CommandResults>(
                        CloneRepositoryPipeline.CreatePipeline(_repositoryUrl, _repositoryDestination),
                        CopyFilesToDestinationPipeline.CreatePipeline(_fileCopier, _repositoryDestination),
                        GenerateDataPipeline.CreatePipeline(_repositoryDestination, BenchmarkingFileNames.GitLogFileName),
                        GenerateDataPipeline.CreatePipeline(_repositoryDestination, BenchmarkingFileNames.GitAnalysisFileName),
                        SummaryDataPipeline.CreatePipeline(_repositoryDestination),
                        OrganisationalMetricsDataPipeline.CreatePipeline(_repositoryDestination),
                        CouplingDataPipeline.CreatePipeline(_repositoryDestination),
                        AgeDataPipeline.CreatePipeline(_repositoryDestination),
                        AbsoluteChurnDataPipeline.CreatePipeline(_repositoryDestination),
                        AuthorChurnDataPipeline.CreatePipeline(_repositoryDestination),
                        EntityChurnDataPipeline.CreatePipeline(_repositoryDestination),
                        EntityEffortDataPipeline.CreatePipeline(_repositoryDestination),
                        EntityOwnershipDataPipeline.CreatePipeline(_repositoryDestination))));

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
