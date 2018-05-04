using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : Controller{

    [SerializeField, Header("初速")]
    public float MinSpeed;
    [SerializeField, Header("最高速度")]
    public float MaxSpeed;
    [SerializeField, Header("スピード")]
    public float Speed;
    [SerializeField, Header("逃げる側のスタミナ")]
    public float Stamina;
    [SerializeField, Header("スタミナがあるかないか")]
    public bool StaminaON;
    [SerializeField, Header("アニメーターを入れる")]
    public Animator animator;

}
