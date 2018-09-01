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
    public bool isHaveItem;
    public bool isBuff;
    public float speed;
    public float walkSpeed;
    public float runSpeed;
    public float buffSpeed;
    public float health;
    public float maxHealth;
    public float buffTime;
    public float maxBuffTime;

    public void StatusInit(PlayerFlag playerFlag)
    {
        switch (playerFlag)
        {
            case PlayerFlag.Runner:
                RunnerStatus();
                break;
            case PlayerFlag.Chaser:
                ChaserStatus();
                break;
        }
    }

    void RunnerStatus()
    {
        isBuff = false;
        isHaveItem = false;
        walkSpeed = 3f;
        runSpeed = 4f;
        buffSpeed = 6f;
        maxHealth = 10f;
        health = maxHealth;
        maxBuffTime = 20f;
    }

    void ChaserStatus()
    {
        runSpeed = 5f;
    }
}
