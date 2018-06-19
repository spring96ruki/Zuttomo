using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RunnerState
{
    normal = 0,
    stan
}

public class RunnerController : SingletonMono<RunnerController>
{

    float m_stanTime;
    public float stanTime{ get { return m_stanTime; } set { m_stanTime = value; } }
    [HideInInspector]
    public float State_timar;
	public bool ChaserFlag;
    float currentSpeed;
    Rigidbody m_rigidBody;
    //RunnerCore m_runnerCore;
    RunnerInput m_runnerInput;
    RunnerMove m_runnerMove;
    RunnerStatus m_runnerStatus;
    protected RunnerStatus m_status;
    [HideInInspector]
    public Rigidbody m_rigidbody;
    bool isStan = false;

    RunnerState m_state;

    void Awake()
    {
        m_runnerInput = GetComponent<RunnerInput>();
        m_runnerMove = GetComponent<RunnerMove>();
        m_runnerStatus = GetComponent<RunnerStatus>();
        m_rigidBody = GetComponent<Rigidbody>();
    }

	void Start()
	{
        //初期ステータス
        m_runnerStatus.firstSpeed = 5;
        m_runnerStatus.maxSpeed = 10;
        m_runnerStatus.health = 5;
        m_runnerStatus.maxHealth = 5;
        m_runnerStatus.isState = true;
        m_runnerStatus.ishave = false;
        m_runnerStatus.isBuff = false;
        m_runnerStatus.isInvincible = false;
        m_runnerStatus.animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
    {
        RunnerStanTime();
        m_runnerInput.PController();
		if (transform.position.y < -10) {
			transform.position = new Vector3 (0, 3, 0);
		} 
    }

    public void RunnerStan(RunnerState state, float skilTime)
    {
        m_state = state;
        if (m_state == RunnerState.stan)
        {
            Debug.Log("スタンしたよ");
            // RunnerStateがStanに変更されたらisStanをtrueに変更
            isStan = true;
        }
    }

    public void RunnerStanTime()
    {
        Debug.Log("スタン時間" + stanTime);
        // isStanがtrueになったらスタン処理開始
        if (isStan == true)
        {
            --stanTime;
            Debug.Log("通った");

            // スタン処理
            // 現在のスピードを別の変数に保持し、スピードを0に変更
            currentSpeed = m_runnerStatus.speed;
            m_runnerStatus.speed = 0f;

            if (stanTime < 0)
            {
                stanTime = 0;
            }
            if (stanTime == 0)
            {
                // stanTimeが0になったらisStanをfalseにする
                // RunnerStanをnormalに変更
                isStan = false;
                m_state = RunnerState.normal;
                Debug.Log("スタン終わったよ");

                // isStanがfalseに変更されたら、スタン処理終了
                // スタン終了時に保持してたスピードをプレイヤーのステータスへ戻す
                if (isStan == false)
                {
                    m_runnerStatus.speed = currentSpeed;
                }
            }
        }
    }

	public void RunnerSkyHigh()
	{
		Debug.Log ("sky");
		this.GetComponent<Rigidbody>().AddForce(0,500,0);
	}

    private void FixedUpdate()
    {
        if (m_runnerStatus.isState == true)
        {
            m_runnerMove.Move();
            m_runnerMove.Button();
        }
        else
        {
            State_timar += Time.deltaTime;
			Vector3 force = Vector3.zero;
			force = this.gameObject.transform.forward * 1000;
			// Rigidbodyに力を加える
			m_rigidBody.AddForce(force,ForceMode.Force);
            force = transform.position * 200;
            // Rigidbodyに力を加えて発射
            GetComponent<Rigidbody>().AddForce(force);
            if (State_timar >= 3)
            {
                m_runnerStatus.isState = true;
            }
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == TagName.Push)
        {
            m_runnerStatus.isState = false;
            Debug.Log("当たった");
            State_timar = 0;
        }
        if (hit.gameObject.tag == TagName.Itimatu)
        {
            Debug.Log("当たった");
            m_runnerStatus.isState = false;
            Destroy(hit.gameObject);
        }
    }

    void OnCollisionStay(Collision col)
    {
        CheckEvent(col);
    }

    void CheckEvent(Collision col)
    {

        if (m_runnerStatus.ishave == false)
        {
            if (col.gameObject.name == ItemName.itimatu)
            {
                Debug.Log("市松人形だよ");
                if (m_runnerInput.button_B == true)
                {
                    //アイテムを持ったらtrueに変更
                    m_runnerStatus.ishave = true;
                    //アイテムの番号を1に変更
                    m_runnerMove.m_itemNum = 1;
                    //拾ったアイテムを消去
                    Destroy(col.gameObject);
                }
            }
            if(col.gameObject.name == ItemName.Drug)
            {
                Debug.Log("薬だよ");
                if (m_runnerInput.button_B == true)
                {
                    //アイテムを持ったらtrueに変更
                    m_runnerStatus.ishave = true;
                    //アイテムの番号を2に変更
                    m_runnerMove.m_itemNum = 2;
                    //拾ったアイテムを消去
                    Destroy(col.gameObject);
                }
            }
            if (col.gameObject.name == ItemName.Bill)
            {
                Debug.Log("お札だよ");
                if (m_runnerInput.button_B == true)
                {
                    //アイテムを持ったらtrueに変更
                    m_runnerStatus.ishave = true;
                    //アイテムの番号を3に変更
                    m_runnerMove.m_itemNum = 3;
                    //拾ったアイテムを消去
                    Destroy(col.gameObject);
                }
            }
        }
        else
        {
            Debug.Log("これ以上は持てないよ");
        }
    }
}
