using System.Collections;
using System.Collections.Generic;
using Spellsword;
using UnityEngine;

public interface IQuestItem : IInteractable
{
    public QuestBase quest{ get; set; }
    public void SetQuest(QuestBase quest);
    public Sprite inventoryIcon { get; set; }

    public void SetIcon(Sprite icon);
}
