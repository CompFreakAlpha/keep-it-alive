using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{

    // NEXT TIME MAKE A GetItemStat()

    public static ItemDatabase instance;

    public List<Item> itemDatabase = new List<Item>();

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        PreInitializeItemDatabase();
    }


    public static Item GetItem(string _ID)
    {
        if (ItemDatabase.instance.itemDatabase.Find(x => x.ID == _ID) != null)
        {
            return ItemDatabase.instance.itemDatabase.Find(x => x.ID == _ID);
        }
        else
        {
            ThrowItemDatabaseError("Item {" + _ID + "} doesn't exist!");
            return null;
        }
    }

    public static void ThrowItemDatabaseError(string _error)
    {

        Debug.Log("\n==// ITEMDATABASE ERROR \\\\==\n# " + _error + "\n==\\\\ ITEMDATABASE ERROR //==\n");

    }

    public Item AddItem(Item _item)
    {

        itemDatabase.Add(_item);
        Debug.Log(itemDatabase.Count + ": Item {" + _item.ID + "} initiliazed");
        return _item;
    }


    public void PostInitializeItemDatabase()
    {

        Debug.Log("==Initialization of [" + itemDatabase.Count + "] items complete==");
        Debug.Log("==\\\\ ITEM INIT //==");

    }



    public void InitializeItemDatabase()
    {


        // ======ITEM INIT======

        AddItem(new ItemTowerCrossbow("tower_crossbow", "Crossbow Tower", "Defends a small range", 1200));
        AddItem(new ItemTowerKnockback("tower_knockback", "Knockback Tower", "Knocks enemies back from it", 2000, 3));

        // ======END ITEM INIT======

        PostInitializeItemDatabase();
    }

    public void PreInitializeItemDatabase()
    {

        Debug.Log("==// ITEM INIT \\\\==");



        InitializeItemDatabase();
    }

    public static void SpawnDrop(Item _data, Vector2 _position)
    {
        GameObject g = Instantiate(Resources.Load<GameObject>("Prefabs/Objects/GroundItem"), _position, Quaternion.identity);
        g.GetComponent<GroundItem>().itemData = _data;
    }
}

