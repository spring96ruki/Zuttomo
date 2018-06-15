using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour {

<<<<<<< HEAD
    public static int Chaserplayer = 0;
=======
    public static int player = 0;
>>>>>>> 8e6a8cd604ae9ac80e4c157e21cacd1362b9ec3e
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
