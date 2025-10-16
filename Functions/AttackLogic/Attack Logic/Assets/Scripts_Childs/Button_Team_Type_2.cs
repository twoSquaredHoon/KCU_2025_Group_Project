using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Team_Type_2 : Button
{
    public Team_Type_2 type2;
    protected override void Start()
    {
        base.Start();
        transform.position = new Vector3(-6f, -3.43f, 0f);
        energyRequired = 30f;
    }

    protected override void OnMouseDown()
    {
        Debug.Log("Button Clicked!");
        if (!canClick) return;

        if (spawner != null)
        {
            Debug.Log("Team Type 2 Spawnder Called!");
            spawner.SpawnTeam(type2);
            //changeColor();
            spawner.reduceEnergy(energyRequired);
        }
    }

}
