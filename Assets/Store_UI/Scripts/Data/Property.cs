using System;


[Serializable]
public class Property
{
    public PropertyId Id;
    public int Value;

    public Property()
    {
    }

    public Property(PropertyId id, int value)
    {
        Id = id;
        Value = value;
    }
}
