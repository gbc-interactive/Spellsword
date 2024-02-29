using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spellsword;

public class BaseAIBehaviour 
{
    public virtual void EnterBehaviour(EnemyBehaviour _enemySelf)
    {}

    public virtual void UpdateBehaviour(EnemyBehaviour _enemySelf)
    {} 

    public virtual void ExitBehaviour(EnemyBehaviour _enemySelf)
    {}

}
