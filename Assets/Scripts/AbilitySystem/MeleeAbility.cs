using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class MeleeAbility : AbilityBase
    {
        private Vector3 originalScale;

        void Start()
        {
            originalScale = _particleSystem.gameObject.transform.localScale;
        }

        public override void PerformAbility()
        {
            //change direction based on player direction
            EDirection playerDirection = GameManager.Instance._playerController.GetFacingDirection();
            if (playerDirection == EDirection.Left)
            {
                _particleSystem.gameObject.transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            }
            else
            {
                _particleSystem.gameObject.transform.localScale = originalScale;
            }

            base.PerformAbility();
        }
    }
}