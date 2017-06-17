using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipelines;
using GitDataMiningTool.Pipes;
using System.Threading.Tasks;

namespace GitDataMiningTool
{
    /// <summary>
    /// Analyses a git repository that is publicly available, given a <see cref="RepositoryUrl"/>,
    /// and a <see cref="RepositoryDestination"/> to process the repository in.
    /// </summary>
    public class RepositoryAnalyser
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="RepositoryAnalyser"/>.
        /// </summary>
        /// <param name="url">A <see cref="RepositoryUrl"/> representing the url of the git repository</param>
        /// <param name="destination">A <see cref="RepositoryDestination"/> for extracting the repository into.</param>
        public RepositoryAnalyser(RepositoryUrl url, RepositoryDestination destination)
        {
            Pipeline = DataAnalysisPipeline.CreatePipeline(new DefaultFileCopier(), url, destination);
        }

        /// <summary>
        /// Gets the pipeline that will be run by this analyser.
        /// </summary>
        public CompositePipe<CommandResults> Pipeline { get; }

        /// <summary>
        /// Pipes a <see cref="CommandResults"/> through the <see cref="Pipeline"/>.
        /// </summary>
        /// <param name="results">An optional set of results to pipe into the pipeline.</param>
        /// <returns>A <see cref="CommandResults"/> after processing the git repository.</returns>

        public CommandResults Analyse(CommandResults results = null) => Pipeline.Pipe(results ?? new CommandResults());

        /// <summary>
        /// Pipes a <see cref="CommandResults"/> through the <see cref="Pipeline"/> asynchronously.
        /// </summary>
        /// <param name="results">An optional set of results to pipe into the pipeline.</param>
        /// <returns>A <see cref="CommandResults"/> after processing the git repository.</returns>

        public Task<CommandResults> AnalyseAsync() => Task.Run(() => Analyse());
    }
}
