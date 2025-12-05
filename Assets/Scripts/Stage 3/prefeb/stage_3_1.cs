using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class stage_3_1 : Enemy
{
    // public Animator anim;
    protected float attackRange;
    protected Transform targetEnemy;
    protected bool isAttacking;
    public ice icePrefab;
    protected float checkTimer;
    protected float checkSpeed;
    [SerializeField] protected GameObject opponentEntity;
    [SerializeField] protected List<GameObject> opponentEntities;
    protected override void Start()
    {
        base.Start();
        hp = 80f;
        attackPower = 13f;
        attackSpeed = 1f;
        moveSpeed = 2f;
        attackRange = 8f;
        isAttacking = false;
        checkTimer = 0f;
        checkSpeed = 0.1f;
        icePrefab.setDamage(attackPower);
        icePrefab.setAttackRange(attackRange);

    }

    protected override void Update()
    {
        if (hp <= 0 || transform.position.x < -15)
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
        opponentEntities = new List<GameObject>(GameObject.FindGameObjectsWithTag("Team"));
        //getEnemyList();를 활용해서 하는게 더 깔끔할 것 같음. -> 시간 남으면

        if (opponentEntities.Count > 0)
        {
            opponentEntities.RemoveAll(e => e == null);
            opponentEntities.Sort((a, b) =>
            {
                float distA = Vector3.Distance(transform.position, a.transform.position);
                float distB = Vector3.Distance(transform.position, b.transform.position);
                return distA.CompareTo(distB);
            });

            opponentEntity = opponentEntities[0];

            float distance = Vector3.Distance(transform.position, opponentEntity.transform.position);
            //Debug.Log("Closest Enemy Distance: " + distance);

            if (distance <= attackRange)
            {
                targetEnemy = opponentEntity.transform;
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
        Instantiate(icePrefab, transform.position, Quaternion.identity);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(name + ": TriggerEnter On!");
        //Debug.Log(name + ": TriggerEnter Terminated");
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log(name + ": TriggerExit On!");
    }

    /* void LateUpdate()
    {
        anim.SetBool("isMoving", canMove);
    } */
}
