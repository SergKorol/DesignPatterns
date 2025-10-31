namespace VisitorPattern.Visitor;

public class CountingVisitor : IFileSystemVisitor
{
    private int TotalFiles { get; set; }
    private int TotalDirectories { get; set; }

    public void Visit(FileElement file)
    {
        TotalFiles++;
    }

    public void Visit(DirectoryElement directory)
    {
        TotalDirectories++;

        foreach (var child in directory.Children)
            child.Accept(this);
    }
}