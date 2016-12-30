using GitDataMiningTool;
using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipes;
using Moq;

namespace GitDataMiningTool.Tests.Builders
{
    internal class PipelineFactoryBuilder
    {
        private RepositoryUrl _repositoryUrl = new RepositoryUrl("URL");
        private RepositoryDestination _repositoryDestination = new RepositoryDestination("DEST");
        private IPipeline<CommandResults> _pipeline = Mock.Of<IPipeline<CommandResults>>(
            p => p.Create() == new CompositePipe<CommandResults>());

        public PipelineFactoryBuilder()
        {
            
        }

        public IPipeline<CommandResults> Pipeline => _pipeline;
        public RepositoryUrl RepositoryUrl => _repositoryUrl;
        public RepositoryDestination RepositoryDestination => _repositoryDestination;

        public PipelineFactoryBuilder SetRepositoryUrl(string repositoryUrl)
        {
            _repositoryUrl = new RepositoryUrl(repositoryUrl);

            return this;
        }

        public PipelineFactoryBuilder SetRepositoryDestination(string repositoryDestination)
        {
            _repositoryDestination = new RepositoryDestination(repositoryDestination);

            return this;
        }

        public PipelineFactoryBuilder SetPipeline(IPipeline<CommandResults> pipeline)
        {
            _pipeline = pipeline;

            return this;
        }

        public IPipelineFactory Build()
        {
            var pipeLineFactory = new Mock<IPipelineFactory>();

            pipeLineFactory
                .Setup(pf => pf.CreateDataAnalysisPipeline(_repositoryUrl, _repositoryDestination))
                .Returns(_pipeline);

            return pipeLineFactory.Object;
        }
    }
}