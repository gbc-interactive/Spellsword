using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class ChestBehaviour : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _prompt;
        [SerializeField] private bool _isInteractable;

        public string InteractionPrompt => _prompt;

        public bool isInteractable { get => _isInteractable; set => SetInteractable(true); }

        public bool Interact(InteractionSystem interactor)
        {
            if (_isInteractable)
            {
                Debug.Log("Chest Open!");
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