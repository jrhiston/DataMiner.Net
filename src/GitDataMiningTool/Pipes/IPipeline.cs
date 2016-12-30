namespace GitDataMiningTool.Pipes
{
    internal interface IPipeline<T>
    {
        CompositePipe<T> Create();
    }
}