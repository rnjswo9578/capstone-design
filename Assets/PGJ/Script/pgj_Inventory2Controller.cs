using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class pgj_Inventory2Controller : MonoBehaviour
{
    //public ScrollRect scroll;
    public GameObject contents;

    public RawImage[] imgs;
    private int contentsLen;
    private int selectedIndex;
    private bool isInit;
    private bool preInit;

    private int oldID;
    private int newID;
    private int oldID2;
    private int newID2;

    private GameObject click;
    private GameObject oldClick;
    private List<Inventory2Info> invenlist;
    private Dictionary<int, ItemInfo> itemlist;




    // Start is called before the first frame update
    void Start()
    {
        // List<InventoryInfo> list = pgj_InventoryManager.INSTANCE.GetAllItems();

        //InitInven();
        isInit = false;
        preInit = false;
        invenlist = new List<Inventory2Info>();
        itemlist = pgj_ItemManager.INSTANCE.GetAllItems(); //key = item.id


        newID = pgj_Inventory2Manager.INSTANCE.getWornID();
        oldID = newID;

        newID2 = pgj_Inventory2Manager.INSTANCE.getWornID2();
        oldID2 = newID2;

        contentsLen = contents.transform.childCount;
        Debug.Log(contentsLen);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf == true && !isInit)
        {
            InitInven();
            isInit = true;
        }



        oldClick = click;

        newID = pgj_Inventory2Manager.INSTANCE.getWornID();
        if (oldID != newID)
        {
            Worn();
        }

        newID2 = pgj_Inventory2Manager.INSTANCE.getWornID2();
        if (oldID2 != newID2)
        {
            Worn2();
        }
    }
    private void LateUpdate()
    {
        if (!preInit)
            if (pgj_invenSceneManager.INSTANCE.needInven2Inint)
            {
                preInit = !preInit;
                Debug.Log("22---------------------------------------------");
                initInstanse();

            }
    }

    private void OnDestroy()
    {
        for (int index = 0; index < 5; index++)
            pgj_invenSceneManager.INSTANCE.inventory2ID[index] = invenlist[index].ID;

    }

    private void InitInven()
    {

        invenlist = pgj_Inventory2Manager.INSTANCE.GetAllItems();

        //Debug.Log("2222" + invenlist.Count);


        string path;
        int id;

        for (int index = 0; index < imgs.Length; index++)
        {
            id = invenlist[index].ID;
            path = "Basic RPG Item Free/img/" + itemlist[id].ICON;
            Debug.Log(path);
            imgs[index].texture = Resources.Load(path) as Texture2D;
        }
    }
    private void initInstanse()
    {

        pgj_Inventory2Manager.INSTANCE.deleteAllItems();

        for (int index = 0; index < 5; index++)
        {
            Inventory2Info temp = new Inventory2Info();
            temp.ID = pgj_invenSceneManager.INSTANCE.inventory2ID[index];
            Debug.Log("preAdd: " + temp.ID);
            temp.ITEM_RANK = 0;
            pgj_Inventory2Manager.INSTANCE.AddItem(temp);
        }
        ChangeInven();
    }
    public void ChangeInven()
    {
        invenlist = pgj_Inventory2Manager.INSTANCE.GetAllItems();

        for (int index = 0; index < 5; index++)
        {
            string path = "Basic RPG Item Free/img/" + itemlist[invenlist[index].ID].ICON;
            Debug.Log(path);
            imgs[3].texture = Resources.Load(path) as Texture2D;
        }
    }


    public void Worn()
    {
        int id = newID;
        Debug.Log("worn cid = " + id + "oldid = " + oldID);
        oldID = newID;

        int rw = (id % 100) / 10;
        int lw = id % 10;
        int wearable = id / 100;
        Debug.Log("worn rw = " + rw + "lw = " + lw + "wearable = " + wearable);

        if (id != 0 && wearable != 5)
        {
            Debug.Log(id + "  lw = " + lw + ", rw = " + rw + "  " + wearable);
            switch (wearable)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    //무기바꾸기3
                    string path = "Basic RPG Item Free/img/" + itemlist[id].ICON;
                    imgs[3].texture = Resources.Load(path) as Texture2D;
                    break;
                case 4:
                    if (rw == 4)
                    {
                        path = "Basic RPG Item Free/img/" + itemlist[id].ICON;
                        imgs[0].texture = Resources.Load(path) as Texture2D;
                    }
                    else if (rw == 5)
                    {
                        //1
                        path = "Basic RPG Item Free/img/" + itemlist[id].ICON;
                        imgs[1].texture = Resources.Load(path) as Texture2D;
                    }
                    else if (rw == 6)
                    {
                        //2
                        path = "Basic RPG Item Free/img/" + itemlist[id].ICON;
                        imgs[2].texture = Resources.Load(path) as Texture2D;
                    }
                    break;
                default: break;
            }
            //ChangeInven();
        }
    }
    public void Worn2()
    {
        int id = newID2;
        Debug.Log("worn cid = " + id + "oldid = " + oldID2);
        oldID2 = newID2;

        int rw = (id % 100) / 10;
        int lw = id % 10;
        int wearable = id / 100;
        Debug.Log("worn rw = " + rw + "lw = " + lw + "wearable = " + wearable);

        if (id != 0 && wearable != 5)
        {
            Debug.Log(id + "  lw = " + lw + ", rw = " + rw + "  " + wearable);
            switch (wearable)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    //무기바꾸기3
                    string path = "Basic RPG Item Free/img/" + itemlist[id].ICON;
                    imgs[4].texture = Resources.Load(path) as Texture2D;
                    break;
                default: break;
            }
            //ChangeInven();
        }
    }
}