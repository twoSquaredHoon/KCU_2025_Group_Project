using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    protected float damage;
    protected float moveSpeed;
    protected float terminatePosition;
    protected float attackRange;

    void Start()
    {
        damage = 10;
        moveSpeed = 10f;
        attackRange = 4f;
        terminatePosition = transform.position.x + attackRange;
    }


    void Update()
    {
        if (transform.position.x > terminatePosition)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
    }

    public void setDamage(float damage)
    {
        this.damage = damage;
    }

    public void setAttackRange(float range)
    {
        this.attackRange = range;
        terminatePosition = transform.position.x + attackRange;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        bool isOpponent = other.CompareTag("Enemy");
        if (isOpponent)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.getDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
