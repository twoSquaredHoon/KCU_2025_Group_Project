using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Team_Type_1 : Button
{
    public Team_Type_1 type1;
    protected override void Start()
    {
        base.Start();
        transform.position = new Vector3(-7.50f, -3.43f, 0f);
        energyRequired = 20f;
    }

    protected override void OnMouseDown()
    {
        Debug.Log("Button Clicked!");
        if (!canClick) return;

        if (spawner != null)
        {
            Debug.Log("Team Type 1 Spawner Called!");
            spawner.SpawnTeam(type1);
            //changeColor();
            spawner.reduceEnergy(energyRequired);
        }
    }
}
