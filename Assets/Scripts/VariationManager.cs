using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariationManager : MonoBehaviour
{
    [SerializeField] List<Sprite> variations = new List<Sprite>();

    void OnEnable()
    {
        foreach (Transform child in transform)
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                int randomIndex = Random.Range(0, variations.Count);
                spriteRenderer.sprite = variations[randomIndex];
            }
        }
    }
}
