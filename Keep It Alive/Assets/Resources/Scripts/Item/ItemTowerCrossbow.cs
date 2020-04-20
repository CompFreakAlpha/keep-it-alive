using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTowerCrossbow : Item
{
    public ItemTowerCrossbow(string id, string name, string description, int value, int limit = int.MaxValue) : base(id, name, description, value, limit)
    {

    }

    public override void OnItemUse()
    {
        GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Objects/Towers/CrossbowTower"), SoldierManager.instance.currentSoldier.transform.position, Quaternion.identity);
        base.OnItemUse();
    }

}
