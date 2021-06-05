using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System;


public class pgj_ItemXMLCrtl : MonoBehaviour
{
    // �Ľ� �� xml ���ϸ�
    string m_strName = "Items";

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
        //Debug.Log("111item" + _strSource);
        XmlNodeList xmlNodeList = null;
        XmlDocument xmlDoc = new XmlDocument();

        TextAsset textAsset = (TextAsset)Resources.Load(_strSource);
        xmlDoc.LoadXml(textAsset.text);


        // �� ���� ��� ����
        xmlNodeList = xmlDoc.SelectNodes("ItemsInfo/Weapon");

        // ����� ���� ������ �Ŵ������ٰ� �ֱ�       
        foreach (XmlNode node in xmlNodeList)
        {
            // �ڽ��� ���� ���� ��
            if (node.Name.Equals("Weapon") && node.HasChildNodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    //Debug.Log("444item");
                    ItemInfo item = new ItemInfo();
                    item.ID = int.Parse(child.Attributes.GetNamedItem("id").Value);
                    item.NAME = child.Attributes.GetNamedItem("name").Value;
                    item.ICON = child.Attributes.GetNamedItem("icon").Value;
                    item.BUY_COST = int.Parse(child.Attributes.GetNamedItem("buy").Value);
                    item.SELL_COST = int.Parse(child.Attributes.GetNamedItem("sell").Value);

                    // �� ����� ���ٸ� ���� �Ŵ����� �־���
                    pgj_ItemManager.INSTANCE.AddItem(item);
                }
            }
        }
    }
}
