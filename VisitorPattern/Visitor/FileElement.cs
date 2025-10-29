namespace VisitorPattern.Visitor;

public class FileElement(string name, long size) : IFileSystemElement
{
    public string Name { get; } = name;
    public long Size { get; } = size;

    public void Accept(IFileSystemVisitor visitor)
    {
        visitor.Visit(this);
    }
}
