using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GusMovement2 : MonoBehaviour {

    CharacterController controller;

    [SerializeField] float verticalVelocity;
    [SerializeField] float gravity = 14.0f;
    [SerializeField] float jumpForce = 10.0f;
    [SerializeField] float moveSpeed = 10.0f;
    bool facingRight = true;
    [SerializeField] GameObject graficsObject;

    bool doubleJump = false;
    [SerializeField] int jumps = 2;


    void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	void Update () {

        if (controller.isGrounded )
        {
            jumps = 2;
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetButton("Jump") && jumps > 0)
            {
                jumps--;
                doubleJump = true;

                verticalVelocity = jumpForce;
            }
        }
        else
        {
            if (Input.GetButton("Jump") && jumps == 1)
            {
                jumps--;
                doubleJump = true;

                verticalVelocity = jumpForce;
            }
            verticalVelocity -= gravity * Time.deltaTime;
        }
        Vector3 moveVector = Vector3.zero;
        moveVector.x = Input.GetAxis("Horizontal") * moveSpeed;
        moveVector.y = verticalVelocity;
        controller.Move(moveVector * Time.deltaTime);


        float h = Input.GetAxis("Horizontal");


        if (h > 0 && !facingRight)
            Flip();
        else if (h< 0 && facingRight)
            Flip();
	}
    void Flip()
    {
        facingRight = !facingRight;
        graficsObject.transform.Rotate(0, 0, 180);
    }
}
