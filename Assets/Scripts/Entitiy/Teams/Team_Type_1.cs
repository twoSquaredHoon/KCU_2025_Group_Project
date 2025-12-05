using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team_Type_1 : Team
{
    public Animator anim;
    protected override void Start()
    {
        base.Start();
        hp = 50000f;
        attackPower = 8f;
        attackSpeed = 0.41f;
        moveSpeed = 2f;
    }
    void LateUpdate()
    {
        anim.SetBool("isMoving", canMove);
    }
}
