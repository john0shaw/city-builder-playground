using Godot;
using Godot.Collections;
using System;

public partial class GameManagerNode : Node2D
{
	[Export] public Team.ColorEnum PlayerColor = Team.ColorEnum.White;
	[Export] public Array<ItemCountResource> StartingItems;

	public static GameManagerNode Instance;

	public UnitManagerNode UnitManager;

	public InventoryNode InventoryNode;
	public Inventory Inventory;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;

		Inventory = new Inventory();

		UnitManager = GetNode<UnitManagerNode>("World/UnitManager");
		InventoryNode = GetNode<InventoryNode>("UI/Inventory");

		Inventory.Changed += InventoryNode._on_inventory_changed;

		SetupTesting();
	}

	private void SetupTesting()
	{
		foreach (ItemCountResource item in StartingItems)
		{
			Inventory.AddItem(item.Item, item.Count);
		}
	}
}
