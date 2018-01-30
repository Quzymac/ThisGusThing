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
    [SerializeField] GameObject JumpGFX;
    [SerializeField] GameObject JumpDownGFX;
    [SerializeField] GameObject JumpUpGFX;

    //Audio
    AudioSource moveSFX;     [SerializeField] AudioClip jumpAudio;     [SerializeField] AudioClip doubleJumpAudio;     [SerializeField] AudioClip landAudio; 

    [SerializeField] float fallMultiplier = 2.5f;   


    public bool doubleJump = false;
    bool isFalling = false;

    void Start () {
        controller = GetComponent<CharacterController>();
        moveSFX = GetComponent<AudioSource>();
	}
	
	void Update () {
        //Snabbfix: Sätter isFalling till true för att ljud ska spelas om man landar från ett fall utan att ha hoppat.
        if (verticalVelocity < -2.5f)         {             isFalling = true;         }

        if (controller.isGrounded && isFalling)         {             moveSFX.clip = landAudio;             moveSFX.Play();             isFalling = false;         }

        if (Input.GetButtonDown("Jump") && doubleJump && !controller.isGrounded)
            {
                moveSFX.clip = doubleJumpAudio;
                moveSFX.Play();
                isFalling = true;
                doubleJump = false;
                verticalVelocity = jumpForce;
            }

        if (controller.isGrounded)
        {
            graficsObject.SetActive(true);
            JumpGFX.SetActive(false);

            doubleJump = true;
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                graficsObject.SetActive(false);
                JumpGFX.SetActive(true);
                moveSFX.clip = jumpAudio;                 moveSFX.Play();                 isFalling = true;

                verticalVelocity = jumpForce;
            }
        }

        //gör att gus inte fastnar i taket när han hoppar
        if ((controller.collisionFlags == CollisionFlags.Above)) 
        {
            verticalVelocity = 0;
            verticalVelocity -= gravity * Time.deltaTime * 3;

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
        
        if (JumpGFX.activeInHierarchy && verticalVelocity < 0)
        {
            JumpUpGFX.SetActive(false);
            JumpDownGFX.SetActive(true);
        }
        else if (JumpGFX.activeInHierarchy && verticalVelocity > 0)
        {
            JumpUpGFX.SetActive(true);
            JumpDownGFX.SetActive(false);
        }
        else if(graficsObject.activeInHierarchy && verticalVelocity < -1.5f)
        {
            graficsObject.SetActive(false);
            JumpGFX.SetActive(true);
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        graficsObject.transform.Rotate(0, 180, 0);
        JumpGFX.transform.Rotate(0, 180, 0);


    }
}