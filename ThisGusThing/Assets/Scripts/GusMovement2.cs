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
    [SerializeField] float fallMultiplier = 2.5f;   

    public bool doubleJump = false;

    void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	void Update () {

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
        //gör att gus inte fastnar i taket när han hoppar
        if ((controller.collisionFlags == CollisionFlags.Above)) 
        {
            verticalVelocity = 0;
            verticalVelocity -= gravity * Time.deltaTime;

        }
        //snabb fix för att gus inte ska fastna när han hoppar upp i hörn
        if ((((controller.collisionFlags & CollisionFlags.Above) != 0) && (controller.collisionFlags & CollisionFlags.Sides) != 0))
        {
            verticalVelocity = 0;
            verticalVelocity -= gravity * Time.deltaTime * 7;
        }

        else
        {
            verticalVelocity -= gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        Vector3 moveVector = Vector3.zero;
        moveVector.x = Input.GetAxis("Horizontal") * moveSpeed;
        moveVector.y = verticalVelocity;
        controller.Move(moveVector * Time.deltaTime);


        //flip modell
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
        graficsObject.transform.Rotate(0, 180, 0);
    }
}
