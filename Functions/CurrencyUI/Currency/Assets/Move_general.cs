using UnityEngine;

public class Move_general : MonoBehaviour
{
    public float speed = 5f;        // movement speed
    public float rightLimit = 12f;  // when to disappear
    private bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            // When character goes off-screen right, deactivate
            if (transform.position.x > rightLimit)
            {
                gameObject.SetActive(false);
                isMoving = false;
            }
        }
    }

    public void StartMoving()
    {
        // reset to left start position
        transform.position = new Vector3(-10f, -1f, 0f);
        gameObject.SetActive(true);
        isMoving = true;
    }
}
