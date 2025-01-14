using UnityEngine;
using Photon.Pun;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerSetUp : MonoBehaviour
{
    PhotonView view;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField][Tooltip("Control the player with defined keyboard buttons")] public InputAction PlayerControls;
    [SerializeField][Tooltip("Movement speed in meters per second")] private float _speed = 5f;

    Vector2 moveDir = Vector2.zero;

    private Rigidbody2D rb;

    void OnEnable()
    {
        PlayerControls.Enable();
    }

    void OnDisable()
    {
        PlayerControls.Disable();
    }

    void Start()
    {
        view = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (view.IsMine)
            moveDir = PlayerControls.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {

        rb.linearVelocity = new Vector2(moveDir.x * _speed, moveDir.y * _speed); //Move corresponding to the vector and speed

    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rb.position);
            stream.SendNext(rb.linearVelocity);
        }
        else
        {
            rb.position = (Vector2)stream.ReceiveNext();
            rb.linearVelocity = (Vector2)stream.ReceiveNext();
        }
    }


}
