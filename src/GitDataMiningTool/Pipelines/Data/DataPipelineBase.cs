using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipes;
using System;

namespace GitDataMiningTool.Pipelines.Data
{
    public abstract class DataPipelineBase
    {
        private readonly string _fileToRead;
        private readonly DataAnalysisResultType _resultType;

        public RepositoryDestination RepositoryDestination { get; }

        protected DataPipelineBase(
            string fileToRead,
            DataAnalysisResultType resultType,
            RepositoryDestination repositoryDestination)
        {
            RepositoryDestination = repositoryDestination
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
