using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

public partial class WorldTileMapNode : TileMap
{
	[ExportGroup("RandomGrass")]
	[Export] public float GrassChance = 0.3f;
	[Export] public Array<TileData> GrassSearch = new();
	[Export] public Array<TileData> AlternativeGrass = new();
	
	/// <summary>
	/// Randomize the grass by replacing tiles found in GrassSearch with random AlternativeGrass
	/// </summary>
	public void RandomizeGrass()
	{
		for (int layer = 0; layer < GetLayersCount(); layer++)
		{
			foreach (TileData search in GrassSearch)
			{
				foreach (Vector2I foundPosition in GetUsedCellsById(layer, search.SourceID, search.AtlasCoords, search.AlternativeTile))
				{
					if (GD.Randf() > GrassChance)
						continue;

					TileData altGrass = AlternativeGrass.PickRandom();
					SetCell(layer, foundPosition, altGrass.SourceID, altGrass.AtlasCoords, altGrass.AlternativeTile);
				}
			}
		}
	}

	/// <summary>
	/// Loads all of the tile data for the tilemap
	/// </summary>
	/// <returns></returns>
	public List<TileData> GetTileData()
	{
        List<TileData> tiles = new List<TileData>();

		for (int layer = 0; layer < GetLayersCount(); layer++)
		{
			foreach (Vector2I tilePosition in GetUsedCells(layer))
			{
				tiles.Add(new TileData()
				{
					Layer = layer,
					Position = tilePosition,
					SourceID = GetCellSourceId(layer, tilePosition),
					AtlasCoords = GetCellAtlasCoords(layer, tilePosition),
					AlternativeTile = GetCellAlternativeTile(layer, tilePosition)
				});
			}
		}

		return tiles;
	}
}
