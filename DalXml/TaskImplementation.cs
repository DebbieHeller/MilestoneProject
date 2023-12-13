﻿using DalApi;
using DO;

namespace Dal;

internal class TaskImplementation : ITask
{
    const string filePath = @"tasks";
    public int Create(DO.Task de1)
    {
        int id = Config.NextTaskId;
        DO.Task copy = de1 with { Id = id };
        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(filePath);
        tasks.Add(copy);
        XMLTools.SaveListToXMLSerializer<DO.Task>(tasks, filePath);
        return id;
    }

    public void Delete(int id)
    {
        throw new DalDeletionImpossible($"Task is indelible entity");
    }

    public DO.Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        return XMLTools.LoadListFromXMLSerializer<DO.Task>(filePath).FirstOrDefault(filter!);
    }

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        return filter == null ? XMLTools.LoadListFromXMLSerializer<DO.Task>(filePath).Select(item => item) : XMLTools.LoadListFromXMLSerializer<DO.Task>(filePath).Where(filter!);
    }



public void Update(DO.Task de1)
{
        var existingTask = Read(t => t.Id == de1.Id);
        if (existingTask is null)
            throw new DalDoesNotExistException($"Task with ID={de1.Id} does not exist");

        List<DO.Task> tasks = XMLTools.LoadListFromXMLSerializer<DO.Task>(filePath);
        tasks.Remove(existingTask);
        tasks.Add(de1);
        XMLTools.SaveListToXMLSerializer<DO.Task>(tasks, filePath);
}
}
