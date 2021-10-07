using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Source:
// https://pavcreations.com/pathfinding-with-a-star-algorithm-in-unity-small-game-project/

public class EnemyAI : BombermanControls
{
    public List<WorldTile> OpenList = new List<WorldTile>();
    public List<WorldTile> ClosedList = new List<WorldTile>();

    private bool isMoving = false;

    // H cost = Birdway to ending cell from starting cell in cost
    // G cost = walkable distance from every cell to ending cell (ending cell = 0 cost)
    // F cost = Sum of G and H

    [SerializeField] private GameObject Colliders;
    [SerializeField] private GameObject NC;
    [SerializeField] private GameObject WC;
    [SerializeField] private GameObject SC;
    [SerializeField] private GameObject EC;

    // public List<WorldTile> AdjacentTiles = new List<WorldTile>();

    public WorldTile destination;

    private int tempLowestCost = 0;

    private bool getNewPath = false;

    private void FixedUpdate()
    {
        
    }

    private void Start()
    {

    }

    private void Update()
    {
        base.Update();

        if (getNewPath)
        {

        }

        // If adjacent breakable wall, place bomb, then move to adjacent floor and then out of bomb range
        // After bomb explodes, recalculate route

        // Am I in danger?
        // Move to safe cell

        if (GetAdjacency(WorldTile.TileState.Breakable))
        {
            PlaceBomb();
        }

        // placedBombs != bombAmount, place bomb
    }

    // After AI has moved to another tile, get adjacency
    public WorldTile GetAdjacency(WorldTile.TileState state)
    {
        //if (NC.GetComponent<Adjacency>().tile.state == state) return NC.GetComponent<Adjacency>().tile;
        //if (WC.GetComponent<Adjacency>().tile.state == state) return WC.GetComponent<Adjacency>().tile;
        //if (SC.GetComponent<Adjacency>().tile.state == state) return SC.GetComponent<Adjacency>().tile;
        //if (EC.GetComponent<Adjacency>().tile.state == state) return EC.GetComponent<Adjacency>().tile;
        return null;
    }

    public void GetStartingTile()
    {

    }

    //public void GetNewDestination()
    //{
    //    WorldTile tempTile = WorldTileGrid.GetRandomTile();
    //    if (tempTile.state == WorldTile.TileState.Wall) GetNewDestination();
    //    else
    //    {
    //        destination = tempTile;
    //    }
    //}

    private float CheckDistance(WorldTile tile1, WorldTile tile2)
    {
        return Vector2.Distance(tile1.transform.position, tile2.transform.position);
    }

    private void GridMove(Vector2 dir)
    {
        //Colliders.transform.Translate(dir * 0.72f);
        transform.Translate(dir * 0.72f);

    }
}
