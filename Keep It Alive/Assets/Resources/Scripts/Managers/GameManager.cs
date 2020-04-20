using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Vector2 cursorPos;

    public Texture2D[] mouseCursors;


    public List<Upgrade> currentUpgradeOptions = new List<Upgrade>();
    public int upgradeOptions = 3;

    public Item[] itemOptions = new Item[3];


    private void Awake()
    {
        Singleton();
        SetDefaultCursor();
        Camera.main.GetComponent<CameraScript>().game = true;
    }

    private void Start()
    {
        RefreshUpgrades(1);
        SetShopItems();
    }

    void Update()
    {
        UpdateMouseCursorPos();
        UpdateControls();
    }


    private void Singleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    private void UpdateMouseCursorPos()
    {
        cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void SetShopItems()
    {
        itemOptions[0] = ItemDatabase.GetItem("tower_crossbow");
        itemOptions[1] = ItemDatabase.GetItem("tower_knockback");
        itemOptions[2] = ItemDatabase.GetItem("tower_knockback");
    }


    private void UpdateControls()
    {
        //Attack
        if (Input.GetButtonDown("Fire1"))
        {
            SoldierManager.instance.currentSoldier.Attack();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            ItemDatabase.SpawnDrop(ItemDatabase.instance.itemDatabase[Random.Range(0, ItemDatabase.instance.itemDatabase.Count)], cursorPos);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            SoldierManager.instance.money = 10000;
        }
        if (Input.GetButtonDown("Use"))
        {
            if (SoldierManager.instance.currentItem != null && !string.IsNullOrEmpty(SoldierManager.instance.currentItem.ID))
            {
                SoldierManager.instance.currentItem.OnItemUse();
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameObject.FindGameObjectWithTag("HUDCanvas").GetComponent<HUDCanvas>().ToggleShop();
        }
    }

    private void SetDefaultCursor()
    {
        Cursor.SetCursor(mouseCursors[0], new Vector2(mouseCursors[0].width / 2, mouseCursors[0].height / 2), CursorMode.Auto);
    }

    public void RefreshUpgrades(int level)
    {
        currentUpgradeOptions.Clear();
        for (int i = 0; i < upgradeOptions; i++)
        {
            Upgrade result = null;
            List<Upgrade> possibleUpgrades = UpgradeManager.instance.upgrades.FindAll(x => x.id.Contains(level.ToString()));
            int rand = Random.Range(0, possibleUpgrades.Count);
            result = possibleUpgrades[rand];
            currentUpgradeOptions.Add(result);
        }
    }

    public static float AngleFromVector(Vector2 p_vector2)
    {
        if (p_vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
        }
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

}
