using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipment : IItem
{
    public void Equip();//apply stat changes
    public void Unequip();//unapply that changes

    public bool isEquipped{get; set;}
}
