using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class pgj_InventoryXMLparser : MonoBehaviour
{
    string m_strName = "Inventory";

    void Start()
    {
        //파싱 시작
        StartCoroutine(Process());
    }

    void Update()
    {


    }

    IEnumerator Process()
    {
        yield return 0;
        Interpret(m_strName);
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
            if (node.Name.Equals("InventoryInfo") && node.HasChildNodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    InventoryInfo item = new InventoryInfo();
                    item.ID = int.Parse(child.Attributes.GetNamedItem("id").Value);
                    item.ITEM_RANK = int.Parse(child.Attributes.GetNamedItem("rank").Value);

                    // 다 만들어 졌다면 이제 매니저에 넣어줌
                    pgj_InventoryManager.INSTANCE.AddItem(item);
                }
            }
        }
    }
}
