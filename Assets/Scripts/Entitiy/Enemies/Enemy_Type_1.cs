using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Type_1 : Enemy
{
    public Animator anim;
    protected override void Start()
    {
        base.Start();
        hp = 40f;
        attackPower = 10f;
        attackSpeed = 0.4f;
        moveSpeed = 1f;
    }

    void LateUpdate()
    {
        anim.SetBool("isMoving", canMove);
    }

}
