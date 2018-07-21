using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectController : MonoBehaviour {

    //鬼か人間かの選択結果を格納
    public int[] allPlayerSelectState = new int[4];
	public static int m_setChaserNum = 0;
    public int m_playerCount = 0;
	public int m_chaserCount = 0;

    // Use this for initialization
    void Start () {
        allPlayerSelectState = new int[] { 0, 0, 0, 0 };
        DontDestroyOnLoad(this.gameObject);
    }

    public void Lottery()
    {
		m_chaserCount = 0;
        for (int i = 0; i < 4; ++i)
        {
            if (allPlayerSelectState[i] == 2)
            {
                m_chaserCount++;
            }
        }
        if (m_playerCount == 4)
        {
			switch (m_chaserCount)
            {
				case 0:
                    //ランダムで鬼にする
					allPlayerSelectState [new System.Random ().Next (4)] = 2;
                    break;

                case 1:
                    break;

                default:
				    while (m_chaserCount >= 2)
                        {
                            //鬼が二人以上いたらランダムで誰かを人間にする
                            allPlayerSelectState[new System.Random().Next(4)] = 1;
					        m_chaserCount = 0;
                            for (int i = 0; i < 4; i++)
                            {
                                //鬼の数を数える
                                if (allPlayerSelectState[i] == 2)
                                {
							    m_chaserCount++;
                                }
                            }
                        }
                    break;
            }

            for (int i = 0; i < 4; i++)
            {
                if (allPlayerSelectState[i] == 2)
                {
					m_setChaserNum = i + 1;
                    Debug.Log("鬼:" + (i + 1));
                }
            }
            
            SceneController.Instance.LoadScene(SceneName.GAME_SCENE);
        }
    }

	public static int GetChaserplayer()
    {
		return m_setChaserNum;
    }
}
