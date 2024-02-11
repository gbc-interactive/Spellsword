using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBase
{
    [HideInInspector] public string _taskDescription;
    [HideInInspector] public int _remaining;

    //Decrement will be called elsewhere, when progress in the task is detected.
    void DecrementRemaining()
    {
        _remaining--;
    }
}
