using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Spawn_Team spawner;
    protected SpriteRenderer sr;
    public Team team;
    protected bool canClick;
    protected float energyRequired;

    protected virtual void Awake()
    {
        canClick = false;
        sr = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnMouseDown()
    {
        
        if (!canClick) return;

        if (spawner != null)
        {
            spawner.SpawnTeam(team);
            //changeColor();
            spawner.reduceEnergy(energyRequired);
        }

    }

    protected virtual void Start()
    {
        energyRequired = 100f;
    }

    protected virtual void Update()
    {
        if (spawner.getEnergy() >= energyRequired)
        {
            canClick = true;
            sr.color = Color.white;
        }
        else
        {
            canClick = false;
            sr.color = Color.grey;
        }

    }

    protected virtual void changeColor()
    {
        /* 색깔 변동? 애니메이션, 등등 추가 */
    }
}
