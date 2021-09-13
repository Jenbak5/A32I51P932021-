using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombermanControls : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public Rigidbody2D rigidbody;

    public GameObject bombPrefab = null;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        // moveSpeed = moveSpeed * 0.0025f;
    }

    private protected void PlaceBomb()
    {
        // Instantiate new bomb at the center of current tile, (the closest one if overlapping 2+ tiles)
        Instantiate(bombPrefab, transform.position, transform.rotation);
    }

    private protected void Move(Vector2 direction)
    {
        //rigidbody.velocity = direction * moveSpeed;
        transform.Translate(direction * moveSpeed);
    }
}
