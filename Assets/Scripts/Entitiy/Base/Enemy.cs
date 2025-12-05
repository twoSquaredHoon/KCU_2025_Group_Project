using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using Random=UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable
{
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected IDamageable opponent;
    Animator animator;

    // Unit Stat (HP, 공격력, 등)
    [SerializeField] protected float hp;
    protected float attackPower;
    protected float moveSpeed;
    protected float attackSpeed;
    [SerializeField] protected float attackTimer;
    [SerializeField] protected bool canMove;
    protected float stopDistance;
    protected Transform targetToStop;
    [SerializeField] protected List<IDamageable> opponents;
    [SerializeField] protected bool frozen;
    [SerializeField] protected float frozenTimer;

    private Coroutine freezeCoroutine = null;
    private Color originalColor;

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
            rb.isKinematic = true;
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
        if (!other.CompareTag("Team") && !other.CompareTag("Player"))
        {
            return;
        }
            
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
        if (opponent != null)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackSpeed)
            {
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
    

    public virtual void setCanMove(bool val)
    {
        canMove = val;
    }

    public virtual void getDamage(float num)
    {
        hp -= num;
        // Debug.Log(spriteRenderer.sprite.name + " received " + num + " damage.");
    }

    protected virtual void moveEntity()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        /* 이동하는 애니메이션 추가 */
    }

    protected virtual void animateAndDestroy()
    {
        /* 사망 애니메이션 추가 */
        EntityManager.Unregister(this);
        EntityManager.addDeadListEnemy(this.name);
        Destroy(gameObject);
    }

    protected virtual bool timeToAttack()
    {
        return !canMove;
    }

    public virtual void freeze(float num)
    {
        // 초기 originalColor 저장
        if (originalColor == default(Color))
            originalColor = spriteRenderer.color;

        // 기존 코루틴이 있으면 중지 + 복구
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
            UnfreezeState();   // 애니메이션/색 복구
        }

        // 새로운 freeze 시작
        freezeCoroutine = StartCoroutine(freezeHelp(num));
    }

    private IEnumerator freezeHelp(float num)
    {
        ApplyFreezeState();

        yield return new WaitForSeconds(num);

        UnfreezeState();

        freezeCoroutine = null;
    }

    private void ApplyFreezeState()
    {
        frozen = true;

        if (animator != null)
            animator.speed = 0f;

        spriteRenderer.color = new Color(0f, 0.2f, 0.7f, 1f);
    }

    private void UnfreezeState()
    {
        frozen = false;

        if (animator != null)
            animator.speed = 1f;

        spriteRenderer.color = originalColor;
    }

    public virtual void knockback(float knockbackDist, float freezeTime)
    {
        StopCoroutine("KnockbackCoroutine");  // avoid duplicate knockbacks
        StartCoroutine(KnockbackCoroutine(knockbackDist, freezeTime));
    }

    private IEnumerator KnockbackCoroutine(float knockbackDist, float freezeTime)
    {
        float knocked = 0f;
        float knockSpeed = 5f;  // tune this value

        setCanMove(false);

        while (knocked < knockbackDist)
        {
            knockSpeed += 0.3f;
            float move = knockSpeed * Time.deltaTime;
            transform.position += Vector3.left * move;
            knocked += move;

            yield return null; // wait for next frame
        }

        setCanMove(true);
        freeze(freezeTime);
    }

}
