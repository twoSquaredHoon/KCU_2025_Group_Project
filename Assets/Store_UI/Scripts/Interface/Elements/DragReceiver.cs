using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragReceiver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<Image> TweenTargets;

    public Color ColorDropAllowed = new Color(0.5f, 1f, 0.5f);

    public Color ColorDropDenied = new Color(1f, 0.5f, 0.5f);

    public string Group;

    public List<ItemType> ItemTypes;

    public List<ItemTag> ItemTags;

    public static bool DropReady;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (InventoryItem.DragTarget == null || InventoryItem.DragTarget.Group == Group) return;

        if (ItemTypes.Any() && !ItemTypes.Contains(InventoryItem.DragTarget.Item.Params.Type))
        {
            Fade(ColorDropDenied);
        }
        else if (ItemTags.Any(i => !InventoryItem.DragTarget.Item.Params.Tags.Contains(i)))
        {
            Fade(ColorDropDenied);
        }
        else
        {
            Fade(ColorDropAllowed);
            DropReady = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DropReady = false;
        Fade(Color.white);
    }

    private void Fade(Color color)
    {
        TweenTargets.ForEach(i => i.CrossFadeColor(color, 0.25f, true, false));
    }
}
