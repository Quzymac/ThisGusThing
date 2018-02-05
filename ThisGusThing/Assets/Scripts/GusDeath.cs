using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GusDeath : MonoBehaviour
    {
    [Header("My Checkpoint")]
    [SerializeField] GameObject checkPoint;
    [SerializeField] GameObject gus;
    [SerializeField] MutationManager myMutationManager;
    Vector3 checkPointCoordinates;

    CharacterController myCharacterController;
    
    bool isColliding = false; // Because Gus has two colliders, we need him to NOT collide two times with things. That'd be silly.

    private void Start()
    {
        checkPointCoordinates = checkPoint.transform.position;
        myCharacterController = this.GetComponent<CharacterController>();

    }
   
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Danger")
        {
            if (isColliding) return;
            isColliding = true;
            print("DANGER DANGER!");
            gus.GetComponent<Rigidbody>().isKinematic = true;
            gus.GetComponent<GusMovement2>().SetIsDed(true);
            StartCoroutine(Die());
        }

        if (col.gameObject.tag == "Checkpoint")
        {
            if (isColliding) return;
            isColliding = true;
            checkPoint = col.gameObject;  // Set new checkpoint.
            print("New checkpoint!" + checkPointCoordinates);
        }
    }

    private void OnTriggerStay(Collider checkpoint)
    {
        if (checkpoint.gameObject.tag == "Checkpoint" && checkpoint.gameObject.name != "CheckPoint1")
        {
            myMutationManager.MMPanelOpen();
        }
    }

    private void OnTriggerExit(Collider colliderino)
    {
        if (colliderino.gameObject.tag == "Checkpoint" && colliderino.gameObject.name != "CheckPoint1")
        {
            myMutationManager.MMPanelClose();
            isColliding = false;
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(3);     
        this.transform.position = checkPointCoordinates;
        gus.GetComponent<GusMovement2>().SetIsDed(false);
        gus.GetComponent<Rigidbody>().isKinematic = false;
        //gus.GetComponent<GusMovement2>().enabled = true;
        isColliding = false;
    }
}
