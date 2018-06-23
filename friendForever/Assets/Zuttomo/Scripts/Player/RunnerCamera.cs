using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCamera : MonoBehaviour 
{
    [SerializeField,Header("カメラが追いかける対象")]
    GameObject m_target;
    Vector3 m_targetPos;
    [SerializeField]
    RaycastHit m_hit;
    RunnerInput m_pInput;

    [SerializeField,Header("プレイヤーとの距離")]
    float m_Distance;
    [SerializeField, Header("視点の高さ")]
    float m_Height;

	void Start()
    {
        m_pInput = GetComponent<RunnerInput>();
        m_targetPos = m_target.transform.position;
        m_Distance = 2.0f;
        m_Height = 1f;
    }

    void Update()
    {
        m_pInput.PController();
    }

	void FixedUpdate()
    {
        var m_lookAt = m_targetPos + Vector3.up * m_Height;
        // targetの移動量分、カメラも移動する
        transform.position = m_lookAt - transform.forward * m_Distance;
        transform.LookAt(m_lookAt);
        m_targetPos = m_target.transform.position;

        float h = m_pInput.Raxis_x * 150 * Time.deltaTime;
        float v = m_pInput.Raxis_y * 150 * Time.deltaTime;




        transform.LookAt(m_lookAt);

        // targetの位置のY軸を中心に、回転する
        transform.RotateAround(m_targetPos, Vector3.up, v);

        // targetの位置のY軸を中心に、回転する
        transform.RotateAround(m_targetPos, Vector3.up, -v);

        // カメラの垂直移動（角度制限なし）
        //transform.RotateAround(targetPos, transform.right, h);
        transform.RotateAround(m_targetPos, Vector3.up, -v);

        //　レイを視覚的に確認
        Debug.DrawLine(m_targetPos + Vector3.up, transform.position, Color.red, 0f, false);
    }
}
