using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_3_1_button : EnemyButton
{
    public stage_3_1 type1;
    protected override void Start()
    {
        base.Start();
    }

    protected override void ButtonPressed()
    {
        if (spawner != null)
        {
            Instantiate(type1);
        }
    }

}
