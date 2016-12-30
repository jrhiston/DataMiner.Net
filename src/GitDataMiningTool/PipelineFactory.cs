using System;
using GitDataMiningTool.Commands;
using GitDataMiningTool.Pipes;

namespace GitDataMiningTool
{
    internal class PipelineFactory : IPipelineFactory
    {
        private readonly IFileCopier _fileCopier;

        public PipelineFactory(IFileCopier fileCopier)
        {
            if (fileCopier == null)
                throw new ArgumentNullException(nameof(fileCopier));

            _fileCopier = fileCopier;
        }

        internal IFileCopier FileCopier => _fileCopier;

        public IPipeline<CommandResults> CreateDataAnalysisPipeline(
            RepositoryUrl repositoryUrl,
            RepositoryDestination repositoryDestination)
        {
            return new DataAnalysisPipeline(
                _fileCopier,
                repositoryUrl,
                repositoryDestination);
        }
    }
}
