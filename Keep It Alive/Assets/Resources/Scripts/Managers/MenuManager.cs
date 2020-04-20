using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public int viewIndex = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }


    public void QuitApplication()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SwitchMenu(int index)
    {
        Transform menuCanvas = GameObject.FindGameObjectWithTag("MenuCanvas").transform;

        foreach (Transform t in menuCanvas)
        {
            if (t.name != "Backdrop")
            {
                t.GetComponent<CanvasGroup>().alpha = 0;
                t.GetComponent<CanvasGroup>().interactable = false;
                t.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }

        this.viewIndex = index;

        Transform target = null;
        switch (index)
        {
            case 0:
                target = menuCanvas.Find("MainMenu");
                break;
            case 1:
                target = menuCanvas.Find("HowToPlayMenu");
                break;
            case 2:
                target = menuCanvas.Find("OptionsMenu");
                break;
        }
        target.GetComponent<CanvasGroup>().blocksRaycasts = true;
        target.GetComponent<CanvasGroup>().interactable = true;
        target.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void HowToPlayMenuOpen()
    {
        SwitchMenu(1);
        Transform menuCanvas = GameObject.FindGameObjectWithTag("MenuCanvas").transform;
        menuCanvas.Find("HowToPlayMenu").GetComponent<HowToPlayMenu>().index = 1;
    }

    public void OptionsMenuOpen()
    {
        SwitchMenu(2);
    }

    public void MainMenuOpen()
    {
        SwitchMenu(0);
    }

}
