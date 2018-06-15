using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonMono<GameController> {

    public int m_randomNumber;
    public GameObject m_player;
    public GameObject m_chaser;
    public GameObject m_responePointCenter;
    public GameObject m_responePointRight;
    public GameObject m_responePointRightEnd;
    public GameObject m_responePointLeft;
    public GameObject m_responePointLeftEnd;

    bool m_isRandom;
    bool m_isInstace;
    List<GameObject> ResponePoint = new List<GameObject>();

	// Use this for initialization
	void Start () {
        AddResponePoint();
	}
	
	// Update is called once per frame
	void Update () {
        DebugScene();
        RandomNumber();
	}

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
