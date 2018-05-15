using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerInput : MonoBehaviour
{

    public float Laxis_x;
    public float Laxis_y;
    public float Raxis_x;
    public float Raxis_y;
    public bool button_RB;
    public bool button_A;
    public bool button_B;
    public bool button_X;
    public bool button_Y;
    [SerializeField, Header("プレイヤーの番号")]
    int runnerNum;

    public void PController()
    {
        switch (runnerNum)
        {
            case 1:
                EscapePlayerInput();
                button_RB = Input.GetKey("z");
                button_A = Input.GetKeyDown("x");
                button_B = Input.GetKeyDown("c");
                button_X = Input.GetKeyDown("v");
                button_Y = Input.GetKeyDown("b");
                break;

            case 2:
                
                break;

            case 3:
                
                break;

            case 4:
                
                break;
        }

    }

    void EscapePlayerInput()
    {
        Laxis_x = Input.GetAxisRaw(GamePadName.GameStick_Left + PlayerNum.ToString() + GamePadName.GameStick_X);
        Laxis_y = Input.GetAxisRaw(GamePadName.GameStick_Left + PlayerNum.ToString() + GamePadName.GameStick_Y);
        Raxis_x = Input.GetAxisRaw(GamePadName.GameStick_Right + PlayerNum.ToString() + GamePadName.GameStick_X);
        Raxis_y = Input.GetAxisRaw(GamePadName.GameStick_Right + PlayerNum.ToString() + GamePadName.GameStick_Y);
        button_RB = Input.GetButton(GamePadName.GamePad_RB + PlayerNum.ToString());
        button_A = Input.GetButtonDown(GamePadName.GamePad_A + PlayerNum.ToString());
        button_B = Input.GetButtonDown(GamePadName.GamePad_B + PlayerNum.ToString());
        button_X = Input.GetButtonDown(GamePadName.GamePad_X + PlayerNum.ToString());
        button_Y = Input.GetButtonDown(GamePadName.GamePad_Y + PlayerNum.ToString());

        Laxis_x = Input.GetAxisRaw(GamePadName.xAxis);
        Laxis_y = Input.GetAxisRaw(GamePadName.zAxis);
    }
}
