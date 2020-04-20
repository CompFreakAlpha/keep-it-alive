using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    public List<Upgrade> upgrades = new List<Upgrade>();

    private void Awake()
    {
        Singleton();
        UpgradeDatabaseInit();
    }


    private void Singleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    public void AddUpgrade(Upgrade upgrade)
    {
        upgrades.Add(upgrade);
        Debug.Log("[Upgrade] " + upgrade.name);
    }

    private void UpgradeDatabaseInit()
    {
        Debug.Log("/ Initializing Upgrades \\");


        AddUpgrade(new IncomeUpgrade("more_money_1", "More Money", "Temp", 20));
        AddUpgrade(new IncomeUpgrade("more_money_2", "More Money", "Temp", 30));
        AddUpgrade(new IncomeUpgrade("more_money_3", "Bottomless Wallet", "Temp", 45));

        AddUpgrade(new WeaponUpgrade("stronger_sword_1", "Stronger Sword", "Temp", 30));
        AddUpgrade(new WeaponUpgrade("stronger_sword_2", "Even Stronger Sword", "Temp", 60));
        AddUpgrade(new WeaponUpgrade("stronger_sword_3", "Super Sword", "Temp", 75));


        Debug.Log("\\ Initialized Upgrades /");
    }

}
