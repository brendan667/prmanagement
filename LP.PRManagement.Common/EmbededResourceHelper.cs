using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LP.PRManagement.Common
{
    public static class EmbededResourceHelper
    {
        public static string ReadResource(string resourceName, Assembly getExecutingAssembly)
        {
            var assembly = getExecutingAssembly;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) throw new ArgumentException(
                    $"{resourceName} resource does not exist in {getExecutingAssembly.FullName.Split(',').First()} assembly");
                return stream.ReadToString();
            }
        }
    }
}
