using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemLocationUI : MonoBehaviour
{
    //on pourrait mettre myItem ReadOnly (voir le cours des outils)
    public Item myItem;
    public Image image;
    public TMP_Text text;

    public void UpdateUI(Item item)
    {
        myItem = item;
        image.sprite = item.itemIcon;

        if (item.isStackable)
        {
            text.text = InventoryController.singleton.currentInventory[item].ToString();
        }
        else
        {
            text.text = "";
        }
    }

}
