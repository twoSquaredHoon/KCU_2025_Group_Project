using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public float speed;
    public Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    public Animator anim;
    public int facingDirection;
    public float hp;
    [SerializeField] protected bool frozen;
    [SerializeField] protected bool canMove;
    [SerializeField] protected float frozenTimer;

    private Coroutine freezeCoroutine = null;
    private Color originalColor;
    void Start()
    {
        frozen = false;
        speed = 5f;
        facingDirection = 1;
        hp = 200f;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void getDamage(float dmg)
    {
        hp -= dmg;
        Debug.Log("Player took " + dmg + " damage! HP = " + hp);

        if (hp <= 0)
        {
            Debug.Log("Player died!");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!frozen)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
            {
                Flip();
            }

            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));

            rb.linearVelocity = new Vector2(horizontal, vertical) * speed;
        } else
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public virtual void freeze(float num)
    {
        // 초기 originalColor 저장
        if (originalColor == default(Color))
            originalColor = spriteRenderer.color;

        // 기존 코루틴이 있으면 중지 + 복구
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
            UnfreezeState();   // 애니메이션/색 복구
        }

        // 새로운 freeze 시작
        freezeCoroutine = StartCoroutine(freezeHelp(num));
    }

    private IEnumerator freezeHelp(float num)
    {
        ApplyFreezeState();

        yield return new WaitForSeconds(num);

        UnfreezeState();

        freezeCoroutine = null;
    }

    private void ApplyFreezeState()
    {
        frozen = true;

        if (anim != null)
            anim.speed = 0f;

        spriteRenderer.color = new Color(0f, 0.2f, 0.7f, 1f);
    }

    private void UnfreezeState()
    {
        frozen = false;

        if (anim != null)
            anim.speed = 1f;

        spriteRenderer.color = originalColor;
    }

    public virtual void knockback(float knockbackDist, float freezeTime)
    {
        StopCoroutine("KnockbackCoroutine");  // avoid duplicate knockbacks
        StartCoroutine(KnockbackCoroutine(knockbackDist, freezeTime));
    }

    private IEnumerator KnockbackCoroutine(float knockbackDist, float freezeTime)
    {
        float knocked = 0f;
        float knockSpeed = 5f;  // tune this value

        canMove = false;

        while (knocked < knockbackDist)
        {
            knockSpeed += 0.3f;
            float move = knockSpeed * Time.deltaTime;
            transform.position += Vector3.left * move;
            knocked += move;

            yield return null; // wait for next frame
        }

        canMove = true;
        freeze(freezeTime);
    }
}