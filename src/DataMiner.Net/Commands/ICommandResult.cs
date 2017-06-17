using GitDataMiningTool.Pipes;

namespace GitDataMiningTool.Commands
{
    public interface ICommandResult
    {
        ICommandVisitor Accept(ICommandVisitor visitor);
    }
}
