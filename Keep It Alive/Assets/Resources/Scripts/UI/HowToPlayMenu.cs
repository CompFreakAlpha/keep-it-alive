using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayMenu : MonoBehaviour
{
    public int index = 1;

    void Update()
    {
        if (MenuManager.instance.viewIndex == 1)
        {
            if (Input.anyKeyDown)
            {
                NextImage();
            }

            for (int i = 1; i < transform.childCount; i++)
            {
                if (i == index)
                {
                    transform.GetChild(i).GetComponent<CanvasGroup>().alpha = 1;
                }
                else
                {
                    transform.GetChild(i).GetComponent<CanvasGroup>().alpha = 0;
                }
            }
        }
    }

    public void NextImage()
    {
        if (index < transform.childCount - 1)
        {
            index++;
        }
        else
        {
            MenuManager.instance.SwitchMenu(0);
        }
    }
}
