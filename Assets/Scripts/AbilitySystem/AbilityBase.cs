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

        [SerializeField] private EAbilityType _abilityType;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] public float MPCost;

        public virtual void PerformAbility()
        {
            _particleSystem.Play();
        }
    }
}
