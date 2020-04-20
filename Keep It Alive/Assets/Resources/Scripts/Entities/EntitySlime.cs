using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySlime : EntitySkeleton
{
    public override void Start()
    {
        base.Start();
        maxHealth = 8 + (8 * (GameObject.FindObjectOfType<SpawnPoint>().waveIndex * 0.1f));
        health = 8 + (8 * (GameObject.FindObjectOfType<SpawnPoint>().waveIndex * 0.1f));
        speed = 1.5f;
    }

    public override void Die(bool audio = true, bool dropMoney = false)
    {
        base.Die(audio, dropMoney);
        DropMoney(Random.Range(6, 8), (int)Mathf.Ceil(20 * (SoldierManager.instance.incomePercentage / 100)), (int)Mathf.Ceil(35 * (SoldierManager.instance.incomePercentage / 100)));
    }
}
