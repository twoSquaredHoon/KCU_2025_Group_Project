using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyUI : MonoBehaviour
{
    [SerializeField] private Spawn_Team spawner;   
    [SerializeField] private Image energyBar;      
    [SerializeField] private TMP_Text energyText;  

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
        energyText.text = $"{Mathf.FloorToInt(current)} / {Mathf.FloorToInt(max)}";
    }
}