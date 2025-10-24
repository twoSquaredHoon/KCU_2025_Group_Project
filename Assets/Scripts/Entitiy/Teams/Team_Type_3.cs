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
    [SerializeField] protected GameObject enemy;
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
            updateEnemy();
            checkTimer = 0f;
        }
    }

    protected virtual void updateEnemy()
    {
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        //getEnemyList();를 활용해서 하는게 더 깔끔할 것 같음. -> 시간 남으면

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
            Debug.Log("Closest Enemy Distance: " + distance);

            if (distance <= attackRange)
            {
                targetEnemy = enemy.transform;
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

    public override void attack()
    {
        Instantiate(arrowPrefab, transform.position, Quaternion.identity);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(name + ": TriggerEnter On!");
        Debug.Log(name + ": TriggerEnter Terminated");
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(name + ": TriggerExit On!");
    }

}