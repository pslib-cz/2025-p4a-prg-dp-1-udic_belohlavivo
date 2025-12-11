using TaskManager.Commands;
using TaskManager.Models;

namespace TaskManager.Tests;

/// <summary>
/// Unit testy pro jednotlivé příkazy (Command třídy).
/// </summary>
public class CommandTests
{
    [Fact]
    public void AddTaskCommand_Execute_AddsTaskToList()
    {
        // Arrange
        var taskList = new TaskList();
        var command = new AddTaskCommand(taskList, "Test úkol");

        // Act
        command.Execute();

        // Assert
        Assert.Equal(1, taskList.Count);
        Assert.Equal("Test úkol", taskList.GetAll()[0].Name);
    }

    [Fact]
    public void AddTaskCommand_Undo_RemovesAddedTask()
    {
        // Arrange
        var taskList = new TaskList();
        var command = new AddTaskCommand(taskList, "Test úkol");
        command.Execute();

        // Act
        command.Undo();

        // Assert
        Assert.Equal(0, taskList.Count);
    }

    [Fact]
    public void RemoveTaskCommand_Execute_RemovesTaskFromList()
    {
        // Arrange
        var taskList = new TaskList();
        var task = new TaskItem("Test úkol");
        taskList.Add(task);

        var command = new RemoveTaskCommand(taskList, task.Id);

        // Act
        command.Execute();

        // Assert
        Assert.Equal(0, taskList.Count);
    }

    [Fact]
    public void RemoveTaskCommand_Undo_RestoresRemovedTask()
    {
        // Arrange
        var taskList = new TaskList();
        var task = new TaskItem("Test úkol");
        taskList.Add(task);

        var command = new RemoveTaskCommand(taskList, task.Id);
        command.Execute();

        // Act
        command.Undo();

        // Assert
        Assert.Equal(1, taskList.Count);
        Assert.Equal("Test úkol", taskList.GetAll()[0].Name);
    }

    [Fact]
    public void CompleteTaskCommand_Execute_MarksTaskAsCompleted()
    {
        // Arrange
        var taskList = new TaskList();
        var task = new TaskItem("Test úkol");
        taskList.Add(task);

        var command = new CompleteTaskCommand(taskList, task.Id);

        // Act
        command.Execute();

        // Assert
        Assert.True(taskList.GetById(task.Id)?.IsCompleted);
    }

    [Fact]
    public void CompleteTaskCommand_Undo_RestoresPreviousState()
    {
        // Arrange
        var taskList = new TaskList();
        var task = new TaskItem("Test úkol");
        taskList.Add(task);

        var command = new CompleteTaskCommand(taskList, task.Id);
        command.Execute();

        // Act
        command.Undo();

        // Assert
        Assert.False(taskList.GetById(task.Id)?.IsCompleted);
    }
}
