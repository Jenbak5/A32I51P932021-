using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldTile
{
    public enum TileState { Floor, Wall, Breakable }

    public TileState state { get; set; }

    public Vector3Int LocalPlace { get; set; }

    public Vector3 WorldLocation { get; set; }

    public TileBase TileBase { get; set; }

    public Tilemap TilemapMember { get; set; }

    public WorldTile parent = null;

    public bool dangerTile = false;

    public int baseCost;

    public int Fcost;

    public int Hcost;
}
