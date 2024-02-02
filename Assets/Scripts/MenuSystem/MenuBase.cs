using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public abstract class MenuBase : MonoBehaviour
    {
        private void Start()
        {
            Disable();
        }

        public abstract void Enable();

        public abstract void Disable();
        public abstract void HandleInput();
        public bool bIsActive = false;
    }
}
