namespace TaskManager.Commands;

/// <summary>
/// Invoker - spravuje historii příkazů a provádí execute/undo.
/// </summary>
public class CommandInvoker
{
    private readonly Stack<ICommand> _history = new();

    /// <summary>
    /// Provede příkaz a uloží ho do historie pro případné undo.
    /// </summary>
    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _history.Push(command);
    }

    /// <summary>
    /// Vrátí poslední příkaz zpět.
    /// </summary>
    public bool Undo()
    {
        if (_history.Count == 0)
        {
            Console.WriteLine("Není co vrátit - historie je prázdná.");
            return false;
        }

        var command = _history.Pop();
        command.Undo();
        return true;
    }

    /// <summary>
    /// Vrátí počet příkazů v historii.
    /// </summary>
    public int HistoryCount => _history.Count;
}
