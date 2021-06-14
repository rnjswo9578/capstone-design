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

    private bool isInit;
    private bool isChange;
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
        isChange = false;
        invenlist = new List<Inventory2Info>();
        itemlist = pgj_ItemManager.INSTANCE.GetAllItems(); //key = item.id

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


        if (Input.GetMouseButtonDown(0))
        {
            click = EventSystem.current.currentSelectedGameObject;
            if (click != null)
            {
                if (click.transform.parent.Equals(contents.transform))
                {
                    
                }
            }

        }

        oldClick = click;

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

    public void ChangeInven2(int id) //pgj_InventoryController 에서 불르면 됨
    {
        invenlist = pgj_Inventory2Manager.INSTANCE.GetAllItems();

        string path;

        path = "Basic RPG Item Free/img/" + itemlist[id].ICON;
        Debug.Log(path);
        
        isChange = false;
    }


    public void Worn()
    {
        isChange = true;

    }

}