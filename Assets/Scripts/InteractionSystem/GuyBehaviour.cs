using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Spellsword
{
    public class GuyBehaviour : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _prompt;
        [SerializeField] private bool _isInteractable;
        [SerializeField] public TextAsset _dialogueText;

        public bool isInteractable { get => _isInteractable; set => SetInteractable(true); }

        public string InteractionPrompt => _prompt;

        public bool Interact(InteractionSystem interactor)
        {
            if (_isInteractable)
            {
                
                FindObjectOfType<DialogueManager>().ReadDialogue(_dialogueText);
                Debug.Log("Guys Speaks!");
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