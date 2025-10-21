using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team_Type_2 : Team
{
    protected override void Start()
    {
        base.Start();
        hp = 150f;
        attackPower = 20f;
        attackSpeed = 0.6f;
        moveSpeed = 1.2f;
        stopDistance = 100f;
    }

}