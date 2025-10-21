public class FileStorage
{
    // Посилання на операції через інтерфейс (агреговані об'єкти).
    private readonly IFileOperation _saveOperation;
    private readonly IFileOperation _loadOperation;

    /// Конструктор, який приймає об'єкти операцій ззовні (Dependency Injection / Агрегація).
    public FileStorage(IFileOperation saveOperation, IFileOperation loadOperation)
    {
        _saveOperation = saveOperation;
        _loadOperation = loadOperation;
    }

    /// Делегування виконання до агрегованого об'єкта збереження.
    public bool SaveFile(string fileName)
    {
        Console.WriteLine($"Сховище: Запит на збереження файлу '{fileName}'.");
        return _saveOperation.Execute(fileName);
    }

    /// Делегування виконання до агрегованого об'єкта завантаження.
    public bool LoadFile(string fileName)
    {
        Console.WriteLine($"Сховище: Запит на завантаження файлу '{fileName}'.");
        return _loadOperation.Execute(fileName);
    }
}