using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TriggerCallback : MonoBehaviour
{
    [SerializeField] private Component _callBackScript;
    private void OnTriggerEnter(Collider other)
    {
        (_callBackScript as ITriggerCallbackable).TriggerEnterCallback(other);
    }

    private void OnTriggerExit(Collider other)
    {
        (_callBackScript as ITriggerCallbackable).TriggerExitCallback(other);
    }
}
