using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class pgj_InventoryController : MonoBehaviour
{
    public GameObject contents;
    

    private List<GameObject> prefabs;

    private int selectedIndex;
    private bool isInit;
    private bool isChange;
    private GameObject click;
    private GameObject oldClick;
    private List<InventoryInfo> invenlist;
    private Dictionary<int, ItemInfo> itemlist;




    // Start is called before the first frame update
    void Start()
    {
        //InitInven();
        isInit = false;
        isChange = false;
        prefabs = new List<GameObject>();
        invenlist = new List<InventoryInfo>();
        itemlist = pgj_ItemManager.INSTANCE.GetAllItems(); //key = item.id
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf == true && !isInit)
        {
            InitInven();
            isInit = true;
        }


        if (Input.GetMouseButtonDown(0))
        {
            click = EventSystem.current.currentSelectedGameObject;
            if (click != null)
            {
                if (click.transform.parent.Equals(contents.transform))
                {
                    //Debug.Log(click.transform.name + '3');
                    //Debug.Log(click.transform.localPosition.x);
                    //Debug.Log(click.transform.localPosition.y);

                    float x = (click.transform.localPosition.x - 35) / 65;
                    float y = (click.transform.localPosition.y + 35) / 65;

                    selectedIndex = (int)(x - y * 5);
                    Debug.Log(selectedIndex);
                }
            }

        }

        oldClick = click;

    }

    private void InitInven()
    {

        invenlist = pgj_InventoryManager.INSTANCE.GetAllItems();

        Debug.Log("2222" + invenlist.Count);


        string path;
        int id;

        for (int index = 0; index < invenlist.Count; index++)
        {
            id = invenlist[index].ID;
            path = "Basic RPG Item Free/" + itemlist[id].NAME;
            Debug.Log(path);
            prefabs.Add(Instantiate(Resources.Load(path, typeof(GameObject)), contents.transform) as GameObject);
        }
    }

    private void ChangeInven()
    {
        for (int i = 0; i < prefabs.Count; i++)
            Destroy(prefabs[i]);
        invenlist = pgj_InventoryManager.INSTANCE.GetAllItems();

        Debug.Log("invenlist = " + invenlist.Count);
        string path;
        int id;
        for (int index = 0; index < invenlist.Count; index++)
        {
            id = invenlist[index].ID;
            path = "Basic RPG Item Free/" + itemlist[id].NAME;
            prefabs.Add(Instantiate(Resources.Load(path, typeof(GameObject)), contents.transform) as GameObject);
        }
    }

    public void Sell()
    {
        Debug.Log("sellitem" + selectedIndex);

        int id = invenlist[selectedIndex].ID;

        lji_statusManager.instance.changeGold(itemlist[id].SELL_COST);//µ· º¯È­

        pgj_InventoryManager.INSTANCE.deleteItem(selectedIndex);
        ChangeInven();
    }


    public void store1Buy()
    {
        int id = pgj_StoreManager.INSTANCE.getSellID();
        Debug.Log("store1 buy" + id);

        if (id > 0)
        {
            InventoryInfo temp = new InventoryInfo();
            temp.ID = itemlist[id].ID;
            temp.ITEM_RANK = id / 100;
            pgj_InventoryManager.INSTANCE.AddItem(temp);
            ChangeInven();
        }
    }
    public void store2Buy()
    {
        int id = pgj_Store2Manager.INSTANCE.getSellID();
        Debug.Log("store2 buy" + id);

        if (id > 0)
        {
            InventoryInfo temp = new InventoryInfo();
            temp.ID = itemlist[id].ID;
            temp.ITEM_RANK = id / 100;
            pgj_InventoryManager.INSTANCE.AddItem(temp);
            ChangeInven();
        }
    }

    public void Worn()
    {
        int id = invenlist[selectedIndex].ID;
        pgj_InventoryManager.INSTANCE.setWornID(id);

        int rw = (id % 100) / 10;
        int lw = id % 10;
        int wearable = id / 100;

        if (id != 0 && wearable != 5)
        {
            Debug.Log(id + "  lw = " + lw + ", rw = " + rw +"  "+ wearable);
            switch (wearable)
            {
                case 0:
                    lji_statusManager.instance.changeW(rw, lw);
                    lji_statusManager.instance.changeTier(0);
                    pgj_Inventory2Manager.INSTANCE.deleteItem(3, id, 0);
                    break;
                case 1:
                    lji_statusManager.instance.changeW(rw, lw);
                    lji_statusManager.instance.changeTier(1);
                    pgj_Inventory2Manager.INSTANCE.deleteItem(3, id, 1);
                    break;
                case 2:
                    lji_statusManager.instance.changeW(rw, lw);
                    lji_statusManager.instance.changeTier(2);
                    pgj_Inventory2Manager.INSTANCE.deleteItem(3, id, 2);
                    break;
                case 3:
                    lji_statusManager.instance.changeW(rw, lw);
                    lji_statusManager.instance.changeTier(3);
                    pgj_Inventory2Manager.INSTANCE.deleteItem(3, id, 3);
                    break;
                case 4:
                    if (rw == 4)
                    {
                        lji_statusManager.instance.changeHead(lw);
                        pgj_Inventory2Manager.INSTANCE.deleteItem(0, id, 0);
                    }
                    else if (rw == 5)
                    {
                        lji_statusManager.instance.changeUArmor(lw);
                        pgj_Inventory2Manager.INSTANCE.deleteItem(1, id, 0);
                    }
                    else if (rw == 6)
                    {
                        lji_statusManager.instance.changeLArmor(lw);
                        pgj_Inventory2Manager.INSTANCE.deleteItem(2, id, 0);
                    }
                    break;
                default: break;
            }
            pgj_InventoryManager.INSTANCE.deleteItem(selectedIndex);
            ChangeInven();
        }

        
    }
}
