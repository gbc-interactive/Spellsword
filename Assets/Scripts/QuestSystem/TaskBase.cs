using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBase
{
    [HideInInspector] public string _taskDescription;
    [HideInInspector] public int _remaining;

    [SerializeField] private bool _isComplete;

    //Decrement will be called elsewhere, when progress in the task is detected.
    public void DecrementRemaining()
    {
        _remaining--;
        if(_remaining<=0){
            _isComplete = true;
        }
    }
}
