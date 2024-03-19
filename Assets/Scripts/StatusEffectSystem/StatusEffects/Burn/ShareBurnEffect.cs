using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;

public class ShareBurnEffect : MonoBehaviour
{
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag != "Player" && collider.tag != "Enemy" && collider.tag != "Water")
            return;

        CharacterBase characterBase = null;

        if (collider.tag == "Water")
        {
            characterBase = gameObject.GetComponentInParent<CharacterBase>();
            
            foreach(StatusEffect effect in characterBase.statusEffects)
            {
                if (effect is BurnStatusEffect)
                {
                    effect.EndEffect();
                    break;
                }
            }

            Destroy(gameObject);
        }
        else if (collider.tag == "Player")
        {
            //characterBase = collider.GetComponent<PlayerController>();
        }
        else if (collider.tag == "Enemy")
        {
            characterBase = collider.GetComponent<EnemyBehaviour>();
        }

        BurnStatusEffect burn = new BurnStatusEffect();
        burn.ApplyEffect(characterBase, 1.0f, 5.0f);


    }
}
