using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteKnight : MonoBehaviour {

    [SerializeField] float MoveSpeed = 5f;

	void Update () {
        transform.Translate(Time.deltaTime * MoveSpeed ,  0 , 0);
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Rotate();
        }
    }
    void Rotate()
    {
        transform.Rotate(0, 180, 0);
    }
}
