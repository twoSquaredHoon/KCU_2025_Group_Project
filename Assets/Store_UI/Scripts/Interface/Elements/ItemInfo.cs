using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfo : MonoBehaviour
{
    public Text Name;
    public Text Description;
    public Text Price;
    public Image Icon;

    public void Reset()
    {
        Name.text = Description.text = Price.text = null;
        Icon.sprite = ImageCollection.Instance.DefaultItemIcon;
    }

    public void Initialize(ItemId itemId, ItemParams itemParams, bool shop = false)
    {
        Icon.sprite = ImageCollection.Instance.GetIcon(itemId);
        Name.text = SplitName(itemId.ToString());
        Description.text = $"Here will be {itemId} description soon...";

        if (itemParams.Tags.Contains(ItemTag.NotForSale))
        {
            Price.text = null;
        }
        else if (shop)
        {
            Price.text = $"Buy price: {itemParams.Price}G{Environment.NewLine}Sell price: {itemParams.Price / Shop.SellRatio}G";
        }
        else
        {
            Price.text = $"Sell price: {itemParams.Price / Shop.SellRatio}G";
        }

        var description = new List<string> { $"Type: {itemParams.Type}" };

        if (itemParams.Tags.Any())
        {
            description[description.Count - 1] += $" <color=grey>[{string.Join(", ", itemParams.Tags.Select(i => $"{i}").ToArray())}]</color>";
        }

        foreach (var attribute in itemParams.Properties)
        {
            description.Add($"{SplitName(attribute.Id.ToString())}: {attribute.Value}");
        }

        Description.text = string.Join(Environment.NewLine, description.ToArray());
    }

    public static string SplitName(string name)
    {
        return Regex.Replace(Regex.Replace(name, "[A-Z]", " $0"), "([a-z])([1-9])", "$1 $2").Trim();
    }
}
