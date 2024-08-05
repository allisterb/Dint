namespace Dint.CLI
{
    internal class Program : Runtime
    {
        static void Main(string[] args)
        {
            Initialize("Dint.CLI", "CLI", (args.Contains("--debug") || args.Contains("-d")), true, true);
        }
    }
}
