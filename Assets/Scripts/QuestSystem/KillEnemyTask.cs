using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;

public class KillEnemyTask : TaskBase
{
    public KillEnemyTask(int amount)
    {
        _taskDescription = "Kill " + amount + " Enemies";
        _remaining = amount;
    }
}
