using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type_3 : Enemy
{
    protected override void Start()
    {
        base.Start();
        hp = 200f;
        attackPower = 14f;
        attackSpeed = 0.4f;
        moveSpeed = 0.3f;
    }

}