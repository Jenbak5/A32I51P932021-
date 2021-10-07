using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Tilemaps;

public class PlayerController : BombermanControls
{
    public string UpClip;
    public string RightClip;
    public string LeftClip;
    public string DownClip;

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
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
        else if (!Input.anyKey)
        {
            Move(Vector2.zero);
            rigidbody.velocity = Vector2.zero;
        }
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaceBomb();
        }

    }
}
