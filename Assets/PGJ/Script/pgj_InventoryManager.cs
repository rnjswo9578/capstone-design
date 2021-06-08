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

    // �Ľ��� ������ ����
    List<InventoryInfo> inven_Data = new List<InventoryInfo>();
    // ������ �߰�.

    public void AddItem(InventoryInfo _cInfo)
    {
        //�� ĭ�� �ִ��� üũ
        if (inven_Data.Count < 50)
            inven_Data.Add(_cInfo); //������ �߰�
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
            //������ �߰�
            inven_Data.RemoveAt(index: index);
            inven_Data.Insert(index: index, item: _cInfo);
        }
        Debug.Log("addinven");
    }
    public void deleteItem(int number)
    {
        //�� ĭ�� �ִ��� üũ
        InventoryInfo temp = new InventoryInfo();
        temp.ID = 0;
        temp.ITEM_RANK = 0;
        if (inven_Data[number].ID == 0 && inven_Data[number].ITEM_RANK ==0)
            return;
        else
        {
            //������ �߰�
            inven_Data.RemoveAt(index: number);
            inven_Data.Insert(index: number, item: temp);
        }
    }
    // ��ü ����Ʈ ���
    public List<InventoryInfo> GetAllItems()
    {
        return inven_Data;
    }

    // ��ü ���� ���

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
