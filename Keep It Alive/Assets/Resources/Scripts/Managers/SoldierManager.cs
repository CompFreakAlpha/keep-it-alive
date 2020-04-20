using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : MonoBehaviour
{
    public static SoldierManager instance;

    public int money = 0;

    public float attackKnockback = 1;
    public float attackDamage = 1;
    public float stunTime = 0.25f;
    public float incomePercentage = 100;

    public Item currentItem = null;

    public SoldierController currentSoldier = null;

    public List<Upgrade> currentSoldierUpgrades = new List<Upgrade>();


    private void Awake()
    {
        Singleton();
        SetDefaultSoldier();
    }

    public void AddSoldierUpgrade(Upgrade upgrade)
    {
        currentSoldierUpgrades.Add(upgrade);
        upgrade.OnEquipped();
        GameObject HUD = Instantiate(Resources.Load<GameObject>("Prefabs/UI/UpgradeIcon"), GameObject.FindGameObjectWithTag("HUDCanvas").transform.Find("UpgradeHUD"));
        HUD.GetComponent<UnityEngine.UI.Image>().sprite = upgrade.icon;
    }


    public void ChangeMoney(int amount)
    {
        if (money + amount < 0)
        {
            money = 0;
        }
        else
        {
            money += amount;
        }
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

    private void SetDefaultSoldier()
    {
        if (currentSoldier == null)
        {
            currentSoldier = GameObject.FindObjectOfType<SoldierController>();
        }
    }



}
