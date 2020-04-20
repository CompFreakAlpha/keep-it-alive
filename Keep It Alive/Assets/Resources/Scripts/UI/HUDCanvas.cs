using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class HUDCanvas : MonoBehaviour
{

    public int selectedUpgradeIndex = 0;

    public bool chosenUpgrade = true;
    public bool upgradePanelOpened = false;

    public bool shop = false;
    public bool openshop = false;

    void Start()
    {

    }

    public void OnKingDeath()
    {
        StartCoroutine(KingDeath());
    }

    IEnumerator KingDeath()
    {
        Time.timeScale = 0;
        transform.Find("YouDied").GetComponent<RectTransform>().DOLocalMoveY(0, 0.5f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(4);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }


    void Update()
    {
        UpdateMoney();
        UpdateInteractivity();
        UpdateUpgradeOptions();
        UpdateItemSlot();
    }


    public void ToggleShop()
    {
        if (!upgradePanelOpened)
        {
            openshop = !openshop;
            if (openshop == true)
            {
                OpenUpgradePanel(true);
            }
            else
            {
                CloseUpgradePanel(true);
            }
        }
    }

    void UpdateMoney()
    {
        Transform money = transform.Find("Money");
        Transform mIcon = money.Find("MoneyIcon");
        Transform mAmount = money.Find("MoneyAmount");

        mAmount.GetComponent<TextMeshProUGUI>().text = SoldierManager.instance.money.ToString();
    }

    void UpdateInteractivity()
    {
        Transform upgradePanel = transform.Find("Upgrades");

        if (upgradePanel.GetComponent<CanvasGroup>().interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (selectedUpgradeIndex != GameManager.instance.upgradeOptions - 1)
                {
                    selectedUpgradeIndex += 1;

                }
                else
                {
                    selectedUpgradeIndex = 0;
                }
                AudioManager.instance.Play("menu_select");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (selectedUpgradeIndex != 0)
                {
                    selectedUpgradeIndex -= 1;
                }
                else
                {
                    selectedUpgradeIndex = GameManager.instance.upgradeOptions - 1;
                }
                AudioManager.instance.Play("menu_select");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!shop)
                {
                    SoldierManager.instance.AddSoldierUpgrade(GameManager.instance.currentUpgradeOptions[selectedUpgradeIndex]);
                    AudioManager.instance.Play("upgrade_select");
                    SoldierManager.instance.currentSoldier.health = SoldierManager.instance.currentSoldier.maxHealth;
                    CloseUpgradePanel(false);
                }
                else
                {
                    if (SoldierManager.instance.money >= GameManager.instance.itemOptions[selectedUpgradeIndex].value)
                    {
                        SoldierManager.instance.currentItem = (GameManager.instance.itemOptions[selectedUpgradeIndex]);
                        SoldierManager.instance.ChangeMoney(-GameManager.instance.itemOptions[selectedUpgradeIndex].value);
                        AudioManager.instance.Play("upgrade_select");
                        CloseUpgradePanel(true);
                    }

                }

            }

        }

    }

    public void KingSpeech(string text)
    {
        StartCoroutine(KingType(text));
    }

    IEnumerator KingType(string text)
    {
        Transform ks = transform.Find("KingWords");
        ks.GetComponent<CanvasGroup>().DOFade(1, 0.1f);

        ks.GetComponent<TextMeshProUGUI>().text = "King: " + text;
        for (int i = 5; i < text.Length + 7; i++)
        {
            AudioManager.instance.Play("king_speech");
            ks.GetComponent<TextMeshProUGUI>().maxVisibleCharacters = i;
            yield return new WaitForSeconds(0.025f);
        }

        yield return new WaitForSecondsRealtime(2);

        ks.GetComponent<CanvasGroup>().DOFade(0, 0.25f);
    }


    void OpenUpgradePanel(bool isShop)
    {
        if (!isShop)
        {
            upgradePanelOpened = true;
            chosenUpgrade = false;
        }
        else
        {
            shop = true;
        }
        Transform upgradePanel = transform.Find("Upgrades");
        Time.timeScale = 0;
        upgradePanel.GetComponent<CanvasGroup>().interactable = true;
        upgradePanel.GetComponent<CanvasGroup>().DOFade(1, 0.5f).SetUpdate(true);
        transform.Find("Selector").GetComponent<Image>().DOFade(1, 0.5f).SetUpdate(true);
        upgradePanel.DOScale(new Vector3(1, 1, 1), 0.75f).SetEase(Ease.OutBounce).SetUpdate(true);

    }

    void CloseUpgradePanel(bool isShop)
    {
        Transform upgradePanel = transform.Find("Upgrades");

        upgradePanel.GetComponent<CanvasGroup>().interactable = false;
        upgradePanel.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetUpdate(true).OnComplete(UpgradePanelClosed);
        transform.Find("Selector").GetComponent<Image>().DOFade(0, 0.5f).SetUpdate(true);
        upgradePanel.DOScale(new Vector3(0, 0, 0), 0.75f).SetEase(Ease.InBounce).SetUpdate(true);
        Time.timeScale = 1;
        if (!isShop)
        {
            chosenUpgrade = true;
            upgradePanelOpened = false;
            shop = false;
        }

    }

    void UpgradePanelClosed()
    {
        shop = false;
    }

    void UpdateUpgradeOptions()
    {
        Transform upgradePanel = transform.Find("Upgrades");


        if (!shop)
        {
            if (GameObject.FindObjectOfType<SpawnPoint>() != null && chosenUpgrade == false && upgradePanelOpened == false)
            {
                OpenUpgradePanel(false);
            }

            if (upgradePanel.childCount > GameManager.instance.upgradeOptions)
            {
                for (int i = 0; i < upgradePanel.childCount - GameManager.instance.upgradeOptions; i++)
                {
                    Destroy(upgradePanel.GetChild(i));
                }
            }
            else if (upgradePanel.childCount < GameManager.instance.upgradeOptions)
            {
                for (int i = 0; i < GameManager.instance.upgradeOptions - upgradePanel.childCount; i++)
                {
                    GameObject upgradeItem = Instantiate(Resources.Load<GameObject>("Prefabs/UI/UpgradeOption"), upgradePanel);
                }
            }

            for (int i = 0; i < upgradePanel.childCount; i++)
            {
                upgradePanel.GetChild(i).Find("UpgradeIcon").GetComponent<Image>().sprite = GameManager.instance.currentUpgradeOptions[i].icon;
                upgradePanel.GetChild(i).Find("Description").GetComponent<TextMeshProUGUI>().text = GameManager.instance.currentUpgradeOptions[i].name + "\n" + GameManager.instance.currentUpgradeOptions[i].description;
            }

            if (upgradePanel.GetComponent<CanvasGroup>().interactable == true)
            {
                Transform selector = transform.Find("Selector");

                selector.position = upgradePanel.GetChild(selectedUpgradeIndex).transform.position;

                for (int i = 0; i < GameManager.instance.upgradeOptions; i++)
                {
                    if (i == selectedUpgradeIndex)
                    {
                        upgradePanel.GetChild(selectedUpgradeIndex).Find("Description").GetComponent<TextMeshProUGUI>().color = Color.white;
                        upgradePanel.GetChild(selectedUpgradeIndex).Find("Cost").GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0);
                    }
                    else
                    {
                        upgradePanel.GetChild(i).Find("Description").GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0);
                        upgradePanel.GetChild(selectedUpgradeIndex).Find("Cost").GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0);
                    }
                }
            }
        }
        else
        {
            //SHOP
            for (int i = 0; i < upgradePanel.childCount; i++)
            {
                if (GameManager.instance.itemOptions[i] != null)
                {
                    upgradePanel.GetChild(i).Find("UpgradeIcon").GetComponent<Image>().sprite = GameManager.instance.itemOptions[i].icon;
                    upgradePanel.GetChild(i).Find("Description").GetComponent<TextMeshProUGUI>().text = GameManager.instance.itemOptions[i].name + "\n" + GameManager.instance.itemOptions[i].description;
                    upgradePanel.GetChild(i).Find("Cost").GetComponent<TextMeshProUGUI>().text = "$" + GameManager.instance.itemOptions[i].value;
                    upgradePanel.GetChild(selectedUpgradeIndex).Find("Cost").GetComponent<TextMeshProUGUI>().color = Color.white;
                }
            }

            if (upgradePanel.GetComponent<CanvasGroup>().interactable == true)
            {
                Transform selector = transform.Find("Selector");

                selector.position = upgradePanel.GetChild(selectedUpgradeIndex).transform.position;

                for (int i = 0; i < GameManager.instance.upgradeOptions; i++)
                {
                    if (i == selectedUpgradeIndex)
                    {
                        upgradePanel.GetChild(selectedUpgradeIndex).Find("Description").GetComponent<TextMeshProUGUI>().color = Color.white;
                    }
                    else
                    {
                        upgradePanel.GetChild(i).Find("Description").GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0);
                    }
                }
            }

        }




    }


    void UpdateItemSlot()
    {
        Transform iSlot = transform.Find("ItemHUD").Find("ItemSlot");
        Transform icon = transform.Find("ItemHUD").Find("ItemSlot").Find("Icon");
        if (SoldierManager.instance.currentItem != null && !string.IsNullOrEmpty(SoldierManager.instance.currentItem.ID))
        {
            icon.GetComponent<Image>().sprite = SoldierManager.instance.currentItem.icon;
            icon.GetComponent<Image>().color = Color.white;
        }
        else
        {
            icon.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }


    }

}
