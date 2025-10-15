using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type_2 : Enemy
{
    protected override void Start()
    {
        base.Start();
        hp = 70f;
        attackPower = 13f;
        attackSpeed = 0.2f;
        moveSpeed = 0.6f;
    }

}