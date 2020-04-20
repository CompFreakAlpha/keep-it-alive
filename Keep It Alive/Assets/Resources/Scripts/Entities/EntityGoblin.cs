using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityGoblin : EntitySkeleton
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        maxHealth = 25 + (8 * (GameObject.FindObjectOfType<SpawnPoint>().waveIndex * 0.1f));
        health = 25 + (8 * (GameObject.FindObjectOfType<SpawnPoint>().waveIndex * 0.1f));
        speed = 0.75f;

    }

    public override GameObject AddHealthBar()
    {
        GameObject hBar = base.AddHealthBar();
        hBar.GetComponent<HealthBar>().sizeMultiplier = 0.5f;
        return hBar;
    }

    public override void Die(bool audio = true, bool dropMoney = false)
    {
        base.Die(audio, dropMoney);
        DropMoney(Random.Range(6, 8), (int)Mathf.Ceil(50 * (SoldierManager.instance.incomePercentage / 100)), (int)Mathf.Ceil(25 * (SoldierManager.instance.incomePercentage / 100)));
    }

    protected override void UpdateSkeletonAI()
    {
        FollowPath();
    }
}
