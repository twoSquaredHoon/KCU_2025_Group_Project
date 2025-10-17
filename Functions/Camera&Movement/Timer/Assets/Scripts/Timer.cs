using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;
 
    void Update() {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        // 초 단위로 시간 보기
        // timerText.text = elapsedTime.ToString();
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); 

    }
}
