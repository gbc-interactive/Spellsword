using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBase
{
    [HideInInspector] public Queue<TaskBase> _tasks = new Queue<TaskBase>();
    [HideInInspector] public string _questName;
    [HideInInspector] public string _questDescription;

    void TaskComplete()
    {
        _tasks.Dequeue();
    }
}
