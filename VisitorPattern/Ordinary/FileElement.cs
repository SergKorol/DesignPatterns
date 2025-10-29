namespace VisitorPattern.Ordinary;

public class FileElement(string name, long size) : FileSystemElement(name)
{
    private long Size { get; } = size;

    public override long GetSize() => Size;

    public override void PrintReport(int depth = 0)
    {
        Console.WriteLine($"{new string(' ', depth * 2)}📄 {Name} ({Size} KB)");
    }
}
