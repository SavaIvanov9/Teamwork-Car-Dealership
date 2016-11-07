using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dealership.Common
{
    public class Utility
    {
        private const string DirectorySearchRegexPattern = @"\d{1,2}-\w{2,3}-\d{4}";

        public static IEnumerable<DirectoryInfo> GetDirectoriesByPattern(string path, string regex = DirectorySearchRegexPattern)
        {
            var dirInfo = new DirectoryInfo(path);
            var matchingDirectories = dirInfo.GetDirectories().Where(d => Regex.IsMatch(d.Name, regex));
            return matchingDirectories;
        }

        public static void CreateDirectoryIfNotExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}

