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

    [SerializeField, Header("プレイヤーの番号")]
    public int runnerNum;
   

    void Start()
    {
        
    }

    public void PController()
    {
        switch (runnerNum)
        {
            case 1:
                EscapePlayerInput();

                Laxis_x = Input.GetAxisRaw(GamePadName.xAxis);
                Laxis_y = Input.GetAxisRaw(GamePadName.zAxis);
                button_RB = Input.GetKey("z");
                button_A = Input.GetKeyDown("x");
                button_B = Input.GetKeyDown("c");
                button_X = Input.GetKeyDown("v");
                button_Y = Input.GetKeyDown("b");

                break;

            case 2:
                EscapePlayerInput();
                break;

            case 3:
                EscapePlayerInput();
                break;

            case 4:
                EscapePlayerInput();
                break;
        }

    }

    void EscapePlayerInput()
    {
        Laxis_x = Input.GetAxis(GamePadName.GameStick_Left + runnerNum.ToString() + GamePadName.GameStick_X);
        Laxis_y = Input.GetAxis(GamePadName.GameStick_Left + runnerNum.ToString() + GamePadName.GameStick_Y);
        Raxis_x = Input.GetAxis(GamePadName.GameStick_Right + runnerNum.ToString() + GamePadName.GameStick_X);
        Raxis_y = Input.GetAxis(GamePadName.GameStick_Right + runnerNum.ToString() + GamePadName.GameStick_Y);
        button_RB = Input.GetButton(GamePadName.GamePad_RB + runnerNum.ToString());
        button_A = Input.GetButtonDown(GamePadName.GamePad_A + runnerNum.ToString());
        button_B = Input.GetButtonDown(GamePadName.GamePad_B + runnerNum.ToString());
        button_X = Input.GetButtonDown(GamePadName.GamePad_X + runnerNum.ToString());
        button_Y = Input.GetButtonDown(GamePadName.GamePad_Y + runnerNum.ToString());

        //Laxis_x = Input.GetAxisRaw(GamePadName.xAxis);
        //Laxis_y = Input.GetAxisRaw(GamePadName.zAxis);
        //button_RB = Input.GetKey("z");
        //button_A = Input.GetKeyDown("x");
        //button_B = Input.GetKeyDown("c");
        //button_X = Input.GetKeyDown("v");
        //button_Y = Input.GetKeyDown("b");
    }
}
