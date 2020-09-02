using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class ScoreTaskController
{
    private Dictionary<int, ScoreAbstractTask> _taskIdsToTasks = new Dictionary<int, ScoreAbstractTask>();

    internal bool AddNewTask(ScoreAbstractTask task)
    {
        if (!_taskIdsToTasks.ContainsKey(task.Id))
        {
            _taskIdsToTasks.Add(task.Id, task);
            return true;
        }
        else
        {
            Debug.LogError($"Task with id {task.Id.ToString()} already added!");
            return false;
        }
    }

    private int GetNextTaskId()
    {
        return _taskIdsToTasks.Count;
    }

    internal ScoreAbstractTask CreateNewTas(int reward, int[] ids, ScoreObjectType type)
    {
        switch (type)
        {
            case ScoreObjectType.Grouped:
                return new GroupScoreTask(GetNextTaskId(), ids, reward, type);
            default:
                return null;
        }
    }

    internal bool CheckTaskCompleteById(int taskId, out int reward)
    {
        if (_taskIdsToTasks.TryGetValue(taskId, out var task) && task.CheckCompleted())
        {
            reward = task.Reward;
            _taskIdsToTasks.Remove(taskId);
            return true;
        }
        else
        {
            reward = 0;
            return false;
        }
    }
}
