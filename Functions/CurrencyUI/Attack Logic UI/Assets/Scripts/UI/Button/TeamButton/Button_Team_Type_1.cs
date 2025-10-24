using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Team_Type_1 : TeamButton
{
    public Team_Type_1 type1;
    protected override void Start()
    {
        base.Start();
        k = KeyCode.Alpha1;
        energyRequired = 20f;
    }

    protected override void callSpawn()
    {
        if (!canClick) return;

        if (spawner != null)
        {
            spawner.SpawnTeam(type1);
            //changeColor();
            spawner.reduceEnergy(energyRequired);
        }
    }

}
