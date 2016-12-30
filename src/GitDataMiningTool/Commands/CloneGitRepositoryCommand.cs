using GitDataMiningTool.Pipes;
using System;
using System.Collections.Generic;

namespace GitDataMiningTool.Commands
{
    internal class CloneGitRepositoryCommand : CommandResultVisitorBase
    {
        private readonly RepositoryUrl _repositoryUrl;
        private readonly RepositoryDestination _repositoryDestination;

        public CloneGitRepositoryCommand(
            RepositoryUrl repositoryUrl,
            RepositoryDestination repositoryDestination)
        {
            _repositoryUrl = repositoryUrl 
                ?? throw new ArgumentNullException(nameof(repositoryUrl));
            _repositoryDestination = repositoryDestination 
                ?? throw new ArgumentNullException(nameof(repositoryDestination));
        }

        public override IEnumerator<ICommandResult> GetEnumerator()
        {
            yield return new CloneResult(ProcessRunner.RunGitCommand(
                $"clone {_repositoryUrl.ToString()} {_repositoryDestination.ToString()}"));
        }
    }
}
