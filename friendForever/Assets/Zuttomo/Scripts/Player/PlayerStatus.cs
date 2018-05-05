using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

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
    [Header("アニメーターを入れる")]
    public Animator animator;
}
