using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dawn_attack : MonoBehaviour
{
    protected float damage;
    protected float moveSpeed;
    protected float terminatePosition;
    protected float attackRange;

    void Start()
    {
        damage = 10;
        moveSpeed = 10f;
        attackRange = 8f;
        terminatePosition = transform.position.x - attackRange;
    }


    void Update()
    {
        if (transform.position.x < terminatePosition)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
        }
    }

    public void setDamage(float damage)
    {
        this.damage = damage;
    }

    public void setAttackRange(float range)
    {
        this.attackRange = range;
        terminatePosition = transform.position.x - attackRange;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        bool isOpponent = other.CompareTag("Team");
        if (isOpponent)
        {
            Team opponent = other.GetComponent<Team>();
            if (opponent != null)
            {
                opponent.getDamage(damage);
                opponent.freeze(2);
                Destroy(gameObject);
            }
        }
    }
}
