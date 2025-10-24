using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Team_Type_3 : TeamButton
{
    public Team_Type_3 type3;
    protected override void Start()
    {
        base.Start();
        k = KeyCode.Alpha3;
        energyRequired = 50f;
    }

    protected override void callSpawn()
    {
        if (!canClick) return;

        if (spawner != null)
        {
            spawner.SpawnTeam(type3);
            //changeColor();
            spawner.reduceEnergy(energyRequired);
        }
    }

}
