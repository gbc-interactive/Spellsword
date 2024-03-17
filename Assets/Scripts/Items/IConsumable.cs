using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConsumable : IItem 
{
    public void Consume(); //decrement uses and does the effect

    public void RemoveEffect();

    public bool isActive{get; set;}

    public int Uses{get; set;}

}
