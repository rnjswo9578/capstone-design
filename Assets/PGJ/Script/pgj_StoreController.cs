using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class pgj_StoreController : MonoBehaviour
{
    public GameObject contents;
    public Text showInfo;

    List<GameObject> store1Prefabs;
    List<GameObject> store2Prefabs;

    private Dictionary<int, ItemInfo> itemlist;
    private List<InventoryInfo> store1List;
    private List<InventoryInfo> store2List;


    private GameObject click;
    private GameObject oldClick;
    private int selectedIndex;
    private bool isInit;
    private bool isChange;
    // Start is called before the first frame update
    void Start()
    {
        //InitInven();
        isInit = false;

        store1Prefabs = new List<GameObject>();
        store2Prefabs = new List<GameObject>();
        store1List = new List<InventoryInfo>();
        store2List = new List<InventoryInfo>();
        itemlist = pgj_ItemManager.INSTANCE.GetAllItems();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf == true && !isInit)
        {
            if (this.transform.name == "Store1 Scroll View")
                InitStore1();
            if (this.transform.name == "Store2 Scroll View")
                InitStore2();
            isInit = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            click = EventSystem.current.currentSelectedGameObject;
            if (click != null)
            {
                if (click.transform.parent.Equals(contents.transform))
                {
                    Debug.Log(click.transform.name + '3');
                    Debug.Log(click.transform.localPosition.x);
                    Debug.Log(click.transform.localPosition.y);

                    float x = (click.transform.localPosition.x - 35) / 65;
                    float y = (click.transform.localPosition.y + 35) / 65;

                    selectedIndex = (int)(x - y * 5);
                    Debug.Log(selectedIndex);

                    int id = 0;
                    if (this.transform.name == "Store1 Scroll View")
                        id = store1List[selectedIndex].ID;
                    if (this.transform.name == "Store2 Scroll View")
                        id = store2List[selectedIndex].ID;
                    if (id != 0)
                    {
                        string info = "아이템 정보\nBuy cost: " + itemlist[id].BUY_COST + "\nSell cost: " + itemlist[id].SELL_COST;
                        showInfo.text = info;
                    }
                }
            }

        }
        oldClick = click;
    }

    private void InitStore1()
    {
        store1List = pgj_StoreManager.INSTANCE.GetAllItems();

        Debug.Log("InitStore1" + store1List.Count);


        string path;
        int id;

        for (int index = 0; index < store1List.Count; index++)
        {
            id = store1List[index].ID;
            path = "Basic RPG Item Free/" + itemlist[id].NAME;
            store1Prefabs.Add(Instantiate(Resources.Load(path, typeof(GameObject)), contents.transform) as GameObject);
        }
    }
    private void InitStore2()
    {
        store2List = pgj_Store2Manager.INSTANCE.GetAllItems();

        Debug.Log("InitStore2" + store2List.Count);


        string path;
        int id;

        for (int index = 0; index < store2List.Count; index++)
        {
            id = store2List[index].ID;
            path = "Basic RPG Item Free/" + itemlist[id].NAME;
            store2Prefabs.Add(Instantiate(Resources.Load(path, typeof(GameObject)), contents.transform) as GameObject);
        }
    }
    private void ChangeInven()
    {
        if (this.transform.name == "Store1 Scroll View")
        {
            for (int i = 0; i < store1Prefabs.Count; i++)
                Destroy(store1Prefabs[i]);

            store1List = pgj_StoreManager.INSTANCE.GetAllItems();

            string path;
            int id;

            for (int index = 0; index < store1List.Count; index++)
            {
                id = store1List[index].ID;
                path = "Basic RPG Item Free/" + itemlist[id].NAME;
                store1Prefabs.Add(Instantiate(Resources.Load(path, typeof(GameObject)), contents.transform) as GameObject);
            }
        }
        if (this.transform.name == "Store2 Scroll View")
        {
            for (int i = 0; i < store2Prefabs.Count; i++)
                Destroy(store2Prefabs[i]);
            store2List = pgj_Store2Manager.INSTANCE.GetAllItems();

            string path;
            int id;

            for (int index = 0; index < store2List.Count; index++)
            {
                id = store2List[index].ID;
                path = "Basic RPG Item Free/" + itemlist[id].NAME;
                store2Prefabs.Add(Instantiate(Resources.Load(path, typeof(GameObject)), contents.transform) as GameObject);
            }
        }
    }

    public void Buy()
    {
        Debug.Log(selectedIndex);
        int id;
        int gold = lji_statusManager.instance.getGold();

        if (this.transform.name == "Store1 Scroll View")
        {
            id = store1List[selectedIndex].ID;
            pgj_StoreManager.INSTANCE.setSellID(id); //buy한 id저장

            if (itemlist[id].BUY_COST <= gold)
            {
                lji_statusManager.instance.changeGold(-itemlist[id].BUY_COST); //돈 변화

                pgj_StoreManager.INSTANCE.deleteItem(selectedIndex);
                ChangeInven();
            }
            else
            {
                id = -100;
                pgj_StoreManager.INSTANCE.setSellID(id);
            }
        }
        if (this.transform.name == "Store2 Scroll View")
        {
            id = store2List[selectedIndex].ID;
            pgj_Store2Manager.INSTANCE.setSellID(id); //buy한 id저장

            if (itemlist[id].BUY_COST <= gold)
            {
                lji_statusManager.instance.changeGold(-itemlist[id].BUY_COST);  //돈 변화

                pgj_Store2Manager.INSTANCE.deleteItem(selectedIndex);
                ChangeInven();
            }
            else
            {
                id = -100;
                pgj_Store2Manager.INSTANCE.setSellID(id);
            }
        }
    }
}
