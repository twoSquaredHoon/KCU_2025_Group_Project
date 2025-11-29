using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class stage_3_boss : Enemy
{
    protected float attackRange;
    protected Transform targetEnemy;
    protected bool isAttacking;
    public dawn_attack dawn_attack_prefab;
    protected float checkTimer;
    protected float checkSpeed;
    [SerializeField] protected float attackTimer1;
    [SerializeField] protected float attackTimer2;
    [SerializeField] protected float attackTimer3;
    protected float attackSpeed1;
    protected float attackSpeed2;
    protected float attackSpeed3;
    [SerializeField] protected GameObject opponentEntity;
    [SerializeField] protected List<GameObject> opponentEntities;
    protected override void Start()
    {
        base.Start();
        hp = 2000f;
        attackPower = 30f;
        attackSpeed = 1f;
        attackSpeed1 = 4f;
        attackSpeed2 = 10f;
        attackSpeed3 = 20f;
        moveSpeed = 0.3f;
        attackRange = 2f;
        isAttacking = false;
        checkTimer = 0f;
        checkSpeed = 0.1f;
        dawn_attack_prefab.setDamage(attackPower);
        dawn_attack_prefab.setAttackRange(attackRange);
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
            attack();
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
        }
    }

    public override void attack()
    {
        if (opponentEntity != null)
        {
            opponent = opponentEntity.GetComponent<Team>();
            attackTimer1 += Time.deltaTime;
            attackTimer2 += Time.deltaTime;
            attackTimer3 += Time.deltaTime;
            
            if (attackTimer3 >= attackSpeed3)
            {
                attack_3();
                attackTimer1 = 0;
                attackTimer2 = 0;
                attackTimer3 = 0;
            } else if  (attackTimer2 >= attackSpeed2) {
                attack_2();
                attackTimer1 = 0;
                attackTimer2 = 0;
            } else if (attackTimer1 >= attackSpeed1)
            {
                attack_1();
                attackTimer1 = 0;
            }
            if (attackTimer >= (attackSpeed1 * attackSpeed2 * attackSpeed3))
            {
                attackTimer = 0;
            }
        }
        if (opponentEntities[0] == null)
        {
            opponentEntities.RemoveAt(0);
            setCanMove(true);
        }
        
    }

    public void attack_1()
    {
        Debug.Log("Attack 1");
        opponent.freeze(2);
        opponent.getDamage(attackPower);
    }

    public void attack_2()
    {
        Debug.Log("Attack 2");
        foreach (GameObject obj in opponentEntities) {
            if (Vector3.Distance(transform.position, obj.transform.position) <= attackRange * 2f)
            {
                opponent = obj.GetComponent<Team>();
                opponent.freeze(3f);
                opponent.getDamage(attackPower * 1.3f);
            }
        }
    }

    public void attack_3()
    {
        Debug.Log("Attack 3");
        foreach (GameObject obj in opponentEntities) {
            if (Vector3.Distance(transform.position, obj.transform.position) <= attackRange * 2f) {
                opponent = obj.GetComponent<Team>();
                opponent.knockback(3f, 3f);
                opponent.getDamage(attackPower * 2f);
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        
    }
}
