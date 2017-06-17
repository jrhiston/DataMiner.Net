namespace GitDataMiningTool.Pipes
{
    public interface IPipe<T>
    {
        T Pipe(T item);
    }
}
