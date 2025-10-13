using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random=UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected Team opponent;
    [SerializeField] protected float hp;
    protected float attackPower;
    protected float moveSpeed;
    protected float attackSpeed;
    protected float attackTimer; 
    [SerializeField] protected bool canMove;
    protected float stopDistance;
    protected Transform targetToStop;
    [SerializeField] protected List<Team> opponents;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hp = 100f;
        attackPower = 20f;
        moveSpeed = 5f;
        attackSpeed = 3f; //n초 마다 공격
        attackTimer = 0f;
        canMove = true;
        stopDistance = 5f;
        opponents = new List<Team>();

        rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }

        float y_random_position = -0.7f * (Random.value * 1 -0.5f);
        transform.position = new Vector3(9.5f, y_random_position, -0.13f);
    }

    protected virtual void Update()
    {
        if (hp <= 0 || transform.position.x > 15)
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
            moveEntity();
        }
        else
        {
            //attack -> method 따로 빼기?
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
    }

    /* 
        Object 함수 
    */
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision!");
        if (canMove)
        {
            //bool isTeam = other.CompareTag(gameObject.tag);
            bool isOpponent = other.CompareTag("Team");
            if (isOpponent)
            {
                Team enemy = other.GetComponent<Team>();
                if (enemy != null && !opponents.Contains(enemy))
                {
                    opponents.Add(enemy);
                    setCanMove(false);
                    opponent = opponents[0];
                }
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
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

    /* 
        Helper 함수 
    */

    public virtual void setCanMove(bool val)
    {
        canMove = val;
    }

    public virtual void getDamage(float num)
    {
        hp -= num;
        //Debug.Log(spriteRenderer.sprite.name + " received " + num + " damage.");
    }

    protected virtual void moveEntity()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    protected virtual void animateAndDestroy()
    {
        /* 사망 애니메이션 추가 */

        Destroy(gameObject);
    }

    protected virtual bool timeToAttack()
    {
        return !canMove;
    }

}
