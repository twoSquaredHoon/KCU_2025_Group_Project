using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HP : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HPText;
    [SerializeField] PlayerHealth playerHealth;
    void Update() {
        HPText.text = string.Format("{0}", playerHealth.currentHealth);
    }
}
