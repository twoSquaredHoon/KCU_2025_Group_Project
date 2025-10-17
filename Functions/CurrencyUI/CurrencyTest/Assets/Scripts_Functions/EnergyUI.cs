using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyUI : MonoBehaviour
{
    [SerializeField] private Spawn_Team spawner;   
    [SerializeField] private Image energyBar;      
    [SerializeField] private TMP_Text energyText;  

    private float currentFill = 0f; // 화면에 표시되는 fill 상태

    private void Start()
    {
        if (spawner == null)
            spawner = FindObjectOfType<Spawn_Team>();
    }

    private void Update()
    {
        if (spawner == null || energyBar == null || energyText == null) return;

        float currentEnergy = spawner.getEnergy();
        float maxEnergy = spawner.getMaxEnergy();

        // 에너지 비율 계산
        float targetFill = currentEnergy / maxEnergy;

        // fillAmount를 부드럽게 보간 (줄거나 늘어날 때 자연스럽게)
        currentFill = Mathf.Lerp(currentFill, targetFill, Time.deltaTime * 8f);
        energyBar.fillAmount = currentFill;

        // 텍스트 표시
        energyText.text = $"⚡ {Mathf.FloorToInt(currentEnergy)} / {Mathf.FloorToInt(maxEnergy)}";
    }
}