using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipes;
using System;

namespace GitDataMiningTool.Pipelines
{
    /// <summary>
    /// Copy the benchmarking files to the destination given.
    /// </summary>
    public class CopyFilesToDestinationPipeline
    {
        private readonly IFileCopier _fileCopier;
        private readonly RepositoryDestination _repositoryDestination;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repositoryDestination">The destination to copy to.</param>
        /// <param name="fileCopier">The implementation that will copy the benchmark files.</param>
        private CopyFilesToDestinationPipeline(
            RepositoryDestination repositoryDestination,
            IFileCopier fileCopier = null)
        {
            _fileCopier = fileCopier 
                ?? new DefaultFileCopier();
            _repositoryDestination = repositoryDestination 
                ?? throw new ArgumentNullException(nameof(repositoryDestination));
        }

        /// <summary>
        /// Create the pipeline.
        /// </summary>
        /// <returns>A pipeline of type <see cref="CompositePipe{T}"/>.</returns>
        private CompositePipe<CommandResults> Create() =>
            new CompositePipe<CommandResults>(
                new CommandVisitorPipe(
                    new CopyFilesToDestinationCommand(
                        _fileCopier, 
                        _repositoryDestination)));

        /// <summary>
        /// Create the pipeline.
        /// </summary>
        /// <param name="fileCopier">The implementation that will copy the benchmark files.</param>
        /// <param name="repositoryDestination">The destination to copy to.</param>
        public static CompositePipe<CommandResults> CreatePipeline(
            IFileCopier fileCopier,
            RepositoryDestination repositoryDestination)
            => new CopyFilesToDestinationPipeline(repositoryDestination, fileCopier);

        public static implicit operator CompositePipe<CommandResults>(
            CopyFilesToDestinationPipeline pipeline) 
            => pipeline.Create();
    }
}
