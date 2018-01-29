using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GusMovement : MonoBehaviour {

    bool facingRight = true;
    [Header("Physics")]
    [SerializeField] float moveForce = 365f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float jumpForce = 1000f;
    [SerializeField] float gravity = -9.81f;

    [Header("Other")]
    [SerializeField] bool doubleJumpAvalible = false;
    bool jump = false;
    bool doubleJump = false;
    bool grounded = false;

    Rigidbody rb;
    [SerializeField] Transform ground;
    [SerializeField] GameObject graficsObject;

    void Start () {
        Physics.gravity = new Vector3(0, gravity, 0);
        rb = GetComponentInChildren<Rigidbody>();
	}
	
	void Update () {


        if (Input.GetButtonDown("Jump") && !grounded && doubleJump && doubleJumpAvalible)
        {
            doubleJump = false;
            jump = true;
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            grounded = false;
            jump = true;
        }
    }

    void FixedUpdate()
    {
        if (jump)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

        float h = Input.GetAxis("Horizontal");

        if (h * rb.velocity.x < maxSpeed)
            rb.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        graficsObject.transform.Rotate(0,0,180);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == ground.tag)
        {
            grounded = true;
           
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == ground.tag)
        {
            grounded = false;
            if (!doubleJumpAvalible) //aktiverar/avaktiverar double jump så det går att slå av/på under körning
            {
                doubleJump = false;
            }
            if (doubleJumpAvalible)
            {
                doubleJump = true;
            }
        }
    }
}
