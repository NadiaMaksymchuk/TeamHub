using System.ComponentModel;

namespace CollectionsAndLinq.DAL.Entities;

public enum TaskState
{
    [Description("To Do")]
    ToDo,
    [Description("In Progress")]
    InProgress,
    [Description("Done")]
    Done,
    [Description("Canceled")]
    Canceled
}