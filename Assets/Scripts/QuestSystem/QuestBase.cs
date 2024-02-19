using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBase
{
    [HideInInspector] public List<TaskBase> _tasks = new List<TaskBase>();
    [HideInInspector] public string _questName;
    [HideInInspector] public string _questDescription;
}
