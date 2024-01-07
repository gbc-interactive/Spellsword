using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class GuyBehaviour : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _prompt;
        [SerializeField] private bool _isInteractable;

        public bool isInteractable { get => _isInteractable; set => SetInteractable(true); }

        public string InteractionPrompt => _prompt;

        public bool Interact(InteractionSystem interactor)
        {
            if (_isInteractable)
            {
                Debug.Log("Guy speaks!");
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetInteractable(bool value)
        {
            _isInteractable = value;
        }
    }
}