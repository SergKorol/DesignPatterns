
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Settings;

namespace VisitorPattern;

public class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<Program>(new BenchmarkConfig());
    }

    [Benchmark]
    public void OrdinaryApproach()
    {
        string targetPath = Path.Combine(AppContext.BaseDirectory);
        Console.WriteLine($"Building file tree from: {targetPath}\n");

        var root = BuildDirectoryTreeForOrdinary(targetPath);

        Console.WriteLine("=== File tree ===");
        root.PrintReport();

        Console.WriteLine($"\nTotal size: {root.GetSize() / 1024.0:F2} KB");
    }
    
    [Benchmark]
    public void VisitorApproach()
    {
        string targetPath = Path.Combine(AppContext.BaseDirectory);
        Console.WriteLine($"Building file tree from: {targetPath}\n");

        var root = BuildDirectoryTreeForVisitor(targetPath);

        var reportVisitor = new Visitor.ReportVisitor();
        Console.WriteLine("=== File tree ===");
        root.Accept(reportVisitor);

        var sizeVisitor = new Visitor.SizeCalculatorVisitor();
        root.Accept(sizeVisitor);
        Console.WriteLine($"\nTotal size: {sizeVisitor.TotalSize / 1024.0:F2} KB");
    }
    
    private static Ordinary.DirectoryElement BuildDirectoryTreeForOrdinary(string path)
    {
        var directory = new Ordinary.DirectoryElement(Path.GetFileName(path));

        foreach (var file in Directory.GetFiles(path))
        {
            var fileInfo = new FileInfo(file);
            directory.Add(new Ordinary.FileElement(fileInfo.Name, fileInfo.Length));
        }

        foreach (var dir in Directory.GetDirectories(path))
        {
            directory.Add(BuildDirectoryTreeForOrdinary(dir));
        }

        return directory;
    }
    
    private static Visitor.DirectoryElement BuildDirectoryTreeForVisitor(string path)
    {
        var directory = new Visitor.DirectoryElement(Path.GetFileName(path));

        foreach (var file in Directory.GetFiles(path))
        {
            var info = new FileInfo(file);
            directory.Add(new Visitor.FileElement(info.Name, info.Length));
        }

        foreach (var dir in Directory.GetDirectories(path))
        {
            directory.Add(BuildDirectoryTreeForVisitor(dir));
        }

        return directory;
    }
}