using TaskManager.Commands;
using TaskManager.Models;

namespace TaskManager.UI;

/// <summary>
/// Konzolové uživatelské rozhraní.
/// Striktně odděleno od logiky měnící data - pouze čte vstupy a zobrazuje výstupy.
/// </summary>
public class ConsoleUI
{
    private readonly TaskList _taskList;
    private readonly CommandInvoker _invoker;
    private readonly CommandFactory _commandFactory;
    private bool _running;

    public ConsoleUI()
    {
        _taskList = new TaskList();
        _invoker = new CommandInvoker();
        _commandFactory = new CommandFactory(_taskList);
        _running = true;
    }

    /// <summary>
    /// Spustí hlavní smyčku programu (REPL).
    /// </summary>
    public void Run()
    {
        PrintWelcome();

        while (_running)
        {
            Console.Write("\n> ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            ProcessInput(input);
        }

        Console.WriteLine("Na shledanou!");
    }

    private void ProcessInput(string input)
    {
        // Zpracování speciálních příkazů (nejsou Command objekty)
        if (CommandFactory.IsSpecialCommand(input))
        {
            var specialCommand = CommandFactory.GetSpecialCommand(input);
            switch (specialCommand)
            {
                case "list":
                    ListTasks();
                    break;
                case "undo":
                    _invoker.Undo();
                    break;
                case "help":
                    PrintHelp();
                    break;
                case "exit":
                case "quit":
                    _running = false;
                    break;
            }
            return;
        }

        // Vytvoření a provedení příkazu přes Command Pattern
        var command = _commandFactory.CreateCommand(input);
        if (command != null)
        {
            _invoker.ExecuteCommand(command);
        }
        else
        {
            Console.WriteLine("Neznámý příkaz. Napište 'help' pro nápovědu.");
        }
    }

    private void ListTasks()
    {
        var tasks = _taskList.GetAll();
        if (tasks.Count == 0)
        {
            Console.WriteLine("Seznam úkolů je prázdný.");
            return;
        }

        Console.WriteLine("\n=== Seznam úkolů ===");
        foreach (var task in tasks)
        {
            Console.WriteLine(task);
        }
        Console.WriteLine($"=== Celkem: {tasks.Count} úkol(ů) ===");
    }

    private void PrintWelcome()
    {
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║     Správce úkolů - Task Manager       ║");
        Console.WriteLine("║     (Command Pattern Demo)             ║");
        Console.WriteLine("╚════════════════════════════════════════╝");
        Console.WriteLine("\nNapište 'help' pro seznam příkazů.");
    }

    private void PrintHelp()
    {
        Console.WriteLine("\n=== Nápověda ===");
        Console.WriteLine("  add <název>     - Přidá nový úkol");
        Console.WriteLine("  remove <id>     - Odstraní úkol podle ID");
        Console.WriteLine("  complete <id>   - Označí úkol jako splněný");
        Console.WriteLine("  list            - Zobrazí všechny úkoly");
        Console.WriteLine("  undo            - Vrátí poslední operaci");
        Console.WriteLine("  help            - Zobrazí tuto nápovědu");
        Console.WriteLine("  exit            - Ukončí program");
    }
}
