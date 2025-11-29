using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_3_3_button : EnemyButton
{
    public stage_3_3 type3;
    protected override void Start()
    {
        base.Start();
    }

    protected override void ButtonPressed()
    {
        if (spawner != null)
        {
            Instantiate(type3);
        }
    }

}
