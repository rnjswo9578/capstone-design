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
        DontDestroyOnLoad(player); //gameObject�� ���� ������� ��������
    }

    // Start is called before the first frame update
    void Start()
    {
        inPlayer = false;

        portalPosition = GameObject.Find("Portal Position").GetComponent<Transform>();
        
        player = GameObject.Find("Player");

        player.transform.position = portalPosition.position;

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
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("F");
                SceneManager.LoadScene("store");
                //SceneManager.LoadScene("store", LoadSceneMode.Additive); "store"���� ���� ���� �ε�
                MoveToOtherScene(player, sceneNum);
            }
        }
    }

    private void MoveToOtherScene(GameObject obj, int sceneNum)
    {
        Scene scene = SceneManager.GetSceneByBuildIndex(sceneNum);
        SceneManager.MoveGameObjectToScene(obj, scene);
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


/*SceneManager.LoadScene("Test2", LoadSceneMode.Additive);
Test2��� �̸��� ���� ���� ���� �߰��� �ε��Ѵ�.
LoadSceneMode.Additive�� ���� ��� Test2 ���� �ε�ǰ� ���� ���� �������.

SceneManager.GetSceneByBuildIndex(sceneNum);
sceneNum(int Ÿ��)�� ���� ���ÿ� ������ �� �ε����� ���� ��(Scene Ŭ������ ������Ʈ)�� �����Ѵ�.
(���� ����Ƽ �����Ϳ��� �����ϴ� ���� �Ұ���!)*/