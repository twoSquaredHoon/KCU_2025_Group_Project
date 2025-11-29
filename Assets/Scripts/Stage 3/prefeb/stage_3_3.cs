using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_3_3 : Enemy
{
    // public Animator anim;
    protected override void Start()
    {
        base.Start();
        hp = 400f;
        attackPower = 10f;
        attackSpeed = 0.4f;
        moveSpeed = 1f;
    }

    // void LateUpdate()
    // {
    //     anim.SetBool("isMoving", canMove);
    // }

}
