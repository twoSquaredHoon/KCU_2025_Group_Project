using UnityEngine;

public class Move_general : MonoBehaviour
{
    public float speed = 5f;
    public float rightLimit = 12f;
    private bool isMoving = false;
    public int spawnCost = 10; // cost to spawn

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (transform.position.x > rightLimit)
            {
                gameObject.SetActive(false);
                isMoving = false;
            }
        }
    }

    public void StartMoving()
    {
        // Spend 10 coins before spawning
        if (CurrencyManager.instance.SpendCoins(spawnCost))
        {
            transform.position = new Vector3(-10f, -2.5f, 0f);
            gameObject.SetActive(true);
            isMoving = true;
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }
}