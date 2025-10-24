using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class EnemyButton : MonoBehaviour
{
    public Spawn_Enemy spawner;
    public Enemy enemy;
    protected Button bt;
    protected Color originalColor;
    protected virtual void Awake()
    {
        bt = GetComponent<Button>();
        originalColor = bt.colors.normalColor;
        bt.onClick.AddListener(ButtonPressed);
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        if (spawner == null) return;
        var colors = bt.colors;
        colors.highlightedColor = new Color(0.9f, 0.9f, 0.9f);
        bt.colors = colors;
    }

    protected virtual void ButtonPressed()
    {
        if (spawner != null)
        {
            Instantiate(enemy);
        }
    }

    protected virtual void changeColor()
    {
        // 색깔 변동? 애니메이션, 등등 추가
    }
}
