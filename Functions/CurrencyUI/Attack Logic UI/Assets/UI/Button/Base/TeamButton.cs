using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class TeamButton : MonoBehaviour
{
    public Spawn_Team spawner;
    public Team team;
    protected bool canClick;
    protected float energyRequired;
    protected Button bt;
    protected Color originalColor;
    protected KeyCode k;
    protected virtual void Awake()
    {
        canClick = false;
        k = KeyCode.None;
        bt = GetComponent<Button>();
        originalColor = bt.colors.normalColor;
        bt.onClick.AddListener(ButtonPressed);
    }

    protected virtual void Start()
    {
        energyRequired = 100f;
    }

    protected virtual void Update()
    {
        if (spawner == null) return;
        bt.interactable = canClick;

        var colors = bt.colors;
        if (spawner.getEnergy() >= energyRequired)
        {
            colors.normalColor = originalColor;
            colors.highlightedColor = new Color(0.9f, 0.9f, 0.9f);
            canClick = true;
        }
        else
        {
            colors.normalColor = Color.grey;
            colors.highlightedColor = Color.grey;
            canClick = false;

        }
        bt.colors = colors;

        if (Input.GetKeyDown(k))
        {
            callSpawn();
        }
    }

    protected virtual void ButtonPressed()
    {
        callSpawn();
    }
    protected virtual void callSpawn()
    {
        if (!canClick) return;

        if (spawner != null)
        {
            spawner.SpawnTeam(team);
            //changeColor();
            spawner.reduceEnergy(energyRequired);
        }
    }

    protected virtual void changeColor()
    {
        // 색깔 변동? 애니메이션, 등등 추가
    }
}
