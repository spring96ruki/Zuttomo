using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public GameObject m_chaserImage;
    public GameObject m_runnerImage;
    public GameObject m_player;
    public Image m_healthUI;
    public Image m_item;
    public Image img;
    public Image m_stan;
    public Image m_invisible;
    private int m_setitemNum;
    public List<Sprite> itemList = new List<Sprite>();
    RunnerInput m_runnerInput;
    RunnerStatus m_status;
    RunnerController m_runnerController;
    RunnerSkill m_runnerSkill;





    void Awake()
    {

        m_status = m_player.GetComponent<RunnerStatus>();
        m_runnerSkill = m_player.GetComponent<RunnerSkill>();
        m_runnerController = m_player.GetComponent<RunnerController>();
        img = m_item.GetComponent<Image>();
        Debug.Log(img);
        //Debug.Log(img.sprite);


    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UICheck()
    {
        if (m_player.GetComponent<RunnerController>().ChaserFlag == true)
        {
            m_chaserImage.SetActive(true);
            m_runnerImage.SetActive(false);
        }
        else
        {
            m_chaserImage.SetActive(false);
            m_runnerImage.SetActive(true);
        }
    }

    public void HealthUIControll()
    {
        m_healthUI.fillAmount = m_status.health / m_status.maxHealth;
    }

    public void ItemUIControll(int ItemNum)
    {
        Debug.Log("test" + ItemNum);
        img.sprite = itemList[ItemNum - 1];
    }

    internal void stanOn(float m_coolTime, float m_maxCoolTime)
    {
        Debug.Log("スタン");
        m_stan.fillAmount = 0;
        m_stan.fillAmount = (m_maxCoolTime - m_coolTime) / m_maxCoolTime;
    }

    //public void InvisibleOn()
    //{
    //    Debug.Log("インビ");
    //    m_invisible.fillAmount = 0;
    //}

    internal void InvisibleOn(float m_coolTime,float m_maxCoolTime)
    {
        Debug.Log("インビ");
        Debug.Log("cool:"+m_coolTime);
        m_invisible.fillAmount = (m_maxCoolTime - m_coolTime) / m_maxCoolTime;
    }
}