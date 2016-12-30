using GitDataMiningTool.Pipes;
using System;

namespace GitDataMiningTool.Commands
{
    public class GeneratedDataResult : ICommandResult
    {

        private readonly string _result;

        public GeneratedDataResult(string result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            _result = result;
        }

        public string Result => _result;

        public ICommandVisitor Accept(ICommandVisitor visitor) => visitor.Visit(this);
    }
}
