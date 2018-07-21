﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour {
    private PlayerInput m_playerInput;
    public SelectController selectController;
    public GameObject RunnerSprite;
    public GameObject ChaserSprite;
    public GameObject SubmitSprite;
    public int player_num;          //プレイヤー番号
    public int selectstate;         //現在選んでる何を選んでるか 
                                    //0で何も選んでない状態
                                    //1でRunnerを選んでる状態
                                    //2でChaserを選んでる状態

    private float scale;            //画像の大きさ
    public float scaleChangeSpeed;  //画像のサイズを変える速さ
    public bool PushSubmit;         //決定ボタンが押されてるか

    // Use this for initialization
    void Awake () {
        selectstate = 0;
        scaleChangeSpeed = 5f;
        scale = 30f;
        PushSubmit = false;
        SubmitSprite.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        m_playerInput.PController(player_num);

        //何かボタンがをされたらその押されたボタンに対応する処理をし、SelectControllerの情報を変更する
        if (m_playerInput.button_A || m_playerInput.button_B || m_playerInput.button_Y || m_playerInput.button_X || Mathf.Abs(m_playerInput.Laxis_x) >= 0.7)
        {
            if (PushSubmit == false)
            {
                if (m_playerInput.Laxis_x >= 0.7)
                {
                    selectstate = 2;
                }
                else if (m_playerInput.Laxis_x <= -0.7)
                {
                    selectstate = 1;
                }

                if (m_playerInput.button_B == true && selectstate != 0)
                {
                    PushSubmit = true;
                    selectController.m_playerCount++;
                    SubmitSprite.SetActive(true);
                }
            } else if (m_playerInput.button_A == true && selectstate != 0)
            {
                PushSubmit = false;
                selectController.m_playerCount--;
                SubmitSprite.SetActive(false);
            }

            selectController.allPlayerSelectState[player_num - 1] = selectstate;
            selectController.Lottery();
        }

        //選んでる画像を大きくしたり小さくしたりする
        if (selectstate == 1)
        {
            if (scale < 45f)
            {
                scale += scaleChangeSpeed;
            }
            else
            {
                scale = 45f;
            }
        }else if(selectstate == 2)
        {
            if (scale > 15f)
            {
                scale -= scaleChangeSpeed;
            }
            else
            {
                scale = 15f;
            }
        }

        RunnerSprite.transform.localScale = new Vector3(scale, scale, 1);
        ChaserSprite.transform.localScale = new Vector3(60f - scale, 60f - scale, 1);
    }
}
