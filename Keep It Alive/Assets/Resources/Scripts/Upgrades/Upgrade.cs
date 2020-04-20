using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Upgrade
{
    public string id;
    public Sprite icon;
    public string name;
    public string description;
    public virtual void OnEquipped()
    {
        Debug.Log("Upgrade " + "\"" + name + "\" equipped!");
    }

    public Upgrade(string id, string name, string description)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        icon = Resources.LoadAll<Sprite>("Sprites/UI/Upgrades/" + id)[0];
    }
}
