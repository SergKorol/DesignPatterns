namespace VisitorPattern.Visitor;

public interface IFileSystemElement
{
    string Name { get; }
    void Accept(IFileSystemVisitor visitor);
}
