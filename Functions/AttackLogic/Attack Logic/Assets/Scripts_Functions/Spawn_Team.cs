using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Team : MonoBehaviour
{
    [SerializeField] private float energy;
    private float energyRate;
    private float maxEnergy;
    private float timer;
    

    void Start()
    {
        transform.position = new Vector3(-9.5f, -0.7f, -0.13f);
        energy = 0f;
        energyRate = 0.25f; //0.25초마다 energy가 1씩 추가
        timer = 0f;
        maxEnergy = 100f;
    }

    void Update()
    {
        if(energy < maxEnergy)
        {
            if (timer >= 1f)
            {
                energy += 1f;
                timer = 0f;
            }
            timer += Time.deltaTime * (1 / energyRate);
        }
    }

    public void SpawnTeam(Team team)
    {
        Instantiate(team);
    }

    public float getEnergy()
    {
        return energy;
    }

    public float getMaxEnergy()
    {
        return maxEnergy;
    }

    public void reduceEnergy(float num)
    {
        energy -= num;
    }

}
