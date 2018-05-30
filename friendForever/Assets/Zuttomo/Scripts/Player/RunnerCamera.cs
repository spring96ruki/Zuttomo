using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCamera : MonoBehaviour 
{

    public GameObject target;
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
    }

    void FixedUpdate()
    {
        // targetの移動量分、カメラも移動する
        transform.position += target.transform.position - targetPos;
        targetPos = target.transform.position;

        float h = m_pInput.Raxis_x * 150 * Time.deltaTime;
        float v = m_pInput.Raxis_y * 150 * Time.deltaTime;


        // targetの位置のY軸を中心に、回転する
        transform.RotateAround(targetPos, Vector3.up, v);
        // カメラの垂直移動（角度制限なし）
        //transform.RotateAround(targetPos, transform.right, h);
    }
}
