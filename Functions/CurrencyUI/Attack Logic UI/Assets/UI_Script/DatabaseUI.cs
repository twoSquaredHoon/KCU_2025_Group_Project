using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;


public class DatabaseUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject panel; // DatabasePanel
    [SerializeField] private TMP_Text summaryText;
    [SerializeField] private Image DatabaseImage;

    private bool isOpen = false;

    private void Start()
    {
        if (panel != null) panel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpen)
                CloseDatabase();
            else
                OpenDatabase();
        }
    }

    private void OpenDatabase()
    {
        if (panel == null) return;

        Time.timeScale = 0f;
        isOpen = true;
        panel.SetActive(true);

        RefreshData();
    }

    private void CloseDatabase()
    {
        if (panel == null) return;

        Time.timeScale = 1f;
        isOpen = false;
        panel.SetActive(false);
    }

    private void RefreshData()
    {
        var enemies = EntityManager.getEnemyList();
        var teams = EntityManager.getTeamList();
        var deadEnemies = EntityManager.getDeadEnemyList();
        var deadTeams = EntityManager.getDeadTeamList();

        var enemyTypeCounts = enemies
            .GroupBy(e => e.name.Replace("(Clone)", ""))
            .OrderBy(g => g.Key) 
            .ToDictionary(g => g.Key, g => g.Count());

        var deadEnemyTypeCounts = deadEnemies
            .GroupBy(n => n)
            .OrderBy(g => g.Key)
            .ToDictionary(g => g.Key, g => g.Count());

        int totalEnemiesAlive = enemies.Count;
        int totalEnemiesDead = deadEnemies.Count;
        int totalTeamsAlive = teams.Count;
        int totalTeamsDead = deadTeams.Count;
        int totalEnemiesSpawned = totalEnemiesAlive + totalEnemiesDead;
        int totalTeamsSpawned = totalTeamsAlive + totalTeamsDead;

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("<b>DATABASE</b>");
        sb.AppendLine();
        sb.AppendLine($"<b>Total Enemies:</b> {totalEnemiesAlive} alive / {totalEnemiesDead} dead (Spawned {totalEnemiesSpawned})");
        sb.AppendLine($"<b>Total Teams:</b> {totalTeamsAlive} alive / {totalTeamsDead} dead (Spawned {totalTeamsSpawned})");
        sb.AppendLine();
        sb.AppendLine("<b>Enemy Types:</b>");
        sb.AppendLine("-------------------------");

        if (enemyTypeCounts.Count == 0 && deadEnemyTypeCounts.Count == 0)
        {
            sb.AppendLine("None");
        }
        else
        {
            var allTypes = enemyTypeCounts.Keys.Union(deadEnemyTypeCounts.Keys).OrderBy(k => k);
            foreach (var typeName in allTypes)
            {
                int alive = enemyTypeCounts.ContainsKey(typeName) ? enemyTypeCounts[typeName] : 0;
                int dead = deadEnemyTypeCounts.ContainsKey(typeName) ? deadEnemyTypeCounts[typeName] : 0;
                int total = alive + dead;
                sb.AppendLine($"  {typeName}: {alive} alive / {dead} dead (Total {total})");
            }
        }

        sb.AppendLine();

        summaryText.text = sb.ToString();
    }

}