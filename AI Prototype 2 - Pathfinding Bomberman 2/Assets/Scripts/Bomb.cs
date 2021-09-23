using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bomb : MonoBehaviour
{
    public float explodeDelay;

    private float timer;
    private int range = 2;

    public void Explode()
    {
        Destroy(gameObject);

        // Use Raycast to collide with breakables
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up, range);
        if (hitUp.collider != null && hitUp.collider.tag == "Breakables")
        {
            DestroyTile(hitUp);
        }

        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, range);
        if (hitLeft.collider != null && hitUp.collider.tag == "Breakables")
        {
            DestroyTile(hitUp);
        }
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, range);
        if (hitDown.collider != null && hitUp.collider.tag == "Breakables")
        {
            DestroyTile(hitUp);
        }
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, range);
        if (hitRight.collider != null && hitUp.collider.tag == "Breakables")
        {
            DestroyTile(hitUp);
        }
    }

    void OnDrawGizmosSelected()
    {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, Vector2.left * range);
    }

    public void Awake()
    {
        Spawn();
    }

    public void Spawn()
    {

    }

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= explodeDelay)
        {
            Explode();
        }
    }

    private void DestroyTile(RaycastHit2D hit)
    {
        Vector3Int hitPosition = Vector3Int.zero;
        hitPosition.x = ((int)hit.point.x);
        hitPosition.y = ((int)hit.point.y);
        hit.collider.GetComponent<Tilemap>().SetTile(hitPosition, null);
    }
}
