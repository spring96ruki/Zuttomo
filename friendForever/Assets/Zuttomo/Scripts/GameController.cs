using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static int m_getChasernum;
    public GameObject playerandcamera;
    public GameObject itimatu;
    public float ItemPosition_x;
    public float ItemPosition_z;
    public float rect_x;
    public float rect_y;
    public int GamePhase;
    public bool OpenDoor;

    bool m_isRandom;
    bool m_isInstace;
    List<GameObject> ResponePoint = new List<GameObject>();
    public int m_randomNumber;
    public GameObject m_player;
    public GameObject m_chaser;
    public GameObject m_responePointCenter;
    public GameObject m_responePointRight;
    public GameObject m_responePointRightEnd;
    public GameObject m_responePointLeft;
    public GameObject m_responePointLeftEnd;

    UIController m_uIController;

    void DebugScene()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneController.Instance.LoadScene(SceneName.SCENE_CHANGE);
        }
    }


    void AddResponePoint()
    {
        // 出現位置をリストに格納
        ResponePoint.Add(m_responePointCenter);
        ResponePoint.Add(m_responePointLeft);
        ResponePoint.Add(m_responePointLeftEnd);
        ResponePoint.Add(m_responePointRight);
        ResponePoint.Add(m_responePointRightEnd);
    }

    // Use this for initialization
    void Awake()
    {
        AddResponePoint();

        m_uIController = GameObject.Find("UIController").GetComponent<UIController>();
            
        int m_getChaserNum = SelectController.GetChaserplayer();

        for (int i = 0; i < 4; i++)
        {
            float x = Random.Range(-System.Math.Abs(ItemPosition_x), ItemPosition_x);
            float z = Random.Range(-System.Math.Abs(ItemPosition_z), ItemPosition_z);

            GameObject PlayerAndCamera = playerandcamera;
            PlayerAndCamera.name = ("Player" + (i + 1));

            var player = PlayerAndCamera.transform.GetChild(0).gameObject;
            var camera = PlayerAndCamera.transform.GetChild(1).gameObject;

            if (i == 0 || i == 2)
            {
                rect_x = 0f;
            }
            else
            {
                rect_x = 0.5f;
            }

            if (i == 0 || i == 1)
            {
                rect_y = 0.5f;
            }
            else
            {
                rect_y = 0f;
            }
            camera.GetComponent<Camera>().rect = new Rect(rect_x, rect_y, 0.5f, 0.5f);

            player.GetComponent<RunnerStatus>().runnerNum = i + 1;
            player.GetComponent<RunnerController>().m_playerNum = i + 1;
           // m_getChaserNum
            if (m_getChaserNum == i + 1)
            {
                player.GetComponent<RunnerController>().ChaserFlag = true;
                
                player.GetComponent<ChaserController>().enabled = true;
                player.GetComponent<RunnerController>().enabled = false;
                player.GetComponent<RunnerMove>().enabled = false;
            }
            else
            {
                player.GetComponent<RunnerController>().ChaserFlag = false;
                player.GetComponent<RunnerController>().enabled = true;
                player.GetComponent<RunnerMove>().enabled = false;
                player.GetComponent<ChaserController>().enabled = false;
                player.GetComponent<ChaserMove>().enabled = false;
                player.GetComponent<ChaserSkill>().enabled = false;
            }
            GameObject m_playerObj =  Instantiate(PlayerAndCamera, new Vector3(x, 1.3f, z), Quaternion.identity);
            m_playerObj.name = PlayerAndCamera.name;
        }

        GameObject.Find("Gimmick Script").GetComponent<gimmickScript>().GimmickStart();
        for (int i = 0; i < 4; i++)
        {
            ItemSpawn();
        }
        m_uIController.GetComponent<UIController>().UIStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            RandomNumber();
        }
    }

    void ItemSpawn()
    {
        GameObject newitimatu = (GameObject)Instantiate(itimatu, new Vector3(Random.Range(-System.Math.Abs(ItemPosition_x), ItemPosition_x), 1.3f, Random.Range(-System.Math.Abs(ItemPosition_z), ItemPosition_z)), Quaternion.identity); ;
        newitimatu.name = itimatu.name;
    }

    public void GamePhaseAdd() {
        GamePhase++;

        if (GamePhase == 3) {
            EndGame();
        }
    }

    public void PlayerChange()
    {

    }

    public void GamePhaseChange()
    {
        GamePhaseAdd();
        GameObject.Find("Gimmick Script").GetComponent<gimmickScript>().GimmickStart();
    }

    public void EndGame()
    {
        Debug.Log("EndGame");
        Debug.Log(SceneController.Instance);
        SceneController.Instance.LoadScene(SceneName.TITLE_SCENE);
    }

    void RandomNumber()
    {
        // isRandomがfalseの間、randomNumberが変化し続ける
        if (m_isRandom == false)
        {
            ++m_randomNumber;
            Debug.Log(m_randomNumber);
            // Kコードを押されると、isRandomがtrueになり変化終了
            if (Input.GetKeyDown(KeyCode.K))
            {
                m_isRandom = true;
            }
        }

        // randomNumberが100を越えると0に戻る
        if (m_randomNumber > 100)
        {
            m_randomNumber = 0;
        }

        if (m_isRandom == true)
        {
            // isRandomがtrueになり、isInstanceがfalseだったらrandomNumberを元に計算を開始
            if (m_isInstace == false)
            {
                // randomNumberを割ったときの余りで出現位置を変更
                // プレイヤーが出現したらisInstanceがtrueになり生成終了
                switch (m_randomNumber % 6)
                {
                    case 0:
                        Instantiate(m_player, ResponePoint[0].transform.position, m_player.transform.rotation);
                        m_isInstace = true;
                        break;
                    case 1:
                        Instantiate(m_player, ResponePoint[1].transform.position, m_player.transform.rotation);
                        m_isInstace = true;
                        break;
                    case 2:
                        Instantiate(m_player, ResponePoint[2].transform.position, m_player.transform.rotation);
                        m_isInstace = true;
                        break;
                    case 3:
                        Instantiate(m_player, ResponePoint[3].transform.position, m_player.transform.rotation);
                        m_isInstace = true;
                        break;
                    case 4:
                        Instantiate(m_player, ResponePoint[4].transform.position, m_player.transform.rotation);
                        m_isInstace = true;
                        break;
                }
            }
        }

    }

}