using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTowerKnockback : Item
{
    public ItemTowerKnockback(string id, string name, string description, int value, int limit = int.MaxValue) : base(id, name, description, value, limit)
    {

    }

    public override void OnItemUse()
    {
        GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Objects/Towers/KnockbackTower"), SoldierManager.instance.currentSoldier.transform.position, Quaternion.identity);
        base.OnItemUse();
    }

}
