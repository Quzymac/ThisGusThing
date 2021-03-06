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
    [SerializeField]
    GameObject playerModel;
    [SerializeField] GameObject defaultModel;
    [SerializeField] GameObject JumpGFX;
    [SerializeField] GameObject JumpDownGFX;
    [SerializeField] GameObject JumpUpGFX;
    [SerializeField] GameObject IdleGFX;
    [SerializeField] GameObject runningGFX;
    [SerializeField] RuntimeAnimatorController runningAnim;
    [SerializeField] RuntimeAnimatorController walkingAnim;


    [Header("Audio")]
    AudioSource moveSFX;

    [SerializeField] AudioClip jumpAudio;
    [SerializeField] AudioClip doubleJumpAudio;
    [SerializeField] AudioClip landAudio;
    [SerializeField] AudioClip superSpeedAudio;
    [SerializeField] AudioClip deathSplatAudio;
    bool superSpeedSoundPlaying = false;

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

    [SerializeField] bool isded;



    void Start()
    {
        controller = GetComponent<CharacterController>();
        moveSFX = GetComponent<AudioSource>();

        isded = false;
    }

    void Update()
    {
        if (mutations.SuperSpeed)
        {


            if (Input.GetButton("Fire3"))
            {
                moveSpeed = 20f;
                runningGFX.GetComponent<Animator>().runtimeAnimatorController = runningAnim;
                if (!superSpeedSoundPlaying)
                {
                    moveSFX.PlayOneShot(superSpeedAudio, 0.1f);
                    superSpeedSoundPlaying = true;
                    StartCoroutine(SuperSpeedAudioCountdown(superSpeedAudio));
                }
            }
            else
            {
                moveSpeed = 10f;
                runningGFX.GetComponent<Animator>().runtimeAnimatorController = walkingAnim;

            }
        }

        if (verticalVelocity < -3.5f)
        {
            isFalling = true;
        }

        if (controller.isGrounded && isFalling)
        {
            moveSFX.PlayOneShot(landAudio);
            isFalling = false;
        }

        if (Input.GetButtonDown("Jump") && airJumps > 0 && !controller.isGrounded && !isded)
        {
            moveSFX.PlayOneShot(doubleJumpAudio);
            isFalling = true;
            doubleJump = false;
            airJumps--;
            verticalVelocity = jumpForce;
        }

        if (controller.isGrounded)
        {
            defaultModel.SetActive(true);
            JumpGFX.SetActive(false);
            if(Input.GetAxis("Horizontal") == 0)
            {
                IdleGFX.SetActive(true);
                runningGFX.SetActive(false);
            }
            else
            {
                IdleGFX.SetActive(false);
                runningGFX.SetActive(true);
            }

            doubleJump = true;
            airJumps = maxAirJumps;


            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump") && !isded)
            {
                defaultModel.SetActive(false);
                JumpGFX.SetActive(true);
                moveSFX.PlayOneShot(jumpAudio);
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

        if (!isded)
        {
            Vector3 moveVector = Vector3.zero;

            moveVector.x = Input.GetAxis("Horizontal") * moveSpeed;
            moveVector.y = verticalVelocity;
            controller.Move(moveVector * Time.deltaTime);

            
            float h = Input.GetAxis("Horizontal");

            if(h != 0 && controller.isGrounded)
            {
                defaultModel.SetActive(true);
                IdleGFX.SetActive(false);
            }

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
        else if (playerModel.activeInHierarchy && verticalVelocity < -3f)
        {
            defaultModel.SetActive(false);
            JumpGFX.SetActive(true);
        }
    }
    public void SetAirJumps(int jumps)
    {
        maxAirJumps = jumps;
    }

    public void SetIsDed(bool isgusded)
    {
        isded = isgusded; 
    }

    public void PlayDeathSplatSound()
    {
        moveSFX.PlayOneShot(deathSplatAudio);
    }

    IEnumerator SuperSpeedAudioCountdown(AudioClip audio)
    {
        yield return new WaitForSeconds(audio.length - 0.2f);
        superSpeedSoundPlaying = false;
    }
}
        
