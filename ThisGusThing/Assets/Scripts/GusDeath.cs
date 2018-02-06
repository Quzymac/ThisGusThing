using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GusDeath : MonoBehaviour
{
    [Header("My Checkpoint")]
    [SerializeField]
    GameObject checkPoint;
    [SerializeField] GameObject gus;
    [SerializeField] MutationManager myMutationManager;
    Vector3 checkPointCoordinates;

    [SerializeField] float respawnTime = 3f;

    bool dead = false;

    bool isColliding = false; // Because Gus has two colliders, we need him to NOT collide two times with things. That'd be silly.

    private void Start()
    {
        checkPointCoordinates = checkPoint.transform.position;

    }
    private void Update()
    {
        if (dead)
        {
            StartCoroutine(Die());
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Danger")
        {
            if (isColliding) return;
            isColliding = true;
            print("DANGER DANGER!");

            dead = true;
        }

        if (col.gameObject.tag == "Checkpoint")
        {
            if (isColliding) return;
            isColliding = true;
            checkPoint = col.gameObject;  // Set new checkpoint.
            checkPointCoordinates = checkPoint.transform.position;
            print("New checkpoint!" + checkPointCoordinates);
        }
    }

    private void OnTriggerStay(Collider checkpoint)
    {
        if (checkpoint.gameObject.tag == "Checkpoint" && checkpoint.gameObject.name != "CheckPoint")
        {
            myMutationManager.MMPanelOpen();
        }
    }

    private void OnTriggerExit(Collider colliderino)
    {
        if (colliderino.gameObject.tag == "Checkpoint" && colliderino.gameObject.name != "CheckPoint")
        {
            myMutationManager.MMPanelClose();
            isColliding = false;
        }
    }

    IEnumerator Die()
    {
        print("hh");
        dead = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<GusMovement2>().SetIsDed(true);

        yield return new WaitForSeconds(3);

        this.transform.position = checkPointCoordinates;
        GetComponent<GusMovement2>().SetIsDed(false);
        GetComponent<Rigidbody>().isKinematic = false;

        isColliding = false;


    }
}