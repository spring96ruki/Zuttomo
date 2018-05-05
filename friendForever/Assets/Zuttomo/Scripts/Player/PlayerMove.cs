using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerCore {

    public GameObject m_camera;

    PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void Move()
    {
        float horizontal = playerInput.Laxis_x * m_status.speed * Time.deltaTime;
        float virtical = playerInput.Laxis_y * m_status.speed * Time.deltaTime;
        PlayerRotation(horizontal, virtical);

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
            // PlayerのAnimation管理
            PlayerAnimation(horizontal, virtical);
        }
    }

    void PlayerAnimation(float h, float v)
    {
        if (m_status.speed <= m_status.firstSpeed)
        {
            m_status.animator.SetBool("Walk", true);
            m_status.animator.SetBool("Run", false);
        }
        else if (m_status.speed >= m_status.firstSpeed)
        {
            m_status.animator.SetBool("Run", true);
            m_status.animator.SetBool("Walk", false);
        }
        else
        {
            m_status.animator.SetBool("Walk", false);
            m_status.animator.SetBool("Run", false);
        }
    }

    void HealthControll()
    {

        if (m_status.isHealth == true)
        {
            if (playerInput.button_RB == true)
            {
                Debug.Log("ダッシュ");
                m_status.speed = m_status.maxSpeed;
                m_status.health += Time.deltaTime;
            }
        }
        else
        {
            m_status.speed = m_status.firstSpeed;
        }

        if (m_status.health > 0f)
        {
            m_status.isHealth = true;
        }

        if (m_status.health <= 0f)
        {
            m_status.isHealth = false;
        }
        //スタミナがなかったら
        if (m_status.isHealth == false)
        {
            //スタミナ回復
            m_status.health += Time.deltaTime;
        }
        if (m_status.health >= m_status.maxHealth)
        {
            m_status.health = m_status.maxHealth;
        }

        //ボタンが押されてなかったら
        if (playerInput.button_RB == false)
        {
            m_status.speed = m_status.firstSpeed;
            //スタミナがのっこていたら
            if (m_status.health >= 0f)
            {
                //スタミナ回復
                m_status.health += Time.deltaTime;
            }
        }
    }

    public void Button()
    {

        if (playerInput.button_A == true)
        {
            Debug.Log("A");
        }

        if (playerInput.button_B == true)
        {
            Debug.Log("B");
        }

        if (playerInput.button_X == true)
        {
            Debug.Log("X");
        }

        if (playerInput.button_Y == true)
        {
            Debug.Log("Y");
        }
    }
}
