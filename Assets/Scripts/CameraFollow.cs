using Unity.VisualScripting;
using UnityEngine;

namespace Spellsword
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;  // Reference to the player object
        [SerializeField] private float _smoothSpeed = 0.125f;  // Smoothing factor for camera movement
        
        private Vector3 _offset;  // Offset from the player position

        private void Awake()
        {
            _offset = transform.position;
        }

        private void LateUpdate()
        {
            if (_target == null)
            {
                // Make sure to assign the player object to the "target" variable in the Unity Editor
                Debug.LogWarning("Target not assigned in CameraFollow script.");
                return;
            }

            // Calculate the desired position for the camera
            Vector3 desiredPosition = _target.position + _offset;

            // Use Mathf.Lerp to smoothly interpolate between the current position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);

            // Set the camera's position to the smoothed position
            transform.position = smoothedPosition;

            // Make the camera look at the player
            transform.LookAt(_target);
        }
    }
}