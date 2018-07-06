using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectController : MonoBehaviour {

    //鬼か人間かの選択結果を格納
    public int[] allplayerSelectState = new int[4];
	public static int setChaser_num = 0;
    public int player_count = 0;
	public int Chaser_count = 0;

    // Use this for initialization
    void Start () {
        allplayerSelectState = new int[] { 0, 0, 0, 0 };
        DontDestroyOnLoad(this.gameObject);
    }

    void Update() {

    }

    public void Lottery()
    {
		Chaser_count = 0;
        for (int i = 0; i < 4; i++)
        {
            if (allplayerSelectState[i] == 2)
            {
                Chaser_count++;
            }
        }
        if (player_count == 4)
        {
			switch (Chaser_count)
            {
				case 0:
                    //ランダムで鬼にする
					allplayerSelectState [new System.Random ().Next (4)] = 2;
                    break;

                case 1:
                    break;

                default:
				    while (Chaser_count >= 2)
                        {
                            //鬼が二人以上いたらランダムで誰かを人間にする
                            allplayerSelectState[new System.Random().Next(4)] = 1;
					        Chaser_count = 0;
                            for (int i = 0; i < 4; i++)
                            {
                                //鬼の数を数える
                                if (allplayerSelectState[i] == 2)
                                {
							    Chaser_count++;
                                }
                            }
                        }
                    break;
            }

            for (int i = 0; i < 4; i++)
            {
                if (allplayerSelectState[i] == 2)
                {
					setChaser_num = i + 1;
                    Debug.Log("鬼:" + (i + 1));
                }
            }
            
            SceneController.Instance.LoadScene(SceneName.GAME_SCENE);
        }
    }

	public static int GetChaserplayer()
    {
		return setChaser_num;
    }
}
