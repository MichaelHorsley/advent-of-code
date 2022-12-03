using System.IO;
using System.Reflection;

namespace _2022
{
    public class FileHelper
    {
        public static string GetInputFromFile(string filename)
        {
            string contents;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"_2022.Inputs.{filename}"))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    contents = streamReader.ReadToEnd();
                }
            }

            return contents;
        }
    }
}