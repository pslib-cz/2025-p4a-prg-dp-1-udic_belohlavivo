namespace TaskManager.Models;

/// <summary>
/// Reprezentuje jeden úkol v seznamu.
/// </summary>
public class TaskItem
{
    private static int _nextId = 1;

    public int Id { get; }
    public string Name { get; }
    public bool IsCompleted { get; set; }

    public TaskItem(string name)
    {
        Id = _nextId++;
        Name = name;
        IsCompleted = false;
    }

    // Konstruktor pro obnovení úkolu s původním ID (pro undo)
    public TaskItem(int id, string name, bool isCompleted)
    {
        Id = id;
        Name = name;
        IsCompleted = isCompleted;
    }

    public override string ToString()
    {
        var status = IsCompleted ? "[✓]" : "[ ]";
        return $"{Id}. {status} {Name}";
    }
}
