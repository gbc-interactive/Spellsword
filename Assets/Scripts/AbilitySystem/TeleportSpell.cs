using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class TeleportSpell : AbilityBase
    {
        public override void PerformAbility()
        {
            Cast();
            Teleport();
            base.PerformAbility();
        }

        private void Teleport()
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            Plane plane = new Plane(Vector3.up, new Vector3(0, 0.7f, 0)); // Plane at the players position y
            if (plane.Raycast(ray, out float enter))
            {
                Vector3 worldPosition = ray.GetPoint(enter);
                GameManager.Instance._playerController.transform.position = worldPosition;
            }
            Time.timeScale = 1; //reset time 
        }

    }
}

