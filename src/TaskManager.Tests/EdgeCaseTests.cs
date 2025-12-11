using TaskManager.Commands;
using TaskManager.Models;

namespace TaskManager.Tests;

/// <summary>
/// Testy edge-case scénářů.
/// </summary>
public class EdgeCaseTests
{
    [Fact]
    public void RemoveTask_NonExistentId_DoesNotThrow()
    {
        // Arrange
        var taskList = new TaskList();
        var command = new RemoveTaskCommand(taskList, 999);

        // Act & Assert - should not throw
        command.Execute();
        Assert.Equal(0, taskList.Count);
    }

    [Fact]
    public void CompleteTask_NonExistentId_DoesNotThrow()
    {
        // Arrange
        var taskList = new TaskList();
        var command = new CompleteTaskCommand(taskList, 999);

        // Act & Assert - should not throw
        command.Execute();
    }

    [Fact]
    public void UndoRemove_NonExistentTask_DoesNotThrow()
    {
        // Arrange
        var taskList = new TaskList();
        var command = new RemoveTaskCommand(taskList, 999);
        command.Execute();

        // Act & Assert - should not throw
        command.Undo();
    }

    [Fact]
    public void TaskList_AddMultiple_MaintainsOrder()
    {
        // Arrange
        var taskList = new TaskList();

        // Act
        taskList.Add(new TaskItem("První"));
        taskList.Add(new TaskItem("Druhý"));
        taskList.Add(new TaskItem("Třetí"));

        // Assert
        var tasks = taskList.GetAll();
        Assert.Equal(3, tasks.Count);
        Assert.Equal("První", tasks[0].Name);
        Assert.Equal("Druhý", tasks[1].Name);
        Assert.Equal("Třetí", tasks[2].Name);
    }

    [Fact]
    public void TaskItem_ToString_FormatsCorrectly()
    {
        // Arrange
        var task = new TaskItem("Test úkol");

        // Act & Assert - uncompleted
        Assert.Contains("[ ]", task.ToString());
        Assert.Contains("Test úkol", task.ToString());

        // Mark as completed
        task.IsCompleted = true;
        Assert.Contains("[✓]", task.ToString());
    }
}
