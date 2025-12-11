using TaskManager.Models;

namespace TaskManager.Commands;

/// <summary>
/// Příkaz pro přidání nového úkolu.
/// </summary>
public class AddTaskCommand : ICommand
{
    private readonly TaskList _taskList;
    private readonly string _taskName;
    private TaskItem? _addedTask;

    public AddTaskCommand(TaskList taskList, string taskName)
    {
        _taskList = taskList;
        _taskName = taskName;
    }

    public string Description => $"Přidat úkol: {_taskName}";

    public void Execute()
    {
        _addedTask = new TaskItem(_taskName);
        _taskList.Add(_addedTask);
        Console.WriteLine($"Úkol přidán: {_addedTask}");
    }

    public void Undo()
    {
        if (_addedTask != null)
        {
            _taskList.Remove(_addedTask.Id);
            Console.WriteLine($"Undo: Úkol odstraněn: {_addedTask}");
        }
    }
}
