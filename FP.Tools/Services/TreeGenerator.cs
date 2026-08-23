using System.Text;

namespace FP.Tools.Services;

public class TreeGenerator
{
    private static readonly HashSet<string> IgnoredFolders =
    [
        ".git",
        ".vs",
        "bin",
        "obj",
        "node_modules"
    ];

    public string Generate(string rootPath)
    {
        StringBuilder sb = new();

        DirectoryInfo root = new(rootPath);

        sb.AppendLine(root.Name);

        GenerateDirectory(root, sb, string.Empty);

        return sb.ToString();
    }

    private static void GenerateDirectory(
        DirectoryInfo directory,
        StringBuilder sb,
        string indent)
    {
        var directories = directory
            .GetDirectories()
            .Where(d => !IgnoredFolders.Contains(d.Name))
            .OrderBy(d => d.Name);

        var files = directory
            .GetFiles()
            .OrderBy(f => f.Name);

        var itemsCount = directories.Count() + files.Count();

        int currentIndex = 0;

        foreach (var dir in directories)
        {
            currentIndex++;

            bool isLast = currentIndex == itemsCount;

            sb.AppendLine(
                $"{indent}{(isLast ? "└───" : "├───")}{dir.Name}");

            GenerateDirectory(
                dir,
                sb,
                indent + (isLast ? "    " : "│   "));
        }

        foreach (var file in files)
        {
            currentIndex++;

            bool isLast = currentIndex == itemsCount;

            sb.AppendLine(
                $"{indent}{(isLast ? "└───" : "├───")}{file.Name}");
        }
    }
}