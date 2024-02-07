using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCallbackable
{
    public void TriggerEnterCallback(Collider other);
    public void TriggerExitCallback(Collider other);
}
