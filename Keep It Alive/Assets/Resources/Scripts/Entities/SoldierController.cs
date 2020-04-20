using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class SoldierController : Entity
{

    Rigidbody2D rb;
    Animator anim;

    private float moveX, moveY;
    private float inputX, inputY;
    public bool moving;

    public bool slashing = false;
    public float slashDelay = 0;
    public float timeBtwnSlashes = 0.5f;

    public float speed = 1;
    public int slashCounter = 0;
    public bool activeSoldier = false;

    public override void Start()
    {
        base.Start();
        SetDefaultVariables();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentSoldier();
        UpdateMovement();
        UpdateAnimations();
        UpdateHeldWeapon();
        UpdateSlashDelay();
    }



    private void UpdateSlashDelay()
    {
        if (slashDelay > 0)
        {
            slashDelay -= Time.deltaTime;
        }
    }

    private void UpdateCurrentSoldier()
    {
        activeSoldier = SoldierManager.instance.currentSoldier == this;
    }

    private void SetDefaultVariables()
    {
        moveY = -1;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public override void Die(bool audio = true, bool dropMoney = false)
    {
        GameObject soldier = Instantiate(Resources.Load<GameObject>("Prefabs/Objects/SoldierA"), new Vector2(-3, 5.5f), Quaternion.identity);
        SoldierManager.instance.currentSoldier = soldier.GetComponent<SoldierController>();
        AudioManager.instance.Play("soldier_death");
        SoldierManager.instance.ChangeMoney(-Random.Range(75, 125));
        GameObject.FindGameObjectWithTag("HUDCanvas").GetComponent<HUDCanvas>().KingSpeech("You! My guard is dead, go replace him!");
        base.Die(false);
    }

    private void UpdateMovement()
    {
        if (!slashing)
        {
            if (activeSoldier)
            {
                if (Time.timeScale > 0)
                {
                    inputX = Input.GetAxisRaw("Horizontal");
                    inputY = Input.GetAxisRaw("Vertical");

                    moving = inputX != 0 || inputY != 0;

                    if (moving)
                    {
                        moveX = inputX;
                        moveY = inputY;

                        MoveSoldier(new Vector2(moveX, moveY));
                    }
                }

            }
        }


    }

    public void MoveSoldier(Vector2 dirVec)
    {
        rb.AddForce(dirVec.normalized * speed * 8, ForceMode2D.Impulse);
    }

    private void UpdateAnimations()
    {
        Vector2 mousePos = GameManager.instance.cursorPos;
        Vector2 dirVec = mousePos - (Vector2)transform.position;

        anim.SetBool("Moving", moving);

        anim.SetFloat("LookX", dirVec.x > 0 ? 1 : -1);

    }

    private void UpdateHeldWeapon()
    {
        Vector2 cursorPos = GameManager.instance.cursorPos;

        if (!slashing)
        {
            float dir = ((slashCounter % 2) * 2) - 1;

            Vector2 direction = cursorPos - (Vector2)transform.Find("HeldWeapon").position;
            direction.Normalize();

            float rot = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            Vector3 targetRot = new Vector3(0, 0, ((180 - rot) + (25 * dir)));
            transform.Find("HeldWeapon").DORotate(targetRot, 0.25f).SetEase(Ease.OutSine);
        }


        if (cursorPos.x - transform.position.x > 0.375f)
        {
            transform.Find("HeldWeapon").localPosition = new Vector2(-0.375f, 0.25f);
        }
        else if (cursorPos.x - transform.position.x < -0.375f)
        {
            transform.Find("HeldWeapon").localPosition = new Vector2(0.375f, 0.25f);
        }



    }

    public void Attack()
    {
        if (slashDelay <= 0)
        {
            slashing = true;

            Vector2 mousePos = GameManager.instance.cursorPos;
            Vector2 dirVec = mousePos - (Vector2)transform.position;
            dirVec.Normalize();

            Vector2 relativePos = (Vector2)transform.position + dirVec;
            relativePos.y += 0.25f;

            GameObject slash = Instantiate(Resources.Load<GameObject>("Prefabs/FX/Slash"), relativePos, Quaternion.identity, transform);

            float rot = Mathf.Atan2(dirVec.x, dirVec.y) * Mathf.Rad2Deg;
            slash.transform.rotation = Quaternion.Euler(0, 0, (360 - rot));

            slash.GetComponent<Animator>().SetFloat("SlashIndex", slashCounter % 2);
            slash.GetComponent<Slash>().damage = SoldierManager.instance.attackDamage;

            AudioManager.instance.Play("sword_slash");

            float dir = ((slashCounter % 2) * 2) - 1;
            float targetRotz;

            targetRotz = rot + (180 - (dir * 165));

            Vector3 targetRot = new Vector3(0, 0, -targetRotz);

            transform.Find("HeldWeapon").DORotate(targetRot, 0.1f).SetEase(Ease.OutSine).OnComplete(SlashFinished);

            slashDelay = timeBtwnSlashes;
            slashCounter++;
        }
    }

    void SlashFinished()
    {
        slashing = false;
    }

}
