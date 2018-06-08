using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCamera : MonoBehaviour 
{
    [Header("カメラが追いかける対象")]
    public GameObject target;
    [Header("カメラの初期位置")]
    public Transform initPos;
    Vector3 targetPos;

    RunnerInput m_pInput;

	void Awake()
	{
        DontDestroyOnLoad(gameObject);
	}

	void Start()
    {
        m_pInput = GetComponent<RunnerInput>();
        targetPos = target.transform.position;
        transform.position = initPos.position;
    }

    void Update()
    {
        m_pInput.PController();
    }

	void FixedUpdate()
    {
        // targetの移動量分、カメラも移動する
        transform.position += target.transform.position - targetPos;
        targetPos = target.transform.position;

        float h = m_pInput.Raxis_x * 150 * Time.deltaTime;
        float v = m_pInput.Raxis_y * 150 * Time.deltaTime;
<<<<<<< HEAD


        // targetの位置のY軸を中心に、回転する
        transform.RotateAround(targetPos, Vector3.up, v);
=======

        // targetの位置のY軸を中心に、回転する
        transform.RotateAround(targetPos, Vector3.up, -v);
>>>>>>> okamoto
        // カメラの垂直移動（角度制限なし）
        //transform.RotateAround(targetPos, transform.right, h);
    }
}
