using Godot;
using System;
using System.Collections.Generic;

public class Inventory
{
    public class ChangedEventArgs : EventArgs
    {
        public ItemResource Item;
        public int Count;
        public ChangedEventArgs(ItemResource item, int count) { Item = item; Count = count; }
    }

    public event EventHandler<ChangedEventArgs> Changed;

    public Dictionary<int, Item> Items = new();

    /// <summary>
    /// Fetch an item component or null
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    public Item GetItem(ItemResource resource)
    {
        return Items.ContainsKey(resource.Id) ? Items[resource.Id] : null;
    }

    /// <summary>
    /// Add an item to the inventory
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="amount"></param>
    public void AddItem(ItemResource resource, int amount)
    {
        if (GetItem(resource) is Item item)
        {
            item.Count += amount;
        }
        else
        {
            Item newItem = Item.FromResource(resource);
            newItem.Count = amount;
            Items.Add(resource.Id, newItem);
        }

        Changed?.Invoke(this, new ChangedEventArgs(resource, amount));
    }

    /// <summary>
    /// Remove an item from the inventory
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="amount"></param>
    public void RemoveItem(ItemResource resource, int amount)
    {
        if (GetItem(resource) is Item item)
        {
            item.Count -= amount;
            if (item.Count < 0)
            {
                Log.Error($"Tried to remove {amount} {resource.Name}, but only had {item.Count} stored");
                item.Count = 0;
            }
        }
        else
        {
            Log.Error($"Tried to remove {amount} {resource.Name}, but had 0 stored");
        }

        Changed?.Invoke(this, new ChangedEventArgs(resource, amount));
    }

    /// <summary>
    /// Determines if player has enough resources for a certain action
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool HasEnoughResource(ItemResource resource, int amount)
    {
        if (GetItem(resource) is Item item && item.Count >= amount)
            return true;

        return false;
    }

}
