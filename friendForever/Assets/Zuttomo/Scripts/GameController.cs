using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonMono<GameController> {

    int getdemon_num;


    void Start()
    {
        getdemon_num = SelectController.Getdemonplayer();
        Debug.Log(getdemon_num);
    }

    // Update is called once per frame
    void Update () {
        
    }
}
