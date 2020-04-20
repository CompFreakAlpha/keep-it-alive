using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraScript : MonoBehaviour
{

    public bool frozen = false;

    public bool game = false;
    void Start()
    {
    }

    void Update()
    {
        UpdatePosition();
    }

    IEnumerator FreezeCam(float sec)
    {
        frozen = true;
        var original = Time.timeScale;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(sec);
        if (!GameObject.FindGameObjectWithTag("HUDCanvas").GetComponent<HUDCanvas>().upgradePanelOpened)
        {
            Time.timeScale = original;
        }

        frozen = false;
    }

    public void FreezeFrame(float sec)
    {
        if (!frozen)
        {
            StartCoroutine(FreezeCam(sec));
        }
    }

    void UpdatePosition()
    {
        if (game)
        {
            Transform player = SoldierManager.instance.currentSoldier.transform;

            Vector2 playerPos = player.position;
            Vector2 mousePos = GameManager.instance.cursorPos;

            Vector2 targetPos = Vector2.Lerp(playerPos, mousePos, 0.25f);

            targetPos.x = Mathf.Clamp(targetPos.x, -2, 2);
            targetPos.y = Mathf.Clamp(targetPos.y, -36, 0);

            transform.DOMove(new Vector3(targetPos.x, targetPos.y, -10), 0.25f);
        }

    }

    public void Shake()
    {
        transform.DOShakePosition(0.25f, 1, 10, 90);
    }

    public void AberrationPulse()
    {
        // StartCoroutine(AberrPulse());
    }

    // private IEnumerator AberrPulse()
    // {
    //     Resources.Load<Volume>("Scenes/Game/Main Camera Profile").chromaticAberration.intensity += 5;
    // }
}
