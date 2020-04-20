using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCrossbow : Tower
{

    public override void Start()
    {
        base.Start();
        shotDamage = 0.5f;
        detectionRange = 10;
    }

    public override void Attack(Transform target)
    {
        GameObject arrowGO = Resources.Load<GameObject>("Prefabs/Objects/Arrow");
        GameObject arrow = Instantiate(arrowGO, transform.position, Quaternion.identity);


        Vector2 dirVec = (target.position - transform.position).normalized;
        float angle = -GameManager.AngleFromVector(dirVec);
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        arrow.transform.rotation = q;
        arrow.GetComponent<Bullet>().direction = (target.position - transform.position).normalized;
        arrow.GetComponent<Bullet>().bulletDamage = shotDamage;

        base.Attack(target);
    }
}
