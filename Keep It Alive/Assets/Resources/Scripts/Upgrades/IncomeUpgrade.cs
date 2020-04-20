using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeUpgrade : Upgrade
{
    public float percentIncrease;

    public IncomeUpgrade(string id, string name, string description, float percentIncrease) : base(id, name, description)
    {
        this.percentIncrease = percentIncrease;
        this.description = "Gives a " + this.percentIncrease + "% increase to income";
    }

    public override void OnEquipped()
    {
        base.OnEquipped();
        SoldierManager.instance.incomePercentage += percentIncrease;
    }
}
