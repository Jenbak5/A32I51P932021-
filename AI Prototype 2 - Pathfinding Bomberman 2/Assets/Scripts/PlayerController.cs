using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : BombermanControls
{
    [SerializeField] private Animator animator;
    public string UpClip;
    public string RightClip;
    public string LeftClip;
    public string DownClip;

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Move(Vector2.up);
            animator.Play(UpClip);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Move(Vector2.left);
            animator.Play(LeftClip);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Move(Vector2.down);
            animator.Play(DownClip);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Move(Vector2.right);
            animator.Play(RightClip);
        }
        else if (!Input.anyKey)
        {
            rigidbody.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlaceBomb();
        }
    }
}
