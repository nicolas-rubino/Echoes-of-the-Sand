using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Create New Base Item")]

public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public bool isStackable = false;
}


//EXEMPLE : utiliser les interfaces pour les différents types d'items
//[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Create New Consumable Item")]
//public class ConsumableItem : Item
//{
//    public void Consume()
//    {
//        Debug.Log("Consume Item");
//    }
//}
