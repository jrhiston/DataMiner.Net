using GitDataMiningTool.Pipes;
using System;

namespace GitDataMiningTool.Commands
{
    public class GeneratedDataResult : ICommandResult
    {

        public GeneratedDataResult(string result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            Result = result;
        }

        public string Result { get; }

        public ICommandVisitor Accept(ICommandVisitor visitor) => visitor.Visit(this);
    }
}
