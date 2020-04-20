using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 5;

    public Vector2 targetLocation;
    public Vector2 dirVec;

    public bool chase = false;

    void Start()
    {
        if (targetLocation == Vector2.zero)
        {
            targetLocation = transform.position;
        }
    }

    void FixedUpdate()
    {
        Transform player = SoldierManager.instance.currentSoldier.transform;
        GetComponent<Rigidbody2D>().AddForce(4 * dirVec * (5 - Vector2.Distance(player.position, transform.position)), ForceMode2D.Impulse);
        if (!chase)
        {
            GetComponent<Rigidbody2D>().AddForce((targetLocation - (Vector2)transform.position).normalized * 10, ForceMode2D.Impulse);
        }
    }

    public void Update()
    {
        Transform player = SoldierManager.instance.currentSoldier.transform;
        if (Vector2.Distance(player.position, transform.position) < 5)
        {
            chase = true;
            dirVec = (player.position - transform.position).normalized;
        }
        else
        {
            dirVec = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<SoldierController>() != null && other.GetComponent<SoldierController>() == SoldierManager.instance.currentSoldier)
        {
            SoldierManager.instance.money += value;
            Camera.main.GetComponent<CameraScript>().AberrationPulse();
            AudioManager.instance.Play("coin_pickup");
            Destroy(gameObject);
        }
    }
}
