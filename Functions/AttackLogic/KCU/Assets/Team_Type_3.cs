using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Team_Type_3 : Team
{
    protected float attackRange;
    protected Transform targetEnemy;
    protected bool isAttacking;
    public Arrow arrowPrefab;
    protected float checkTimer;
    protected float checkSpeed;
    [SerializeField] protected List<GameObject> enemies;
    protected override void Start()
    {
        base.Start();
        hp = 80f;
        attackPower = 13f;
        attackSpeed = 1f;
        moveSpeed = 0.8f;
        attackRange = 4f;
        isAttacking = false;
        checkTimer = 0f;
        checkSpeed = 0.1f;
        arrowPrefab.setDamage(attackPower);
        arrowPrefab.setAttackRange(attackRange);
    }

    protected override void Update()
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
            checkForEnemy();
            checkTimer = 0f;
        }
    }

    protected virtual void checkForEnemy()
    {
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        if (!(enemies.Count == 0))
        {
            if (enemies[0] == null)
            {
                enemies.RemoveAt(0);
            }
            else
            {
                GameObject enemy = enemies[0];
                float distance = enemy.transform.position.x - transform.position.x;
                Debug.Log("Distance: " + distance);

                if (distance <= attackRange)
                {
                    targetEnemy = enemy.transform;
                    setCanMove(false);
                    isAttacking = true;
                }
                else
                {
                    isAttacking = false;
                }
            }
        }
        else
        {
            if (!isAttacking)
            {
                setCanMove(true);
            }
            //Debug.Log("No enemy on the list");
        }
    }

    public override void attack()
    {
        Instantiate(arrowPrefab, transform.position, Quaternion.identity);
    }
    
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(name + ": TriggerEnter On!");
        Debug.Log(name + ": TriggerEnter Terminated");
    }

}