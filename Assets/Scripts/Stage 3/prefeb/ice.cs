using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ice : MonoBehaviour
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
        if (!other.CompareTag("Team") && !other.CompareTag("Player"))
        {
            return;
        }

        IDamageable opponent = other.GetComponent<IDamageable>();
        if (opponent != null)
        {
            opponent.getDamage(damage);
            opponent.freeze(0.5f);
            Destroy(gameObject);
        }
    }
}
