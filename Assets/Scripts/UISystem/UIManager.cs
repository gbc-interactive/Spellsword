using Spellsword;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance = null;
        public static UIManager Instance => _instance;

        public HUDSystem _headsOverDisplay = null;

        private void Awake()
        {
            _instance = this;
        }
    }
}
