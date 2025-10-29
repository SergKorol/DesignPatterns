
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
        var root = new Ordinary.DirectoryElement("Root");
        var docs = new Ordinary.DirectoryElement("Documents");
        var pics = new Ordinary.DirectoryElement("Pictures");

        docs.Add(new Ordinary.FileElement("Resume.pdf", 250));
        docs.Add(new Ordinary.FileElement("Report.docx", 120));

        pics.Add(new Ordinary.FileElement("Photo1.jpg", 2048));
        pics.Add(new Ordinary.FileElement("Photo2.png", 1024));

        root.Add(docs);
        root.Add(pics);
        root.Add(new Ordinary.FileElement("todo.txt", 10));

        Console.WriteLine("=== File tree ===");
        root.PrintReport();

        Console.WriteLine($"\nTotal size: {root.GetSize()} KB");
    }
    
    [Benchmark]
    public void VisitorApproach()
    {
        var root = new Visitor.DirectoryElement("Root");
        var docs = new Visitor.DirectoryElement("Documents");
        var pics = new Visitor.DirectoryElement("Pictures");

        docs.Add(new Visitor.FileElement("Resume.pdf", 250));
        docs.Add(new Visitor.FileElement("Report.docx", 120));

        pics.Add(new Visitor.FileElement("Photo1.jpg", 2048));
        pics.Add(new Visitor.FileElement("Photo2.png", 1024));

        root.Add(docs);
        root.Add(pics);
        root.Add(new Visitor.FileElement("todo.txt", 10));

        var reportVisitor = new Visitor.ReportVisitor();
        Console.WriteLine("=== File tree ===");
        root.Accept(reportVisitor);

        var sizeVisitor = new Visitor.SizeCalculatorVisitor();
        root.Accept(sizeVisitor);
        Console.WriteLine($"\nTotal size: {sizeVisitor.TotalSize} KB");
    }
}