using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
    
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
    public int PlayerNum;

    public void PController()
    {
        switch (PlayerNum)
        {
            case 1:
                Laxis_x = Input.GetAxisRaw("L_GamePad1_X");
                Laxis_y = Input.GetAxisRaw("L_GamePad1_Y");
                Raxis_x = Input.GetAxisRaw("R_GamePad1_X");
                Raxis_y = Input.GetAxisRaw("R_GamePad1_Y");
                button_RB = Input.GetButton("RB_1") || Input.GetKey("z");
                button_A = Input.GetButtonDown("A_1") || Input.GetKeyDown("x");
                button_B = Input.GetButtonDown("B_1") || Input.GetKeyDown("c");
                button_X = Input.GetButtonDown("X_1") || Input.GetKeyDown("v");
                button_Y = Input.GetButtonDown("Y_1") || Input.GetKeyDown("b");

                Laxis_x = Input.GetAxisRaw("Horizontal");
                Laxis_y = Input.GetAxisRaw("Vertical");
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
        
}
