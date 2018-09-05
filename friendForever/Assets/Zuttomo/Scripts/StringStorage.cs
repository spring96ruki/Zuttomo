using System.Collections;
using System.Collections.Generic;

public class GamePadStorage
{
    public static readonly string[] GamePadStickLeft_Y =
    {
        "L_GamePad1_Y",
        "L_GamePad2_Y",
        "L_GamePad3_Y",
        "L_GamePad4_Y"
    };

    public static readonly string[] GamePadStickLeft_X =
    {
        "L_GamePad1_X",
        "L_GamePad2_X",
        "L_GamePad3_X",
        "L_GamePad4_X"
    };

    public static readonly string[] GamePadStickRight_Y =
    {
        "R_GamePad1_Y",
        "R_GamePad2_Y",
        "R_GamePad3_Y",
        "R_GamePad4_Y"
    };

    public static readonly string[] GamePadStickRight_X =
    {
        "R_GamePad1_X",
        "R_GamePad2_X",
        "R_GamePad3_X",
        "R_GamePad4_X"
    };

    public static readonly string[] GamePad_RB =
    {
        "RB_1",
        "RB_2",
        "RB_3",
        "RB_4"
    };

    public static readonly string[] GamePad_A =
    {
        "A_1",
        "A_2",
        "A_3",
        "A_4"
    };

    public static readonly string[] GamePad_B =
    {
        "B_1",
        "B_2",
        "B_3",
        "B_4"
    };

    public static readonly string[] GamePad_X =
    {
        "X_1",
        "X_2",
        "X_3",
        "X_4"
    };

    public static readonly string[] GamePad_Y =
    {
        "Y_1",
        "Y_2",
        "Y_3",
        "Y_4"
    };
}

public static class GamePadName
{
    public const string xAxis = "Horizontal";
    public const string zAxis = "Vertical";
    public const string GameStick_X = "_X";
    public const string GameStick_Y = "_Y";
    public const string GameStick_Left = "L_GamePad";
    public const string GameStick_Right = "R_GamePad";
    public const string GamePad_RB = "RB_";
    public const string GamePad_A = "A_";
    public const string GamePad_B = "B_";
    public const string GamePad_X = "X_";
    public const string GamePad_Y = "Y_";
}

public static class TagName
{
    public const string TestTag = "TestTarget";
    public const string Player = "Player";
    public const string Player_1P = "Player_1P";
    public const string Player_2P = "Player_2P";
    public const string Player_3P = "Player_3P";
    public const string Runner = "Runner";
    public const string Chaser = "Chaser";
    public const string Push = "Push";
    public const string Item = "item";
    public const string NPC = "NPC";
    public const string Plane = "Plane";
}

public static class ItemName
{
    public const string ITIMATSU = "itimatsu";
    public const string DRUG = "drug";
    public const string AMULETS = "amulets";
}

public static class SceneName
{
    public const string TITLE_SCENE = "Title";
    public const string SELECT_SCENE = "Select";
    public const string GAME_SCENE = "okamoto";
    public const string RESULT_SCENE = "Result";
    public const string SCENE_TEST = "SceneTest";
    public const string SCENE_CHANGE = "SceneChange";
    public const string FUJIWARA = "fujiwara";
}