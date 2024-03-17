using Spellsword;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleQuest :  QuestBase
{
    // Start is called before the first frame update
    public SampleQuest()
    {
        TaskBase TaskOne = new SampleTask();
        TaskBase TaskTwo = new SampleTask();
        TaskBase TaskThree = new GetItemTask(typeof(QuestItemExample));

        //_tasks.Add(TaskOne);
        //_tasks.Add(TaskTwo);
        //_tasks.Add(TaskThree);

        _questDescription = "Explore";
        _questName = "SAMPLE";
    }
    

}
