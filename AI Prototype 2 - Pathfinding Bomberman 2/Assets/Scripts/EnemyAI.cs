using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Source:
// https://pavcreations.com/pathfinding-with-a-star-algorithm-in-unity-small-game-project/

public class EnemyAI : BombermanControls
{
    public List<WorldTile> OpenList = new List<WorldTile>();
    public List<WorldTile> ClosedList = new List<WorldTile>();
    public List<WorldTile> Route = new List<WorldTile>();
    public WorldTile CurrentWT = null;

    private Vector3 AlgoMarker;
    private bool getNewPath = false;
    private bool moveRoute = false;
    private bool isMoving = false;
    public Vector3 firstDestination;
    public WorldTile destination;

    private int lowestF = 300;
    private int currentG = 0;
    private WorldTile lowestWT = null;
    private WorldTile currentAdjWT = null;

    private bool wtIsInClosed = false;
    private bool wtIsInOpen = false;

    public TextMeshPro debugCounter;

    private IEnumerator moveCoroutine;
    private IEnumerator waitCoroutine;
    private Vector3 moveMarker;
    private bool moveMarkerPlaced = false;
    private float lerpProgress = 0;
    private WorldTile nextTileInRoute = null;
    private bool nextTileIsAdj = false;

    private bool waiting = false;
    private int tilesTraveled = 0;


    private void Start()
    {
        AlgoMarker = transform.position;

        getNewPath = true;
        if (WorldTileGrid.tiles.TryGetValue(AlgoMarker, out CurrentWT)) { OpenList.Add(CurrentWT); };
        if (OpenList.Count < 1) { Debug.LogError("No starting tile for: " + name); }
        else { Debug.Log("Starting tile for: " + name + " is a " + CurrentWT.state); }

        // WorldTileGrid.tiles.TryGetValue(firstDestination, out destination);
        SetRandomDestination();
    }

    public override void Update()
    {
        base.Update();

        while (getNewPath)
        {
            // LOOP
            for (int i = 0; i < OpenList.Count; i++)
            {
                if (OpenList[i].Fcost < lowestF) { lowestF = OpenList[i].Fcost; lowestWT = OpenList[i]; }

                if (i >= OpenList.Count - 1)
                {
                    // current_cell = cell in OPEN_LIST with the lowest F_COST
                    CurrentWT = lowestWT;
                    //Debug.Log("WT with lowest F cost is: " + (CurrentWT.WorldLocation).ToString() + " with " + lowestF);
                    AlgoMarker = CurrentWT.WorldLocation;
                    lowestF = 300;
                    lowestWT = null;

                    Instantiate(debugCounter, CurrentWT.WorldLocation, Quaternion.identity).text = "C";
                    // REMOVE current_cell from OPEN_LIST 
                    OpenList.Remove(CurrentWT);
                    // ADD current_cell to CLOSED_LIST
                    ClosedList.Add(CurrentWT);

                      
                    // If tile 


                }
            }

            // IF current_cell is finish_cell
            if (CurrentWT.WorldLocation == destination.WorldLocation)
            {
                Debug.Log("A* destination reached at " + destination.WorldLocation);

                // Create the route to travel
                TraceRoute(ClosedList);

                OpenList.Clear();
                ClosedList.Clear();
                getNewPath = false;
                // RETURN
                return;
            }


            // FOR EACH adjacent_cell to current_cell
            // SKIP to the next adjacent_cell       
            // IF adjacent_cell is unwalkable OR adjacent_cell is in CLOSED_LIST
            for (int i = 0; i < 3; i++)
            {
                if (i == 0) currentAdjWT = AdjacenctWT(Vector3.up);
                if (i == 1) currentAdjWT = AdjacenctWT(Vector3.left);
                if (i == 2) currentAdjWT = AdjacenctWT(Vector3.down);
                if (i == 3) currentAdjWT = AdjacenctWT(Vector3.right);

                if (currentAdjWT == null) { Debug.LogError("Adjacent tile is null"); continue; }

                if (currentAdjWT.state != WorldTile.TileState.Wall)
                {
                    for (int y = 0; y < ClosedList.Count; y++)
                    {
                        if (currentAdjWT == ClosedList[y]) { wtIsInClosed = true; y = ClosedList.Count; continue; }
                    }
                    if (!wtIsInClosed)
                    {
                        for (int z = 0; z < OpenList.Count; z++)
                        {
                            if (currentAdjWT == OpenList[z])
                            {
                                wtIsInOpen = true;
                                z = OpenList.Count;
                                continue;
                                // Skip this WT
                            }
                        }
                    }
                    // IF new_path to adjacent_cell is shorter OR adjacent_cell is not in OPEN_LIST
                    // IF adjacent_cell is not in OPEN_LIST
                    // ADD adjacent_cell to OPEN_LIST
                    if (!wtIsInOpen && !wtIsInClosed)
                    {
                        // SET F_COST of adjacent_cell
                        OpenList.Add(currentAdjWT); SetFCost(currentAdjWT);
                        continue;
                    }
                }
                if (currentAdjWT.state == WorldTile.TileState.Wall) { continue; }
            }

            wtIsInClosed = false;
            wtIsInOpen = false;

            //return;
            break;
        }


        //while (moveRoute)
        //{
        //    StartCoroutine("MoveRoute");

        //    break;
        //}
    }

    private void TraceRoute(List<WorldTile> listInput)
    {
        if (!moveRoute)
        {
            for (int i = 0; i < listInput.Count; i++)
            {
                Route.Add(listInput[i]);
            }
            moveRoute = true;
        }
        if (moveRoute)
        {
            StartCoroutine("MoveRoute");
        }
        Debug.Log("Amount in ClosedList:" + ClosedList.Count);
    }

    IEnumerator MoveRoute()
    {
        while (tilesTraveled <= Route.Count)
        {
            // Check next tile in route
            if (nextTileInRoute == null)
            {
                Debug.Log("Getting new tile");
                WorldTileGrid.tiles.TryGetValue(Route[tilesTraveled].WorldLocation, out nextTileInRoute);
                continue;
            }

            if (transform.position == destination.WorldLocation)
            {
                moveRoute = false;
                getNewPath = true;

                Debug.Log("The AI has reached current destination, getting a new one...");
                SetRandomDestination();
                yield return null;
            }

            // If next tile isnt reached
            if (transform.position != nextTileInRoute.WorldLocation)
            {
                // If tile is Adjacent
                if (CheckDistance(transform.position, nextTileInRoute.WorldLocation) == 1)
                {
                    if (nextTileIsAdj == false)
                    {
                        Debug.Log("Tile is ADJ, tilesTraveled = " + tilesTraveled); nextTileIsAdj = true;
                    }
                }

                if (nextTileIsAdj)
                {
                    if (nextTileInRoute.state == WorldTile.TileState.Breakable)
                    {
                        PlaceBomb();
                        yield return new WaitForSeconds(3.5f);
                        nextTileIsAdj = false;
                        continue;
                    }

                    // If next tile is open, move there
                    if (nextTileInRoute.state == WorldTile.TileState.Floor)
                    {
                        Debug.Log("Trying to move");
                        if (!isMoving)
                        {
                            GridMove(nextTileInRoute.WorldLocation);
                        }
                        //yield return new WaitForSeconds(1f);
                        continue;
                    }
                }
                // If tile is not adjacent, go back
                else if (!nextTileIsAdj)
                {
                    //Debug.LogError("Tile is not ADJ");
                    Debug.LogError("Distance between:" + CheckDistance(transform.position, nextTileInRoute.WorldLocation));

                    //if ((tilesTraveled - 1) > -1)
                    //{
                    //    GridMove(Route[tilesTraveled - 1].WorldLocation);
                    //    tilesTraveled -= 2;
                    //    Debug.Log(tilesTraveled);
                    //}

                    GridMove(Route[tilesTraveled - 1].WorldLocation);

                    // If tile is Adjacent
                    if (CheckDistance(transform.position, nextTileInRoute.WorldLocation) == 1)
                    {
                        Debug.Log("Tile is ADJ 2");
                        if (nextTileIsAdj == false) nextTileIsAdj = true;
                    }

                    nextTileIsAdj = true;
                    nextTileInRoute = null;
                    continue;

                    //else
                    //{
                    //    GridMove(nextTileInRoute.WorldLocation);
                    //}
                }
            }

            if (transform.position == nextTileInRoute.WorldLocation)
            {
                Debug.Log("Reached new Tile");
                nextTileInRoute = null;
                tilesTraveled++;
                continue;
            }

            if (tilesTraveled == Route.Count) { moveRoute = false; yield return null; }
            break;
        }
    }

    private void GridMove(Vector3 destination)
    {
        moveCoroutine = GridMoveSequence(destination);
        StartCoroutine(moveCoroutine);
        //if (!moveMarkerPlaced) { moveMarker = destination; moveMarkerPlaced = true; }

        //if (transform.position != moveMarker)
        //{
        //    lerpProgress += moveSpeed * Time.deltaTime;
        //    transform.position = Vector3.Lerp(transform.position, moveMarker, lerpProgress);
        //}

        //else if (transform.position == moveMarker && moveMarkerPlaced == true)
        //{
        //    moveMarkerPlaced = false;
        //    lerpProgress = 0;
        //    nextTileIsAdj = false;
        //    if (tilesTraveled + 1 < Route.Count)
        //    {
        //        tilesTraveled++;
        //    }
        //}
    }

    IEnumerator GridMoveSequence(Vector3 destination)
    {
        //if (!isMoving) isMoving = true;

        if (!moveMarkerPlaced) { moveMarker = destination; moveMarkerPlaced = true; }

        if (transform.position != moveMarker)
        {
            lerpProgress += moveSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, moveMarker, lerpProgress);
        }

        else if (transform.position == moveMarker && moveMarkerPlaced == true)
        {
            moveMarkerPlaced = false;
            lerpProgress = 0;
            nextTileIsAdj = false;

            tilesTraveled++;

            yield return new WaitForSeconds(1f);
            isMoving = false;
            yield return null;
        }
    }

    private void SetFCost(WorldTile wt)
    {
        // H cost = Birdway from ending cell to starting cell in cost
        // G cost = walkable distance from starting cell to ending cell
        // F cost = Sum of G and H

        int H;
        if (wt.WorldLocation == destination.WorldLocation)
        {
            H = 0;
        }
        H = Mathf.RoundToInt(CheckDistance(wt.WorldLocation, destination.WorldLocation) * 5);
        int G = currentG + 1;
        int tileCost = wt.baseCost;

        wt.Fcost = H + G + tileCost;
        wt.Hcost = H;
        //Instantiate(debugCounter, wt.WorldLocation, Quaternion.identity).text = wt.Fcost.ToString();

        Debug.Log("Setting " + wt.TileBase.name + " F cost to: " + wt.Fcost);
    }

    private WorldTile AdjacenctWT(Vector3 dir)
    {
        Vector3 checkPos = new Vector3(Mathf.Round(AlgoMarker.x + dir.x), Mathf.Round(AlgoMarker.y + dir.y), 0);
        WorldTile tempWT = null;
        if (WorldTileGrid.tiles.TryGetValue(checkPos, out tempWT))
        {
            //Debug.Log(tempWT.state + " was found at " + checkPos);
            return tempWT;
        }
        else
        {
            //Debug.LogError("Adjacent WT not found at " + checkPos);
            return null;
        }
    }

    private float CheckDistance(Vector3 pos1, Vector3 pos2)
    {
        return Vector2.Distance(pos1, pos2);
    }

    private void SetRandomDestination()
    {
        // Max X = 5
        // Min X = -7

        // Max Y = 4
        // Min Y = -6

        // Create a random Vector3 within these boundaries while also being within close range of the Agent

        float maxX = (transform.position.x + 4);
        if (maxX > 5) maxX = 5;
        float minX = (transform.position.x - 4);
        if (minX < -7) minX = -7;

        float maxY = (transform.position.y + 4);
        if (maxY > 4) maxY = 4;
        float minY = (transform.position.y - 4);
        if (minY < -6) minY = -6;

        WorldTile wt;
        for (int i = 0; i < 10; i++)
        {
            Vector3 rv = new Vector3(Mathf.RoundToInt(Random.Range(minX, maxX)), Mathf.RoundToInt(Random.Range(minY, maxY)));
            Debug.Log(rv);
            if (WorldTileGrid.tiles.TryGetValue(rv, out wt))
            {
                if (wt.state != WorldTile.TileState.Wall)
                {
                    destination = wt;
                    Debug.Log("New destination acquired");
                    return;
                }
            }
        }
    }
}
