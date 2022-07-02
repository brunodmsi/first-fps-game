using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController _controller;

    private Vector3 _velocity;
    private bool _groundedPlayer;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.0f;

    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Transform groundCheck;

    private void Start() {
        _controller = gameObject.GetComponent<CharacterController>();

        if (_controller == null) {
            Debug.LogError("Player is missing controller");
        }
    }

    void Update()
    {
        _groundedPlayer = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_groundedPlayer && _velocity.y < 0) {
            _velocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && _groundedPlayer) {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        if (move.magnitude > 1) {
            move /= move.magnitude;
        }

        _velocity.y += gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }
}
