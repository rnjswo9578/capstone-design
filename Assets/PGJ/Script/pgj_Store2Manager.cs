using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_Store2Manager : MonoBehaviour
{
    private static pgj_Store2Manager m_pInstance;
    private static object m_pLock = new object();
    private int sellid = 0;

    public static pgj_Store2Manager INSTANCE
    {
        get
        {
            lock (m_pLock)
            {
                if (m_pInstance == null)
                {
                    m_pInstance = (pgj_Store2Manager)FindObjectOfType(typeof(pgj_Store2Manager));

                    if (FindObjectsOfType(typeof(pgj_Store2Manager)).Length > 1)
                    {
                        return m_pInstance;
                    }

                    if (m_pInstance == null)
                    {
                        GameObject singleton = new GameObject();
                        m_pInstance = singleton.AddComponent<pgj_Store2Manager>();
                        singleton.name = typeof(pgj_Store2Manager).ToString();
                        DontDestroyOnLoad(singleton);
                    }
                }
            }
            return m_pInstance;
        }
    }

    // 파싱한 정보를 저장
    List<InventoryInfo> inven_Data = new List<InventoryInfo>();
    // 아이템 추가.

    public void AddItem(InventoryInfo _cInfo)
    {
        //빈 칸이 있는지 체크
        if (inven_Data.Count <= 50)
            inven_Data.Add(_cInfo); //아이템 추가
        else
        {
            InventoryInfo temp = new InventoryInfo();
            temp.ID = 0;
            temp.ITEM_RANK = 0;
            int index = 0;
            for (index = 0; index < inven_Data.Count; index++)
            {
                if (inven_Data[index].Equals(temp))
                    break;
            }
            //아이템 추가
            inven_Data.RemoveAt(index: index);
            inven_Data.Insert(index: index, item: _cInfo);
        }
        Debug.Log("addinven");
    }
    public void deleteItem(int number)
    {
        InventoryInfo temp = new InventoryInfo();
        temp.ID = 0;
        temp.ITEM_RANK = 0;
        if (inven_Data[number].ID == 0 && inven_Data[number].ITEM_RANK == 0)
            return;
        else
        {
            //아이템 추가
            inven_Data.RemoveAt(index: number);
            inven_Data.Insert(index: number, item: temp);
        }
    }
    public void deleteAllItem()
    {
        InventoryInfo temp = new InventoryInfo();
        temp.ID = 0;
        temp.ITEM_RANK = 0;
        for (int index = 0; index < inven_Data.Count; index++)
        {
            inven_Data.RemoveAt(index: index);
            inven_Data.Insert(index: index, item: temp);
        }
    }
    // 전체 리스트 얻기
    public List<InventoryInfo> GetAllItems()
    {
        return inven_Data;
    }

    // 전체 갯수 얻기

    public int GetItemsCount()
    {
        return inven_Data.Count;
    }
    public void setSellID(int id)
    {
        sellid = id;
    }
    public int getSellID()
    {
        return sellid;
    }
}

