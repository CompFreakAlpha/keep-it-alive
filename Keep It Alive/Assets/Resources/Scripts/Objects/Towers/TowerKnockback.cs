using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerKnockback : Tower
{

    public float knockback = 4;

    public override void Start()
    {
        base.Start();
        shotDamage = 0f;
        timeBetweenShots = 0.5f;
    }

    public override void Attack(Transform target)
    {
        GameObject slashGO = Resources.Load<GameObject>("Prefabs/FX/Slash");


        GameObject slash1 = Instantiate(slashGO, transform.Find("Slashpoint1").position, Quaternion.identity, transform);

        slash1.GetComponent<Slash>().damage = shotDamage;
        slash1.GetComponent<Slash>().knockback = knockback;

        Vector2 dirVec1 = (target.position - transform.position).normalized;
        float angle1 = -GameManager.AngleFromVector(dirVec1) - 180;
        Quaternion q1 = Quaternion.AngleAxis(angle1, Vector3.forward);

        slash1.transform.rotation = q1;


        GameObject slash2 = Instantiate(slashGO, transform.Find("Slashpoint2").position, Quaternion.identity, transform);

        slash2.GetComponent<Slash>().damage = shotDamage;
        slash2.GetComponent<Slash>().knockback = knockback;

        Vector2 dirVec2 = (target.position - transform.position).normalized;
        float angle2 = -GameManager.AngleFromVector(dirVec2);
        Quaternion q2 = Quaternion.AngleAxis(angle2, Vector3.forward);

        slash2.transform.rotation = q2;




        base.Attack(target);
    }
}
