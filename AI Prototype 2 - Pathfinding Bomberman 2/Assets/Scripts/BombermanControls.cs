using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombermanControls : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rigidbody;

    public GameObject bombPrefab = null;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        // moveSpeed = moveSpeed * 0.0025f;
    }

    public void PlaceBomb()
    {
        // Instantiate new bomb at the center of current tile, (the closest one if overlapping 2+ tiles)
        Instantiate(bombPrefab, transform.position, transform.rotation);
    }

    public void Move(Vector2 direction)
    {
        rigidbody.velocity = direction * moveSpeed;
        // transform.Translate(direction * moveSpeed);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Move(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector2.down);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Move(Vector2.right);
        }
        else if (!Input.anyKey)
        {
            rigidbody.velocity = Vector2.zero;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            PlaceBomb();
        }
    }
}
