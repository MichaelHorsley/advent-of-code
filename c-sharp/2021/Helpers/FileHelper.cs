using System.Reflection;

namespace _2021.Helpers;

public class FileHelper
{
    public static string GetResourceFile(string fileName)
    {
        string input;

        using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName))
        using (var reader = new StreamReader(stream))
        {
            input = reader.ReadToEnd();
        }

        return input;
    }
}