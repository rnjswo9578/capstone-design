using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_ItemManager : MonoBehaviour
{
    private static pgj_ItemManager m_pInstance;
    private static object m_pLock = new object();

    public static pgj_ItemManager INSTANCE
    {
        get
        {
            lock (m_pLock)
            {
                if (m_pInstance == null)
                {
                    m_pInstance = (pgj_ItemManager)FindObjectOfType(typeof(pgj_ItemManager));

                    if (FindObjectsOfType(typeof(pgj_ItemManager)).Length > 1)
                    {
                        return m_pInstance;
                    }

                    if (m_pInstance == null)
                    {
                        GameObject singleton = new GameObject();
                        m_pInstance = singleton.AddComponent<pgj_ItemManager>();
                        singleton.name = typeof(pgj_ItemManager).ToString();
                        DontDestroyOnLoad(singleton);
                    }
                }
            }
            return m_pInstance;
        }
    }

    // �Ľ��� ������ ����
    Dictionary<int, ItemInfo> m_dicData = new Dictionary<int, ItemInfo>();
    // ������ �߰�.

    public void AddItem(ItemInfo _cInfo)
    {
        Debug.Log("additem");
        // �������� �̹� �ִ��� üũ
        if (m_dicData.ContainsKey(_cInfo.ID)) return;
        //������ �߰�
        m_dicData.Add(_cInfo.ID, _cInfo);
        Debug.Log("additem");
    }

    // �ϳ��� ������ ���
    public ItemInfo GetItem(int _nID)
    {
        if (m_dicData.ContainsKey(_nID)) //�ִ��� üũ
            return m_dicData[_nID];
        return null;
    }

    // ��ü ����Ʈ ���
    public Dictionary<int, ItemInfo> GetAllItems()
    {
        return m_dicData;
    }

    // ��ü ���� ���

    public int GetItemsCount()
    {
        return m_dicData.Count;
    }

}

public class ItemInfo
{
    private int item_id;
    private string item_name;
    private string item_icon;
    private int buy_cost;
    private int sell_cost;


    public int ID
    {
        set { item_id = value; }
        get { return item_id; }
    }
    public string NAME
    {
        set { item_name = value; }
        get { return item_name; }
    }
    public string ICON
    {
        set { item_icon = value; }
        get { return item_icon; }
    }
    public int BUY_COST
    {
        set { buy_cost = value; }
        get { return buy_cost; }
    }
    public int SELL_COST
    {
        set { sell_cost = value; }
        get { return sell_cost; }

    }
}


