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
        if (instance == null) //instance�� null. ��, �ý��ۻ� �����ϰ� ���� ������ 
        {
            instance = this; //���ڽ��� instance�� �־��ݴϴ�. 
            DontDestroyOnLoad(gameObject); //OnLoad(���� �ε� �Ǿ�����) �ڽ��� �ı����� �ʰ� ���� 
        }
        else
        {
            if (instance != this) //instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ� 
                Destroy(this.gameObject); //�� �̻� �����ϸ� �ȵǴ� ��ü�̴� ��� AWake�� �ڽ��� ���� 
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
