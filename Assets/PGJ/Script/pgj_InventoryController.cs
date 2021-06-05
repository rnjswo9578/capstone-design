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
        invenlist = pgj_InventoryManager.INSTANCE.GetAllItems();

        string path;
        int id;

        id = invenlist[invenlist.Count - 1].ID;
        path = "Basic RPG Item Free/" + itemlist[id].NAME;
        Debug.Log(path);
        prefabs.Add(Instantiate(Resources.Load(path, typeof(GameObject)), contents.transform) as GameObject);
        isChange = false;
    }


    public void Sell()
    {
        Debug.Log("sellitem");
        pgj_InventoryManager.INSTANCE.deleteItem(selectedIndex);
        isChange = true;
        Debug.Log(prefabs[selectedIndex].transform.name); //여기에 돈 주고 받는 것 넣기
        Destroy(prefabs[selectedIndex]);

    }


    public void Buy()
    { 
        
    }
}
