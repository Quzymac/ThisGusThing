using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GusDeath : MonoBehaviour
    {
    [Header("My Checkpoint")]
    [SerializeField] GameObject checkPoint;
    [SerializeField] GameObject gus;

    Vector3 checkPointCoordinates;

    private void Start()
    {
        checkPointCoordinates = checkPoint.transform.position;
    }
   

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Danger")
        {
            print("DANGER DANGER!");
            gus.GetComponent<Rigidbody>().isKinematic = true;
            gus.GetComponent<GusMovement2>().enabled = false;
            StartCoroutine(Die());
        }

        if (col.gameObject.tag == "Checkpoint")
        {
            checkPoint = col.gameObject;  // Set new checkpoint.
            print("New checkpoint!" + checkPointCoordinates);
        }
    }
   IEnumerator Die()
    {
        yield return new WaitForSeconds(3);
        
        this.transform.position = checkPointCoordinates;
        gus.GetComponent<Rigidbody>().isKinematic = false;
        gus.GetComponent<GusMovement2>().enabled = true;



    }
}
