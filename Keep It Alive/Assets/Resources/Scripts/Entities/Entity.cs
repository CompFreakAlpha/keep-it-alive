using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float health = 5;
    public float maxHealth = 5;

    public bool healthBar = true;

    public Vector2 healthBarOffset;

    public Vector2 nextFollowPoint;
    public int followPointIndex;


    public virtual void Hurt(Damage source)
    {
        for (int i = 0; i < source.amount; i++)
        {
            if (health > 1)
            {
                health--;
            }
            else
            {
                health--;
                Die();
                break;
            }
        }
        TakeKnockback(source.source.GetComponent<Slash>() != null ? source.source.GetComponent<Slash>().knockback : SoldierManager.instance.attackKnockback, source.source);
    }

    public virtual void TakeKnockback(float kb, GameObject source)
    {
        if (GetComponent<Rigidbody2D>() != null)
        {
            Vector2 kbDir = (transform.position - source.transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(kbDir * kb * 250 * GetComponent<Rigidbody2D>().mass, ForceMode2D.Impulse);
        }
    }

    public virtual void Die(bool audio = true, bool dropMoney = true)
    {
        if (audio)
        {
            AudioManager.instance.Play("entity_death");
        }

        Instantiate(Resources.Load<GameObject>("Prefabs/FX/DeathParticles"), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public virtual void Start()
    {
        Spawn();
    }

    public virtual void Spawn()
    {
        AddHealthBar();
    }

    public virtual GameObject AddHealthBar()
    {
        if (healthBar)
        {
            GameObject healthbar = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Healthbar"), (Vector2)transform.position + healthBarOffset, Quaternion.identity, GameObject.FindGameObjectWithTag("WorldCanvas").transform);
            healthbar.GetComponent<HealthBar>().linkedObj = this;
            healthbar.GetComponent<HealthBar>().offset = this.healthBarOffset;
            return healthbar;
        }
        return null;
    }

    public virtual void FollowPath()
    {
        Transform path = GameObject.FindGameObjectWithTag("Path").transform;
        int i = 0;
        foreach (Transform t in path)
        {
            if (t.position.y < transform.position.y)
            {
                i++;
            }
            else
            {
                followPointIndex = i;

                Vector2 nodePos = path.GetChild(followPointIndex).transform.position;

                nextFollowPoint = new Vector2(nodePos.x + Random.Range(-2, 2), nodePos.y + 0.1f);

                break;
            }
        }
    }
}
