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
        DontDestroyOnLoad(player); //gameObject가 씬과 상관없이 관리가능
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
                //SceneManager.LoadScene("store", LoadSceneMode.Additive); "store"씬을 현재 씬에 로드
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
Test2라는 이름의 씬을 현재 씬에 추가로 로드한다.
LoadSceneMode.Additive가 없을 경우 Test2 씬이 로드되고 현재 씬은 사라진다.

SceneManager.GetSceneByBuildIndex(sceneNum);
sceneNum(int 타입)를 빌드 세팅에 나오는 씬 인덱스로 갖는 씬(Scene 클래스의 오브젝트)을 리턴한다.
(씬은 유니티 에디터에서 연결하는 것이 불가능!)*/