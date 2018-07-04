using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunnerMove : MonoBehaviour
{
    public GameObject m_camera;
    public GameObject m_Push;
    public GameObject m_item;
    public GameObject m_UIController;
    public Transform m_FiringPosition;
    public float m_healthTime = 5f;
    public float m_itemspeed = 1000;
    [HideInInspector]
    public float m_timer;
    RunnerInput m_runnerInput;
    RunnerStatus m_runnerStatus;
	RunnerSkill m_runnerSkill;
    RunnerAnimator m_animaton;
    [HideInInspector]
    Rigidbody m_rigidbody;
    float m_coolTime;
    UIController m_uIController;

    private Vector3 m_prevPos;
    [SerializeField]
    float m_moveSpeed;

    private void Awake()
    {
        m_uIController = GameObject.Find("UIController").GetComponent<UIController>();
        m_runnerInput = GetComponent<RunnerInput>();
        m_runnerStatus = GetComponent<RunnerStatus>();
		m_runnerSkill = GetComponent<RunnerSkill>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_animaton = GetComponent<RunnerAnimator>();
        //m_uIController = GetComponent<UIController>();
    }

	public void Move()
	{
        //Debug.Log("MOVE_INPUT" + m_runnerInput.button_RB);
		float horizontal = m_runnerInput.Laxis_x * m_runnerStatus.speed * Time.deltaTime;
		float virtical = m_runnerInput.Laxis_y * m_runnerStatus.speed * Time.deltaTime;
        PlayerRotation(horizontal, virtical);
        //Debug.Log("MOVE_INPUT" + m_runnerInput.button_RB);
        HealthControll();
        KillPlayerAnimation();
        m_animaton.MoveAnimation(horizontal, virtical);
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
		}
	}

    void KillPlayerAnimation()
    {
        if (this.GetComponent<RunnerController>().ChaserFlag == true)
        {
            if (m_runnerInput.button_B == true)
            {
                m_runnerStatus.animator.SetBool("Kill", true);
                m_coolTime = 0;
            }
            m_coolTime += Time.deltaTime;
            if(m_coolTime >= 3)
            {
                m_runnerStatus.animator.SetBool("Kill", false);
            }

        }
    }

    void HealthControll()
	{
        Debug.Log("----MOVE_INPUT" + m_runnerInput.button_RB + "   " + this.gameObject.transform.parent.gameObject.name);
        if (this.GetComponent<RunnerController> ().ChaserFlag == true) {
			m_runnerStatus.speed = m_runnerStatus.maxSpeed;
		} else {
            Debug.Log("MOVE_INPUT" + m_runnerInput.button_RB + "   " + this.gameObject.transform.parent.gameObject.name);
            if (m_runnerStatus.isHealth == true) {
                if (m_runnerInput.Laxis_y >= 0.1f || m_runnerInput.Laxis_y <= -0.1f || m_runnerInput.Laxis_x >= 0.1f || m_runnerInput.Laxis_x <= -0.1f)
                {
                    //Debug.Log("MOVE_INPUT" + m_runnerInput.button_RB);
                    //Debug.Log("RBDASH" + m_runnerInput.button_RB);
                    if (m_runnerInput.button_RB == true)
                    {
                        Debug.Log("ダッシュ");
                        m_runnerStatus.speed = m_runnerStatus.maxSpeed;
                        m_runnerStatus.health -= Time.deltaTime;
                    }
                }   

			} else {
				m_runnerStatus.speed = m_runnerStatus.firstSpeed;
			}

			if (m_runnerStatus.health > m_runnerStatus.maxHealth) {
				m_runnerStatus.isHealth = true;
			}

			if (m_runnerStatus.health <= 0f) {
				m_runnerStatus.isHealth = false;
			}
			

			if (m_runnerStatus.health >= m_runnerStatus.maxHealth) {
				m_runnerStatus.health = m_runnerStatus.maxHealth;
			}

			//ボタンが押されてなかったら
			if (m_runnerInput.button_RB == false || m_prevPos == transform.position) {
				m_runnerStatus.speed = m_runnerStatus.firstSpeed;
                //スタミナ回復
                m_runnerStatus.health += Time.deltaTime;
                m_uIController.HealthUIControll();
            }
		}
	}

    public void Button()
    {
        if (m_animaton.m_action == false)
        {
            if (m_runnerInput.button_A == true)
            {
                Debug.Log("突き飛ばし");
                m_animaton.PushAnimation();
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
                m_runnerSkill.ItemEvent();
            }

            if (m_runnerInput.button_Y == true)
            {
                Debug.Log("Y");
            }
        }
    }
}
