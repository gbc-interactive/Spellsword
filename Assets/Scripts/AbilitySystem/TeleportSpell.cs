using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class TeleportSpell : AbilityBase
    {
        public override void PerformAbility(CharacterBase character, bool isPlayer)
        {
            Cast();
            Teleport();
            base.PerformAbility(character, isPlayer);
        }

        private void Teleport()
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            Plane plane = new Plane(Vector3.up, new Vector3(0, 0.7f, 0)); // Plane at the players position y
            if (plane.Raycast(ray, out float enter))
            {
                InterruptEnemies();

                Vector3 worldPosition = ray.GetPoint(enter);
                GameManager.Instance._playerController.transform.position = worldPosition;
            }
            Time.timeScale = 1; //reset time 
        }

        private void InterruptEnemies()
        {
            int playerLayer = LayerMask.NameToLayer("Player");
            int ignoreLayer = LayerMask.NameToLayer("IgnoreLayer");
            int layerMask = ~((1 << playerLayer) | (1 << ignoreLayer)); // will ignore the players layer and other ignore layers

            Collider[] hitColliders = Physics.OverlapSphere(GameManager.Instance._playerController.transform.position, 10.0f, layerMask); //NOTE use a variable later on for range
            foreach (var hitCollider in hitColliders)
            {
                EnemyBehaviour enemy = hitCollider.GetComponent<EnemyBehaviour>();
                if (enemy == null)
                    continue;

                enemy.ResetAttackCooldowns();
            }
        }
    }
}

