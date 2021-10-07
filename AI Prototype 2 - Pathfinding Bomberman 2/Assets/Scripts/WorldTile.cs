using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile : MonoBehaviour
{
    public enum TileState { Floor, Wall, Breakable }

    public TileState state;

    public bool dangerTile = false;

    public int tileCost = 1;

    private void Start()
    {
        name = "WorldTile [" + transform.position.x + ", " + transform.position.y + "]";
        GetTileState();
    }

    private void GetTileState()
    {
        LayerMask breakLayer = LayerMask.GetMask("Breakables");
        RaycastHit2D hitB = Physics2D.Raycast(transform.position, Vector2.up, 0.001f, breakLayer);
        if (hitB.collider != null)
        {
            if (hitB.collider.gameObject.tag == "Breakables") state = TileState.Breakable;
            tileCost = 3;
            return;
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.001f);
            if (hit.collider != null)
            {
                // Will hit player. Fix later
                if (hit.collider.gameObject.tag == "Floor")
                {
                    state = TileState.Floor; tileCost = 1; return;
                }
                if (hit.collider.gameObject.tag == "Breakables") { state = TileState.Breakable; tileCost = 3; return; }

                if (hit.collider.gameObject.tag == "Wall") { state = TileState.Wall; tileCost = 90; return; }
            }
        }
    }
}
