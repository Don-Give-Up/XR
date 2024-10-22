using System;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager.AddItem(ItemType.Meat);
        inventoryManager.AddItem(ItemType.Bread);
        inventoryManager.AddItem(ItemType.Bread);
        inventoryManager.AddItem(ItemType.Meat);
    }
}
