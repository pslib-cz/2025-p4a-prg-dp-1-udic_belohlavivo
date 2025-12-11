using TaskManager.Commands;
using TaskManager.Models;

namespace TaskManager.UI;

/// <summary>
/// Factory pro vytváření příkazů z textového vstupu.
/// Mapuje textové příkazy na objekty příkazů.
/// </summary>
public class CommandFactory
{
    private readonly TaskList _taskList;

    public CommandFactory(TaskList taskList)
    {
        _taskList = taskList;
    }

    /// <summary>
    /// Vytvoří příkaz z textového vstupu.
    /// Vrátí null pokud příkaz nebyl rozpoznán nebo je to speciální příkaz (list, undo, help, exit).
    /// </summary>
    public ICommand? CreateCommand(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        var parts = input.Trim().Split(' ', 2);
        var commandName = parts[0].ToLower();
        var argument = parts.Length > 1 ? parts[1] : string.Empty;

        return commandName switch
        {
            "add" when !string.IsNullOrWhiteSpace(argument) => new AddTaskCommand(_taskList, argument),
            "remove" when int.TryParse(argument, out var id) => new RemoveTaskCommand(_taskList, id),
            "complete" when int.TryParse(argument, out var id) => new CompleteTaskCommand(_taskList, id),
            _ => null
        };
    }

    /// <summary>
    /// Zkontroluje, zda je vstup speciální příkaz (ne Command objekt).
    /// </summary>
    public static bool IsSpecialCommand(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        var command = input.Trim().Split(' ')[0].ToLower();
        return command is "list" or "undo" or "help" or "exit" or "quit";
    }

    /// <summary>
    /// Vrátí název speciálního příkazu.
    /// </summary>
    public static string GetSpecialCommand(string input)
    {
        return input.Trim().Split(' ')[0].ToLower();
    }
}
