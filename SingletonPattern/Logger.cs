namespace SingletonPattern;

public static class Logger
{
    private static readonly Lock Lock = new();
    private static readonly string LogFilePath = "log.txt";

    public static void Info(string message) => Log("INFO", message);
    public static void Debug(string message) => Log("DEBUG", message);
    public static void Warn(string message) => Log("WARN", message);
    public static void Error(string message) => Log("ERROR", message);

    private static void Log(string level, string message)
    {
        string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";

        lock (Lock)
        {
            Console.WriteLine(logEntry);
            try
            {
                using StreamWriter writer = new(LogFilePath, append: true);
                writer.WriteLine(logEntry);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Logger Error: {ex.Message}");
            }
        }
    }
}