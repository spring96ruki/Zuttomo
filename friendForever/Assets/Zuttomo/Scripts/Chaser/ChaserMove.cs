using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserMove
{

    //public GameObject m_camera;
    //public GameObject m_touch;

    //RunnerInput m_runnerInput;
    //protected RunnerStatus m_status;
    float m_timer;


    private void Awake()
    {
        //m_runnerInput = GetComponent<RunnerInput>();
        //m_status = GetComponent<RunnerStatus>();
    }

    public void Move(RunnerStatus status, RunnerInput input)
    {
        float horizontal = input.Laxis_x * status.speed * Time.deltaTime;
        float virtical = input.Laxis_y * status.speed * Time.deltaTime;
        PlayerRotation(horizontal, virtical);
        PlayerAnimation(horizontal, virtical);
    }

    void PlayerRotation(GameObject chaser ,GameObject chaserCamera ,float horizontal, float virtical)
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(chaserCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * virtical + chaserCamera.transform.right * horizontal;

        if (moveForward != Vector3.zero)
        {
            //カメラを向きを前に移動する
            chaser.transform.position += cameraForward * virtical + chaserCamera.transform.right * horizontal;
            //体の向きを変更
            chaser.transform.rotation = Quaternion.LookRotation(moveForward);
            //PlayerのAnimation管理
            PlayerAnimation(horizontal, virtical);
        }
    }

    void PlayerAnimation(RunnerInput input, RunnerStatus status, float h, float v)
    {
        if (input.Laxis_y >= 0.1f || input.Laxis_y <= -0.1f || input.Laxis_x >= 0.1f || input.Laxis_x <= -0.1f)
        {
            status.animator.SetBool("Run", true);
        }
        else
        {
            status.animator.SetBool("Run", false);
        }
    }

    public void ChaserButton(RunnerInput input)
    {

        if (input.button_A == true)
        {
            Debug.Log("スキル_1");
        }

        if (input.button_B == true)
        {
            Debug.Log("タッチ");
            m_touch.SetActive(true);
            m_timer = 0;
        }
        else
        {
            if (m_timer <= 0.5)
            {
                m_timer += Time.deltaTime;
                m_touch.SetActive(false);
            }
        }

        if (input.button_X == true)
        {
            Debug.Log("スキル_2");
        }

        if (input.button_Y == true)
        {
            Debug.Log("Y");
        }
    }
}
