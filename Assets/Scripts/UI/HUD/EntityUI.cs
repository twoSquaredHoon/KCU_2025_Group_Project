using UnityEngine;
using TMPro;
using System.Linq;
using System.Text;
using System.Collections.Generic;

public class TeamCounterUI : MonoBehaviour
{
    [SerializeField] private TMP_Text teamCountText;
    [SerializeField] private TMP_Text enemyCountText;
    [SerializeField] private float refreshRate = 0.5f; 

    private float timer = 0f;

    private readonly string[] allTeamTypes = { "Team_Type_1", "Team_Type_2", "Team_Type_3" };
    private readonly string[] allEnemyTypes = { "Enemy_Type_1", "Enemy_Type_2", "Enemy_Type_3" };

     private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= refreshRate)
        {
            RefreshTeamCount();
            RefreshEnemyCount(); //
            timer = 0f;
        }
    }

    private void RefreshTeamCount()
    {
        List<Team> teams = EntityManager.getTeamList();

        var teamGroups = teams
            .GroupBy(t => t.name.Replace("(Clone)", ""))
            .ToDictionary(g => g.Key, g => g.Count());

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<b>Teams on Field:</b>");
        sb.AppendLine("------------------------");

        foreach (string typeName in allTeamTypes)
        {
            int count = teamGroups.ContainsKey(typeName) ? teamGroups[typeName] : 0;
            sb.AppendLine($"{typeName}: {count}");
        }

        teamCountText.text = sb.ToString();
    }

    private void RefreshEnemyCount()
    {
        List<Enemy> enemies = EntityManager.getEnemyList();

        var enemyGroups = enemies
            .GroupBy(e => e.name.Replace("(Clone)", ""))
            .ToDictionary(g => g.Key, g => g.Count());

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<b>Enemies on Field:</b>");
        sb.AppendLine("--------------------------");

        foreach (string typeName in allEnemyTypes)
        {
            int count = enemyGroups.ContainsKey(typeName) ? enemyGroups[typeName] : 0;
            sb.AppendLine($"{typeName}: {count}");
        }

        enemyCountText.text = sb.ToString();
    }
}