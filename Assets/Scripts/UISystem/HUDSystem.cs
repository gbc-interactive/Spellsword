using Spellsword;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDSystem : MonoBehaviour
{
    public TextMeshProUGUI InteractionPrompt;
    public Image HPBar;
    public Image MPBar;

    private float maxHP;
    private float maxMP;

    public List<Image> spellTimers;

    public void Initialize()
    {
        SetMaxHP(GameManager.Instance._playerController._maxHP);
        SetMaxMP(GameManager.Instance._playerController._maxMP);
    }

    public void SetMaxHP(float hp)
    {
        maxHP = hp;
        HPBar.fillAmount = 1;
    }

    public void SetMaxMP(float mp)
    {
        maxMP = mp;
        MPBar.fillAmount = 1;
    }

    public void SetCurrentHP(float currentHP)
    {
        HPBar.fillAmount = currentHP / maxHP;
    }

    public void SetCurrentMP(float currentMP)
    {
        MPBar.fillAmount = currentMP / maxMP;
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

    public void StartCooldown(int index, float duration)
    {
        StartCoroutine(AnimateFill(spellTimers[index], duration));
    }

    IEnumerator AnimateFill(Image image, float duration)
    {
        float elapsedTime = 0f;
        float startFillAmount = 1f;
        float targetFillAmount = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            image.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, t);
            yield return null;
        }
        image.fillAmount = targetFillAmount;
    }
}
