using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public float Laxis_x;
    public float Laxis_y;
    public float Raxis_x;
    public float Raxis_y;

    public float axisKey_x;
    public float axisKey_z;

    public bool button_RB;
    public bool button_A;
    public bool button_B;
    public bool button_X;
    public bool button_Y;

    public bool button_I;
    public bool button_U;
    public bool button_T;
    public bool button_E;
    public bool button_R;

    public void PController(int playerNum)
    {
        switch (playerNum)
        {
            case 1:
                KeyBoardEvent();
                break;

            case 2:
                break;

            case 3:
                break;

            case 4:
                break;
        }

    }

    void PlayerInputCon(int playerNum)
    {
        Laxis_x = Input.GetAxis(GamePadName.GameStick_Left + playerNum.ToString() + GamePadName.GameStick_X);
        Laxis_y = Input.GetAxis(GamePadName.GameStick_Left + playerNum.ToString() + GamePadName.GameStick_Y);
        Raxis_x = Input.GetAxis(GamePadName.GameStick_Right + playerNum.ToString() + GamePadName.GameStick_X);
        Raxis_y = Input.GetAxis(GamePadName.GameStick_Right + playerNum.ToString() + GamePadName.GameStick_Y);
        button_RB = Input.GetButton(GamePadName.GamePad_RB + playerNum.ToString());
        button_A = Input.GetButtonDown(GamePadName.GamePad_A + playerNum.ToString());
        button_B = Input.GetButtonDown(GamePadName.GamePad_B + playerNum.ToString());
        button_X = Input.GetButtonDown(GamePadName.GamePad_X + playerNum.ToString());
        button_Y = Input.GetButtonDown(GamePadName.GamePad_Y + playerNum.ToString());
    }

    void KeyBoardEvent()
    {
        axisKey_x = Input.GetAxisRaw(GamePadName.xAxis);
        axisKey_z = Input.GetAxisRaw(GamePadName.zAxis);
        button_I = Input.GetKeyDown(KeyCode.I);
        button_U = Input.GetKeyDown(KeyCode.U);
        button_T = Input.GetKeyDown(KeyCode.T);
        button_E = Input.GetKeyDown(KeyCode.E);
        button_R = Input.GetKey(KeyCode.R);
    }
}
