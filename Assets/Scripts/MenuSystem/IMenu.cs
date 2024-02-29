using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public interface IMenu
    {
        public void Enable();

        public void Disable();
        public void HandleInput();
    }
}
