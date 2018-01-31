using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutationManager : MonoBehaviour {

    [SerializeField] int mutationCurrency = 1;

    [Header("Tier1Ability")]
    [SerializeField] bool doubleJump = false;

    [Header("Tier2Ability")]
    [SerializeField] bool trippleJump = false;
    [SerializeField] bool superSpeed = false;


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

    //Tier1
    public void ActivateDoubleJump()
    {
        if(mutationCurrency > 0 && (!trippleJump || !superSpeed))
        {
            doubleJump = true;
            mutationCurrency--;
        }
    }

    //Tier2
    public void ActivateTrippleJump()
    {
        if (mutationCurrency > 0 && doubleJump && !superSpeed) // && check for tier3
        {
            trippleJump = true;
            mutationCurrency--;
        }
    }

    public void ActivateSuperSpeed()
    {
        if (mutationCurrency > 0 && doubleJump && !trippleJump) // && check for tier3
        {
            superSpeed = true;
            mutationCurrency--;
        }
    }

    public void PickUpMutationCurrency()
    {
        mutationCurrency++;
        print(mutationCurrency);
    }

}
