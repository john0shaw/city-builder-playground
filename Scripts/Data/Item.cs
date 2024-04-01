using Godot;
using System;

public class Item
{
    public int Id;
    public string Name;
    public int Count;

    public static Item FromResource(ItemResource resource)
    {
        return new Item
        {
            Id = resource.Id,
            Name = resource.Name,
            Count = 0
        };
    }
}
