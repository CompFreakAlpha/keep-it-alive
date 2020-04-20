using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityKing : Entity
{

    public float combatTimer = 0;
    public override void Start()
    {
        base.Start();
        health = 50f;
        maxHealth = 50f;
    }

    public virtual void Update()
    {
        if (combatTimer > 0)
        {
            combatTimer -= Time.deltaTime;
        }
    }

    public override GameObject AddHealthBar()
    {
        GameObject hBar = base.AddHealthBar();
        hBar.GetComponent<HealthBar>().color = Color.green;
        hBar.GetComponent<HealthBar>().sizeMultiplier = 0.5f;
        return hBar;
    }

    public override void Hurt(Damage source)
    {
        if (combatTimer > 0)
        {
            combatTimer = 3f;
        }
        else
        {
            GameObject.FindGameObjectWithTag("HUDCanvas").GetComponent<HUDCanvas>().KingSpeech("Oi! Do ya job peasant!");
            combatTimer = 3f;
        }

        base.Hurt(source);
        AudioManager.instance.Play("king_hurt");
        GetComponent<Animator>().SetTrigger("Hit");
    }

    public override void Die(bool audio = true, bool dropMoney = false)
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/FX/DeathParticles"), transform.position, Quaternion.identity);
        Destroy(transform.Find("King").gameObject);
        Destroy(this);
    }
}
