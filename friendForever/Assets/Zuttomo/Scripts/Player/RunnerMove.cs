using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunnerMove : MonoBehaviour
{
    Animator animator;
    public GameObject m_camera;
    public GameObject m_Push;
    public GameObject m_item;
    public Transform m_FiringPosition;
    public float m_healthTime = 5f;

    public Transform m_player;

    public float m_itemspeed = 1000;
    [HideInInspector]
    public float m_timer;
    [HideInInspector]
    public float m_buffTimer;
    [HideInInspector]
    public int m_itemNum;

    RunnerInput m_runnerInput;
    RunnerStatus m_status;
    [HideInInspector]
    Rigidbody m_rigidbody;
    public float m_bufftimer;
    float m_coolTime;

    UIController m_uIController;

    private Vector3 m_prevPos;

    private void Awake()
    {
        m_runnerInput = GetComponent<RunnerInput>();
        m_status = GetComponent<RunnerStatus>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_uIController = GetComponent<UIController>();
    }

	public void Move()
	{
		float horizontal = m_runnerInput.Laxis_x * m_status.speed * Time.deltaTime;
		float virtical = m_runnerInput.Laxis_y * m_status.speed * Time.deltaTime;
		PlayerRotation(horizontal, virtical);
		PlayerAnimation(horizontal, virtical);
        KillPlayerAnimation();
        HealthControll();
        m_prevPos = transform.position;
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
			if (m_status.speed <= m_status.firstSpeed)
			{
				m_status.animator.SetBool("HalfRun", true);
				m_status.animator.SetBool("FullRun", false);
			}
			else if (m_status.speed >= m_status.firstSpeed)
			{
				m_status.animator.SetBool("FullRun", true);
			}
		} else
		{
			m_status.animator.SetBool("HalfRun", false);
			m_status.animator.SetBool("FullRun", false);
		}
	}

    void KillPlayerAnimation()
    {
        if (this.GetComponent<RunnerController>().ChaserFlag == true)
        {
            if (m_runnerInput.button_B == true)
            {
                m_status.animator.SetBool("Kill", true);
                m_coolTime = 0;
            }
            m_coolTime += Time.deltaTime;
            if(m_coolTime >= 3)
            {
                m_status.animator.SetBool("Kill", false);
            }

        }
    }

    public void HealthControll()
    {
        if (this.GetComponent<RunnerController>().ChaserFlag == true)
        {
            //m_status.speed = m_status.maxSpeed;
            //m_status.health -= Time.deltaTime;
            //m_healthUI.fillAmount -= 1f * Time.deltaTime;
        }
        else
        {
            if (m_status.isHealth == true)
            {
                if (m_runnerInput.Laxis_y >= 0.1f || m_runnerInput.Laxis_y <= -0.1f || m_runnerInput.Laxis_x >= 0.1f || m_runnerInput.Laxis_x <= -0.1f)
                {
                    if (m_runnerInput.button_RB == true)
                    {
                        Debug.Log("ダッシュ");
                        m_status.speed = m_status.maxSpeed;
                        m_status.health -= Time.deltaTime;
                        m_uIController.m_healthUI.fillAmount = m_status.health / m_status.maxHealth;
                    }
                }
            }
            else
            {
                m_status.speed = m_status.firstSpeed;
            }

            if (m_status.health > m_status.maxHealth)
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
                m_uIController.m_healthUI.fillAmount = m_status.health / m_status.maxHealth;
            }
            if (m_status.health >= m_status.maxHealth)
            {
                m_status.health = m_status.maxHealth;
            }
            
            //ボタンが押されてなかったら
            if (m_runnerInput.button_RB == false || m_prevPos == transform.position)
            {
                
                m_status.speed = m_status.firstSpeed;
                //スタミナ回復
                Debug.Log("1_" + m_status.health);
                m_status.health += Time.deltaTime;
                Debug.Log("2_" + m_status.health);
                m_uIController.m_healthUI.fillAmount = m_status.health / m_status.maxHealth;

            }
        }
    }

    public void Button()
    {

        if (m_runnerInput.button_A == true)
        {
            Debug.Log("突き飛ばし");
            m_Push.SetActive(true);
            m_timer = 0;
        }
        else
        {
            if (m_timer <= 0.5)
            {
                m_timer += Time.deltaTime;
                m_Push.SetActive(false);
            }
        }

        if (m_runnerInput.button_B == true)
        {
            Debug.Log("決定");
        }

        if (m_runnerInput.button_X == true)
        {
            if (m_status.ishave == true)
            {
                switch (m_itemNum)
                {
                    case 1:
                        Debug.Log("市松人形を投げたよ");
                        Vector3 force;
                        GameObject bullets = Instantiate(m_item) as GameObject;
                        force = this.gameObject.transform.forward * m_itemspeed;
                        // Rigidbodyに力を加えて発射
                        bullets.GetComponent<Rigidbody>().AddForce(force);
                        // アイテムの位置を調整
                        bullets.transform.position = m_FiringPosition.position;
                        bullets.tag = "Itimathu";
                        m_status.ishave = false;

                        break;

                    case 2:
                        Debug.Log("力が上がったよ");
                        m_status.ishave = false;
                        break;

                    case 3:
                        Debug.Log("無敵");
                        break;
                }             
             }

        }

        if (m_runnerInput.button_Y == true)
        {
            Debug.Log("Y");
        }
    }
}
