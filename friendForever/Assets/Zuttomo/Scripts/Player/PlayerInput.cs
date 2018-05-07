using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
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
    int PlayerNum;

    public void PController()
    {
        switch (PlayerNum)
        {
            case 1:
                EscapePlayerInput();
                break;

            case 2:
                //Laxis_x = Input.GetAxisRaw("L_GamePad2_X");
                //Laxis_y = Input.GetAxisRaw("L_GamePad2_Y");
                //Raxis_x = Input.GetAxisRaw("R_GamePad2_X");
                //Raxis_y = Input.GetAxisRaw("R_GamePad2_Y");
                //button_RB = Input.GetButton("RB_2");
                //button_A = Input.GetButtonDown("A_2");
                //button_B = Input.GetButtonDown("B_2");
                //button_X = Input.GetButtonDown("X_2");
                //button_Y = Input.GetButtonDown("Y_2");
                break;

            case 3:
                //Laxis_x = Input.GetAxisRaw("L_GamePad3_X");
                //Laxis_y = Input.GetAxisRaw("L_GamePad3_Y");
                //Raxis_x = Input.GetAxisRaw("R_GamePad3_X");
                //Raxis_y = Input.GetAxisRaw("R_GamePad3_Y");
                //button_RB = Input.GetButton("RB_3");
                //button_A = Input.GetButtonDown("A_3");
                //button_B = Input.GetButtonDown("B_3");
                //button_X = Input.GetButtonDown("X_3");
                //button_Y = Input.GetButtonDown("Y_3");
                break;

            case 4:
                //Laxis_x = Input.GetAxisRaw("L_GamePad4_X");
                //Laxis_y = Input.GetAxisRaw("L_GamePad4_Y");
                //Raxis_x = Input.GetAxisRaw("R_GamePad4_X");
                //Raxis_y = Input.GetAxisRaw("R_GamePad4_Y");
                //button_RB = Input.GetButton("RB_4");
                //button_A = Input.GetButtonDown("A_4");
                //button_B = Input.GetButtonDown("B_4");
                //button_X = Input.GetButtonDown("X_4");
                //button_Y = Input.GetButtonDown("Y_4");
                break;
        }

    }

    void EscapePlayerInput()
    {
        Laxis_x = Input.GetAxisRaw(StringName.GameStick_Left_X);
        Laxis_y = Input.GetAxisRaw(StringName.GameStick_Left_Y);
        Raxis_x = Input.GetAxisRaw(StringName.GameStick_Right_X);
        Raxis_y = Input.GetAxisRaw(StringName.GameStick_Right_Y);
        button_RB = Input.GetButton(StringName.GamePad_RB) || Input.GetKey("z");
        button_A = Input.GetButtonDown(StringName.GamePad_A) || Input.GetKeyDown("x");
        button_B = Input.GetButtonDown(StringName.GamePad_B) || Input.GetKeyDown("c");
        button_X = Input.GetButtonDown(StringName.GamePad_X) || Input.GetKeyDown("v");
        button_Y = Input.GetButtonDown(StringName.GamePad_Y) || Input.GetKeyDown("b");

        Laxis_x = Input.GetAxisRaw(StringName.xAxis);
        Laxis_y = Input.GetAxisRaw(StringName.zAxis);
    }
}
