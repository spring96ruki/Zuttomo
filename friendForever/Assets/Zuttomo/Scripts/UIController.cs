using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour {

    public Image m_healthUI;
    RunnerInput m_runnerInput;
    RunnerStatus m_status;

    void Awake()
    {
        m_runnerInput = GetComponent<RunnerInput>();
        m_status = GetComponent<RunnerStatus>();
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void HealthUIControll()
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
                if (m_runnerInput.button_RB == true)
                {
                    Debug.Log("ダッシュ");
                    m_status.speed = m_status.maxSpeed;
                    m_status.health -= Time.deltaTime;
                    m_healthUI.fillAmount = m_status.health / m_status.maxHealth;
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
                m_healthUI.fillAmount = m_status.health / m_status.maxHealth;
            }
            if (m_status.health >= m_status.maxHealth)
            {
                m_status.health = m_status.maxHealth;
            }

            //ボタンが押されてなかったら
            if (m_runnerInput.button_RB == false)
            {
                m_status.speed = m_status.firstSpeed;
                //スタミナがのっこていたら
                if (m_status.health >= 0f)
                {
                    //スタミナ回復
                    m_status.health += Time.deltaTime;
                    m_healthUI.fillAmount = m_status.health / m_status.maxHealth;
                }
            }

            //if (m_status.isBuff == false)
            //{
            //    m_status.maxHealth = 5;
            //    m_status.maxSpeed = 10;
            //}
            //else
            //{
            //    m_bufftimer += Time.deltaTime;
            //    m_status.maxHealth = 10;
            //    m_status.maxSpeed = 15;
            //    if (m_bufftimer > 4)
            //    {
            //        m_status.isBuff = false;
            //    }
            //}
        }
    }
}
