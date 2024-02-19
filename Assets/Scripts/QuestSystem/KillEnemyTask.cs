using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;

public class KillEnemyTask : TaskBase
{
    public System.Type _targetType;
    public KillEnemyTask(System.Type type, int amount)
    {
        _taskDescription = "Kill " + amount + " Enemies";
        _remaining = amount;
        _targetType = type;
        QuestActions.Killed+=Killed;
    }
    void Killed(EnemyBehaviour enemy){
        if(enemy.GetType() == _targetType){
            DecrementRemaining();
        }
    }
}
