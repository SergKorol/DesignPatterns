namespace VisitorPattern.Visitor;

public class ReportVisitor : IFileSystemVisitor
{
    private int _depth;

    public void Visit(FileElement file)
    {
        Console.WriteLine($"{new string(' ', _depth * 2)}📄 {file.Name} ({file.Size} KB)");
    }

    public void Visit(DirectoryElement directory)
    {
        Console.WriteLine($"{new string(' ', _depth * 2)}📁 {directory.Name}");
        _depth++;
        foreach (var child in directory.Children)
        {
            child.Accept(this);
        }
        _depth--;
    }
}
