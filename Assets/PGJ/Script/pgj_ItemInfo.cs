using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgj_ItemInfo : MonoBehaviour
{
    private string item_id;
    private string item_name;
    private string item_icon;
    private int buy_cost;
    private int sell_cost;


    public string ID
    {
        set { item_id = value; }
        get { return item_id; }
    }
    public string NAME
    {
        set { item_name = value; }
        get { return item_name; }
    }
    public string ICON
    {
        set { item_icon = value; }
        get { return item_icon; }
    }
    public int BUY_COST
    {
        set { buy_cost = value; }
        get { return buy_cost; }
    }
    public int SELL_COST
    {
        set { sell_cost = value; }
        get { return sell_cost; }

    }
}
