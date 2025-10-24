using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Enemy_Type_2 : EnemyButton
{
    public Enemy_Type_2 type2;
    protected override void Start()
    {
        base.Start();
    }

    protected override void ButtonPressed()
    {
        if (spawner != null)
        {
            Instantiate(type2);
        }
    }

}
