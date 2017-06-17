using System;
using GitDataMiningTool.Pipes;

namespace GitDataMiningTool.Commands
{
    public class CloneResult : ICommandResult
    {

        public CloneResult(string result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            Result = result;
        }

        public string Result { get; }

        public ICommandVisitor Accept(ICommandVisitor visitor) => visitor.Visit(this);
    }
}
