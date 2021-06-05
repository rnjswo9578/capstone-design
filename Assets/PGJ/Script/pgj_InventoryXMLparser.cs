using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class pgj_InventoryXMLparser : MonoBehaviour
{
    public string m_strName = "Inventory";

    void Start()
    {
        //�Ľ� ����
        Interpret(m_strName);
    }

    void Update()
    {


    }

    private void OnDestroy()
    {
        
    }

    private void Interpret(string _strSource)
    {
        XmlNodeList xmlNodeList = null;
        XmlDocument xmlDoc = new XmlDocument();

        TextAsset textAsset = (TextAsset)Resources.Load(_strSource);
        xmlDoc.LoadXml(textAsset.text);


        // �� ���� ��� ����
        xmlNodeList = xmlDoc.SelectNodes("InventoryInfo/Items");

        // ����� ���� ������ �Ŵ������ٰ� �ֱ�       
        foreach (XmlNode node in xmlNodeList)
        {
            // �ڽ��� ���� ���� ��
            if (node.Name.Equals("Items") && node.HasChildNodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    InventoryInfo item = new InventoryInfo();
                    item.ID = int.Parse(child.Attributes.GetNamedItem("id").Value);
                    item.ITEM_RANK = int.Parse(child.Attributes.GetNamedItem("rank").Value);

                    // �� ����� ���ٸ� ���� �Ŵ����� �־���
                    if (m_strName.Equals("Inventory"))
                        pgj_InventoryManager.INSTANCE.AddItem(item);
                    else if (m_strName.Equals("Store1"))
                        pgj_StoreManager.INSTANCE.AddItem(item);
                    else if (m_strName.Equals("Store2"))
                        pgj_Store2Manager.INSTANCE.AddItem(item);
                    else
                    {
                        Debug.Log("Erorr: pgj_InventoryXMLparser �߸��� m_strName ����");
                    }
                }
            }
        }
    }
}
