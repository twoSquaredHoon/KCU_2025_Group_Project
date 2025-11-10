using System;


[Serializable]
public class Item
{
    public ItemId Id;
    public int Count;

    public ItemParams Params => Items.Params[Id];

    public Item()
    {
    }

    public Item(ItemId id, int count)
    {
        Id = id;
        Count = count;
    }
}
