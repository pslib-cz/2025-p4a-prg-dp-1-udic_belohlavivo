using TaskManager.Models;

namespace TaskManager.Commands;

/// <summary>
/// Příkaz pro označení úkolu jako splněného.
/// </summary>
public class CompleteTaskCommand : ICommand
{
    private readonly TaskList _taskList;
    private readonly int _taskId;
    private bool? _previousState;

    public CompleteTaskCommand(TaskList taskList, int taskId)
    {
        _taskList = taskList;
        _taskId = taskId;
    }

    public string Description => $"Označit úkol {_taskId} jako splněný";

    public void Execute()
    {
        _previousState = _taskList.SetComplete(_taskId, true);
        if (_previousState.HasValue)
        {
            var task = _taskList.GetById(_taskId);
            Console.WriteLine($"Úkol označen jako splněný: {task}");
        }
        else
        {
            Console.WriteLine($"Úkol s ID {_taskId} nebyl nalezen.");
        }
    }

    public void Undo()
    {
        if (_previousState.HasValue)
        {
            _taskList.SetComplete(_taskId, _previousState.Value);
            var task = _taskList.GetById(_taskId);
            Console.WriteLine($"Undo: Úkol vrácen do předchozího stavu: {task}");
        }
    }
}
