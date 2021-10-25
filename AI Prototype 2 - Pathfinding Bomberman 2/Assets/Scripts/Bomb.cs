using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bomb : MonoBehaviour
{
    public float explodeDelay;

    private float timer;
    public float range = 0.5f;

    public GameObject collisionBox = null;


    // TODO: Update the exploded tiles into WorldTileGrid.


    public void Explode()
    {
        LayerMask layer = LayerMask.GetMask("Breakables");

        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, range, layer);
        if (hitUp.collider != null && hitUp.collider.gameObject.tag == "Breakables")
        {
            Debug.Log("Hit a breakable up");
            DestroyTile(hitUp, Vector2.up);
            UpdateTile(hitUp.collider.transform.position);
            DrawRays(hitUp);
        }
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, range, layer);
        if (hitLeft.collider != null && hitLeft.collider.gameObject.tag == "Breakables")
        {
            Debug.Log("Hit a breakable left");
            DestroyTile(hitLeft, Vector2.up);
            UpdateTile(hitLeft.collider.transform.position);
            DrawRays(hitLeft);
        }
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, range, layer);
        if (hitDown.collider != null && hitDown.collider.gameObject.tag == "Breakables")
        {
            Debug.Log("Hit a breakable down");
            DestroyTile(hitDown, Vector2.down);
            UpdateTile(hitDown.collider.transform.position);
            DrawRays(hitDown);
        }
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, range, layer);
        if (hitRight.collider != null && hitRight.collider.gameObject.tag == "Breakables")
        {
            Debug.Log("Hit a breakable right");
            DestroyTile(hitRight, Vector2.down);
            UpdateTile(hitRight.collider.transform.position);
            DrawRays(hitRight);

        }
        Destroy(gameObject);
    }

    //void OnDrawGizmosSelected()
    //{
    //        // Draws a blue line from this transform to the target
    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawLine(transform.position, Vector2.left * range);
    //}

    public void Awake()
    {
        Spawn();
    }

    public void Spawn()
    {
        collisionBox.SetActive(false);
    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= explodeDelay)
        {
            Explode();
        }
    }

    public void UpdateTile(Vector3 position)
    {
        WorldTileGrid.instance.ChangeToFloor(position);
    }

    private void DestroyTile(RaycastHit2D hit, Vector2 dir)
    {
        // hit.distance += 0.25f;
        Vector3 hitPosition = Vector3.zero;
        Tilemap tilemap = hit.collider.gameObject.GetComponent<Tilemap>();

        if (dir == Vector2.up)
        {
            hitPosition.x = hit.point.x - 0.01f;
            hitPosition.y = hit.point.y + 0.01f;
        }
        if (dir == Vector2.down)
        {
            hitPosition.x = hit.point.x + 0.01f;
            hitPosition.y = hit.point.y - 0.01f;
        }

        tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);

        Debug.Log("Destroying tile at: [" + hitPosition.x + ", " + hitPosition.y + "]");
    }

    private void DrawRays(RaycastHit2D hit)
    {
        Debug.DrawRay(hit.transform.position, Vector2.up, Color.blue, 2);
        Debug.DrawRay(hit.transform.position, Vector2.down, Color.blue, 2);
        Debug.DrawRay(hit.transform.position, Vector2.left, Color.blue, 2);
        Debug.DrawRay(hit.transform.position, Vector2.right, Color.blue, 2);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collisionBox.SetActive(true);
        }
    }
}
