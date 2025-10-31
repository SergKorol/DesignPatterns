namespace VisitorPattern.Visitor;

public class DirectoryElement(string name) : IFileSystemElement
{
    public string Name { get; } = string.IsNullOrEmpty(name) ? "root" : name;
    public List<IFileSystemElement> Children { get; } = [];

    public void Add(IFileSystemElement element)
    {
        Children.Add(element);
    }

    public void Accept(IFileSystemVisitor visitor)
    {
        visitor.Visit(this);
    }
}