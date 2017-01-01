using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipes;
using System;
using System.Threading.Tasks;

namespace GitDataMiningTool
{
    public class RepositoryAnalyser
    {
        private readonly CompositePipe<CommandResults> _pipeline;

        public RepositoryAnalyser(CompositePipe<CommandResults> pipeline)
        {
            _pipeline = pipeline ?? throw new ArgumentNullException(nameof(pipeline));
        }

        public CompositePipe<CommandResults> PipelineFactory => _pipeline;

        public CommandResults Analyse(
            RepositoryUrl repositoryUrl,
            RepositoryDestination repositoryDestination) 
                => _pipeline.Pipe(new CommandResults());

        public Task<CommandResults> AnalyseAsync(
            RepositoryUrl repository,
            RepositoryDestination repositoryDestination) 
                => Task.Run(() => Analyse(repository, repositoryDestination));
    }
}
