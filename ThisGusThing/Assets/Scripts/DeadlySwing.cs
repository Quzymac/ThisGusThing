using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlySwing : MonoBehaviour {

   [SerializeField] GameObject rotationPoint;

    [SerializeField] float speed = 3f;
    [SerializeField] float swingAmount = 0.7f;
    Quaternion startPos;

    void Start()
    {
        startPos = rotationPoint.transform.rotation;
    }
    void Update()
    {
        Quaternion a = startPos;
        
        a.z += swingAmount * Mathf.Sin(Time.time * speed);
        rotationPoint.transform.rotation = a;
    }
}
