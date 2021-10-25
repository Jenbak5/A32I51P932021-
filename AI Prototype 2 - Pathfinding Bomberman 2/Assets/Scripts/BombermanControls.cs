using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombermanControls : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public float moveSpeed;
    [SerializeField] private int bombAmount = 1;
    [SerializeField] private float bombRange = 1;

    public Rigidbody2D rigidbody;

    public GameObject bombPrefab = null;
    public GameObject bombPlacement = null;

    public Vector3 currentTile = Vector3.zero;

    public int placedBombs = 0;
    private float bombTimer = 3f;
    private Vector3 currentTileOffset = new Vector3(0.5f, 0.5f, 0);

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public virtual void PlaceBomb()
    {
        GetCellPosition();
        if (placedBombs < bombAmount)
        {
            //GameObject bombClone = Instantiate(bombPrefab, bombPlacement.transform.position, Quaternion.identity);
            GameObject bombClone = Instantiate(bombPrefab, (currentTile + currentTileOffset), Quaternion.identity);
            bombClone.GetComponent<Bomb>().range = bombRange;
            placedBombs++;
        }
    }

    public virtual void Move(Vector3 direction)
    {
        transform.position += direction * moveSpeed * Time.deltaTime;
        if (direction == Vector3.up) { animator.SetBool("WalkUp", true); } else { animator.SetBool("WalkUp", false); }
        if (direction == Vector3.left) { animator.SetBool("WalkLeft", true); } else { animator.SetBool("WalkLeft", false); }
        if (direction == Vector3.down) { animator.SetBool("WalkDown", true); } else { animator.SetBool("WalkDown", false); }
        if (direction == Vector3.right) { animator.SetBool("WalkRight", true); } else { animator.SetBool("WalkRight", false); }
    }

    public virtual void TakeDamage()
    {

    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("It gets here");
    //    if (collision.gameObject.tag == "Floor")
    //    {
    //        Vector2 hitPosition = Vector2.zero;
    //        Vector3Int cellPosition = Vector3Int.zero;
    //        Tilemap tilemap = collision.gameObject.GetComponent<Tilemap>();

    //        hitPosition.x = collision.transform.position.x;
    //        hitPosition.y = collision.transform.position.y;
    //        Debug.Log(hitPosition.x + " " + hitPosition.y);

    //        cellPosition = tilemap.WorldToCell(hitPosition);
    //        currentTile = cellPosition;
    //        Debug.Log("Current tile: " + currentTile);
    //    }
    //    //currentTile = tilemap.CellToLocal(cellPosition);
    //}

    private void GetCellPosition()
    {
        LayerMask layer = LayerMask.GetMask("Floor");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.001f, layer);
        if (hit.collider != null && hit.collider.gameObject.tag == "Floor")
        {
            Tilemap tilemap = hit.collider.gameObject.GetComponent<Tilemap>();
            Vector3Int cell = tilemap.WorldToCell(hit.point);
            currentTile = tilemap.CellToWorld(cell);
            //Debug.Log("Current tile: " + currentTile);
        }
    }

    public virtual void Update()
    {
        if (placedBombs > 0)
        {
            bombTimer -= Time.deltaTime;
            if (bombTimer <= 0)
            {
                placedBombs -= 1;
                bombTimer = 3f;
            }
        }
    }
}

