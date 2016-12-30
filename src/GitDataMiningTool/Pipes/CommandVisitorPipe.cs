using GitDataMiningTool.Commands;
using System.Linq;

namespace GitDataMiningTool.Pipes
{
    internal class CommandVisitorPipe : IPipe<CommandResults>
    {
        private readonly ICommandVisitor visitor;

        public CommandVisitorPipe(ICommandVisitor visitor)
        {
            this.visitor = visitor;
        }

        public CommandResults Pipe(CommandResults results)
        {
            var v = results.Accept(this.visitor);
            return new CommandResults(results.Concat(v).ToArray());
        }

        public ICommandVisitor Visitor
        {
            get { return this.visitor; }
        }
    }
}
