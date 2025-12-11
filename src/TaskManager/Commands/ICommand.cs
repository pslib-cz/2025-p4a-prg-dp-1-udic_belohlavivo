namespace TaskManager.Commands;

/// <summary>
/// Interface příkazu pro Command Pattern.
/// Každý příkaz musí implementovat Execute() a Undo().
/// </summary>
public interface ICommand
{
    /// <summary>
    /// Provede příkaz.
    /// </summary>
    void Execute();

    /// <summary>
    /// Vrátí příkaz zpět (undo).
    /// </summary>
    void Undo();

    /// <summary>
    /// Popis příkazu pro uživatele.
    /// </summary>
    string Description { get; }
}
