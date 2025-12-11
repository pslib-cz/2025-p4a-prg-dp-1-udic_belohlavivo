namespace TaskManager.Models;

/// <summary>
/// Receiver - spravuje kolekci úkolů.
/// Obsahuje metody pro manipulaci s úkoly, volané příkazy (Commands).
/// </summary>
public class TaskList
{
    private readonly List<TaskItem> _tasks = new();

    /// <summary>
    /// Přidá úkol do seznamu.
    /// </summary>
    public void Add(TaskItem task)
    {
        _tasks.Add(task);
    }

    /// <summary>
    /// Odstraní úkol podle ID a vrátí ho (pro možnost undo).
    /// </summary>
    public TaskItem? Remove(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            _tasks.Remove(task);
        }
        return task;
    }

    /// <summary>
    /// Obnoví dříve odstraněný úkol (pro undo remove).
    /// </summary>
    public void Restore(TaskItem task)
    {
        _tasks.Add(task);
    }

    /// <summary>
    /// Nastaví stav dokončení úkolu a vrátí předchozí stav (pro undo).
    /// </summary>
    public bool? SetComplete(int id, bool complete)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            var previousState = task.IsCompleted;
            task.IsCompleted = complete;
            return previousState;
        }
        return null;
    }

    /// <summary>
    /// Vrátí úkol podle ID.
    /// </summary>
    public TaskItem? GetById(int id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    /// <summary>
    /// Vrátí všechny úkoly.
    /// </summary>
    public IReadOnlyList<TaskItem> GetAll()
    {
        return _tasks.AsReadOnly();
    }

    /// <summary>
    /// Vrátí počet úkolů.
    /// </summary>
    public int Count => _tasks.Count;
}
