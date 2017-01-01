using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipes;
using System;

namespace GitDataMiningTool.Pipelines.Data
{
    public abstract class DataPipelineBase
    {
        private readonly string _fileToRead;
        private readonly RepositoryDestination _repositoryDestination;
        private readonly DataAnalysisResultType _resultType;

        public RepositoryDestination RepositoryDestination => _repositoryDestination;

        protected DataPipelineBase(
            string fileToRead,
            DataAnalysisResultType resultType,
            RepositoryDestination repositoryDestination)
        {
            _repositoryDestination = repositoryDestination
                ?? throw new ArgumentNullException(nameof(repositoryDestination));
            _fileToRead = fileToRead
                ?? throw new ArgumentNullException(nameof(fileToRead));

            _resultType = resultType;
        }

        protected CompositePipe<CommandResults> Create() =>
            new CompositePipe<CommandResults>(
                new CommandVisitorPipe(
                    new DataAnalysisFileReaderCommand(
                        _fileToRead,
                        _resultType,
                        RepositoryDestination)));

        public static implicit operator CompositePipe<CommandResults>(
            DataPipelineBase pipeline)
            => pipeline.Create();
    }
}
