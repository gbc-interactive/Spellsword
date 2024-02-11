using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class MeleeAbility : AbilityBase
    {
        private Vector3 originalScale;
        private float dashDistance = 0.0f;
        void Start()
        {
            originalScale = _particleSystem.gameObject.transform.localScale;
        }

        public override void PerformAbility()
        {
            Cast();
            //change direction based on player direction
            EDirection playerDirection = GameManager.Instance._playerController.GetFacingDirection();
            Vector3 dashDirection;
            if (playerDirection == EDirection.Left)
            {
                dashDirection = Vector3.left;
                GameManager.Instance._playerController.TryMove(dashDirection * dashDistance);
                _particleSystem.gameObject.transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            }
            else
            {
                dashDirection = Vector3.right;
                GameManager.Instance._playerController.TryMove(dashDirection * dashDistance);
                _particleSystem.gameObject.transform.localScale = originalScale;
            }
           
            base.PerformAbility();
        }
    }
}