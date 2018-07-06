using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCamera : MonoBehaviour 
{
    [SerializeField,Header("カメラが追いかける対象")]
    GameObject m_target;
    Vector3 m_targetPos;
    RunnerInput m_pInput;
    public int playerNum;

    [SerializeField,Header("プレイヤーとの距離")]
    float m_Distance;
    [SerializeField, Header("視点の高さ")]
    float m_Height;
    float m_moveSpeed;
    Vector3 nextLoc;
    RaycastHit m_hit;
    float m_rayDistance = 10f;
    float cameraMoveSpeed = 3f;

    void Start()
    {
        m_pInput = GetComponent<RunnerInput>();
        m_targetPos = m_target.transform.position;
    }

    void Update()
    {
        m_pInput.PController(playerNum);
    }

    void FixedUpdate()
    {
        var m_lookAt = m_targetPos + Vector3.up * m_Height;
        //targetの移動量分、カメラも移動する
        transform.position = m_lookAt - transform.forward * m_Distance;
        transform.LookAt(m_lookAt);
        m_targetPos = m_target.transform.position;

        float h = m_pInput.Raxis_x * 150 * Time.deltaTime;
        float v = m_pInput.Raxis_y * 150 * Time.deltaTime;

        CameraCheck(m_target);

        // targetの位置のY軸を中心に、回転する
        transform.RotateAround(m_targetPos, Vector3.up, v);

        // targetの位置のY軸を中心に、回転する
        transform.RotateAround(m_targetPos, Vector3.up, -v);

        // カメラの垂直移動（角度制限なし）
        //transform.RotateAround(targetPos, transform.right, h);
        transform.RotateAround(m_targetPos, Vector3.up, -v);

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
