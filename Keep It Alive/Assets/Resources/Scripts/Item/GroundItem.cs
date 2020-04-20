using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
    public Item itemData;

    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        if (string.IsNullOrEmpty(itemData.ID))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<SoldierController>() != null)
        {
            if (SoldierManager.instance.currentItem == null || string.IsNullOrEmpty(SoldierManager.instance.currentItem.ID))
            {
                SoldierManager.instance.currentItem = itemData;
                Destroy(gameObject);
            }
            else
            {
                Item i = SoldierManager.instance.currentItem;
                SoldierManager.instance.currentItem = itemData;
                itemData = i;
            }
        }
    }
}