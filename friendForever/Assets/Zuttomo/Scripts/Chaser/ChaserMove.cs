using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserMove : MonoBehaviour 
{
    RunnerInput m_runnerInput;
    protected RunnerStatus m_status;
    [HideInInspector]
    public Rigidbody m_rigidbody;

    private void Awake()
    {
        m_runnerInput = GetComponent<RunnerInput>();
        m_status = GetComponent<RunnerStatus>();
        m_rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(GameObject cameraObject)
    {
        float horizontal = m_runnerInput.Laxis_x * m_status.speed * Time.deltaTime;
        float virtical = m_runnerInput.Laxis_y * m_status.speed * Time.deltaTime;
        PlayerRotation(cameraObject, horizontal, virtical);
    }

    void PlayerRotation(GameObject cameraObject, float horizontal, float virtical)
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(cameraObject.transform.forward, new Vector3(1, 0, 1)).normalized;
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * virtical + cameraObject.transform.right * horizontal;

        if (moveForward != Vector3.zero)
        {
            //カメラを向きを前に移動する
            transform.position += cameraForward * virtical + cameraObject.transform.right * horizontal;
            //体の向きを変更
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }
}
