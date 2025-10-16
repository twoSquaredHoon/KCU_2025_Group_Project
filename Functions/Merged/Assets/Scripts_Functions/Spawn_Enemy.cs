using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour
{
    //각 Enemy type마다 타이머를 따로 둬서 다 따로 스폰하는 것도 나쁘진 않음. <- 작업해야함.
    protected GameObject enemy;
    public Enemy_Type_1 enemy_1;
    public Enemy_Type_2 enemy_2;
    [SerializeField] protected float spawnRate_type_1;
    [SerializeField] protected float spawnRate_type_2;

    [SerializeField] protected float timer_Type1;
    [SerializeField] protected float timer_Type2;
    [SerializeField] protected float actualTime;
    void Start()
    {
        transform.position = new Vector3(9.5f, -0.7f, -0.13f);
        spawnRate_type_1 = 13f;
        spawnRate_type_2 = 24f;
        timer_Type1 = 0;
        timer_Type2 = 0;
        actualTime = 0;
    }

    void Update()
    {
        if (timer_Type1 > spawnRate_type_1)
        {
            timer_Type1 = 0;
            Instantiate(enemy_1);

        }
        else
        {
            timer_Type1 = timer_Type1 + Time.deltaTime * (Random.value * 3);
        }

        if (timer_Type2 > spawnRate_type_2)
        {
            timer_Type2 = 0;
            Instantiate(enemy_2);
        }
        else
        {
            timer_Type2 = timer_Type2 + Time.deltaTime * (Random.value * 3);
        }
    }
}
