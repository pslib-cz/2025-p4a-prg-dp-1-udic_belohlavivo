using TaskManager.Commands;
using TaskManager.Models;

namespace TaskManager.Tests;

/// <summary>
/// Integrační testy pro CommandInvoker a undo funkcionalitu.
/// </summary>
public class InvokerTests
{
    [Fact]
    public void CommandInvoker_ExecuteCommand_AddsToHistory()
    {
        // Arrange
        var taskList = new TaskList();
        var invoker = new CommandInvoker();
        var command = new AddTaskCommand(taskList, "Test");

        // Act
        invoker.ExecuteCommand(command);

        // Assert
        Assert.Equal(1, invoker.HistoryCount);
    }

    [Fact]
    public void CommandInvoker_Undo_ReversesLastCommand()
    {
        // Arrange
        var taskList = new TaskList();
        var invoker = new CommandInvoker();
        
        invoker.ExecuteCommand(new AddTaskCommand(taskList, "Úkol 1"));
        invoker.ExecuteCommand(new AddTaskCommand(taskList, "Úkol 2"));
        Assert.Equal(2, taskList.Count);

        // Act
        invoker.Undo();

        // Assert
        Assert.Equal(1, taskList.Count);
        Assert.Equal("Úkol 1", taskList.GetAll()[0].Name);
    }

    [Fact]
    public void CommandInvoker_MultipleUndo_ReversesInOrder()
    {
        // Arrange
        var taskList = new TaskList();
        var invoker = new CommandInvoker();
        
        invoker.ExecuteCommand(new AddTaskCommand(taskList, "Úkol 1"));
        invoker.ExecuteCommand(new AddTaskCommand(taskList, "Úkol 2"));
        invoker.ExecuteCommand(new AddTaskCommand(taskList, "Úkol 3"));

        // Act
        invoker.Undo(); // Removes Úkol 3
        invoker.Undo(); // Removes Úkol 2

        // Assert
        Assert.Equal(1, taskList.Count);
        Assert.Equal("Úkol 1", taskList.GetAll()[0].Name);
    }

    [Fact]
    public void CommandInvoker_UndoEmptyHistory_ReturnsFalse()
    {
        // Arrange
        var invoker = new CommandInvoker();

        // Act
        var result = invoker.Undo();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CommandInvoker_UndoRemove_RestoresTask()
    {
        // Arrange
        var taskList = new TaskList();
        var invoker = new CommandInvoker();
        
        invoker.ExecuteCommand(new AddTaskCommand(taskList, "Důležitý úkol"));
        var taskId = taskList.GetAll()[0].Id;
        invoker.ExecuteCommand(new RemoveTaskCommand(taskList, taskId));
        Assert.Equal(0, taskList.Count);

        // Act
        invoker.Undo();

        // Assert
        Assert.Equal(1, taskList.Count);
        Assert.Equal("Důležitý úkol", taskList.GetAll()[0].Name);
    }

    [Fact]
    public void CommandInvoker_ComplexScenario_WorksCorrectly()
    {
        // Arrange
        var taskList = new TaskList();
        var invoker = new CommandInvoker();

        // Add, complete, undo complete, undo add
        invoker.ExecuteCommand(new AddTaskCommand(taskList, "Nakoupit"));
        var taskId = taskList.GetAll()[0].Id;
        invoker.ExecuteCommand(new CompleteTaskCommand(taskList, taskId));
        
        Assert.True(taskList.GetById(taskId)?.IsCompleted);

        // Act
        invoker.Undo(); // Undo complete
        Assert.False(taskList.GetById(taskId)?.IsCompleted);

        invoker.Undo(); // Undo add
        Assert.Equal(0, taskList.Count);
    }
}
