using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adjacency : MonoBehaviour
{
    [HideInInspector] public WorldTile tile = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            tile = collision.gameObject.GetComponent<WorldTile>();
        }
    }
}
