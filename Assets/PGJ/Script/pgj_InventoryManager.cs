using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_InventoryManager : MonoBehaviour
{
    private static pgj_InventoryManager m_pInstance;
    private static object m_pLock = new object();

    public static pgj_InventoryManager INSTANCE
    {
        get
        {
            lock (m_pLock)
            {
                if (m_pInstance == null)
                {
                    m_pInstance = (pgj_InventoryManager)FindObjectOfType(typeof(pgj_InventoryManager));

                    if (FindObjectsOfType(typeof(pgj_InventoryManager)).Length > 1)
                    {
                        return m_pInstance;
                    }

                    if (m_pInstance == null)
                    {
                        GameObject singleton = new GameObject();
                        m_pInstance = singleton.AddComponent<pgj_InventoryManager>();
                        singleton.name = typeof(pgj_InventoryManager).ToString();
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
        if (inven_Data.Count < 50)
            inven_Data.Add(_cInfo); //아이템 추가
        else
        {
            InventoryInfo temp = new InventoryInfo();
            temp.ID = 0;
            temp.ITEM_RANK = 0;
            int index =0;
            for (index = 0; index < 50; index++)
            {
                if(inven_Data[index].Equals(temp))
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
        //빈 칸이 있는지 체크
        InventoryInfo temp = new InventoryInfo();
        temp.ID = 0;
        temp.ITEM_RANK = 0;
        if (inven_Data[number].ID == 0 && inven_Data[number].ITEM_RANK ==0)
            return;
        else
        {
            //아이템 추가
            inven_Data.RemoveAt(index: number);
            inven_Data.Insert(index: number, item: temp);
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

public class InventoryInfo
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
