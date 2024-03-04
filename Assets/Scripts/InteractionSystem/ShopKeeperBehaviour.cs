using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;

namespace Spellsword
{
    public class ShopKeeperBehaviour : MonoBehaviour, IInteractable
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
                DialogueManager.Instance.ReadDialogue(_dialogueText);
                Debug.Log("Guys Speaks!");
                if(DialogueManager.Instance._started == false){
                    Debug.Log("Open Shop");
                    MenuManager.Instance.ChangeMenu(FindObjectOfType<ShopMenu>());
                }
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