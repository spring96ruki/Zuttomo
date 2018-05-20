using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void SceneLoad()
    {
        //SceneController.Instance.LoadScene(SceneName.SELECT_SCENE);
        FadeManager.Instance.LoadScene(SceneName.SELECT_SCENE, 1.0f);
    }
}
