﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonMove : RunnerCore 
{
    
    public GameObject m_camera;
    RunnerInput m_runnerInput;


    private void Awake()
    {
        m_runnerInput = GetComponent<RunnerInput>();
    }

    public void Move()
    {
        float horizontal = m_runnerInput.Laxis_x * m_status.speed * Time.deltaTime;
        float virtical = m_runnerInput.Laxis_y * m_status.speed * Time.deltaTime;
        PlayerRotation(horizontal, virtical);
        PlayerAnimation(horizontal, virtical);
    }

    void PlayerRotation(float horizontal, float virtical)
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(m_camera.transform.forward, new Vector3(1, 0, 1)).normalized;
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * virtical + m_camera.transform.right * horizontal;

        if (moveForward != Vector3.zero)
        {
            //カメラを向きを前に移動する
            transform.position += cameraForward * virtical + m_camera.transform.right * horizontal;
            //体の向きを変更
            transform.rotation = Quaternion.LookRotation(moveForward);
            //PlayerのAnimation管理
            PlayerAnimation(horizontal, virtical);
        }
    }

    void PlayerAnimation(float h, float v)
    {
        if (m_runnerInput.Laxis_y >= 0.1f || m_runnerInput.Laxis_y <= -0.1f || m_runnerInput.Laxis_x >= 0.1f || m_runnerInput.Laxis_x <= -0.1f)
        {
            m_status.animator.SetBool("Run", true);
        }
        else
        {
            m_status.animator.SetBool("Run", false);
        }
    }

    public void DemonButton()
    {

        if (m_runnerInput.button_A == true)
        {
           
        }

        if (m_runnerInput.button_B == true)
        {
            Debug.Log("決定");
        }

        if (m_runnerInput.button_X == true)
        {
            
        }

        if (m_runnerInput.button_Y == true)
        {
            Debug.Log("Y");
        }
    }
	
}
