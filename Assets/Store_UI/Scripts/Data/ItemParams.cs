using System;
using System.Collections.Generic;

[Serializable]
public class ItemParams
{
    public ItemType Type;
    public List<ItemTag> Tags = new List<ItemTag>();
    public List<Property> Properties = new List<Property>();
    public int Price;
}
