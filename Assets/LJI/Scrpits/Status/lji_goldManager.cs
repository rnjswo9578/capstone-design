using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Xml;

public class lji_goldManager : MonoBehaviour
{
    public static lji_goldManager instance = null; //private static lji_statusManager instance = null;
    
    public int gold = 0;
    
    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때 
        {
            instance = this; //내자신을 instance로 넣어줍니다. 
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지 
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미 
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제 
        }
        //LoadXml("Gold");
    }

    //private void OnDestroy()
    //{
    //    OverwriteXml();
    //}


    public void changeGold(int other)
    {
        gold = gold + other;
        GameObject.FindWithTag("PLAYER").SendMessage("GetPlayerStatus");
    }
    public void setGold(int other)
    {
        gold = other;
        GameObject.FindWithTag("PLAYER").SendMessage("GetPlayerStatus");
    }
    public int getGold()
    {
        return gold;
    }

    //void LoadXml(string filename)
    //{
    //    TextAsset textAsset = (TextAsset)Resources.Load(filename);
    //    XmlDocument xmlDoc = new XmlDocument();
    //    xmlDoc.LoadXml(textAsset.text);

    //    XmlNode goldXml = xmlDoc.SelectSingleNode("GoldInfo/Gold");
    //    gold = int.Parse(goldXml.InnerText);

    //}

    //public void OverwriteXml()
    //{
    //    TextAsset textAsset = (TextAsset)Resources.Load("Gold");
    //    XmlDocument xmlDoc = new XmlDocument();
    //    xmlDoc.LoadXml(textAsset.text);

    //    XmlNode goldXml = xmlDoc.SelectSingleNode("GoldInfo/Gold");
    //    goldXml.InnerText = gold + "";

    //    xmlDoc.Save("./Assets/Resources/Gold.xml");
    //}
}
