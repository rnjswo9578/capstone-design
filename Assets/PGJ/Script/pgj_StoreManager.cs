using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_StoreManager : MonoBehaviour
{
    private static pgj_StoreManager m_pInstance;
    private static object m_pLock = new object();

    public static pgj_StoreManager INSTANCE
    {
        get
        {
            lock (m_pLock)
            {
                if (m_pInstance == null)
                {
                    m_pInstance = (pgj_StoreManager)FindObjectOfType(typeof(pgj_StoreManager));

                    if (FindObjectsOfType(typeof(pgj_StoreManager)).Length > 1)
                    {
                        return m_pInstance;
                    }

                    if (m_pInstance == null)
                    {
                        GameObject singleton = new GameObject();
                        m_pInstance = singleton.AddComponent<pgj_StoreManager>();
                        singleton.name = typeof(pgj_StoreManager).ToString();
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
            for (index = 0; index < 50; index++)
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
        //빈 칸이 있는지 체크
        if (inven_Data[number].Equals(temp))
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

}

