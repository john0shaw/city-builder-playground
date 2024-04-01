using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

public partial class InventoryNode : PanelContainer
{
    [Export] public PackedScene ItemScene;

    public Dictionary<int, InventoryItemComponent> Items = new();
    public HBoxContainer ItemsContainer;

    public override void _Ready()
    {
        ItemsContainer = GetNode<HBoxContainer>("ItemsMargin/Items");
    }

    /// <summary>
    /// Fetch an item component by ItemResource or null
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    public InventoryItemComponent GetItemComponent(ItemResource resource)
    {
        return Items.ContainsKey(resource.Id) ? Items[resource.Id] : null;
    }

    /// <summary>
    /// Sets the display amount
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="amount"></param>
    public void SetAmount(ItemResource resource, int amount)
    {
        InventoryItemComponent component = GetItemComponent(resource);
        if (amount >= 0)
        {
            if (component is InventoryItemComponent)
            {
                component.Count = amount;
            }
            else
            {
                InventoryItemComponent newItem = (InventoryItemComponent)ItemScene.Instantiate();
                newItem.Count = amount;
                newItem.Resource = resource;
                ItemsContainer.AddChild(newItem);
                Items.Add(resource.Id, newItem);
            }
        }
        else
        {
            if (component is InventoryItemComponent)
            {
                component.QueueFree();
                Items.Remove(resource.Id);
            }
        }
    }

    /// <summary>
    /// Update a specific item
    /// </summary>
    /// <param name="resource"></param>
    /// <param name="amount"></param>
    public void _on_inventory_changed(object sender, Inventory.ChangedEventArgs args)
    {
        SetAmount(args.Item, args.Count);
    }
}