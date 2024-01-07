using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spellsword
{
    public class InteractionSystem : MonoBehaviour
    {
        public bool isInteracting = false;

        [SerializeField] private Transform _interactionPoint;
        [SerializeField] private float _interactionPointRadius = 0.5f;
        [SerializeField] private LayerMask _interactableMask;

        private readonly Collider[] _colliders = new Collider[3];
        [SerializeField] private int _numFound;

        private void Update()
        {
            _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

            if (_numFound > 0)
            {
                var interactable = _colliders[0].GetComponent<IInteractable>();

                if (interactable != null)
                {
                    UIManager.Instance._headsOverDisplay.ShowInteractionPrompt(interactable.InteractionPrompt);

                    if (isInteracting)
                    {
                        isInteracting = false;
                        interactable.Interact(this);
                    }
                }
            }
            else
            {
                UIManager.Instance._headsOverDisplay.HideInteractionPrompt();
            }
        }

        public void PerformInteraction()
        {

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
        }
    }
}
