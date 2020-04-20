using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityRunner : EntitySkeleton
{
    public override void Start()
    {
        base.Start();
        maxHealth = 2 + (2 * (GameObject.FindObjectOfType<SpawnPoint>().waveIndex * 0.1f));
        health = 2 + (2 * (GameObject.FindObjectOfType<SpawnPoint>().waveIndex * 0.1f));
        speed = 8f;
    }

    public override void Die(bool audio = true, bool dropMoney = false)
    {
        base.Die(audio, false);
    }
}
