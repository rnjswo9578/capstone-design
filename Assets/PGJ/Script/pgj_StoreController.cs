using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class pgj_StoreController : MonoBehaviour
{
    public GameObject contents;
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
                }
            }

        }
        oldClick = click;

        if (isChange)
        {
            ChangeInven();
        }
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
            store1List = pgj_StoreManager.INSTANCE.GetAllItems();

            string path;
            int id;

            id = store1List[store1List.Count - 1].ID;
            path = "Basic RPG Item Free/" + itemlist[id].NAME;
            Debug.Log(path);
            store1Prefabs.Add(Instantiate(Resources.Load(path, typeof(GameObject)), contents.transform) as GameObject);
            isChange = false;
        }
        if (this.transform.name == "Store2 Scroll View")
        {
            store2List = pgj_Store2Manager.INSTANCE.GetAllItems();

            string path;
            int id;

            id = store2List[store1List.Count - 1].ID;
            path = "Basic RPG Item Free/" + itemlist[id].NAME;
            Debug.Log(path);
            store2Prefabs.Add(Instantiate(Resources.Load(path, typeof(GameObject)), contents.transform) as GameObject);
            isChange = false;
        }
    }

    public void Buy()
    {
        Debug.Log(selectedIndex);
        if (this.transform.name == "Store1 Scroll View")
        {
            pgj_StoreManager.INSTANCE.deleteItem(selectedIndex);
            isChange = true;
            Debug.Log(store2Prefabs[selectedIndex].transform.name); //���⿡ �� �ְ� �޴� �� �ֱ�
            Destroy(store2Prefabs[selectedIndex]);
        }
        if (this.transform.name == "Store2 Scroll View")
        {
            pgj_Store2Manager.INSTANCE.deleteItem(selectedIndex);
            isChange = true;
            Debug.Log(store2Prefabs[selectedIndex].transform.name); //���⿡ �� �ְ� �޴� �� �ֱ�
            Destroy(store2Prefabs[selectedIndex]);
        }
    }
}