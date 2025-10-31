namespace VisitorPattern.Ordinary;

public abstract class FileSystemElement(string name)
{
    protected string Name { get; } = string.IsNullOrEmpty(name) ? "root" : name;

    public abstract long GetSize();
    public abstract void PrintReport(int depth = 0);
}
