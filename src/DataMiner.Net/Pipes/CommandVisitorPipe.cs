using GitDataMiningTool.Commands;
using System.Linq;

namespace GitDataMiningTool.Pipes
{
    internal class CommandVisitorPipe : IPipe<CommandResults>
    {

        public CommandVisitorPipe(ICommandVisitor visitor)
        {
            this.Visitor = visitor;
        }

        public CommandResults Pipe(CommandResults results)
        {
            var v = results.Accept(this.Visitor);
            return new CommandResults(results.Concat(v).ToArray());
        }

        public ICommandVisitor Visitor { get; }
    }
}
