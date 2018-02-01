using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GusMovement2 : MonoBehaviour
{

    CharacterController controller;

    [Header("Physics")]
    [SerializeField]
    float verticalVelocity;
    [SerializeField] float gravity = 14.0f;
    [SerializeField] float jumpForce = 10.0f;
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float fallMultiplier = 2.5f;
    bool isFalling = false;

    [Header("GameObjects")]
    [SerializeField] GameObject playerModel;
    [SerializeField] GameObject defaultModel;
    [SerializeField] GameObject JumpGFX;
    [SerializeField] GameObject JumpDownGFX;
    [SerializeField] GameObject JumpUpGFX;

    [Header("Audio")]
    AudioSource moveSFX;
    [SerializeField] AudioClip jumpAudio;
    [SerializeField] AudioClip doubleJumpAudio;
    [SerializeField] AudioClip landAudio;

    //Rotations
    float rotationTime;
    Quaternion targetRotation;
    float rotationSpeed = 1f;
    bool rotating = false;
    bool facingRight = true;

    [Header("PowerUps")]

    public int airJumps = 0;
    public int maxAirJumps = 0;

    bool doubleJump = true;


    [SerializeField] MutationManager mutations;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        moveSFX = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(mutations.SuperSpeed)
        {
            if (Input.GetButton("Fire3"))
                moveSpeed = 20f;
            else
                moveSpeed = 10f;
        }

        if (verticalVelocity < -2.5f)
        {
            isFalling = true;
        }

        if (controller.isGrounded && isFalling)
        {
            moveSFX.clip = landAudio;
            moveSFX.Play();
            isFalling = false;
        }

        if (Input.GetButtonDown("Jump") && airJumps > 0 && !controller.isGrounded)
        {
            moveSFX.clip = doubleJumpAudio;
            moveSFX.Play();
            isFalling = true;
            doubleJump = false;
            airJumps--;
            verticalVelocity = jumpForce;
        }

        if (controller.isGrounded)
        {
            defaultModel.SetActive(true);
            JumpGFX.SetActive(false);

            doubleJump = true;
            airJumps = maxAirJumps;


            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                defaultModel.SetActive(false);
                JumpGFX.SetActive(true);
                moveSFX.clip = jumpAudio;
                moveSFX.Play();
                isFalling = true;

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

        float h = Input.GetAxis("Horizontal");

        if (h > 0 && !facingRight) //d
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
            facingRight = true;
            rotating = true;
            rotationTime = 0;
        }

        else if (h < 0 && facingRight) //a
        {
            targetRotation = Quaternion.Euler(0, -180, 0);
            facingRight = false;
            rotating = true;
            rotationTime = 0;
        }

        if (rotating)
        {
            rotationTime += Time.deltaTime * rotationSpeed;
            playerModel.transform.rotation = Quaternion.Lerp(playerModel.transform.rotation, targetRotation, rotationTime);

            if (rotationTime > 1)
            {
                rotating = false;
            }
        }
        
        if (JumpGFX.activeInHierarchy && verticalVelocity< 0)
        {
            JumpUpGFX.SetActive(false);
            JumpDownGFX.SetActive(true);
        }
        else if (JumpGFX.activeInHierarchy && verticalVelocity > 0)
        {
            JumpUpGFX.SetActive(true);
            JumpDownGFX.SetActive(false);
        }
        else if(playerModel.activeInHierarchy && verticalVelocity< -1.5f)
        {
            defaultModel.SetActive(false);
            JumpGFX.SetActive(true);
        }
    }
    public void SetAirJumps(int jumps)
    {
        maxAirJumps = jumps;
    }
}