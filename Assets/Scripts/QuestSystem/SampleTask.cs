using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleTask : TaskBase
{
    // Start is called before the first frame update
    public SampleTask()
    {
        _taskDescription = "Sample Task Description";
        _remaining = 99;
    }

}
