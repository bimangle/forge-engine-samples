namespace Bimangle.ForgeEngine.Dwg.Core
{
    public interface ILog
    {
        void WriteLine(string s);

        void Write(string s);
        void WriteProgress(int progress);
    }
}
