using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System;


public class pgj_ItemXMLCrtl : MonoBehaviour
{
    // �Ľ� �� xml ���ϸ�
    string m_strName = "MyItems.xml";

    void Start()
    {
        //�Ľ� ����
        StartCoroutine(Process());
    }

    void Update()
    {


    }


    IEnumerator Process()
    {
        string strPath = string.Empty;
#if ( UNITY_EDITOR || UNITY_STANDALONE_WIN )
        strPath += ("file:///");
        strPath += (Application.streamingAssetsPath + "/" + m_strName);
#elif UNITY_ANDROID
        strPath =  "jar:file://" + Application.dataPath + "!/assets/" + m_strName;

#endif
        WWW www = new WWW(strPath);
        yield return www;
        Debug.Log("Read Content : " + www.text);
        Interpret(www.text);
    }



    private void Interpret(string _strSource)
    {
        // ���ڵ� ���� ����
        // ���� �������� �� 2����Ʈ ����(BOM����)
        StringReader stringReader = new StringReader(_strSource);

        stringReader.Read();    // BOM ���� �� �����ͷ� �Ľ��ؿ�.

        XmlNodeList xmlNodeList = null;
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            // XML �ε�
            xmlDoc.LoadXml(stringReader.ReadToEnd());
        }
        catch (Exception e)
        {
            xmlDoc.LoadXml(_strSource);
        }

        // �� ���� ��� ����
        xmlNodeList = xmlDoc.SelectNodes("Items");

        // ����� ���� ������ �Ŵ������ٰ� �ֱ�       
        foreach (XmlNode node in xmlNodeList)
        {
            // �ڽ��� ���� ���� ��
            if (node.Name.Equals("Items") && node.HasChildNodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    ItemInfo item = new ItemInfo();
                    item.ID = int.Parse(child.Attributes.GetNamedItem("id").Value);
                    item.NAME = child.Attributes.GetNamedItem("name").Value;
                    item.ICON = child.Attributes.GetNamedItem("icon").Value;
                    item.BUY_COST = int.Parse(child.Attributes.GetNamedItem("buy_cost").Value);
                    item.SELL_COST = int.Parse(child.Attributes.GetNamedItem("sell_cost").Value);

                    // �� ����� ���ٸ� ���� �Ŵ����� �־���
                    pgj_ItemManager.INSTANCE.AddItem(item);
                }
            }
        }
    }
}
