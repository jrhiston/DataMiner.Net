using GitDataMiningTool.Pipes;

namespace GitDataMiningTool.Commands
{
    internal interface ICommand
    {
        ICommandVisitor Execute(ICommandVisitor visitor);
    }
}
