using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntitySkeleton : Entity
{

    public float targetRange = 6f;
    public float speed = 1f;
    public float stunTime = 0;
    public float attackTime = 0;
    public float attackInterval = 0.5f;
    public float attackDamage = 1;

    public float aggroAddition = 2f;
    public float aggroTime = 0;
    Rigidbody2D rb;

    public int sourceWave = 0;

    public bool contacted = false;
    public Entity contactedPlayer;

    public override void Start()
    {
        base.Start();
        SetDefaultVariables();
    }

    void FixedUpdate()
    {
        UpdateSkeletonAI();
    }

    void Update()
    {
        UpdateStunTime();
        UpdateAttackTime();
        UpdateAggroTime();
    }

    Transform GetClosestSoldier(SoldierController[] soldiers)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (SoldierController t in soldiers)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin;
    }

    void SetDefaultVariables()
    {
        maxHealth = 5 + (5 * (GameObject.FindObjectOfType<SpawnPoint>().waveIndex * 0.1f));
        health = 5 + (5 * (GameObject.FindObjectOfType<SpawnPoint>().waveIndex * 0.1f));
        speed = 3.5f;
        rb = GetComponent<Rigidbody2D>();
    }

    void UpdateAttackTime()
    {
        if (attackTime > 0)
        {
            attackTime -= Time.deltaTime;
        }
        if (contacted)
        {
            if (attackTime <= 0)
            {
                if (contactedPlayer != null)
                {
                    contactedPlayer.Hurt(new Damage(attackDamage, gameObject));
                }
                attackTime = attackInterval;
            }
        }
    }

    void UpdateAggroTime()
    {
        if (aggroTime > 0)
        {
            aggroTime -= Time.deltaTime;
        }
    }

    void UpdateStunTime()
    {
        if (stunTime > 0)
        {
            stunTime -= Time.deltaTime;
        }
    }

    protected virtual void UpdateSkeletonAI()
    {
        Transform closestSoldier = GetClosestSoldier(GameObject.FindObjectsOfType<SoldierController>());

        if (!contacted && stunTime <= 0)
        {

            if (Vector2.Distance(transform.position, closestSoldier.position) < targetRange)
            {
                aggroTime = aggroAddition;
            }
        }

        if (aggroTime > 0)
        {
            Vector2 moveDir = (closestSoldier.position - transform.position).normalized;
            rb.AddForce(moveDir * 50 * speed, ForceMode2D.Impulse);
        }
        else
        {
            FollowPath();
        }
    }

    public override void Die(bool audio = true, bool dropMoney = true)
    {
        base.Die(audio, dropMoney);
        if (dropMoney)
            DropMoney(Random.Range(2, 4), (int)Mathf.Ceil(6 * (SoldierManager.instance.incomePercentage / 100)), (int)Mathf.Ceil(25 * (SoldierManager.instance.incomePercentage / 100)));
    }

    public virtual void DropMoney(int drops, int amountMin, int amountMax)
    {
        for (int i = 0; i < drops; i++)
        {
            GameObject coin = Instantiate(Resources.Load<GameObject>("Prefabs/Objects/Pickups/Coin"), transform.position, Quaternion.identity);
            coin.GetComponent<Coin>().value = Random.Range(amountMin, amountMax);
            coin.GetComponent<Coin>().targetLocation = transform.position + new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);
        }
    }

    public override void Hurt(Damage source)
    {
        base.Hurt(source);
        this.stunTime = SoldierManager.instance.stunTime;
        Camera.main.transform.GetComponent<CameraScript>().FreezeFrame(0.025f);
        Camera.main.transform.GetComponent<CameraScript>().Shake();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("PlayerAttack") && stunTime <= 0)
        {
            Hurt(new Damage(other.GetComponent<Slash>() != null ? other.GetComponent<Slash>().damage : SoldierManager.instance.attackDamage, SoldierManager.instance.currentSoldier.gameObject));
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.GetComponent<EntityKing>() != null || other.transform.GetComponent<SoldierController>() != null)
        {
            contacted = true;
            contactedPlayer = other.transform.GetComponent<Entity>();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.GetComponent<EntityKing>() != null || other.transform.GetComponent<SoldierController>() != null)
        {
            contacted = false;
            contactedPlayer = null;
        }
    }

    public override void FollowPath()
    {
        base.FollowPath();
        Vector2 moveDir = (nextFollowPoint - (Vector2)transform.position).normalized;
        rb.AddForce(moveDir * 50 * speed, ForceMode2D.Impulse);
    }
}
