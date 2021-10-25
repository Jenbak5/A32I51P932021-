using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Source 2:
// https://medium.com/@allencoded/unity-tilemaps-and-storing-individual-tile-data-8b95d87e9f32

public class WorldTileGrid : MonoBehaviour
{
    public static WorldTileGrid instance;
    public Tilemap Tilemap;
    public static Dictionary<Vector3, WorldTile> tiles;

    public GameObject marker;

    private string breakableString = "Breakable";
    private string wallString = "Wall";
    private string wallString2 = "Wall 2";
    private string floorString = "Floor";

    public int breakableCost = 18;
    public int floorCost = 8;

    private WorldTile tempWT;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            tiles = new Dictionary<Vector3, WorldTile>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        GetWorldTiles();
    }

    private void GetWorldTiles()
    {
        foreach (Vector3Int pos in Tilemap.cellBounds.allPositionsWithin)
        {
            var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

            if (!Tilemap.HasTile(localPlace)) { continue; }
            WorldTile tile = new WorldTile
            {
                LocalPlace = localPlace,
                WorldLocation = Tilemap.CellToWorld(localPlace) + new Vector3(0.5f, 0.5f, 0f),
                TileBase = Tilemap.GetTile(localPlace),
                TilemapMember = Tilemap
            };

            if (tile.TileBase.name == breakableString) { tile.state = WorldTile.TileState.Breakable; tile.baseCost = breakableCost; }
            if (tile.TileBase.name == floorString) { tile.state = WorldTile.TileState.Floor; tile.baseCost = floorCost; }
            if (tile.TileBase.name == wallString || tile.TileBase.name == wallString2) { tile.state = WorldTile.TileState.Wall; tile.baseCost = 100; }

            if (tiles.TryGetValue(tile.WorldLocation, out tempWT) && tempWT.state == WorldTile.TileState.Floor) { tiles.Remove(tile.WorldLocation); }
            tiles.Add(tile.WorldLocation, tile);
            //Instantiate(marker, tile.WorldLocation, Quaternion.identity);
            //Debug.Log("Added [ " + tile.TileBase.name + "] to: ["+ tile.WorldLocation +" ]");
        }
        Debug.Log(name + " has: " + tiles.Count + " tiles");
    }

    public void ChangeToFloor(Vector3 position)
    {
        if (tiles.TryGetValue(position, out tempWT))
        {

            tempWT.state = WorldTile.TileState.Floor;
            tempWT.baseCost = floorCost;
            Debug.Log("Changed " + tempWT.WorldLocation + " to Floor");
            //tempWT.TileBase = Tilemap.GetTile(tempWT.LocalPlace);
        }
    }
}

// Breakable tiles are not able to be added because of duplicate keys. 
// Maybe make a way for breakables to take priority? By removing floor and stuff?
// Then when bombs are destroying the tiles we update them and add floor tiles to the past breakables. 