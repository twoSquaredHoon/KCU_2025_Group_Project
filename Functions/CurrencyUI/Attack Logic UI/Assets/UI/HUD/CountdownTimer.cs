using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText; 

    [SerializeField] private float startTimeMinutes = 3f; 

    private float remainingTime;
    private bool isRunning = true;

    private void Start()
    {
        remainingTime = startTimeMinutes * 60f; 
        UpdateTimerDisplay(); 
    }

    private void Update()
    {
        if (!isRunning) return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;     
            isRunning = false;                
        }

        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        timerText.text = $"{minutes:0}:{seconds:00}";
    }

    public void PauseTimer() => isRunning = false;
    public void ResumeTimer() => isRunning = true;
}