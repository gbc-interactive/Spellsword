using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Spellsword
{
    public class AbilityBase : MonoBehaviour
    {
        public ParticleSystem _particleSystem;
        [SerializeField] private EAbilityType _abilityType;
        [SerializeField] private float _manaCost;
        void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();

        }
        public enum EAbilityType 
        {
            Attack1,
            Attack2,
            Default = Attack1
        }

        public void ApplyDamage(ref float hp,float howmuch)
        {
            hp -= howmuch;
        }

        //ability collision
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //ApplyDamage(ref HP, 10);
                Debug.Log("Collsion");
            }
            else if (other.CompareTag("Enemy"))
            {
                //ApplyDamage(ref HP, 10);
                Debug.Log("CollsionEnemy");
            }
        }

        public virtual void PerformAbility()
        {
            _particleSystem.Play();
        }
        //https://docs.unity3d.com/Manual/PartSysTriggersModule.html
    }

}
