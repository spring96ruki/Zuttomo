using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour {

    public static int demonplayer = 0;

    
    public int player_num;
    public int selectstate;
    public SelectController selectController;


    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Title()
    {
        FadeManager.Instance.LoadScene(SceneName.SELECT_SCENE, 1.0f);
    }
    public void Select()
    {
        //SelectControllerに選択結果を送っている
        selectController.allplayer_selectstate[player_num - 1] = selectstate;
        selectController.Lottery();
        //Debug.Log(selectController.allplayer_selectstate[player_num - 1]);
       //FadeManager.Instance.LoadScene(SceneName.GAME_SCENE,1.0f);
    }
}
