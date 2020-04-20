using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public float shotTimer = 0;
    public float timeBetweenShots = 1f;

    public float detectionRange = 5;

    public float rotateSpeed = 1;

    public float shotDamage = 1;

    public virtual EntitySkeleton GetClosestEnemyInRange(float dist)
    {
        EntitySkeleton bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (EntitySkeleton potentialTarget in GameObject.FindObjectsOfType<EntitySkeleton>())
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (Vector2.Distance(currentPosition, potentialTarget.transform.position) <= detectionRange)
            {
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }
        }
        return bestTarget;
    }

    public virtual void Attack(Transform target)
    {
        shotTimer = timeBetweenShots;
    }

    public virtual void UpdateShotTimer()
    {
        if (shotTimer > 0)
        {
            shotTimer -= Time.deltaTime;
        }
        else
        {
            if (GetClosestEnemyInRange(detectionRange) != null)
            {
                Transform enemy = GetClosestEnemyInRange(detectionRange).transform;
                if (enemy != null)
                {
                    Attack(enemy.transform);
                }
            }
        }
    }

    public virtual void RotateToEnemy()
    {
        EntitySkeleton enemy = GetClosestEnemyInRange(detectionRange);
        if (enemy != null)
        {
            Vector2 dirVec = (enemy.transform.position - transform.position).normalized;
            float angle = -GameManager.AngleFromVector(dirVec) - 180;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed);
        }

    }

    public virtual void Update()
    {
        UpdateShotTimer();
        RotateToEnemy();
    }

    public virtual void Start()
    {

    }
}
