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

    private int oldID;
    private int newID;

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
        invenlist = new List<Inventory2Info>();
        itemlist = pgj_ItemManager.INSTANCE.GetAllItems(); //key = item.id


        newID = pgj_Inventory2Manager.INSTANCE.getWornID();
        oldID = newID;


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

    public void ChangeInven() //pgj_InventoryController 에서 불르면 됨
    {
        invenlist = pgj_Inventory2Manager.INSTANCE.GetAllItems();

        
    }


    public void Worn()
    {
        int id = newID;
        oldID = newID;

        int rw = (id % 100) / 10;
        int lw = id % 10;
        int wearable = id / 100;

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
            pgj_InventoryManager.INSTANCE.deleteItem(selectedIndex);
            ChangeInven();
        }
    }

}