using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyUI : MonoBehaviour
{
    [SerializeField] private Spawn_Team spawner;   // energy 데이터를 가져올 스크립트
    [SerializeField] private Image energyBar;      // 에너지 바 이미지
    [SerializeField] private TMP_Text energyText;  // 에너지 수치 텍스트

    private void Start()
    {
        if (spawner == null)
            spawner = FindObjectOfType<Spawn_Team>();
    }

    private void Update()
    {
        if (spawner == null || energyBar == null || energyText == null) return;

        float current = spawner.getEnergy();
        float max = spawner.getMaxEnergy();

        // 게이지 채우기
        energyBar.fillAmount = current / max;

        // 텍스트 표시
        energyText.text = $"⚡ {Mathf.FloorToInt(current)} / {Mathf.FloorToInt(max)}";
    }
}