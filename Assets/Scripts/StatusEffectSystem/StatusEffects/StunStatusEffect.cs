using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;

public class StunStatusEffect : StatusEffect
{
    float originalMoveSpeed;

    public override bool ApplyEffect(CharacterBase target, float intervals, float duration)
    {
        if (!base.ApplyEffect(target, intervals, duration))
            return false;

        PlayerController pc;
        EnemyBehaviour eb;

        if (pc = target as PlayerController)
        {
            originalMoveSpeed = pc._moveSpeed;
            pc._moveSpeed = 0;
        }
        else if (eb = target as EnemyBehaviour)
        {
            originalMoveSpeed = eb._navAgent.speed;
            eb._navAgent.speed = 0;
        }

        //NOTE Find a better way to get the correct child.
        target.gameObject.transform.GetChild(1).gameObject.SetActive(false);

        return true;
    }

    public override void EndEffect()
    {
        PlayerController pc;
        EnemyBehaviour eb;

        if (pc = affectedCharacter as PlayerController)
        {
            pc._moveSpeed = originalMoveSpeed;
        }
        else if (eb = affectedCharacter as EnemyBehaviour)
        {
            eb._navAgent.speed = originalMoveSpeed;
        }

        //NOTE Find a better way to get the correct child.
        affectedCharacter.gameObject.transform.GetChild(1).gameObject.SetActive(true);

        base.EndEffect();
    }

    
}
