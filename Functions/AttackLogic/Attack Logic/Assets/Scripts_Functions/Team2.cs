/*
using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using Random=UnityEngine.Random;

public class Team2 : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected Transform target;
    [SerializeField] protected GameObject enemy;

    // Unit Stat (HP, 공격력, 등)
    [SerializeField] protected float hp;
    protected float attackPower;
    protected float moveSpeed;
    protected float attackSpeed;

    // 리스트
    [SerializeField] protected List<GameObject> enemies;
    [SerializeField] private static List<Enemy> killedList;

    // 타이머, 거리, 등등
    protected Transform targetToStop;
    protected float attackRange;
    protected float attackTimer;
    protected float checkTimer;
    protected float checkSpeed;
    [SerializeField] protected bool canMove;
    protected bool isAttacking;
    

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hp = 100f;
        attackPower = 20f;
        moveSpeed = 5f;
        attackSpeed = 3f; //n초 마다 공격
        attackTimer = 0f;
        canMove = true;
        checkTimer = 0f;
        checkSpeed = 0.1f;
        attackRange = 0.5f;
        isAttacking = false;  

        rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }

        float y_random_position = -0.7f * (Random.value * 1 - 0.5f);
        transform.position = new Vector3(-9.5f, y_random_position, -0.13f);
    }

    protected virtual void Update()
    {
        if (hp <= 0 || transform.position.x > 15)
        {
            animateAndDestroy();
        }

        if (canMove)
        {
            moveEntity();
        }
        else if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackSpeed)
            {
                attack();
                attackTimer = 0f;
            }
        }

        checkTimer += Time.deltaTime;
        if (checkTimer >= checkSpeed)
        {
            updateEnemy();
            checkTimer = 0f;
        }
    }

    protected virtual void updateEnemy()
    {
        enemies = new List<Enemy>();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Enemy e = obj.GetComponent<Enemy>();
            if (e != null)
            {
                enemies.Add(e);
            }
        }

        if (enemies.Count > 0)
        {
            enemies.RemoveAll(e => e == null);
            enemies.Sort((a, b) =>
            {
                float distA = Vector3.Distance(transform.position, a.transform.position);
                float distB = Vector3.Distance(transform.position, b.transform.position);
                return distA.CompareTo(distB);
            });

            enemy = enemies[0];
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            //Debug.Log("Closest Enemy Distance: " + distance);

            if (distance <= attackRange)
            {
                setCanMove(false);
                isAttacking = true;
            }
            else
            {
                isAttacking = false;
                setCanMove(true);
            }
        }
        else
        {
            setCanMove(true);
            // Debug.Log("No enemies found");
        }
    }

    public virtual void attack()
    {
        if (enemy is Enemy) {
            opponent = (Enemy) enemy;
        }

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
            if (!killedList.Contains(opponents[0]))
            {
                killedList.Add(opponents[0]);
            }
            opponents.RemoveAt(0);
            setCanMove(true);
        }
    }

    
    public static void addKilledList(Enemy enemy)
    {
        killedList.Add(enemy);
    }
    public static List<Enemy> getKilledList(Enemy enemy)
    {
        List<Enemy> listCopy = new List<Enemy>(killedList);
        return listCopy;
        
    }

    public virtual void setCanMove(bool val)
    {
        canMove = val;
    }

    public virtual void getDamage(float num)
    {
        hp -= num;
        Debug.Log(spriteRenderer.sprite.name + " received " + num + " damage.");
    }

    protected virtual void moveEntity()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        // 이동하는 애니메이션 추가
    }

    protected virtual void animateAndDestroy()
    {
        // 사망 애니메이션 추가

        Destroy(gameObject);
    }

    protected virtual bool timeToAttack()
    {
        return !canMove;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        /*
        bool isOpponent = other.CompareTag("Enemy");
        if (isOpponent)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && !opponents.Contains(enemy))
            {
                opponents.Add(enemy);
                setCanMove(false);
                opponent = opponents[0];
            }
        }
        */
//}

/*
protected virtual void OnTriggerExit2D(Collider2D other)
{
    /*
    Debug.Log(name + ": TriggerExit On!");
    bool isOpponent = other.CompareTag("Enemy");
    if (isOpponent)
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
    */
    /*
}
    

}
*/