using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTileGrid : MonoBehaviour
{
    private int horizontalLength = 15;
    private int verticalLength = 13;

    public GameObject worldTile;

    bool gridComplete = false;

    public List<GameObject> worldTiles;

    private void Update()
    {
        if (!gridComplete)
        {
            for (int i = 0; i < verticalLength; i++)
            {
                for (int y = 0; y < horizontalLength; y++)
                {
                     CreateWT(new Vector3(transform.position.x + (y * 0.72f), transform.position.y - (i * 0.72f), transform.position.z));
                }
                if (i >= (verticalLength-1))
                {
                    Debug.Log("Stopping grid");
                    gridComplete = true;
                }
            }
        }
    }

    //public static WorldTile GetWorldTile(Vector2 pos)
    //{

    //}

    public void CreateWT(Vector3 pos)
    {
        GameObject newTile = Instantiate(worldTile, pos, Quaternion.identity);
        worldTiles.Add(newTile);
    }
}
