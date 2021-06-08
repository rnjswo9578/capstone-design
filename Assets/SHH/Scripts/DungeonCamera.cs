using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCamera : MonoBehaviour
{
    public GameObject player;
    public float x, y, z;
    private List<GameObject> mTransparentWalls=new List<GameObject>();
    private List<GameObject> newAddedWall=new List<GameObject>();
    private float maxdis,maxdis1,maxdis2;
    private Vector3 pos,pos1,pos2;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("PLAYER");
        maxdis = Mathf.Sqrt(x * x + (y-1) * (y-1) + z * z);
        maxdis1 = Mathf.Sqrt(x * x + 1+ z * z);
        maxdis2 = Mathf.Sqrt(x * x + (y - 1) * (y - 1) + (z+5) * (z+5));

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = new Vector3(x, y, z); //0, 13, -20
        pos1 =player.transform.position + new Vector3(x, 1, z);
        pos2 = player.transform.position + new Vector3(x,y,z+5);
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform);
        FadeOutWall();
    }

    private void FadeOutWall()
    {
        Vector3 ScreenPos = Camera.main.WorldToScreenPoint(player.transform.position );
        var list = new List<RaycastHit>();
        for (int i = 1; i <= y+5; i++)
        {
            pos = player.transform.position + new Vector3(x, i, z);
            Ray ray4 = new Ray(pos, player.transform.position);
            maxdis = Mathf.Sqrt(x * x + y * y + z * z);
            RaycastHit[] hits4 = Physics.RaycastAll(ray4,maxdis);
            list.AddRange(hits4);
            
        }

        /*Ray ray = Camera.main.ScreenPointToRay(ScreenPos);
        Ray ray2 = new Ray(pos1, ScreenPos);
        Ray ray3 = new Ray(pos2, ScreenPos);
        RaycastHit[] hits1 = Physics.RaycastAll(ray, maxdis);// Physics.RaycastAll(ray2,maxdis1)+Physics.RaycastAll(ray3,maxdis2);
        RaycastHit[] hits2 = Physics.RaycastAll(ray2, maxdis);
        RaycastHit[] hits3 = Physics.RaycastAll(ray3, maxdis);
        
        list.AddRange(hits1);
        list.AddRange(hits2);
        list.AddRange(hits3);*/
        newAddedWall.Clear();
        foreach (RaycastHit hit in list)
        {
            if (hit.collider.gameObject != player.gameObject && hit.collider.tag != "Stair")
            {
                bool bFind = false;
                newAddedWall.Add(hit.collider.gameObject);
                foreach (GameObject saved in mTransparentWalls)
                {
                    if (saved == hit.collider.gameObject)
                    {
                        bFind = true;
                        Debug.Log(hit.collider.gameObject+"1");
                        break;
                    }
                }
                if (bFind == false)
                {
                    GameObject hitWall = hit.collider.gameObject;
                    MeshRenderer mc = hitWall.GetComponent<MeshRenderer>();
                    mc.material.shader = Shader.Find("Transparent/Diffuse");
                    Color c = mc.material.color;
                    c.a = 0.3f;
                    mc.material.color = c;
                   
                    mTransparentWalls.Add(hitWall);
                    
                    Debug.Log(hit.collider.gameObject + "2");
                }

            }
        }
        foreach (GameObject oldWall in mTransparentWalls)
            {
            bool bFind = false;
                foreach (GameObject newWall in newAddedWall)
                {
                    if (newWall == oldWall)
                    {
                        bFind = true;
                    Debug.Log(oldWall + "3");
                    Debug.Log(newWall + "4");
                    break;
                    }
                   
                }
                if (bFind == false)
                {
                Debug.Log(oldWall + "5");
                MeshRenderer mc = oldWall.GetComponent<MeshRenderer>();
                    mc.material.shader = Shader.Find("Standard");
                    Color c = mc.material.color;
                    c.a = 1.0f;
                    mc.material.color = c;
                    mTransparentWalls.Remove(oldWall);
                break;
                    
                }

            }
        }
  }
