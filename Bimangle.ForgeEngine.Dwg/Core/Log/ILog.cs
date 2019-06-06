namespace Bimangle.ForgeEngine.Dwg.CLI.Core.Log
{
    public interface ILog
    {
        void WriteLine(string s);

        void Write(string s);

        void WriteProgress(int progress);
    }
}
