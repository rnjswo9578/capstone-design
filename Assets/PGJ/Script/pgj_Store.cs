using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pgj_Store : MonoBehaviour
{
    private bool inPlayer;
    public GameObject player;
    public Transform portalPosition;
    public int sceneNum = 1;

    private void Awake()
    {
        /*var obj = GameObject.FindGameObjectsWithTag("PLAYER");
        if (obj.Length == 1) //�ߺ����� ����
        { 
            DontDestroyOnLoad(player);   //player�� ���� ������� ��������
        } 
        else { 
            Destroy(player); 
        }*/
    }

    // Start is called before the first frame update
    void Start()
    {
        inPlayer = false;

        //portalPosition = GameObject.Find("Portal Position").GetComponent<Transform>();
        //player = GameObject.Find("Player");

        if (SceneManager.GetActiveScene().name == "store")
        {
            sceneNum = 0;
        }
        else if (SceneManager.GetActiveScene().name == "monster test3")
        {
            sceneNum = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inPlayer)
        {
            Debug.Log("in");
            if (sceneNum == 1)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("F");
                    player.transform.position = portalPosition.position;
                    SceneManager.LoadScene("store");
                }
            }
            //SceneManager.LoadScene("store", LoadSceneMode.Additive); "store"���� ���� ���� �ε�
            //pgj_GameManager.instance.MoveToOtherScene(player, sceneNum);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PLAYER")
        {
            inPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PLAYER")
        {
            inPlayer = false;
        }
    }

}