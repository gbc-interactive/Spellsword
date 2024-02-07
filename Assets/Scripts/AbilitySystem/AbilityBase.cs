using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Spellsword
{
    public class AbilityBase : MonoBehaviour
    {
        public ParticleSystem _particleSystem;
        [SerializeField] private EAbilityType _abilityType;
        [SerializeField] public float _MPCost;

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
        public enum Status
        {
            Gust
        }

        public void ApplyDamage(ref float hp,float howMuch)
        {
            hp -= howMuch;
        }

        public virtual void PerformAbility()
        {
            if(_particleSystem != null)
            _particleSystem.Play();
        }
        //https://docs.unity3d.com/Manual/PartSysTriggersModule.html
    }

}
