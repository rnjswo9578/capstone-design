using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System;


public class pgj_ItemXMLCrtl : MonoBehaviour
{
    // 파싱 할 xml 파일명
    string m_strName = "Items";

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
        xmlNodeList = xmlDoc.SelectNodes("ItemsInfo/Weapon");

        // 만들어 놓은 아이템 매니져에다가 넣기       
        foreach (XmlNode node in xmlNodeList)
        {
            // 자식이 있을 때에 돔
            if (node.Name.Equals("ItemsInfo") && node.HasChildNodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    ItemInfo item = new ItemInfo();
                    Debug.Log(child.Attributes.GetNamedItem("id").Value);
                    item.ID = int.Parse(child.Attributes.GetNamedItem("id").Value);
                    item.NAME = child.Attributes.GetNamedItem("name").Value;
                    item.ICON = child.Attributes.GetNamedItem("icon").Value;
                    item.BUY_COST = int.Parse(child.Attributes.GetNamedItem("buy_cost").Value);
                    item.SELL_COST = int.Parse(child.Attributes.GetNamedItem("sell_cost").Value);

                    // 다 만들어 졌다면 이제 매니저에 넣어줌
                    pgj_ItemManager.INSTANCE.AddItem(item);
                }
            }
        }
    }
}
