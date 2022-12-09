namespace SystemOfLinearEq
{
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using System.Globalization;

    public static class FileHelper
    {
        public static void WriteFile(string fileName, params double[][] results)
        {
            File.WriteAllLines(fileName, GetText(results));
        }

        private static IEnumerable<string> GetText(IEnumerable<double[]> results)
        {
            return results.Select(line => string.Join("\t", line.Select(l => l.ToString(CultureInfo.InvariantCulture))) + "\n");
        }
    }
}