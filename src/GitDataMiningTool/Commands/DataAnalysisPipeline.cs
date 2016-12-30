using GitDataMiningTool.Pipes;
using System.Linq;
using System.IO;

namespace GitDataMiningTool.Commands
{
    internal class DataAnalysisPipeline : IPipeline<CommandResults>
    {
        private readonly IFileCopier _fileCopier;
        private readonly RepositoryUrl _repositoryUrl;
        private readonly RepositoryDestination _repositoryDestination;

        public IFileCopier FileCopier => _fileCopier;
        public RepositoryUrl RepositoryUrl => _repositoryUrl;
        public RepositoryDestination RepositoryDestination => _repositoryDestination;

        public DataAnalysisPipeline(
            IFileCopier fileCopier,
            RepositoryUrl repositoryUrl,
            RepositoryDestination repositoryDestination)
        {
            _fileCopier = fileCopier;
            _repositoryUrl = repositoryUrl;
            _repositoryDestination = repositoryDestination;
        }

        public CompositePipe<CommandResults> Create()
            => new CompositePipe<CommandResults>(
                new ConditionalPipe<CommandResults>(
                    r => Directory.Exists(_repositoryDestination.ToString()), 
                    CreateFileDataReaderPipe(),
                    new CompositePipe<CommandResults>(
                        GenerateData().Concat(CreateFileDataReaderPipe()).ToArray())));

        private CompositePipe<CommandResults> CreateFileDataReaderPipe()
        {
            return new CompositePipe<CommandResults>(
                CreateFileDataReaderPipe("summary.csv", DataAnalysisResultType.Summary, _repositoryDestination),
                CreateFileDataReaderPipe("org-metrics.csv", DataAnalysisResultType.OrganisationMetrics, _repositoryDestination),
                CreateFileDataReaderPipe("coupling.csv", DataAnalysisResultType.Coupling, _repositoryDestination),
                CreateFileDataReaderPipe("age.csv", DataAnalysisResultType.Age, _repositoryDestination),
                CreateFileDataReaderPipe("abs-churn.csv", DataAnalysisResultType.AbsoluteChurn, _repositoryDestination),
                CreateFileDataReaderPipe("author-churn.csv", DataAnalysisResultType.AuthorChurn, _repositoryDestination),
                CreateFileDataReaderPipe("entity-churn.csv", DataAnalysisResultType.EntityChurn, _repositoryDestination),
                CreateFileDataReaderPipe("entity-ownership.csv", DataAnalysisResultType.EntityOwnership, _repositoryDestination),
                CreateFileDataReaderPipe("entity-effort.csv", DataAnalysisResultType.EntityEffort, _repositoryDestination));
        }

        private CommandVisitorPipe CreateFileDataReaderPipe(
            string fileToRead,
            DataAnalysisResultType type,
            RepositoryDestination destination) 
                => new CommandVisitorPipe(
                    new FileDataReaderCommand(fileToRead, type, destination));

        private CompositePipe<CommandResults> GenerateData() => 
            new CompositePipe<CommandResults>(
                new CommandVisitorPipe(
                    new CloneGitRepositoryCommand(_repositoryUrl, _repositoryDestination)),
                new CommandVisitorPipe(
                    new CopyFilesToDestinationCommand(FileCopier, _repositoryDestination)),
                new CommandVisitorPipe(
                    new GenerateDataCommand(_repositoryDestination, BenchmarkingFileNames.GitLogFileName)),
                new CommandVisitorPipe(
                    new GenerateDataCommand(_repositoryDestination, BenchmarkingFileNames.GitAnalysisFileName)));
    }
}
