using System;
using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;

public interface IQuestItem : IInteractable
{
    public Sprite inventoryIcon { get; set; }

    public void SetIcon(Sprite icon);

    public int shopPrice {get; set;}

    public void SetShopPrice(int cost);

    public string description {get; set;}

    public void SetDescription(string desc);

    public string itemName {get; set;}

    public void SetName(string itemName);
}
