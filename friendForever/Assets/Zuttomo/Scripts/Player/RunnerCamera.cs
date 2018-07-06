using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCamera : MonoBehaviour 
{
    [SerializeField,Header("カメラが追いかける対象")]
    GameObject m_target;
    Vector3 m_targetPos;
    RunnerInput m_pInput;
    RunnerController m_runnerController;
    [SerializeField,Header("プレイヤーとの距離")]
    float m_Distance;
    [SerializeField, Header("視点の高さ")]
    float m_Height;
    float m_moveSpeed;
    Vector3 nextLoc;
    RaycastHit m_hit;
    float m_rayDistance = 10f;
    float cameraMoveSpeed = 3f;

    public int m_playerNum;

	void Start()
    {
        m_pInput = m_target.GetComponent<RunnerInput>();
        m_runnerController = m_target.GetComponent<RunnerController>();
        m_targetPos = m_target.transform.position;
        m_playerNum = m_runnerController.m_playerNum;
    }

    void Update()
    {
        m_pInput.PController(m_playerNum);
    }

    void FixedUpdate()
    {
        var m_lookAt = m_targetPos + Vector3.up * m_Height;
        //targetの移動量分、カメラも移動する
        transform.position = m_lookAt - transform.forward * m_Distance;
        transform.LookAt(m_lookAt);
        m_targetPos = m_target.transform.position;

        float h = m_pInput.Raxis_x * 300 * Time.deltaTime;
        float v = m_pInput.Raxis_y * 300 * Time.deltaTime;

        CameraCheck(m_target);

        // targetの位置のY軸を中心に、回転する
        transform.RotateAround(m_targetPos, Vector3.up, -v);

        // カメラの垂直移動（角度制限なし）
        //transform.RotateAround(targetPos, transform.right, h);

        //CameraMovement(m_target, m_Distance);
    }

    void CameraCheck(GameObject player)
    {
        Ray ray = new Ray(player.transform.position, player.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, m_rayDistance, LayerMask.GetMask("Field")))
        {
            transform.position = Vector3.Lerp(transform.position , hit.point, cameraMoveSpeed * Time.deltaTime);
            Debug.Log("壁ドン");
        }
    }
}
