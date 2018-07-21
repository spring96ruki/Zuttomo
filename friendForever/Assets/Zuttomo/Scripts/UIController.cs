using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    private int m_setitemNum;

    public int m_Chasernum;
    public List<Sprite> itemList = new List<Sprite>();
    public GameObject[] playerList;
    public List<Image> m_UI1List = new List<Image>();
    public List<Image> m_UI2List = new List<Image>();
    public GameObject[] Result;
    public Text[] ResultText1;
    public Text[] ResultText2;
    public Text[] ResultText3;

    void Start()
    {
       
    }

    public void UIStart()
    { 
        m_Chasernum = SelectController.GetChaserplayer() - 1;
        Debug.Log("Pn:" + m_Chasernum);
        for (int i = 0; i < 4; i++)
        {
            playerList[i] = GameObject.Find("Player" + (i + 1)).transform.GetChild(0).gameObject;
            if (m_Chasernum == i)
            {
                GameObject.Find("Player" + (i + 1) + "Stan").SetActive(true);
                GameObject.Find("Player" + (i + 1) + "Invisible").SetActive(true);
                m_UI1List[i] = GameObject.Find("Player" + (i + 1) + "Stanforward").GetComponent<Image>();
                m_UI2List[i] = GameObject.Find("Player" + (i + 1) + "Invisibleforward").GetComponent<Image>();
                GameObject.Find("Player" + (i + 1) + "Stamina").SetActive(false);
                GameObject.Find("Player" + (i + 1) + "Item").SetActive(false);
            }
            else
            {
                GameObject.Find("Player" + (i + 1) + "Stamina").SetActive(true);
                GameObject.Find("Player" + (i + 1) + "Item").SetActive(true);
                m_UI1List[i] = GameObject.Find("Player" + (i + 1) + "Staminaforward").GetComponent<Image>();
                m_UI2List[i] = GameObject.Find("Player" + (i + 1) + "Item").GetComponent<Image>();
                GameObject.Find("Player" + (i + 1) + "Stan").SetActive(false);
                GameObject.Find("Player" + (i + 1) + "Invisible").SetActive(false);
            }
        }
    }

    public void HealthUIControll()
    {
        for(int i = 0; i < 4; i++)
        {
            float health = playerList[i].GetComponent<PlayerStatus>().health;
            float maxHealth = playerList[i].GetComponent<PlayerStatus>().maxHealth;
            m_UI1List[i].GetComponent<Image>().fillAmount = health / maxHealth;
        }
    }

    public void ItemUIControll(int ItemNum,int playerNum)
    {
        Debug.Log("test" + ItemNum);
        m_UI2List[playerNum - 1].sprite = itemList[ItemNum - 1];
    }

    internal void StanOn(float m_stanCoolTime, float m_maxStanCoolTime)
    {
        m_UI1List[m_Chasernum].fillAmount = (m_maxStanCoolTime - m_stanCoolTime) / m_maxStanCoolTime;
        Debug.Log("スタン");
    }

    

    internal void InvisibleOn(float m_invisibleCoolTime, float m_maxInvisibleCoolTime)
    {
        Debug.Log("インビ");
        //Debug.Log("cool:" + m_coolTime);
        m_UI2List[m_Chasernum].fillAmount = (m_maxInvisibleCoolTime - m_invisibleCoolTime) / m_maxInvisibleCoolTime;
        //Debug.Log(m_UI2List[m_Chasernum].fillAmount = (m_maxInvisibleCoolTime - m_invisibleCoolTime) / m_maxInvisibleCoolTime);
    }

    public Image GetStanImage()
    {
        return m_UI1List[m_Chasernum];
    }

    public Image GetInvisibleImage()
    {
        return m_UI2List[m_Chasernum];
    }
}