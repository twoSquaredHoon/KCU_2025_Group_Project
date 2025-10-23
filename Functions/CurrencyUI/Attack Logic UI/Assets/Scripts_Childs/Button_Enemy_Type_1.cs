using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Team_Type_1 : EnemyButton
{
    public Enemy_Type_1 type1;
    protected override void Start()
    {
        base.Start();
    }

    protected override void ButtonPressed()
    {
        Debug.Log("Click");

        if (spawner != null)
        {
            Debug.Log("Calls");
            Instantiate(type1);
        }
    }

}
