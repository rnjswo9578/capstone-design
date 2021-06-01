using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class pgj_InventoryXMLparser : MonoBehaviour
{
    string m_strName = "Inventory";

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
        yield return 0;
        Interpret(m_strName);
    }



    private void Interpret(string _strSource)
    {
        XmlNodeList xmlNodeList = null;
        XmlDocument xmlDoc = new XmlDocument();

        TextAsset textAsset = (TextAsset)Resources.Load(_strSource);
        xmlDoc.LoadXml(textAsset.text);


        // �� ���� ��� ����
        xmlNodeList = xmlDoc.SelectNodes("InventoryInfo/Items");

        // ����� ���� ������ �Ŵ������ٰ� �ֱ�       
        foreach (XmlNode node in xmlNodeList)
        {
            // �ڽ��� ���� ���� ��
            if (node.Name.Equals("InventoryInfo") && node.HasChildNodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                {
                    InventoryInfo item = new InventoryInfo();
                    item.ID = int.Parse(child.Attributes.GetNamedItem("id").Value);
                    item.ITEM_RANK = int.Parse(child.Attributes.GetNamedItem("rank").Value);

                    // �� ����� ���ٸ� ���� �Ŵ����� �־���
                    pgj_InventoryManager.INSTANCE.AddItem(item);
                }
            }
        }
    }
}
