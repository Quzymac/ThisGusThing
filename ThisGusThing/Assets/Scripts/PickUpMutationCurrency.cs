using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMutationCurrency : MonoBehaviour {

    [SerializeField] GameObject gus;
    [SerializeField] GameObject mutationManager;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == gus)
        {
            mutationManager.GetComponent<MutationManager>().PickUpMutationCurrency();
            Destroy(this.gameObject);
        }
    }
}
