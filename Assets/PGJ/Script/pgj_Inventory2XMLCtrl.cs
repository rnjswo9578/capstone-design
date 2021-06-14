using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class pgj_Inventory2XMLCtrl : MonoBehaviour
{
    public string m_strName = "Inventory2";

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
                    Inventory2Info item = new Inventory2Info();
                    item.ID = int.Parse(child.Attributes.GetNamedItem("id").Value);
                    item.ITEM_RANK = int.Parse(child.Attributes.GetNamedItem("rank").Value);

                    // 다 만들어 졌다면 이제 매니저에 넣어줌
                    if (m_strName.Equals("Inventory2"))
                        pgj_Inventory2Manager.INSTANCE.AddItem(item);
                    else
                    {
                        Debug.Log("Erorr: pgj_Inventory2XMLCrtl ");
                    }
                }
            }
        }
    }
}
