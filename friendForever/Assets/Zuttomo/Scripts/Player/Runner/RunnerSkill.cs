using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSkill {

    public GameObject m_runnerArea;

    UIController m_uiController;
    PlayerStatus m_runnerStatus;
    PlayerAnimator m_runnerAnimator;
    PlayerInput m_playerInput;

    float m_eventTime;

    public int m_itemNum;
    float m_itemspeed = 1000;

	public void HitEvent(Collision hit,  bool isState)
    {
        switch(hit.gameObject.tag)
        {
            case TagName.Push:
                isState = false;
                Debug.Log("当たった");
                break;
            case TagName.Itimatu:
                Debug.Log("当たった");
                isState = false;
                //Destroy(hit.gameObject);
                break;
        }

        if(hit.gameObject.name == "RunnerArea")
        {
            isState = false;
            Debug.Log("当たった");
        }
    }

    public void CheckEvent(Collision check , PlayerStatus playerStatus, int playerNum)
    {
        if (playerStatus.isHave == false)
        {
            switch (check.gameObject.name)
            {
                case ItemName.itimatu:
                    Debug.Log("市松人形だよ");
                    if (m_playerInput.button_B == true)
                    {
                        //アイテムを持ったらtrueに変更
                        playerStatus.isHave = true;
                        //アイテムの番号を1に変更
                        m_itemNum = 1;
                        m_uiController.ItemUIControll(m_itemNum, playerNum);
                        //拾ったアイテムを消去
                        //Destroy(check.gameObject);
                    }
                    break;

                case ItemName.Drug:
                    Debug.Log("薬だよ");
                    if (m_playerInput.button_B == true)
                    {
                        //アイテムを持ったらtrueに変更
                        playerStatus.isHave = true;
                        //アイテムの番号を2に変更
                        m_itemNum = 2;
                        m_uiController.ItemUIControll(m_itemNum, playerNum);
                        //拾ったアイテムを消去
                        //Destroy(check.gameObject);
                    }
                    break;

                case ItemName.Ohuda:
                    Debug.Log("お札だよ");
                    if (m_playerInput.button_B == true)
                    {
                        //アイテムを持ったらtrueに変更
                        playerStatus.isHave = true;
                        //アイテムの番号を3に変更
                        m_itemNum = 3;
                        m_uiController.ItemUIControll(m_itemNum, playerNum);
                        //拾ったアイテムを消去
                        //Destroy(check.gameObject);
                    }
                    break;
            }
        }
    }

	public void ItemEvent(PlayerStatus playerStatus, int playerNum)
	{
        if (playerStatus.isHave == true)
        {
            switch (m_itemNum)
            {
                case 1:
                    Debug.Log("市松人形を投げたよ");
                    m_uiController.ItemUIControll(4, playerNum);
                    m_runnerAnimator.ThrowAnimation();
                    break;

                case 2:
                    Debug.Log("力が上がったよ");
                    m_runnerAnimator.PillAnimation();
                    playerStatus.isHave = false;
                    break;

                case 3:
                    Debug.Log("無敵");
                    m_runnerAnimator.BarrierAnimation();
                    playerStatus.isHave = false;
                    break;
            }
        }
	}

    public void ItimatuEvent(GameObject myObject , PlayerMove playerMove , PlayerStatus playerStatus)
    {
        Vector3 force;
        //GameObject bullets = Instantiate(playerMove.m_item) as GameObject;
        force = myObject.transform.forward * m_itemspeed;
        // Rigidbodyに力を加えて発射
        //bullets.GetComponent<Rigidbody>().AddForce(force);
        // アイテムの位置を調整
        //bullets.transform.position = m_runnerMove.m_FiringPosition.position;
        //bullets.tag = "itimatu";
        playerStatus.isHave = false;
    }

    IEnumerator DrugEvent(int playerNum, PlayerStatus playerStatus)
    {
        Debug.Log("発動したよ");
        float m_buffTimer;
        bool isBuff = true;
        //while文を指定した回数ループする
        m_buffTimer = m_eventTime;
        while (m_buffTimer > 0)
        {
            m_buffTimer -= Time.deltaTime;
            playerStatus.health = playerStatus.maxHealth;
            m_uiController.m_UI2List[playerNum - 1].fillAmount = m_buffTimer / m_eventTime;

            yield return null;
        }
        m_uiController.m_UI2List[playerNum - 1].fillAmount = 1;
        m_uiController.ItemUIControll(4, playerNum);
    }

    IEnumerator Invincible(GameObject myObject , int playerNum)
    {
        Debug.Log("発動したよ");
        //レイヤーをPlayerInvincibleに変更
        myObject.layer = LayerMask.NameToLayer("PlayerInvincible");
        m_runnerArea.SetActive(true);
        //while文を指定した回数ループする
        float m_invincibleTime = m_eventTime;
        while (m_invincibleTime > 0)
        {
            m_uiController.m_UI2List[playerNum - 1].fillAmount = m_invincibleTime / m_eventTime;
            m_invincibleTime -= Time.deltaTime;
            yield return null;
        }
        m_runnerArea.SetActive(false);
        m_uiController.m_UI2List[playerNum - 1].fillAmount = 1;
        m_uiController.ItemUIControll(4, playerNum);
        //レイヤーをPlayerに戻す
        myObject.layer = LayerMask.NameToLayer("Player");
        Debug.Log("効果が切れたよ");
    }
}
