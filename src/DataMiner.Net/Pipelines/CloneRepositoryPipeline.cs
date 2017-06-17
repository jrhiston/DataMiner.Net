using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipes;

namespace GitDataMiningTool.Pipelines
{
    /// <summary>
    /// A pipeline for cloning repositories.
    /// </summary>
    public sealed class CloneRepositoryPipeline
    {
        private RepositoryDestination _repositoryDestination;
        private RepositoryUrl _repositoryUrl;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repositoryUrl">The url to clone.</param>
        /// <param name="repositoryDestination">The destination to clone into.</param>
        private CloneRepositoryPipeline(
            RepositoryUrl repositoryUrl,
            RepositoryDestination repositoryDestination)
        {
            _repositoryUrl = repositoryUrl;
            _repositoryDestination = repositoryDestination;
        }

        /// <summary>
        /// Create the pipeline.
        /// </summary>
        /// <returns></returns>
        private CompositePipe<CommandResults> Create() =>
            new CompositePipe<CommandResults>(
                new CommandVisitorPipe(
                    new CloneGitRepositoryCommand(
                        _repositoryUrl,
                        _repositoryDestination)));

        /// <summary>
        /// Create this pipeline.
        /// </summary>
        /// <param name="repositoryUrl">The url to clone.</param>
        /// <param name="repositoryDestination">The destination to clone into.</param>
        public static CompositePipe<CommandResults> CreatePipeline(
            RepositoryUrl repositoryUrl,
            RepositoryDestination repositoryDestination)
                => new CloneRepositoryPipeline(repositoryUrl, repositoryDestination);

        public static implicit operator CompositePipe<CommandResults>(CloneRepositoryPipeline pipeline)
            => pipeline.Create();
    }
}