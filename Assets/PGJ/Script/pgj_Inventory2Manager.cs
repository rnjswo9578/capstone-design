using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_Inventory2Manager : MonoBehaviour
{
    private static pgj_Inventory2Manager m_pInstance;
    private static object m_pLock = new object();
    private int wornid = 0;
    private int oldwornid = 0;

    public static pgj_Inventory2Manager INSTANCE
    {
        get
        {
            lock (m_pLock)
            {
                if (m_pInstance == null)
                {
                    m_pInstance = (pgj_Inventory2Manager)FindObjectOfType(typeof(pgj_Inventory2Manager));

                    if (FindObjectsOfType(typeof(pgj_Inventory2Manager)).Length > 1)
                    {
                        return m_pInstance;
                    }

                    if (m_pInstance == null)
                    {
                        GameObject singleton = new GameObject();
                        m_pInstance = singleton.AddComponent<pgj_Inventory2Manager>();
                        singleton.name = typeof(pgj_Inventory2Manager).ToString();
                        DontDestroyOnLoad(singleton);
                    }
                }
            }
            return m_pInstance;
        }
    }

    // 파싱한 정보를 저장
    //Dictionary<int, InventoryInfo> m_dicData = new Dictionary<int, InventoryInfo>();
    List<Inventory2Info> inven_Data = new List<Inventory2Info>();
    // 아이템 추가.

    public void AddItem(Inventory2Info _cInfo)
    {
        //빈 칸이 있는지 체크
        if (inven_Data.Count < 5)
            inven_Data.Add(_cInfo); //아이템 추가
        else
        {
            Inventory2Info temp = new Inventory2Info();
            temp.ID = 0;
            temp.ITEM_RANK = 0;
            int index = 0;
            for (index = 0; index < 5; index++)
            {
                if (inven_Data[index].Equals(temp))
                    break;
            }
            //아이템 추가
            inven_Data.RemoveAt(index: index);
            inven_Data.Insert(index: index, item: _cInfo);
            Debug.Log("addinven");
        }
    }
    public void deleteItem(int number, int id, int rank)
    {
        //빈 칸이 있는지 체크
        Inventory2Info temp = new Inventory2Info();
        temp.ID = id;
        temp.ITEM_RANK = rank;
        if (inven_Data[number].ID == 0)
            return;
        else
        {
            oldwornid = inven_Data[number].ID;
            wornid = id;
            //아이템 추가
            inven_Data.RemoveAt(index: number);
            inven_Data.Insert(index: number, item: temp);
        }
    }
    // 전체 리스트 얻기
    public List<Inventory2Info> GetAllItems()
    {
        return inven_Data;
    }

    // 전체 갯수 얻기

    public int GetItemsCount()
    {
        return inven_Data.Count;
    }
    public int GetIoldid()
    {
        return oldwornid;
    }

    public void setWornID(int id)
    {
        wornid = id;
    }
    public int getWornID()
    {
        return wornid;
    }
}

public class Inventory2Info
{
    private int item_id;
    private int item_rank;


    public int ID
    {
        set { item_id = value; }
        get { return item_id; }
    }
    public int ITEM_RANK
    {
        set { item_rank = value; }
        get { return item_rank; }
    }
}

