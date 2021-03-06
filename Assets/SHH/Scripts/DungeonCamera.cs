using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCamera : MonoBehaviour
{
    public GameObject player;
    public float x, y, z;
    private int targetx, targety;
    private List<GameObject> mTransparentWalls=new List<GameObject>();
    private List<GameObject> newAddedWall=new List<GameObject>();
    private float maxdis;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("PLAYER");
        targetx = (int)x - 2;
        targety = (int)y - 6;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = new Vector3(x, y, z); //0, 13, -20
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform);
        FadeOutWall();
    }

    private void FadeOutWall()
    {
       // Vector3 ScreenPos = Camera.main.WorldToScreenPoint(player.transform.position );
        var list = new List<RaycastHit>();
        for (int j = targetx; j <= x + 2; j++)
        {
            for (int i = targety; i <= y + 4; i+=2)
            {
                pos = player.transform.position + new Vector3(j, i, z);
                Ray ray4 = new Ray(pos, (player.transform.position - pos).normalized);
                maxdis = (player.transform.position - pos).magnitude;
                RaycastHit[] hits4 = Physics.RaycastAll(ray4, maxdis);
                //Debug.DrawRay(pos, (player.transform.position - pos).normalized * maxdis, Color.red, 0.3f);
                list.AddRange(hits4);

            }
        }
        newAddedWall.Clear();
        foreach (RaycastHit hit in list)
        {
            if (hit.collider.gameObject != player.gameObject && hit.collider.tag =="Wall")
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
