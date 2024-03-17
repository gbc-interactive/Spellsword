using System;
using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;

public interface IItem : IInteractable
{
    public Sprite inventoryIcon { get; set; }

    public int shopPrice {get; set;}

    public string description {get; set;}

    public string itemName {get; set;}

    public void OnClick();

}
