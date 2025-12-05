using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Enemy : Enemy
{
    public Animator anim;

    protected override void Start()
    {
        base.Start();

        // ---- Boss 스탯 설정 (원하는 값으로 자유롭게 수정 가능) ----
        hp = 300f;          // 체력 크게
        attackPower = 35f;  // 공격력 높게
        attackSpeed = 0.4f; // 공격 속도 (값이 작을수록 더 자주 공격)
        moveSpeed = 0.5f;   // 보스라서 약간 느리게 이동
    }

    void LateUpdate()
    {
        // Enemy 클래스의 canMove 값에 따라 애니메이션 제어
        if (anim != null)
        {
            anim.SetBool("isMoving", canMove);
        }
    }
}
