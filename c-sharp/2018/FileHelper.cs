using System.Reflection;

namespace _2018;

public class FileHelper
{
    public static string GetInputFromFile(string filename)
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"_2018.Inputs.{filename}");
        using var streamReader = new StreamReader(stream);

        var contents = streamReader.ReadToEnd();

        return contents;
    }
}