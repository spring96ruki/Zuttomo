﻿using System;
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

    void Start()
    {
       
    }

    public void UIStart()
    {
        Debug.Log("for1");
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
            float health = playerList[i].GetComponent<RunnerStatus>().health;
            float maxHealth = playerList[i].GetComponent<RunnerStatus>().maxHealth;
            m_UI1List[i].GetComponent<Image>().fillAmount = health / maxHealth;
        }
    }

    public void ItemUIControll(int ItemNum,int playerNum)
    {
        Debug.Log("test" + ItemNum);
        m_UI2List[playerNum - 1].sprite = itemList[ItemNum - 1];
    }

    internal void StanOn(float m_coolTime, float m_maxCoolTime)
    {
        m_UI1List[m_Chasernum].fillAmount = (m_maxCoolTime - m_coolTime) / m_maxCoolTime;
        Debug.Log("スタン");
    }

    

    internal void InvisibleOn(float m_coolTime,float m_maxCoolTime)
    {
        Debug.Log("インビ");
        Debug.Log("cool:" + m_coolTime);
        m_UI2List[m_Chasernum].fillAmount = (m_maxCoolTime - m_coolTime) / m_maxCoolTime;
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