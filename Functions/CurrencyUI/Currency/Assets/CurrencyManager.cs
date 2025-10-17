using UnityEngine;
using TMPro; // lets us show text on screen

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    public int coins = 100; // how many coins you start with
    public TextMeshProUGUI coinText; // this will point to the UI text

    void Awake() { instance = this; }

    void Start() { UpdateCoinUI(); }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            UpdateCoinUI();
            return true;
        }
        else
        {
            Debug.Log("Not enough coins!");
            return false;
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinUI();
    }

    void UpdateCoinUI()
    {
        coinText.text = "Coins: " + coins;
    }
}