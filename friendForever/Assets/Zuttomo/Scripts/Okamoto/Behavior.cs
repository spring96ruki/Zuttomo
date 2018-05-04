using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : Status  {

    public GameObject Camera;

    public void Move()
    {
        float h = Laxis_x * Speed * Time.deltaTime;
        float v = Laxis_y * Speed * Time.deltaTime;
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.transform.forward, new Vector3(1, 0, 1)).normalized;
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * v + Camera.transform.right * h;
        
        if (moveForward != Vector3.zero)
        {
            //カメラを向きを前に移動する
            transform.position += cameraForward * v + Camera.transform.right * h;
            //体の向きを変更
            transform.rotation = Quaternion.LookRotation(moveForward);

            if (Speed <= MinSpeed)
            {
                animator.SetBool("Walk", true);
                animator.SetBool("Run", false);
            }
            else if (Speed >= MaxSpeed)
            {
                animator.SetBool("Run", true);
                animator.SetBool("Walk", false);
            }

            if (StaminaON == true)
            {
                if (button_RB == true)
                {
                    Debug.Log("ダッシュ");
                    Speed = MaxSpeed;
                    Stamina += Time.deltaTime;
                }
            }
            else
            {
                Speed = MinSpeed;
            }

            if (Stamina >= 5f)
            {
                StaminaON = false;
            }
        } 
        else {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }

        //スタミナがなかったら
        if (StaminaON == false)
        {
            //スタミナ回復
            Stamina -= Time.deltaTime;
        }

        if (Stamina <= 0f)
        {
            StaminaON = true;
        }

        //ボタンが押されてなかったら
        if (button_RB == false)
        {
            Speed = MinSpeed;
            //スタミナがのっこていたら
            if (Stamina >= 0f)
            {
                //スタミナ回復
                Stamina -= Time.deltaTime;
            }
        }
    }

	public void Button()
	{
        if (button_A == true)
        {
            Debug.Log("A");
        }

        if (button_B == true)
        {
            Debug.Log("B");
        }

        if (button_X == true)
        {
            Debug.Log("X");
        }

        if (button_Y == true)
        {
            Debug.Log("Y");
        }
	}
}
