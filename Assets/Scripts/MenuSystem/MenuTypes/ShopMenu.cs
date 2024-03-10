using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Spellsword;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour, IMenu
{
    [SerializeField] private GameObject _shopMenu;

    [SerializeField] public List<IQuestItem> _shopItems = new List<IQuestItem>();

    [SerializeField] private TMP_Text _coinText;

    [SerializeField] private InventoryMenu _inventoryMenu;

    [SerializeField] private GameObject _itemGrid;

    [SerializeField] private GameObject _shopEntryPrefab;
    // Start is called before the first frame update
    void Start()
    {
        _inventoryMenu = FindObjectOfType<InventoryMenu>();

        _shopItems.Add(new QuestItemExample());
        _shopItems.Add(new QuestItemExample());

        _shopMenu.SetActive(false);
        foreach (IQuestItem shopitem in _shopItems)
        {
            Debug.Log("Creatiing Shop Grid");
            var newItem = Instantiate(_shopEntryPrefab, _itemGrid.transform);
            newItem.GetComponentInChildren<TMP_Text>().text = shopitem.itemName + "\n" + shopitem.description + "\n" + shopitem.shopPrice;
            newItem.GetComponentInChildren<Image>().sprite = shopitem.inventoryIcon;
        }
    }
    public void Enable()
    {
        _shopMenu.SetActive(true);
        _coinText.text = "Your Coins: " + _inventoryMenu._coins;
        
    }
    public void Disable()
    {
        MenuManager.Instance._currentMenu = FindObjectOfType<JournalMenu>();
        _shopMenu.SetActive(false);
    }


    public void HandleInput()
    {
    }
}
