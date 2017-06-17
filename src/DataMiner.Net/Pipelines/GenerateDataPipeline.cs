using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipes;
using System;

namespace GitDataMiningTool.Pipelines
{
    /// <summary>
    /// Generate the data to be analysed by the tool.
    /// </summary>
    public sealed class GenerateDataPipeline
    {
        private readonly string _fileToRun;
        private readonly RepositoryDestination _repositoryDestination;

        private GenerateDataPipeline(
            RepositoryDestination repositoryDestination,
            string fileToRun)
        {
            _repositoryDestination = repositoryDestination
                ?? throw new ArgumentNullException(nameof(repositoryDestination));
            _fileToRun = fileToRun
                ?? throw new ArgumentNullException(nameof(fileToRun));
        }

        private CompositePipe<CommandResults> Create()
        {
            return new CompositePipe<CommandResults>(
                new CommandVisitorPipe(
                    new GenerateDataCommand(_repositoryDestination, _fileToRun)));
        }

        public static CompositePipe<CommandResults> CreatePipeline(
            RepositoryDestination repositoryDestination,
            string fileToRun)
            => new GenerateDataPipeline(repositoryDestination, fileToRun);

        public static implicit operator CompositePipe<CommandResults>(
            GenerateDataPipeline pipeline)
            => pipeline.Create();
    }
}
