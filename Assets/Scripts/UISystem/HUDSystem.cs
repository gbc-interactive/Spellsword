using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDSystem : MonoBehaviour
{
    public TextMeshProUGUI _interactionPrompt;

    public void ShowInteractionPrompt(string promptString)
    {
        if (!_interactionPrompt.gameObject.activeInHierarchy)
        {
            _interactionPrompt.text = promptString;
            _interactionPrompt.gameObject.SetActive(true);
        }
    }

    public void HideInteractionPrompt()
    {
        if (_interactionPrompt.gameObject.activeInHierarchy)
        {
            _interactionPrompt.gameObject.SetActive(false);
        }
    }
}
