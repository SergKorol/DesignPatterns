namespace VisitorPattern.Ordinary;

public class DirectoryElement(string name) : FileSystemElement(name)
{
    private List<FileSystemElement> Children { get; } = [];

    public void Add(FileSystemElement element)
    {
        Children.Add(element);
    }

    public override long GetSize()
    {
        long total = 0;
        foreach (var child in Children)
            total += child.GetSize();
        return total;
    }

    public override void PrintReport(int depth = 0)
    {
        Console.WriteLine($"{new string(' ', depth * 2)}📁 {Name} ({CountElementsInCurrentDirectory()} items)");
        foreach (var child in Children)
            child.PrintReport(depth + 1);
    }

    private int CountElementsInCurrentDirectory() => Children.Count;
}