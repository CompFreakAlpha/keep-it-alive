using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : Upgrade
{
    public float damageIncreasePercentage;
    public WeaponUpgrade(string id, string name, string description, float percentage) : base(id, name, description)
    {
        this.damageIncreasePercentage = percentage;
        this.description = "Gives a " + this.damageIncreasePercentage + "% increase to damage";
    }

    public override void OnEquipped()
    {
        base.OnEquipped();
        SoldierManager.instance.attackDamage += damageIncreasePercentage / 100;
    }
}
