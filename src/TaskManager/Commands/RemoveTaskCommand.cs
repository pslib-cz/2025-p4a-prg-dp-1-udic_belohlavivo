using TaskManager.Models;

namespace TaskManager.Commands;

/// <summary>
/// Příkaz pro odstranění úkolu.
/// </summary>
public class RemoveTaskCommand : ICommand
{
    private readonly TaskList _taskList;
    private readonly int _taskId;
    private TaskItem? _removedTask;

    public RemoveTaskCommand(TaskList taskList, int taskId)
    {
        _taskList = taskList;
        _taskId = taskId;
    }

    public string Description => $"Odstranit úkol s ID: {_taskId}";

    public void Execute()
    {
        _removedTask = _taskList.Remove(_taskId);
        if (_removedTask != null)
        {
            Console.WriteLine($"Úkol odstraněn: {_removedTask}");
        }
        else
        {
            Console.WriteLine($"Úkol s ID {_taskId} nebyl nalezen.");
        }
    }

    public void Undo()
    {
        if (_removedTask != null)
        {
            _taskList.Restore(_removedTask);
            Console.WriteLine($"Undo: Úkol obnoven: {_removedTask}");
        }
    }
}
