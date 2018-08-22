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
    public float speed;
    public float walkSpeed;
    public float runSpeed;
    public float buffSpeed;
    public float health;
    public float maxHealth;

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
        isHaveItem = false;
        walkSpeed = 3f;
        runSpeed = 4f;
        buffSpeed = 6f;
        maxHealth = 10f;
        health = maxHealth;
    }

    void ChaserStatus()
    {
        runSpeed = 5f;
    }
}
