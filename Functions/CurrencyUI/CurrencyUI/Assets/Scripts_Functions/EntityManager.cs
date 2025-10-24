using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EntityManager : MonoBehaviour
{
    public static List<Enemy> enemies = new List<Enemy>();
    public static List<Team> teams = new List<Team>();
    public static List<string> deadEnemyList = new List<string>();
    public static List<string> deadTeamList = new List<string>();


    [SerializeField] private List<Enemy> debugEnemies;
    [SerializeField] private List<Team> debugTeams;
    [SerializeField] private List<string> debugDeadEnemies;
    [SerializeField] private List<string> debugDeadTeams;

    private void Update()
    {
        debugEnemies = new List<Enemy>(enemies);
        debugTeams = new List<Team>(teams);
        debugDeadEnemies = new List<string>(deadEnemyList);
        debugDeadTeams = new List<string>(deadTeamList);

        enemies.RemoveAll(e => e == null || e.gameObject == null);
        teams.RemoveAll(t => t == null || t.gameObject == null);
    }

    public static void Register(Enemy e)
    {
        enemies.Add(e);
    }
    public static void Register(Team t)
    {
        teams.Add(t);
    }
    public static void Unregister(Enemy e)
    {
        enemies.Remove(e);
    }
    public static void Unregister(Team t)
    {
        teams.Remove(t);
    }

    public static void addDeadListEnemy(string e)
    {
        deadEnemyList.Add(e);
    }

    public static void addDeadListTeam(string t)
    {
        deadTeamList.Add(t);
    }


    public static List<Enemy> getEnemyList()
    {
        List<Enemy> copy = new List<Enemy>(enemies);
        return copy;
    }

    public static List<Team> getTeamList()
    {
        List<Team> copy = new List<Team>(teams);
        return copy;
    }

    public static List<string> getDeadEnemyList()
    {
        List<string> copy = new List<string>(deadEnemyList);
        return copy;
    }

    public static List<string> getDeadTeamList()
    {
        List<string> copy = new List<string>(deadTeamList);
        return copy;
    }

}