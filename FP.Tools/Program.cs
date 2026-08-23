
 
namespace FP.Tools;
using FP.Tools.Services;


 

public class Program
{
    public static void Main(string[] args)
    {
        string solutionPath = Directory.GetParent(
            AppDomain.CurrentDomain.BaseDirectory)!
            .Parent!
            .Parent!
            .Parent!
            .Parent!
            .FullName;

        TreeGenerator generator = new();

        string tree = generator.Generate(solutionPath);

        string outputPath =
            Path.Combine(solutionPath, "Docs", "tree.md");

        File.WriteAllText(outputPath, tree);

        Console.WriteLine($"Tree generated: {outputPath}");
    }
}