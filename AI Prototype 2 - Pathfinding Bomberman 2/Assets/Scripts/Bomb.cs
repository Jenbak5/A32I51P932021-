using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explodeDelay;

    private float timer;

    public void Explode()
    {
        Destroy(gameObject);
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
}
