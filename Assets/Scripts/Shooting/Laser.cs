using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Laser : MonoBehaviourPun
{

    [Range(1, 10)]
    [SerializeField] private float speed = 10f;

    [Range(1, 10)]
    [SerializeField] private float lifetime = 3f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }


    private void FixedUpdate()
    {
        rb.linearVelocity = transform.up * speed;
    }

}