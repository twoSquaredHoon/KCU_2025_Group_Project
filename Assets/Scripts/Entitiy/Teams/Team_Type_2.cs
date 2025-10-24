using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team_Type_2 : Team
{
    public Animator anim;
    protected override void Start()
    {
        base.Start();
        hp = 150f;
        attackPower = 20f;
        attackSpeed = 0.5f;
        moveSpeed = 4f;
        stopDistance = 100f;
    }
    void LateUpdate()
    {
        anim.SetBool("isMoving", canMove);
    }

}