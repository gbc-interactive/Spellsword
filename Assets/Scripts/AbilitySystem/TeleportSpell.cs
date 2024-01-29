using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class TeleportSpell : AbilityBase
    {
        private float teleportCDTime = 1f;

        // Start is called before the first frame update
        void Start()
        {

        }
        public override void PerformAbility()
        {
            Debug.Log("casting teleport spell");
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            Plane plane = new Plane(Vector3.up, new Vector3(0, 0.7f, 0)); // Plane at the players position y
            if (plane.Raycast(ray, out float enter))
            {
                Vector3 worldPosition = ray.GetPoint(enter);
                Debug.Log("mouseposition" + mousePosition.x + ", " + mousePosition.y + ", " + worldPosition.z);
                GameManager.Instance._playerController.transform.position = worldPosition;
                base.PerformAbility();
            }
        }
        // Update is called once per frame
        void Update()
        {

        }

    }
}

