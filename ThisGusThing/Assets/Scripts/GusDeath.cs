using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GusDeath : MonoBehaviour
    {
    [Header("My Checkpoint")]
    [SerializeField]
    public GameObject checkPoint;

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
            Death();
        }

        if (col.gameObject.tag == "Checkpoint")
        {
            checkPoint = col.gameObject;  // Set new checkpoint.
            print("New checkpoint!" + checkPointCoordinates);
        }
    }

    void Death()
    {
        this.transform.position = checkPointCoordinates;
    }

}
