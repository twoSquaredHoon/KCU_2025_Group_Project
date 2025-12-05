using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour
{
    protected GameObject enemy;
    public Enemy_Type_1 enemy1;
    public Enemy_Type_2 enemy2;
    public Enemy_Type_3 enemy3;
    public Boss_Enemy bossEnemyPrefab;

    [SerializeField] protected float stage;
    [SerializeField] protected float stageAcc;
    [SerializeField] protected float spawnrate1;
    [SerializeField] protected float spawnrate2;
    [SerializeField] protected float spawnrate3;
    [SerializeField] private float bossSpawnRate = 60f;

    [SerializeField] protected float timer1;
    [SerializeField] protected float timer2;
    [SerializeField] protected float timer3;
    [SerializeField] private float bossTimer = 0f;


    [SerializeField] protected float actualTime;
    void Start()
    {
        transform.position = new Vector3(9.5f, -0.7f, -0.13f);
        stage = 0;
        stageAcc = 0.2f; //처음보다 (0.2)면 20%씩 빨라짐
        spawnrate1 = 5f;
        spawnrate2 = 9f;
        spawnrate3 = 15f;
        timer1 = 0;
        timer2 = 0;
        timer3 = 0;
        actualTime = 0;
        bossTimer = 0f;
    }

    void Update()
    {

        if (timer1 > spawnrate1)
        {
            timer1 = 0;
            Instantiate(enemy1);

        }
        else
        {
            timer1 = timer1 + Time.deltaTime * stage;
        }

        if (timer2 > spawnrate2)
        {
            timer2 = 0;
            Instantiate(enemy2);
        }
        else
        {
            timer2 = timer2 + Time.deltaTime * stage;
        }

        if (timer3 > spawnrate3)
        {
            timer3 = 0;
            Instantiate(enemy3);
        }
        else
        {
            timer3 = timer3 + Time.deltaTime * stage;
        }

        stage = 1 + stageAcc * Mathf.Floor(actualTime / 30f); //30초마다 stageAcc 만큼 빨라짐
        actualTime = actualTime + Time.deltaTime;

        // ✅ 보스 스폰 로직
        bossTimer += Time.deltaTime;

        if (bossTimer >= bossSpawnRate)
        {
            bossTimer = 0f;

            var enemyList = EntityManager.getEnemyList();
            if (enemyList != null)
            {
                // 1) Destroy되었거나 null인 Enemy들 제거
                enemyList.RemoveAll(e => e == null);

                // 2) 보스 존재 여부를 foreach로 직접 검사 (람다/Exists 안 씀)
                bool bossExists = false;
                foreach (var e in enemyList)
                {
                    if (e is Boss_Enemy)
                    {
                        bossExists = true;
                        break;
                    }
                }

                // 3) 보스가 없으면 새로 소환
                if (!bossExists)
                {
                    if (bossEnemyPrefab == null)
                    {
                        Debug.LogError("Boss prefab is not assigned on Spawn_Enemy!");
                    }
                    else
                    {
                        Instantiate(bossEnemyPrefab, transform.position, Quaternion.identity);
                        Debug.Log("Boss is born!");
                    }
                }
            }
        }

    }
}
