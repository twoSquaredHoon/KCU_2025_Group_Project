using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_3_boss_button : EnemyButton
{
    public stage_3_boss type1;
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
