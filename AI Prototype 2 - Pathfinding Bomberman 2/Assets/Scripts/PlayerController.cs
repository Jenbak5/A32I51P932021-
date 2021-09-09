using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private void Start()
    {
        moveSpeed = moveSpeed * 0.0025f;
    }

    public void PlaceBomb()
    {
        // Instantiate new bomb at the center of current tile, (the closest one if overlapping 2+ tiles)
    }

    public void Move(Vector2 direction)
    {
        transform.Translate(direction * moveSpeed);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Move(Vector2.up);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Move(Vector2.left);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Move(Vector2.down);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Move(Vector2.right);
        }
    }
}
