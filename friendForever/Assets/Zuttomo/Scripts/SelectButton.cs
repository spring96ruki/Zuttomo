using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour {



    public static int Chaserplayer = 0;

    public static int player = 0;

    public int player_num;
    
    public int selectstate;
    public SelectController selectController;

    public void Title()
    {
        SceneController.Instance.LoadScene(SceneName.SELECT_SCENE);
    }


    public void Select()
    {
        //SelectControllerに選択結果を送っている
        selectController.allplayerSelectState[player_num - 1] = selectstate;
        selectController.Lottery();
        //Debug.Log(selectController.allplayer_selectstate[player_num - 1]);
       //FadeManager.Instance.LoadScene(SceneName.GAME_SCENE,1.0f);
    }
}
