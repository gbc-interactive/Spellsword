using Spellsword;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDSystem : MonoBehaviour
{
    public TextMeshProUGUI InteractionPrompt;
    public Slider HPBar;
    public Slider MPBar;

    private void Awake()
    {
        SetMaxHP(GameManager.Instance._playerController._maxHP);
        SetMaxMP(GameManager.Instance._playerController._maxMP);
    }

    public void SetMaxHP(float hp)
    {
        HPBar.maxValue = hp;
        HPBar.value = HPBar.maxValue;
    }

    public void SetMaxMP(float mp)
    {
        MPBar.maxValue = mp;
        MPBar.value = MPBar.maxValue;
    }

    public void SetCurrentHP(float currentHP)
    {
        HPBar.value = currentHP;
    }

    public void SetCurrentMP(float currentMP)
    {
        MPBar.value = currentMP;
    }

    public void ShowInteractionPrompt(string promptString)
    {
        if (!InteractionPrompt.gameObject.activeInHierarchy)
        {
            InteractionPrompt.text = promptString;
            InteractionPrompt.gameObject.SetActive(true);
        }
    }

    public void HideInteractionPrompt()
    {
        if (InteractionPrompt.gameObject.activeInHierarchy)
        {
            InteractionPrompt.gameObject.SetActive(false);
        }
    }
}
