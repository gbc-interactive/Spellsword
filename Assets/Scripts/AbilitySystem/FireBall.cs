using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Spellsword
{
    public class FireBall : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                Debug.Log("Collided with ground");
                FireBallSpell.isGrounded = true;                                
            }
        }

    }

}

