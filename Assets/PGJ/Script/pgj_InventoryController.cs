using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class pgj_InventoryController : MonoBehaviour
{
    public GameObject contents;
    public Text showInfo;
    

    private List<GameObject> prefabs;

    private int selectedIndex;
    private bool isInit;
    private bool preInit;
    private GameObject click;
    private List<InventoryInfo> invenlist;
    private Dictionary<int, ItemInfo> itemlist;




    // Start is called before the first frame update
    void Start()
    {
        //InitInven();
        isInit = false;
        preInit = false;
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

                    int id = invenlist[selectedIndex].ID;
                    if (id != 0)
                    {
                        string info = "내 아이템 정보\nBuy cost: " + itemlist[id].BUY_COST + "\nSell cost: " + itemlist[id].SELL_COST;
                        showInfo.text = info;
                    }
                }
            }

        }
    }

    private void LateUpdate()
    {
        if (!preInit)
            if (pgj_invenSceneManager.INSTANCE.needInven1Inint)
            {

                Debug.Log("---------------------------------------------");
                initInstanse();
                preInit = !preInit;
            }
    }

    private void OnDestroy()
    {
        for (int index = 0; index < 50; index++)
            pgj_invenSceneManager.INSTANCE.inventoryID[index] = invenlist[index].ID;
    }

    private void initInstanse() 
    {
        InventoryInfo temp;

        pgj_InventoryManager.INSTANCE.deleteAllItems();

        for (int index = 0; index < 50; index++)
        {
            temp = new InventoryInfo();
            temp.ID = pgj_invenSceneManager.INSTANCE.inventoryID[index];
            temp.ITEM_RANK = 0;
            pgj_InventoryManager.INSTANCE.AddItem(temp);
        }
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

        lji_statusManager.instance.changeGold(itemlist[id].SELL_COST);//돈 변화

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

            //장비창에 있던 아이템 불러오기
            id = pgj_Inventory2Manager.INSTANCE.GetIoldid();

            InventoryInfo temp = new InventoryInfo();
            temp.ID = itemlist[id].ID;
            temp.ITEM_RANK = id / 100;
            pgj_InventoryManager.INSTANCE.AddItem(temp);

            ChangeInven();
        }  
    }
    public void Worn2()
    {
        int id = invenlist[selectedIndex].ID;
        pgj_InventoryManager.INSTANCE.setWornID(id);

        int rw = (id % 100) / 10;
        int lw = id % 10;
        int wearable = id / 100;

        if (id != 0 && wearable != 5)
        {
            Debug.Log(id + " SUB  lw = " + lw + ", rw = " + rw + "  " + wearable);
            switch (wearable)
            {
                case 0:
                    Worn2f(id, rw, lw, wearable, 4, 0);
                    break;
                case 1:
                    Worn2f(id, rw, lw, wearable, 4, 1);
                    break;
                case 2:
                    Worn2f(id, rw, lw, wearable, 4, 2);
                    break;
                case 3:
                    Worn2f(id, rw, lw, wearable, 4, 3);
                    break;
                default: break;
            }
        }
    }

    private void Worn2f(int id,int rw,int lw,int wearable, int number, int rank) 
    {
        lji_statusManager.instance.changeSubW(rw, lw);
        lji_statusManager.instance.changeSubTier(rank);
        pgj_Inventory2Manager.INSTANCE.deleteItem2(number, id, rank);
        pgj_InventoryManager.INSTANCE.deleteItem(selectedIndex);


        //장비창에 있던 아이템 불러오기
        id = pgj_Inventory2Manager.INSTANCE.GetIoldid2();

        InventoryInfo temp = new InventoryInfo();
        temp.ID = itemlist[id].ID;
        temp.ITEM_RANK = id / 100;
        pgj_InventoryManager.INSTANCE.AddItem(temp);

        ChangeInven();
    }
}
