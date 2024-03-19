using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    //le dictionnaire doit être privé pour être sérialisé?
    public Dictionary<Item, int> currentInventory = new Dictionary<Item, int>();
    public static event Action<Item> OnInventoryModified;

    public Item[] startingItems;

    public static InventoryController singleton;
    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        foreach(Item item in startingItems)
        {
            AddItem(item);
        }
    }

    public void AddItem(Item item, int amount = 1)
    {
        if (currentInventory.TryAdd(item, amount))
        {
            currentInventory[item] += amount;
            OnInventoryModified?.Invoke(item);
        }
    }

    public void RemoveItem(Item item, int amount = 1)
    {
        if (!currentInventory.ContainsKey(item)) return;

        currentInventory[item] -= amount;        

        if (currentInventory[item] <= 0)
        {
            currentInventory.Remove(item);
        }

        OnInventoryModified?.Invoke(item);
    }
}
