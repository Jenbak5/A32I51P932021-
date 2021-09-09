using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed;

    void Move()
    {

    }

    void Update()
    {
        if (Input.GetButton("Horizontal")) 
        {

        }
        if (Input.GetButton("Vertical"))
        {
            float translation = Input.GetAxis("Vertical") * moveSpeed;
        }
    }
}
