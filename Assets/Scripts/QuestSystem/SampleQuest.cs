using Spellsword;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleQuest : QuestBase
{
    // Start is called before the first frame update
    public SampleQuest()
    {
        TaskBase TaskOne = new SampleTask();
        TaskBase TaskTwo = new KillEnemyTask(20);
        TaskBase TaskThree = new SampleTask();
        TaskBase TaskFour = new KillEnemyTask(15);

        _tasks.Enqueue(TaskOne);
        _tasks.Enqueue(TaskTwo);
        _tasks.Enqueue(TaskThree);
        _tasks.Enqueue(TaskFour);

        _questDescription = "Sample Quest Description";
        _questName = "Sample Quest Name";
    }
    

}
