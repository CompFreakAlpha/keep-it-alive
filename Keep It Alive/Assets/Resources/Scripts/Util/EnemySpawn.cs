using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawn
{
    public string id;
    public float delay = 0.5f;

    public EnemySpawn(string _id, float _delay)
    {
        this.id = _id;
        this.delay = _delay;
    }
}
