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

        public static string GetContentRelativePath(string file)
        {
            return Path.Combine(Engine.Instance.ContentDirectory, file);
        }
    }
}
