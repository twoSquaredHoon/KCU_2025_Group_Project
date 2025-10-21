using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Team_Type_2 : TeamButton
{
    public Team_Type_2 type2;
    protected override void Start()
    {
        base.Start();
        k = KeyCode.Alpha2;
        energyRequired = 30f;
    }

    protected override void callSpawn()
    {
        if (!canClick) return;

        if (spawner != null)
        {
            spawner.SpawnTeam(type2);
            //changeColor();
            spawner.reduceEnergy(energyRequired);
        }
    }

}
