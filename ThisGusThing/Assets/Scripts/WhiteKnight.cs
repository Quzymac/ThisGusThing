using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteKnight : MonoBehaviour {

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float raycastLength;

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
            if (Physics.Raycast(transform.position, right, raycastLength))
            {
                facingRight = false;
            }
            if (Physics.Raycast(transform.position, right, raycastLength + 1.3f))
            {
                targetRotation = Quaternion.Euler(0, 180, 0);
                rotating = true;
                rotationTime = 0;
            }
        }
        else if (!facingRight)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            Vector3 left = transform.TransformDirection(Vector3.left);
            if (Physics.Raycast(transform.position, left, raycastLength))
            {
                facingRight = true;
            }
            if (Physics.Raycast(transform.position, left, raycastLength + 1.3f))
            {
                targetRotation = Quaternion.Euler(0, 0, 0);
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
