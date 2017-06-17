using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipes;
using System;

namespace GitDataMiningTool.Pipelines.Data
{
    /// <summary>
    /// Allows the reader of a file.
    /// </summary>
    public sealed class DataAnalysisFileReaderPipeline
    {
        private readonly string _fileToRead;
        private readonly RepositoryDestination _repositoryDestination;
        private readonly DataAnalysisResultType _resultType;

        private DataAnalysisFileReaderPipeline(
            string fileToRead,
            DataAnalysisResultType resultType,
            RepositoryDestination repositoryDestination)
        {
            _fileToRead = fileToRead
                ?? throw new ArgumentNullException(nameof(fileToRead));
            _repositoryDestination = repositoryDestination
                ?? throw new ArgumentNullException(nameof(repositoryDestination));
            _resultType = resultType;
        }

        private CompositePipe<CommandResults> Create() =>
            new CompositePipe<CommandResults>(
                new CommandVisitorPipe(
                    new DataAnalysisFileReaderCommand(
                        _fileToRead,
                        _resultType,
                        _repositoryDestination)));

        public static CompositePipe<CommandResults> CreatePipeline(
            string fileToRead,
            DataAnalysisResultType resultType,
            RepositoryDestination repositoryDestination)
                => new DataAnalysisFileReaderPipeline(
                    fileToRead,
                    resultType,
                    repositoryDestination);

        public static implicit operator CompositePipe<CommandResults>(DataAnalysisFileReaderPipeline pipeline)
            => pipeline.Create();
    }
}
