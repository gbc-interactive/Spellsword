using System;
using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;

public class StatusEffect
{
    protected CharacterBase affectedCharacter;

    protected float intervalTimeCounter;
    protected float totalTimeCounter;

    protected float timeEffectIntervals;
    protected float totalTimeAffectedFor;

    public virtual bool ApplyEffect(CharacterBase target, float intervals, float duration)
    {
        for (int i = 0; i < target.statusEffects.Count; i++)
        {
           if (GetType() == target.statusEffects[i].GetType())
                return false;
        }
        

        affectedCharacter = target;
        timeEffectIntervals = intervals;
        totalTimeAffectedFor = duration;

        affectedCharacter.statusEffects.Add(this);
        return true;
    }

    public virtual void UpdateEffect()
    {
        totalTimeCounter += Time.deltaTime;
        intervalTimeCounter += Time.deltaTime;

        if (intervalTimeCounter >= timeEffectIntervals)
        {
            intervalTimeCounter -= timeEffectIntervals;
            PerformEffect();
        }

        if (totalTimeCounter > totalTimeAffectedFor)
        {
            EndEffect();
        }

    }

    public virtual void EndEffect()
    {
        affectedCharacter.statusEffects.Remove(this);
    }

    public virtual void PerformEffect()
    {
        
    }
}
