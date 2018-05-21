using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerMove : RunnerCore
{

    public GameObject m_camera;
    public GameObject m_collider;
    Renderer rend;
    RunnerInput runnerInput;
    public float timar;
    public float State_timar;

    private void Awake()
    {
        runnerInput = GetComponent<RunnerInput>();
        rend = GetComponent<Renderer>();
    }

    public void Move()
    {
        if (m_status.isState == true)
        {
            float horizontal = runnerInput.Laxis_x * m_status.speed * Time.deltaTime;
            float virtical = runnerInput.Laxis_y * m_status.speed * Time.deltaTime;
            rend.material.color = Color.white;
            PlayerRotation(horizontal, virtical);
            HealthControll();
        } else {
            State_timar += Time.deltaTime;
            if(State_timar >= 3)
            {
                m_status.isState = true;
            }
        }
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
            //PlayerAnimation(horizontal, virtical);
        }
    }

    //void PlayerAnimation(float h, float v)
    //{
        //if (m_status.speed <= m_status.firstSpeed)
        //{
        //    m_status.animator.SetBool("Walk", true);
        //    m_status.animator.SetBool("Run", false);
        //}
        //else if (m_status.speed >= m_status.firstSpeed)
        //{
        //    m_status.animator.SetBool("Run", true);
        //    m_status.animator.SetBool("Walk", false);
        //}
        //else
        //{
        //    m_status.animator.SetBool("Walk", false);
        //    m_status.animator.SetBool("Run", false);
        //}
    //}

    void HealthControll()
    {

        if (m_status.isHealth == true)
        {
            if (runnerInput.button_RB == true)
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

        if (m_status.health > 5f)
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
        if (runnerInput.button_RB == false)
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

        if (runnerInput.button_A == true)
        {
            Debug.Log("突き飛ばし");
            m_collider.SetActive(true);
            timar = 0;
        } else {
            if(timar <= 0.5){
                timar += Time.deltaTime;
                m_collider.SetActive(false);
            }
        }

        if (runnerInput.button_B == true)
        {
            Debug.Log("決定");
        }

        if (runnerInput.button_X == true)
        {
            Debug.Log("アイテム使用");
        }

        if (runnerInput.button_Y == true)
        {
            Debug.Log("Y");
        }
    }

    private void OnCollisionEnter(Collision hit)
    {
        if(hit.gameObject.tag == "Push"){
            m_status.isState = false;
            rend.material.color = Color.blue;
            Debug.Log("当たった");
            State_timar = 0;
        }
    }
}
