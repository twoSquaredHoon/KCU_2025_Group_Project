using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Team_Type_3 : Button
{
    public Team_Type_3 type3;
    protected override void Start()
    {
        base.Start();
        transform.position = new Vector3(-4.5f, -3.43f, 0f);
        energyRequired = 10f;
    }

    protected override void OnMouseDown()
    {
        Debug.Log("Button Clicked!");
        if (!canClick) return;

        if (spawner != null)
        {
            Debug.Log("Team Type 3 Spawnder Called!");
            spawner.SpawnTeam(type3);
            //changeColor();
            spawner.reduceEnergy(energyRequired);
        }
    }

}
