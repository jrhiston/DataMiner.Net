using GitDataMiningTool.Commands;
using System.Collections.Generic;

namespace GitDataMiningTool.Pipes
{
    public interface ICommandVisitor : IEnumerable<ICommandResult>
    {
        ICommandVisitor Visit(CloneResult cloneResult);
        ICommandVisitor Visit(CopyFilesResult copyFilesResult);
        ICommandVisitor Visit(DataAnalysisResult dataAnalysisResult);
        ICommandVisitor Visit(GeneratedDataResult dataAnalysisResult);
    }
}