﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static int m_getChasernum;
	public GameObject playerandcamera;
	public float ItemPosition_x;
	public float ItemPosition_z;
	public float rect_x;
	public float rect_y;

    // Use this for initialization
    void Start () {
		int m_getChaserNum = SelectController.GetChaserplayer();

		for(int i = 0;i < 4;i++){
			float x = UnityEngine.Random.Range(-System.Math.Abs(ItemPosition_x), ItemPosition_x);
			float z = UnityEngine.Random.Range(-System.Math.Abs(ItemPosition_z), ItemPosition_z);
            //var PlayerAndCamera = Instantiate(playerandcamera, new Vector3(x, 1.3f, z), Quaternion.identity)/* as GameObject*/; 
            GameObject PlayerAndCamera = Instantiate(playerandcamera, new Vector3(x, 1.3f, z), Quaternion.identity);
			PlayerAndCamera.name = ("Player" + (i+1));
			var player = PlayerAndCamera.transform.GetChild(0).gameObject;
			var camera = PlayerAndCamera.transform.GetChild(1).gameObject;

			if (i == 0 || i == 1) {
				rect_x = 0f;
			} else {
				rect_x = 0.5f;
			}

			if (i == 0 || i == 2) {
				rect_y = 0.5f;
			} else {
				rect_y = 0f;
			}
			camera.GetComponent<Camera>().rect = new Rect(rect_x, rect_y, 0.5f, 0.5f);

			player.GetComponent<RunnerInput>().runnerNum = i+1;

			if (m_getChasernum == i) {
				player.GetComponent<RunnerController>().ChaserFlag = true;
			}
		}
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void EndGame()
    {

    }
}
