using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerFlag
{
    unknown = 0,
    Runner,
    Chaser
}

public class PlayerStatus : MonoBehaviour{

    [Header("初速")]
    public float firstSpeed;
    [Header("最高速度")]
    public float maxSpeed;
    [Header("スピード")]
    public float speed;
    [Header("スタミナ")]
    public float health;
    [Header("スタミナ上限値")]
    public float maxHealth;
    [Header("スタミナがあるか")]
    public bool isHealth;
    [Header("プレイヤーが動けるかどうか")]
    public bool isState;
    [Header("アイテムを持っているかどうか")]
    public bool isHave;
    [Header("アニメーターを入れる")]
    public Animator animator;
    [Header("プレイヤーの番号")]
    public int playerNum;

    public void StatusInit(PlayerFlag flag)
    {
        switch (flag)
        {
            case PlayerFlag.Runner:
                firstSpeed = 2;
                maxSpeed = 3;
                health = 5;
                maxHealth = 5;
                isState = true;
                isHave = false;
                //animator = GetComponent<Animator>();
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Runner");
                break;
            case PlayerFlag.Chaser:
                firstSpeed = 4;
                maxSpeed = 5;
                speed = firstSpeed;
                isState = true;
                isHave = false;
                //animator = GetComponent<Animator>();
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Chaser");
                break;
        }
    }

}
