using System;
using GitDataMiningTool.Pipes;

namespace GitDataMiningTool.Commands
{
    public class DataAnalysisResult : ICommandResult
    {

        public DataAnalysisResult(string result, DataAnalysisResultType type)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));

            Result = result;
            ResultType = type;
        }

        public string Result { get; }
        public DataAnalysisResultType ResultType { get; }

        public ICommandVisitor Accept(ICommandVisitor visitor) => visitor.Visit(this);
    }
}
