using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System;


public class pgj_ItemXMLCrtl : MonoBehaviour
{
    // 파싱 할 xml 파일명
    string m_strName = "MyItems.xml";

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
        // 인코딩 문제 예외
        // 읽은 데이터의 앞 2바이트 제거(BOM제거)
        StringReader stringReader = new StringReader(_strSource);

        stringReader.Read();    // BOM 제거 한 데이터로 파싱해요.

        XmlNodeList xmlNodeList = null;
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            // XML 로드
            xmlDoc.LoadXml(stringReader.ReadToEnd());
        }
        catch (Exception e)
        {
            xmlDoc.LoadXml(_strSource);
        }

        // 최 상위 노드 선택
        xmlNodeList = xmlDoc.SelectNodes("Items");

        // 만들어 놓은 아이템 매니져에다가 넣기       
        foreach (XmlNode node in xmlNodeList)
        {
            // 자식이 있을 때에 돔
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

                    // 다 만들어 졌다면 이제 매니저에 넣어줌
                    pgj_ItemManager.INSTANCE.AddItem(item);
                }
            }
        }
    }
}
