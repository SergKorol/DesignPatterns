namespace VisitorPattern.Visitor;

public class SizeCalculatorVisitor : IFileSystemVisitor
{
    public long TotalSize { get; private set; }

    public void Visit(FileElement file)
    {
        TotalSize += file.Size;
    }

    public void Visit(DirectoryElement directory)
    {
        foreach (var child in directory.Children)
        {
            child.Accept(this);
        }
    }
}