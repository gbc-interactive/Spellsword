using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Spellsword
{
    public class TeleportSpell : AbilityBase
    {
        public override bool PerformAbility(CharacterBase character, bool isPlayer)
        {
            Cast();
            if (Teleport())
            {
                character._timeSinceLastAbility = 0;
                base.PerformAbility(character, isPlayer);
                if (isPlayer)
                {
                    UIManager.Instance._headsOverDisplay.StartCooldown(4, _cooldownTime);
                }
                return true;
            }
            return false;
        }

        private bool Teleport()
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            Plane plane = new Plane(Vector3.up, new Vector3(0, 0.7f, 0)); // Plane at the player's position y
            if (plane.Raycast(ray, out float enter))
            {
                Vector3 worldPosition = ray.GetPoint(enter);

                // Check if the clicked position is on the NavMesh
                if (IsPositionOnNavMesh(worldPosition))
                {
                    GameManager.Instance._playerController.transform.position = worldPosition;
                    Time.timeScale = 1; // Reset time
                    return true;
                }
                else
                {
                    // Handle case where the clicked position is not on the NavMesh
                    Debug.Log("Clicked position is not on the NavMesh.");
                }
            }
            return false;
        }

        private bool IsPositionOnNavMesh(Vector3 position)
        {
            NavMeshHit hit;
            // Check if the position is on the NavMesh
            position = new Vector3(position.x, 0, position.z);
            return NavMesh.SamplePosition(position, out hit, 0.1f, NavMesh.AllAreas);
        }

    }
}

