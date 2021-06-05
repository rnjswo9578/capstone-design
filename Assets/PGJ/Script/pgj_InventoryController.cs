using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pgj_InventoryController : MonoBehaviour
{
    public ScrollRect scroll;
    // Start is called before the first frame update
    void Start()
    {
        List<InventoryInfo> list = pgj_InventoryManager.INSTANCE.GetAllItems();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
