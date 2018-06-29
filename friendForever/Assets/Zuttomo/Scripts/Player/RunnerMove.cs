using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunnerMove : MonoBehaviour
{
    public GameObject m_camera;
    public GameObject m_Push;
    public GameObject m_item;
    public Transform m_FiringPosition;
    public Image m_healthUI;
    public float m_healthTime = 5f;
    [HideInInspector]
    public float m_timer;
    RunnerInput m_runnerInput;
    RunnerStatus m_status;
	RunnerSkill m_runnerSkill;
    RunnerAnimator m_animaton;
    [HideInInspector]
    Rigidbody m_rigidbody;
    float m_coolTime;
    [SerializeField]
    float m_moveSpeed;

    private void Awake()
    {
        m_runnerInput = GetComponent<RunnerInput>();
        m_status = GetComponent<RunnerStatus>();
		m_runnerSkill = GetComponent<RunnerSkill>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_animaton = GetComponent<RunnerAnimator>();
    }

	public void Move()
	{
		float horizontal = m_runnerInput.Laxis_x * m_status.speed * Time.deltaTime;
		float virtical = m_runnerInput.Laxis_y * m_status.speed * Time.deltaTime;
        PlayerRotation(horizontal, virtical);
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

    void HealthControll()
	{
		if (this.GetComponent<RunnerController> ().ChaserFlag == true) {
			m_status.speed = m_status.maxSpeed;
		} else { 
			if (m_status.isHealth == true) {
                
				if (m_runnerInput.button_RB == true) {
					Debug.Log ("ダッシュ");
					m_status.speed = m_status.maxSpeed;
					m_status.health -= Time.deltaTime;
				}

			} else {
                
				m_status.speed = m_status.firstSpeed;
			}

			if (m_status.health > m_status.maxHealth) {
				m_status.isHealth = true;
			}

			if (m_status.health <= 0f) {
				m_status.isHealth = false;
			}
			
			if (m_status.health >= m_status.maxHealth) {
				m_status.health = m_status.maxHealth;
			}

			//ボタンが押されてなかったら
			if (m_runnerInput.button_RB == false) {
				m_status.speed = m_status.firstSpeed;
                //スタミナ回復
                m_status.health += Time.deltaTime;
			}
		}
	}

    public void Button()
    {

        if (m_runnerInput.button_A == true)
        {
            Debug.Log("突き飛ばし");
            m_animaton.PushAnimatio();
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
