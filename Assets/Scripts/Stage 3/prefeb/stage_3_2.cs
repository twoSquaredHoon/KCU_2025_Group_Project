using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_3_2 : Enemy
{
    // public Animator anim;
    protected override void Start()
    {
        base.Start();
        hp = 60f;
        attackPower = 20f;
        attackSpeed = 1f;
        moveSpeed = 1f;
    }

    public override void attack()
    {
        if (opponent != null)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackSpeed)
            {
                opponent.freeze(attackSpeed * 0.3f);
                opponent.getDamage(attackPower);
                attackTimer = 0f;
            }
        }
        if (opponents[0] == null)
        {
            opponents.RemoveAt(0);
            setCanMove(true);
        }
    }

    // void LateUpdate()
    // {
    //     anim.SetBool("isMoving", canMove);
    // }

}
