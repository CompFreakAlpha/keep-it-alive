using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[System.Serializable]
public class Item
{

    public string ID;
    public string name;
    public string description;
    public Sprite icon;
    public int value;
    public int limit;

    public Item(string _ID, string _name, string _description, int _value, int _limit = int.MaxValue)
    {

        ID = _ID;
        name = _name;
        description = _description;

        icon = Resources.LoadAll<Sprite>("Sprites/Items/" + _ID)[0];
        limit = _limit;
        value = _value;

    }

    public virtual Item SetID(string _ID)
    {
        ID = _ID;
        return this;
    }

    public virtual Item SetName(string _name)
    {
        name = _name;
        return this;
    }

    public virtual Item SetDescription(string _description)
    {
        description = _description;
        return this;
    }

    public virtual void OnItemUse()
    {
        ClearItem();
    }

    public void ClearItem()
    {
        SoldierManager.instance.currentItem = null;
    }
}