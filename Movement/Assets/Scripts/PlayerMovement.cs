using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // How fast player moves
    public float speed = 5;
    // Apply the speed
    public Rigidbody2D rb;
    // public Animator anim;
    public int facingDirection = 1;


    void Start()
    {
        
    }

    // Update is called x50 per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // if looking direction and movement movement direction is opposite, flip
        if ((horizontal > 0 && transform.localScale.x < 0) || (horizontal < 0 && transform.localScale.x > 0))
        {
            Flip();
        }

        // anim.SetFloat("horizontal", Mathf.Abs(horizontal));
        // anim.SetFloat("vertical", Mathf.Abs(vertical));

        rb.linearVelocity = new Vector2(horizontal, vertical) * speed;
    }
    
    void Flip() {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
