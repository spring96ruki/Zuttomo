﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerStatus : MonoBehaviour{

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
    [Header("プレイヤーが動けかどうか")]
    public bool isState;
    [Header("アイテムを持っているかどうか")]
    public bool ishave;
    [Header("アニメーターを入れる")]
    public Animator animator;
    [Header("プレイヤーの番号")]
    public int runnerNum;

}
