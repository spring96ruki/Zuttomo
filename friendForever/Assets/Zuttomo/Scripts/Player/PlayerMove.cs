using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove
{
    public GameObject m_camera;
    public GameObject m_UIController;

	RunnerSkill m_runnerSkill;
    UIController m_uIController;

	public void Move(PlayerFlag playerFlag, PlayerAnimator playerAnimation, PlayerInput playerInput, PlayerStatus playerStatus)
	{
        switch (playerFlag)
        {
            case PlayerFlag.Runner:
                float horizontal = playerInput.Laxis_x * playerStatus.speed * Time.deltaTime;
                float virtical = playerInput.Laxis_y * playerStatus.speed * Time.deltaTime;
                //PlayerRotation(horizontal, virtical);
                //HealthControll();
                playerAnimation.MoveAnimation(playerInput, playerStatus, horizontal, virtical);
                break;
            case PlayerFlag.Chaser:
                horizontal = playerInput.Laxis_x * playerStatus.speed * Time.deltaTime;
                virtical = playerInput.Laxis_y * playerStatus.speed * Time.deltaTime;
                //PlayerRotation(cameraObject, horizontal, virtical);
                break;

        }
    }

    void PlayerRotation(float horizontal, float virtical)
	{
		// カメラの方向から、X-Z平面の単位ベクトルを取得
		Vector3 cameraForward = Vector3.Scale(m_camera.transform.forward, new Vector3(1, 0, 1)).normalized;
		// 方向キーの入力値とカメラの向きから、移動方向を決定
		Vector3 moveForward = cameraForward * virtical + m_camera.transform.right * horizontal;

		//if (moveForward != Vector3.zero)
		//{
		//	//カメラを向きを前に移動する
		//	transform.position += cameraForward * virtical + m_camera.transform.right * horizontal;
		//	//体の向きを変更
		//	transform.rotation = Quaternion.LookRotation(moveForward);
		//}
	}
}
