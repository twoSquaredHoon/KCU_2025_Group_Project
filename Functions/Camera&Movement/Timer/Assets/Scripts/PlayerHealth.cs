using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public void changeHealth(int amount) {
        currentHealth += amount;
        // damage is negative number

        if (currentHealth <= 0) {
            gameObject.SetActive(false);
        }
    }
}
