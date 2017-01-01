namespace GitDataMiningTool
{
    public interface IFileCopier
    {
        void CopyGenerateGitLogFileToPath(
            string source,
            string destination,
            string file);
    }
}