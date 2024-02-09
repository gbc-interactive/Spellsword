using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    [SerializeField] List<Sprite> treeVariations = new List<Sprite>();

    void OnEnable()
    {
        foreach (Transform child in transform)
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                int randomIndex = Random.Range(0, treeVariations.Count);
                spriteRenderer.sprite = treeVariations[randomIndex];
            }
        }
    }
}
