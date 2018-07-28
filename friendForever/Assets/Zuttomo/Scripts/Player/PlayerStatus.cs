using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerFlag
{
    Unknown,
    Runner,
    Chaser
}

public class PlayerStatus
{
    public int playerNum;
    public float speed;
    public float runSpeed;
    public float buffSpeed;
    public float health;
}
