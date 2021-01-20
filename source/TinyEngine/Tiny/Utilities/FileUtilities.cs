using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Tiny
{
    public static class FileUtilities
    {
        public static readonly string AssemblyDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public static string GetContentRelativePath(string contentDirectory, string file)
        {
            return Path.Combine(AssemblyDirectory, contentDirectory, file);
        }
    }
}
