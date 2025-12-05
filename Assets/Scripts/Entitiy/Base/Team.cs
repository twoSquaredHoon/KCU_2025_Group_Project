using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Team : MonoBehaviour, IDamageable
{
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected IDamageable opponent;

    // Unit Stat
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

    // Freeze system (from 11/17/2025 branch)
    [SerializeField] protected bool frozen = false;
    [SerializeField] protected float frozenTimer = 0f;

    protected virtual void Start()
    {
        EntityManager.Register(this);
        spriteRenderer = GetComponent<SpriteRenderer>();

        hp = 100f;
        attackPower = 20f;
        moveSpeed = 5f;   // 최신 버전 유지
        attackSpeed = 3f;
        attackTimer = 0f;
        canMove = true;
        stopDistance = 5f;
        opponents = new List<IDamageable>();

        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }

        float y_random_position = -0.7f * (Random.value * 1 - 0.5f);
        transform.position = new Vector3(-19f, y_random_position, -0.13f);
    }

    protected virtual void Update()
    {
        if (hp <= 0 || transform.position.x > 18)
        {
            animateAndDestroy();
        }

        if (frozen) return; // freeze 상태면 아무것도 못함

        if (targetToStop != null && canMove)
        {
            float distance = targetToStop.position.x - transform.position.x;
            if (Math.Abs(distance) < stopDistance)
                setCanMove(false);
        }

        if (canMove)
        {
            if (opponents.Count == 0)
                moveEntity();
        }
        else
        {
            attack();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        IDamageable target = other.GetComponent<IDamageable>();
        if (target != null && !opponents.Contains(target))
        {
            opponents.Add(target);
            setCanMove(false);
            opponent = opponents[0];
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy"))
            return;

        IDamageable target = other.GetComponent<IDamageable>();
        if (target == null) return;

        opponents.Remove(target);

        if (opponents.Count > 0)
        {
            opponent = opponents[0];
            setCanMove(false);
        }
        else
        {
            opponent = null;
            setCanMove(true);
        }
    }

    public virtual void attack()
    {
        opponents.RemoveAll(o => o == null);

        if (opponents.Count == 0)
        {
            opponent = null;
            setCanMove(true);
            return;
        }

        opponent = opponents[0];

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
        if (!frozen)
            canMove = val;
    }

    public virtual void getDamage(float num)
    {
        if (this == null || spriteRenderer == null)
            return;

        hp -= num;

        if (hp <= 0)
            animateAndDestroy();
    }

    protected virtual void moveEntity()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

    protected virtual void animateAndDestroy()
    {
        if (isDead) return;
        isDead = true;

        EntityManager.Unregister(this);

        string deadName = gameObject != null ? gameObject.name : "Unknown";
        EntityManager.addDeadListTeam(deadName);

        Destroy(gameObject);
    }

    // Freeze system fully restored
    public virtual void freeze(float duration)
    {
        if (frozen)
        {
            frozenTimer = 0f;
        }
        else
        {
            StartCoroutine(freezeCoroutine(duration));
        }
    }

    protected IEnumerator freezeCoroutine(float duration)
    {
        frozen = true;
        frozenTimer = 0f;

        Color original = spriteRenderer.color;
        spriteRenderer.color = new Color(0f, 0.2f, 0.7f, 1f);

        while (frozenTimer < duration)
        {
            frozenTimer += Time.deltaTime;
            yield return null;
        }

        frozen = false;
        frozenTimer = 0f;
        spriteRenderer.color = original;
    }
}
