using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public float amount;
    public GameObject source;

    public Damage(float amount, GameObject source)
    {
        this.amount = amount;
        this.source = source;
    }
}
