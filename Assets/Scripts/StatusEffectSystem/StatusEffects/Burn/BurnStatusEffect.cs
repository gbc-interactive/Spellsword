using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;

public class BurnStatusEffect : StatusEffect
{
    GameObject burnObject;


    public override bool ApplyEffect(CharacterBase target, float intervals, float duration)
    {
        if (!base.ApplyEffect(target, intervals, duration))
            return false;

        burnObject = new GameObject();
        burnObject.transform.parent = target.transform;
        burnObject.transform.position = target.transform.position;
        burnObject.name = "BurnObject";

        SphereCollider burnCollider = burnObject.AddComponent<SphereCollider>();
        burnCollider.isTrigger = true;
        burnCollider.radius = 3.0f;

        burnObject.AddComponent<ShareBurnEffect>();

        return true;
    }

    public override void PerformEffect()
    {
        affectedCharacter.TakeDamage(5);
    }

    public override void EndEffect()
    {
        base.EndEffect();

        GameObject.Destroy(burnObject);
    }
}
