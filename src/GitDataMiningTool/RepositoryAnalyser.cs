using GitDataMiningTool.Commands;
using System;
using System.Threading.Tasks;

namespace GitDataMiningTool
{
    internal class RepositoryAnalyser : IRepositoryAnalyser
    {
        private readonly IPipelineFactory _pipelineFactory;

        public RepositoryAnalyser(IPipelineFactory pipelineFactory)
        {
            if (pipelineFactory == null)
                throw new ArgumentNullException(nameof(pipelineFactory));

            _pipelineFactory = pipelineFactory;
        }

        public IPipelineFactory PipelineFactory => _pipelineFactory;

        public CommandResults Analyse(
            RepositoryUrl repositoryUrl,
            RepositoryDestination repositoryDestination) 
                => _pipelineFactory
                    .CreateDataAnalysisPipeline(repositoryUrl, repositoryDestination)
                    .Create()
                    .Pipe(new CommandResults());

        public Task<CommandResults> AnalyseAsync(
            RepositoryUrl repository,
            RepositoryDestination repositoryDestination) 
                => Task.Run(() => Analyse(repository, repositoryDestination));
    }
}
