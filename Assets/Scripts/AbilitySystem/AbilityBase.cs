using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class AbilityBase : MonoBehaviour
    {
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
    }
}
