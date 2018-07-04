using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerInput : MonoBehaviour
{
    [HideInInspector]
    public float Laxis_x;
    [HideInInspector]
    public float Laxis_y;
    [HideInInspector]
    public float Raxis_x;
    [HideInInspector]
    public float Raxis_y;
    [HideInInspector]
    public bool button_RB;
    [HideInInspector]
    public bool button_A;
    [HideInInspector]
    public bool button_B;
    [HideInInspector]
    public bool button_X;
    [HideInInspector]
    public bool button_Y;

    public void PController(int playerNum)
    {
        switch (playerNum)
        {
            case 1:
                PlayerInput(playerNum);
                //KeyBoardEvent();
                break;

            case 2:
                PlayerInput(playerNum);
                break;

            case 3:
                PlayerInput(playerNum);
                break;

            case 4:
                PlayerInput(playerNum);
                break;
        }

    }

    void PlayerInput(int playerNum)
    {
        Laxis_x = Input.GetAxis(GamePadName.GameStick_Left + playerNum.ToString() + GamePadName.GameStick_X);
        Laxis_y = Input.GetAxis(GamePadName.GameStick_Left + playerNum.ToString() + GamePadName.GameStick_Y);
        Raxis_x = Input.GetAxis(GamePadName.GameStick_Right + playerNum.ToString() + GamePadName.GameStick_X);
        Raxis_y = Input.GetAxis(GamePadName.GameStick_Right + playerNum.ToString() + GamePadName.GameStick_Y);
        //button_RB = Input.GetButton(GamePadName.GamePad_RB + playerNum.ToString());
        button_A = Input.GetButtonDown(GamePadName.GamePad_A + playerNum.ToString());
        button_B = Input.GetButtonDown(GamePadName.GamePad_B + playerNum.ToString());
        button_X = Input.GetButtonDown(GamePadName.GamePad_X + playerNum.ToString());
        button_Y = Input.GetButtonDown(GamePadName.GamePad_Y + playerNum.ToString());
    }

    void KeyBoardEvent()
    {
        Laxis_x = Input.GetAxisRaw(GamePadName.xAxis);
        Laxis_y = Input.GetAxisRaw(GamePadName.zAxis);
        button_RB = Input.GetKey("z");
        Debug.Log("z"+button_RB);
        button_A = Input.GetKeyDown("x");
        button_B = Input.GetKeyDown("c");
        button_X = Input.GetKeyDown("v");
        button_Y = Input.GetKeyDown("b");
        //Debug.Log("ヘルス");
    }
}
