using Spellsword;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullingManager : MonoBehaviour
{
    public float fadeDistance = 5f;
    public float fadeSmoothness = 2f;

    private Transform player;
    private Transform cameraTransform;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        player = GameManager.Instance._playerController.transform;
        cameraTransform = Camera.main.transform;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        // Calculate vectors between objects
        Vector3 playerToTree = transform.position - player.position;
        Vector3 playerToCamera = cameraTransform.position - player.position;

        // Check if tree is between player and camera
        if (Vector3.Dot(playerToTree, playerToCamera) > 0)
        {
            // Calculate distance from player to tree along the camera direction
            float distance = Vector3.Project(playerToTree, playerToCamera).magnitude;

            // Adjust alpha based on distance
            float alpha = Mathf.Clamp01((distance - fadeDistance) / fadeSmoothness);

            // Apply fading effect
            Color fadedColor = originalColor;
            fadedColor.a = 1 - alpha;
            spriteRenderer.color = fadedColor;
        }
        else
        {
            // Tree is not between player and camera, restore original color
            spriteRenderer.color = originalColor;
        }
    }
}
