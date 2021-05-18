using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class lji_XMLctrl : MonoBehaviour
{

    private string name;
    private int level;
    private int hp;


    // Start is called before the first frame update
    void Start()
    {
        //CreateXml();
        LoadXml(name, level, hp);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        name = "name3";
        level = 300;
        hp = 12;
        OverwriteXml(name, level, hp);
    }

    void CreateXml() //씬이 끝나고 생성됨
    {
        XmlDocument xmlDoc = new XmlDocument();

        // Xml을 선언한다(xml의 버전과 인코딩 방식을 정해준다.)
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        //루트 노드 생성
        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "CharacterInfo", string.Empty);
        xmlDoc.AppendChild(root);

        //자식 노드 생성
        XmlNode child = xmlDoc.CreateNode(XmlNodeType.Element, "Character", string.Empty);
        root.AppendChild(child);

        //자식 노드에 들어갈 속성 생성
        XmlElement name = xmlDoc.CreateElement("Name");
        name.InnerText = "name";
        child.AppendChild(name);

        XmlElement lv = xmlDoc.CreateElement("Level");
        lv.InnerText = "1";
        child.AppendChild(lv);

        XmlElement hp = xmlDoc.CreateElement("Hp");
        hp.InnerText = "200";
        child.AppendChild(hp);

        xmlDoc.Save("./Assets/Resources/Character.xml");
    }
    void LoadXml(string name, int level, int hp)
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Character");
        Debug.Log(textAsset);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodes = xmlDoc.SelectNodes("CharacterInfo/Character");

        foreach (XmlNode node in nodes)
        {
            Debug.Log("Name :: " + node.SelectSingleNode("Name").InnerText);
            Debug.Log("Level :: " + node.SelectSingleNode("Level").InnerText);
            Debug.Log("Exp :: " + node.SelectSingleNode("Hp").InnerText);

            name = node.SelectSingleNode("Name").InnerText;
            level = int.Parse(node.SelectSingleNode("Level").InnerText);
            hp = int.Parse(node.SelectSingleNode("Hp").InnerText);
        }
    }

    void OverwriteXml(string name, int level, int hp)
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Character");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodes = xmlDoc.SelectNodes("CharacterInfo/Character");
        XmlNode character = nodes[0];

        character.SelectSingleNode("Name").InnerText = name;
        character.SelectSingleNode("Level").InnerText = level + "";
        character.SelectSingleNode("Hp").InnerText = hp + "";

        xmlDoc.Save("./Assets/Resources/Character.xml");
    }



}
