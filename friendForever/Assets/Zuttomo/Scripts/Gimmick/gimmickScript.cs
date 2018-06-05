﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class gimmickScript : MonoBehaviour {
    [SerializeField]
    private GameObject[] player;
    [SerializeField]
    private GameObject[] gimmickItem;
	[SerializeField]
	private GameObject door;
	public int[] GetGimmickItem_word = new int[7];		//獲得された文字の種類
	public int[] GetGimmickItem_player = new int[3];	//各プレイヤーの獲得文字数
	public int GamePhase;								//ゲームのフェーズ
	public float ItemPosition_x;
	public float ItemPosition_z;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < 7; i++) {
			float x = UnityEngine.Random.Range(-System.Math.Abs(ItemPosition_x), ItemPosition_x);
			float z = UnityEngine.Random.Range(-System.Math.Abs(ItemPosition_z), ItemPosition_z);
			Instantiate(gimmickItem[i], new Vector3(x, 1.3f, z), Quaternion.identity);
        }
		//Instantiate(door, new Vector3( UnityEngine.Random.Range(-System.Math.Abs(ItemPosition_z), ItemPosition_x), 0f, UnityEngine.Random.Range(-System.Math.Abs(ItemPosition_z), ItemPosition_z) ), Quaternion.identity);
		Instantiate(door, new Vector3( -3, 0f, 7 ), Quaternion.identity);

		for (int i = 0; i < 5; i++) {
			float x = UnityEngine.Random.Range(-System.Math.Abs(ItemPosition_x), ItemPosition_x);
			float z = UnityEngine.Random.Range(-System.Math.Abs(ItemPosition_z), ItemPosition_z);
			Instantiate(gimmickItem[UnityEngine.Random.Range(0, 6)], new Vector3(x, 1.3f, z), Quaternion.identity);
		}

		GetGimmickItem_word = new int[] { 0, 0, 0, 0, 0, 0, 0 };
		GetGimmickItem_player = new int[] { 0, 0, 0 };
		GamePhase = 0;
	}

	// Update is called once per frame
	void Update () {
		
	}

    public void GetWord() {
		if (GetGimmickItem_player [0] + GetGimmickItem_player [1] + GetGimmickItem_player [2] == 7) {
			GamePhase = 1;

			int max = GetGimmickItem_player.Select(s => s).Max();
			for (int i = 0; i < 3; i++) {
				if (GetGimmickItem_player [i] >= max) {
					GetGimmickItem_player [i] = 1;
				} else {
					GetGimmickItem_player [i] = 0;
				}
			}
		}
    }
}
