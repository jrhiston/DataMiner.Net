using GitDataMiningTool.Commands;
using System.Threading.Tasks;

namespace GitDataMiningTool
{
    public interface IRepositoryAnalyser
    {
        CommandResults Analyse(
            RepositoryUrl repository,
            RepositoryDestination repositoryDestination);

        Task<CommandResults> AnalyseAsync(
            RepositoryUrl repository,
            RepositoryDestination repositoryDestination);
    }
}