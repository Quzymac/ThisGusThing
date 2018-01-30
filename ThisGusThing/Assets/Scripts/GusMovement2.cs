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

    public bool doubleJump = false;
    [SerializeField] int jumps = 2;


    void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	void Update () {

        print(verticalVelocity);
        if (Input.GetButtonDown("Jump") && doubleJump && !controller.isGrounded)
            {
                doubleJump = false;

                verticalVelocity = jumpForce;
            }

        if (controller.isGrounded)
        {
            
            doubleJump = true;
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
            }
        }
        if ((controller.collisionFlags == CollisionFlags.Above))
        {
            print("coll");
            verticalVelocity = 0;
            verticalVelocity -= gravity * Time.deltaTime;

        }
        if ((controller.collisionFlags & CollisionFlags.Sides) != 0)
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        Vector3 moveVector = Vector3.zero;
        moveVector.x = Input.GetAxis("Horizontal") * moveSpeed;
        moveVector.y = verticalVelocity;
        controller.Move(moveVector * Time.deltaTime);


        //flip
        float h = Input.GetAxis("Horizontal");
        
        if (h > 0 && !facingRight)
        {
            Flip();
        }
        else if (h< 0 && facingRight)
        {
            Flip();
        } 
	}
    void Flip()
    {
        facingRight = !facingRight;
        graficsObject.transform.Rotate(0, 0, 180);
    }
}
