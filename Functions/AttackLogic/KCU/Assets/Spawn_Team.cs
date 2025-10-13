using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Team : MonoBehaviour
{
    public float energy;
    private float energyRate;
    private float timer;


    void Start()
    {
        transform.position = new Vector3(-9.5f, -0.7f, -0.13f);
        energy = 0;
        energyRate = 0.25f; //0.25초마다 energy가 1씩 추가
        timer = 0f;
    }

    void Update()
    {
        if (timer >= 1f)
        {
            energy += 1f;
            timer = 0f;
        }
        timer += Time.deltaTime * (1 / energyRate);
    }

    public void SpawnTeam(Team team)
    {
        Instantiate(team);
    }

    public float getEnergy()
    {
        return energy;
    }

    public void reduceEnergy(float num)
    {
        energy -= num;
    }
}
