using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class Boss : Enemy
{
    private bool isEnraged = false;
    private float aoeRange = 2.5f;
    private float aoeDamageMultiplier = 0.8f;
    protected override void Start()
    {
        base.Start();
        hp = 1000f;
        attackPower = 50f;
        attackSpeed = 1.2f;
        moveSpeed = 0.25f;
        stopDistance = 6f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (!isEnraged && hp <= 500f)
        {
            EnterEnrageMode();
        }
    }

    public override void attack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackSpeed)
        {
        // ✅ opponent가 null이거나 파괴되었으면 바로 스킵
            if (opponent == null || opponent.gameObject == null)
            {
                if (opponents.Count > 0)
                    opponents.RemoveAt(0);

                setCanMove(true);
                attackTimer = 0f;
                return;
            }

            // 기본 단일 공격
            opponent.getDamage(attackPower);

            // 근처의 다른 Team 유닛에게 범위 대미지
            AOEAttack();

            attackTimer = 0f;
        }
    }

    private void AOEAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, aoeRange);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Team"))
            {
                Team target = hit.GetComponent<Team>();
                if (target != null && target != opponent)
                {
                    target.getDamage(attackPower * aoeDamageMultiplier);
                }
            }
        }

        Debug.Log("Boss used AOE Attack!");
    }

    private void EnterEnrageMode()
    {
        isEnraged = true;
        attackSpeed *= 0.5f;
        attackPower *= 1.5f;
        spriteRenderer.color = Color.red;
        Debug.Log("Boss entered ENRAGE MODE!");
    }

    protected override void moveEntity()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    protected override void animateAndDestroy()
    {
        Debug.Log("Boss defeated! Explosion triggered.");
        EntityManager.addDeadListEnemy(this.name);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aoeRange);
    }
}
