using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class pgj_InventoryXMLparser : MonoBehaviour
{
    public string m_strName = "Inventory";

    void Start()
    {
        //파싱 시작
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


        // 최 상위 노드 선택
        xmlNodeList = xmlDoc.SelectNodes("InventoryInfo/Items");

        // 만들어 놓은 아이템 매니져에다가 넣기       
        foreach (XmlNode node in xmlNodeList)
        {
            // 자식이 있을 때에 돔
            if (node.Name.Equals("Items") && node.HasChildNodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    InventoryInfo item = new InventoryInfo();
                    item.ID = int.Parse(child.Attributes.GetNamedItem("id").Value);
                    item.ITEM_RANK = int.Parse(child.Attributes.GetNamedItem("rank").Value);

                    // 다 만들어 졌다면 이제 매니저에 넣어줌
                    if (m_strName.Equals("Inventory"))
                        pgj_InventoryManager.INSTANCE.AddItem(item);
                    else if (m_strName.Equals("Store1"))
                        pgj_StoreManager.INSTANCE.AddItem(item);
                    else if (m_strName.Equals("Store2"))
                        pgj_Store2Manager.INSTANCE.AddItem(item);
                    else
                    {
                        Debug.Log("Erorr: pgj_InventoryXMLparser 잘못된 m_strName 넣음");
                    }
                }
            }
        }
    }
}
