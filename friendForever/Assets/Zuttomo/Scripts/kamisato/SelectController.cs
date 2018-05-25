using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectController : MonoBehaviour {

    //鬼か人間かの選択結果を格納
    public int[] allplayerSelectState = new int[4];
    private int player_count = 0;
    private int demon_count = 0;

    // Use this for initialization
    void Start () {
        allplayerSelectState = new int[] { 0, 0, 0, 0 };
    }

    public void Lottery()
    {
        Debug.Log("player_count =" + player_count);
        player_count = 0;
        for (int i = 0; i < 4; i++)
        {
            //プレイヤーの数を数えてカウントする
            if (allplayerSelectState[i] != 0)
            {
                player_count++;
                //鬼の数を数える
                if (allplayerSelectState[i] == 2)
                {
                    demon_count++;
                }
            }
        }
        if (player_count == 4)
        {
            switch (demon_count)
            {
                case 0:
                    //ランダムで鬼にする
                    allplayerSelectState[new System.Random().Next(4)] = 2;
                    break;

                case 1:
                    break;

                default:
                    while (demon_count >= 2)
                    {
                        //鬼が二人以上いたらランダムで誰かを人間にする
                        allplayerSelectState[new System.Random().Next(4)] = 1;
                        demon_count = 0;
                        for (int i = 0; i < 4; i++)
                        {
                            //鬼の数を数える
                            if (allplayerSelectState[i] == 2)
                            {
                                demon_count++;
                            }
                        }
                    }
                    break;
            }
            for (int i = 0; i < 4; i++)
            {
                if (allplayerSelectState[i] == 2)
                {
                    Debug.Log("鬼" + (i + 1));
                }
            }
            SceneController.Instance.LoadScene(SceneName.GAME_SCENE);
        }
    }

    // Update is called once per frame
    void Update () {

        
    }
}
