using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteKnight : MonoBehaviour {

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float raycastLength;
    bool hitObject = false;

    [SerializeField] GameObject graficsModel;
    float rotationTime;
    Quaternion targetRotation;
    [SerializeField] float rotationSpeed = 1f;
    bool rotating = false;
    bool facingRight = true;
    
    void Update()
    {
        if (facingRight)
        {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

            Vector3 right = transform.TransformDirection(Vector3.right);
            if (hitObject == false && Physics.Raycast(transform.position, right, raycastLength))
            {
                hitObject = false;
                targetRotation = Quaternion.Euler(0, 270, 0);
                facingRight = false;
                rotating = true;
                rotationTime = 0;
            }
        }
        else if (!facingRight)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            Vector3 left = transform.TransformDirection(Vector3.left);
            if (hitObject == false && Physics.Raycast(transform.position, left, raycastLength))
            {
                hitObject = false;
                targetRotation = Quaternion.Euler(0, 90, 0);
                facingRight = true;
                rotating = true;
                rotationTime = 0;
            }
        }

        if (rotating)
        {
            
            rotationTime += Time.deltaTime * rotationSpeed;
            graficsModel.transform.rotation = Quaternion.Lerp(graficsModel.transform.rotation, targetRotation, rotationTime);
            if (rotationTime > 1)
            {
                rotating = false;
            }
        }
    }
}
