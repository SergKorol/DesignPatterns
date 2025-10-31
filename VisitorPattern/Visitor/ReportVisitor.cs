namespace VisitorPattern.Visitor;

public class ReportVisitor : IFileSystemVisitor
{
    private int _depth = 0;

    public void Visit(FileElement file)
    {
        Console.WriteLine($"{new string(' ', _depth * 2)}📄 {file.Name} ({file.Size / 1024.0:F2} KB)");
    }

    public void Visit(DirectoryElement directory)
    {
        Console.WriteLine($"{new string(' ', _depth * 2)}📁 {directory.Name} ({directory.Children.Count} items)");

        _depth++;
        foreach (var child in directory.Children)
            child.Accept(this);
        _depth--;
    }
}