using GitDataMiningTool.Pipes;
using System;

namespace GitDataMiningTool.Commands
{
    public class CopyFilesResult : ICommandResult
    {

        public CopyFilesResult(string result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            Result = result;
        }

        public string Result { get; }

        public ICommandVisitor Accept(ICommandVisitor visitor) => visitor.Visit(this);
    }
}
