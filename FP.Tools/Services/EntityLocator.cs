using FP.Tools.Models;

namespace FP.Tools.Services;

public class EntityLocator
{
    private static readonly string[] IgnoredFolders =
    {
        ".git",
        ".vs",
        "bin",
        "obj"
    };

    public IEnumerable<EntityInfo> Find(string rootPath, string entityName)
    {
        return Directory
            .GetFiles(rootPath, "*.cs", SearchOption.AllDirectories)
            .Where(file => !IsIgnored(file))
            .Where(file =>
                Path.GetFileNameWithoutExtension(file)
                    .Contains(entityName, StringComparison.OrdinalIgnoreCase))
            .Select(file => new EntityInfo
            {
                Name = Path.GetFileNameWithoutExtension(file),
                FilePath = file,
                Namespace = GetNamespace(file)
            })
            .OrderBy(e => e.Name);
    }

    private static bool IsIgnored(string path)
    {
        return IgnoredFolders.Any(folder =>
            path.Contains(
                $"{Path.DirectorySeparatorChar}{folder}{Path.DirectorySeparatorChar}",
                StringComparison.OrdinalIgnoreCase));
    }

    private static string GetNamespace(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);

        string? namespaceLine = lines
            .FirstOrDefault(l => l.TrimStart().StartsWith("namespace "));

        return namespaceLine?.Replace("namespace ", "") ?? "Unknown";
    }
}