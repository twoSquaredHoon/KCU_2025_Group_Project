using System.Collections.Generic;
using UnityEngine;

public abstract class ItemContainer : MonoBehaviour
{
    public List<Item> Items { get; protected set; }

    public bool Expanded;

    public abstract void Refresh();

    public void Initialize(ref List<Item> items)
    {
        Items = items;
        Refresh();
    }
}
