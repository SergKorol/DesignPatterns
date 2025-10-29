namespace VisitorPattern.Visitor;

public interface IFileSystemVisitor
{
    void Visit(FileElement file);
    void Visit(DirectoryElement directory);
}