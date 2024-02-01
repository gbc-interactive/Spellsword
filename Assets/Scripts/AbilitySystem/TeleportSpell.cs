using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class TeleportSpell : AbilityBase
    {
        private float teleportCDTime = 1f;
        private float lastCastTime;
        // Start is called before the first frame update
        void Start()
        {
            //_manaCost = 10f;
            lastCastTime = -teleportCDTime;
        }
        public override void PerformAbility()
        {
            if (Time.time - lastCastTime < teleportCDTime)
            {
                return; // Ability is on cooldown
            }
            lastCastTime = Time.time;
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

