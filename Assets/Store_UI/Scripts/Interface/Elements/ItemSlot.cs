using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DragReceiver))]
public class ItemSlot : MonoBehaviour
{
    public Image Icon;
    public ItemType ItemType;

    public List<ItemTag> ItemTags;

    public void Start()
    {
        var dragReceiver = GetComponent<DragReceiver>();

        dragReceiver.ItemTypes = new List<ItemType> { ItemType };
        dragReceiver.ItemTags = ItemTags;
    }
}
