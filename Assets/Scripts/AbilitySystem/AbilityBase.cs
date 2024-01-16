using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace Spellsword
{
    public class AbilityBase : MonoBehaviour
    {
        void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }
        public enum EAbilityType
        {
            OneStep,
            TwoStep,
            Default = OneStep
        }

        public ParticleSystem _particleSystem;
        [SerializeField] private EAbilityType _abilityType;
        [SerializeField] private float _manaCost;

        public virtual void PerformAbility()
        {
            _particleSystem.Play();
        }
        //https://docs.unity3d.com/Manual/PartSysTriggersModule.html
    }

}
