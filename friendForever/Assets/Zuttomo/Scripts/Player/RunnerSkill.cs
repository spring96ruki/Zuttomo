using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSkill : MonoBehaviour {

    [SerializeField]
    GameObject m_playerArea;
    RunnerInput m_runnerInput;
    RunnerStatus m_runnerStatus;
    RunnerController m_runnerController;
    RunnerMove m_runnerMove;

	void Start()
	{
        m_runnerStatus = GetComponent<RunnerStatus>();
        m_runnerInput = GetComponent<RunnerInput>();
        m_runnerMove = GetComponent<RunnerMove>();
        m_runnerController = GetComponent<RunnerController>();
	}

	public void HitEvent(Collision hit)
    {
        if (hit.gameObject.tag == TagName.Push)
        {
            m_runnerStatus.isState = false;
            Debug.Log("当たった");
            m_runnerController.State_timer = 0;
        }

        if (hit.gameObject.tag == TagName.Itimatu)
        {
            Debug.Log("当たった");
            m_runnerStatus.isState = false;
            Destroy(hit.gameObject);
        }
    }

    public void CheckEvent(Collision check)
    {

        if (m_runnerStatus.ishave == false)
        {
            if (check.gameObject.name == ItemName.itimatu)
            {
                Debug.Log("市松人形だよ");
                if (m_runnerInput.button_B == true)
                {
                    //アイテムを持ったらtrueに変更
                    m_runnerStatus.ishave = true;
                    //アイテムの番号を1に変更
                    m_runnerMove.m_itemNum = 1;
                    //拾ったアイテムを消去
                    Destroy(check.gameObject);
                }
            }
            if (check.gameObject.name == ItemName.Drug)
            {
                Debug.Log("薬だよ");
                if (m_runnerInput.button_B == true)
                {
                    //アイテムを持ったらtrueに変更
                    m_runnerStatus.ishave = true;
                    //アイテムの番号を2に変更
                    m_runnerMove.m_itemNum = 2;
                    //拾ったアイテムを消去
                    Destroy(check.gameObject);
                }
            }
            if (check.gameObject.name == ItemName.Ohuda)
            {
                Debug.Log("お札だよ");
                if (m_runnerInput.button_B == true)
                {
                    //アイテムを持ったらtrueに変更
                    m_runnerStatus.ishave = true;
                    //アイテムの番号を3に変更
                    m_runnerMove.m_itemNum = 3;
                    //拾ったアイテムを消去
                    Destroy(check.gameObject);
                }
            }
        }
        else
        {
            Debug.Log("これ以上は持てないよ");
        }
    }

	public void ItemEvent()
	{
        if (m_runnerStatus.ishave == true)
        {
            switch (m_runnerMove.m_itemNum)
            {
                case 1:
                    Debug.Log("市松人形を投げたよ");
                    Vector3 force;
                    GameObject bullets = Instantiate(m_runnerMove.m_item) as GameObject;
                    force = this.gameObject.transform.forward * m_runnerMove.m_itemspeed;
                    // Rigidbodyに力を加えて発射
                    bullets.GetComponent<Rigidbody>().AddForce(force);
                    // アイテムの位置を調整
                    bullets.transform.position = m_runnerMove.m_FiringPosition.position;
                    bullets.tag = "itimatu";
                    m_runnerStatus.ishave = false;
                    break;

                case 2:
                    Debug.Log("力が上がったよ");
                    m_runnerStatus.ishave = false;
                    break;

                case 3:
                    Debug.Log("無敵");
                    StartCoroutine("Invincible");
                    m_runnerStatus.ishave = false;
                    break;
            }
        }
	}

    IEnumerator Invincible()
    {
        //レイヤーをPlayerInvincibleに変更
        gameObject.layer = LayerMask.NameToLayer("PlayerInvincible");
        m_playerArea.SetActive(true);
        //while文を50回ループ
        float m_invincibleTime = 3f;
        while (m_invincibleTime > 0)
        {
            m_invincibleTime -= Time.deltaTime;
            yield return null;
            Debug.Log(m_invincibleTime);
        }
        m_playerArea.SetActive(false);
        //レイヤーをPlayerに戻す
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
