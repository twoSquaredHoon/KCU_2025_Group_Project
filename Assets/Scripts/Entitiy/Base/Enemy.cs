using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable
{
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected IDamageable opponent;


    // Unit Stat (HP, 공격력, 등)
    protected bool isDead = false;
    [SerializeField] protected float hp;
    protected float attackPower;
    protected float moveSpeed;
    protected float attackSpeed;
    protected float attackTimer;
    [SerializeField] protected bool canMove;
    protected float stopDistance;
    protected Transform targetToStop;
    [SerializeField] protected List<IDamageable> opponents;

    protected virtual void Start()
    {
        EntityManager.Register(this);
        spriteRenderer = GetComponent<SpriteRenderer>();
        hp = 100f;
        attackPower = 20f;
        moveSpeed = 5f;
        attackSpeed = 3f; //n초 마다 공격
        attackTimer = 0f;
        canMove = true;
        stopDistance = 5f;
        opponents = new List<IDamageable>();

        rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic; ;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }

        float y_random_position = -0.7f * (Random.value * 1 - 0.5f);
        transform.position = new Vector3(19f, y_random_position, -0.13f);
    }

    protected virtual void Update()
    {
        if (hp <= 0 || transform.position.x < -18)
        {
            animateAndDestroy();
        }

        if (targetToStop != null && canMove)
        {
            float distance = targetToStop.position.x - transform.position.x;

            if (Math.Abs(distance) < stopDistance)
            {
                setCanMove(false);
            }
        }

        if (canMove)
        {
            if (opponents.Count == 0)
            {
                moveEntity();
            }
        }
        else
        {
            attack();
        }
    }

    /* 
        Object 함수 
    */



    /* 다른 Collider이랑 부딪혔을 때 Tag가 Enemy이면 Opponent List에 opponent를 추가함
     * canMove를 false로 바꿈
     * 
     * @param other : 다른 유닛 collider (Team & Enemy 포함)
     */
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        // Enemy should only attack TEAM and PLAYER
        if (!other.CompareTag("Team") && !other.CompareTag("Player"))
            return;

        IDamageable target = other.GetComponent<IDamageable>();

        if (target != null && !opponents.Contains(target))
        {
            opponents.Add(target);
            setCanMove(false);
            opponent = opponents[0];
        }
    }


    /* 부딪혔던 Collider이랑 더 이상 부딪힌 상태가 아니라면 발동 됨
     * opponents 리스트가 비어있으면 움직이도록 설정
     * 
     * @param other : 다른 유닛 collider (Team & Enemy 포함)
     */
    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        IDamageable target = other.GetComponent<IDamageable>();
        if (target == null) return;

        // Remove the thing that just left our range
        opponents.Remove(target);

        if (opponents.Count > 0)
        {
            opponent = opponents[0];
            setCanMove(false);  // still someone to fight
        }
        else
        {
            opponent = null;
            setCanMove(true);   // no one nearby, start moving again
        }
    }


    /* 
        Helper 함수 
    */

    /* opponents 리스트 가장 첫번째 유닛 (opponent)에게 attackPower만큼 대미지를 줌.
     * 
     */
    public virtual void attack()
    {
        // 1. Remove destroyed/null opponents BEFORE attacking
        opponents.RemoveAll(o => o == null);

        // 2. No opponents left? Stop attacking
        if (opponents.Count == 0)
        {
            opponent = null;
            setCanMove(true);
            return;
        }

        // 3. Always use a valid opponent
        opponent = opponents[0];

        // 4. Attack safely
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackSpeed)
        {
            opponent.getDamage(attackPower);
            attackTimer = 0f;

            opponents.RemoveAll(o => o == null);

            if (opponents.Count == 0)
            {
                opponent = null;
                setCanMove(true);
            }
        }
    }



    public virtual void setCanMove(bool val)
    {
        canMove = val;
    }

    public virtual void getDamage(float num)
    {
        hp -= num;

        Debug.Log("Enemy took " + num + " damage.");

        if (hp <= 0)
        {
            animateAndDestroy();
        }
    }

    protected virtual void moveEntity()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        /* 이동하는 애니메이션 추가 */
    }

    protected virtual void animateAndDestroy()
    {
        if (isDead) return;
        isDead = true;

        EntityManager.Unregister(this);

        string deadName = gameObject != null ? gameObject.name : "Unknown";
        EntityManager.addDeadListEnemy(deadName);

        Destroy(gameObject);
    }


    protected virtual bool timeToAttack()
    {
        return !canMove;
    }



}
