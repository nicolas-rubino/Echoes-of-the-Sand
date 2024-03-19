using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private ItemLocationUI[] locations;

    private void UpdateUI(Item item)
    {
        if (item.isStackable)
        {
            foreach (ItemLocationUI e in locations)
            {
                if(e.myItem == item)
                {
                    e.UpdateUI(item);
                    return;
                }
            }
        }

        foreach (ItemLocationUI e in locations)
        {
            if (e.myItem == null)
            {
                e.UpdateUI(item);
                return;
            }
        }
    }

    private void OnEnable()
    {
        InventoryController.OnInventoryModified += UpdateUI;
    }

    private void OnDisable()
    {
        InventoryController.OnInventoryModified -= UpdateUI;
    }
}
