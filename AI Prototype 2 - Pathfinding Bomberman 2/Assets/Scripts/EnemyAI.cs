using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://pavcreations.com/pathfinding-with-a-star-algorithm-in-unity-small-game-project/

public class EnemyAI : MonoBehaviour
{
    public List<WorldTile> OpenList;
    public List<WorldTile> ClosedList;

    private bool isMoving = false;

    // H cost = distance to ending cell
    // G cost = walkable distance to ending cell
    // F cost = Sum of G and H

    [SerializeField] private Collider2D NC;
    [SerializeField] private Collider2D WC;
    [SerializeField] private Collider2D SC;
    [SerializeField] private Collider2D EC;

    public void GetH()
    {
        // GetDistance?
        // Calculate H cost
    }

    private void Update()
    {
        // If adjacent breakable wall, place bomb, then move to adjacent floor and then out of bomb range
        // After bomb explodes, recalculate route

        // Am I in danger?
        // Move to safe cell

        // placedBombs != bombAmount, place bomb
    }


}
