using Godot;
using Godot.Collections;
using System;

[GlobalClass]
public partial class ResourceResource : Resource
{
    [Export] public string Name;
    [Export] public Array<ItemRangeResource> HarvestItems;
    [Export] public Texture2D SpriteSheet;

    /// <summary>
    /// Get an array of harvest items based on randomized values
    /// </summary>
    /// <returns></returns>
    public Array<ItemCountResource> GetHarvest()
    {
        Array<ItemCountResource> items = new();
        foreach (ItemRangeResource item in HarvestItems)
        {
            items.Add(new ItemCountResource
            {
                Item = item.Item,
                Count = item.GetRandom()
            });
        }
        return items;
    }
}

