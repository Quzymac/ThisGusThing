using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutationManager : MonoBehaviour {

    [SerializeField] int mutationCurrency = 3;

    [Header("Tier1Ability")]
    [SerializeField] bool doubleJump = false;

    [Header("Tier2Ability")]
    [SerializeField] bool tripleJump = false;
    [SerializeField] bool superSpeed = false;


    public bool DoubleJump
    {
        get{
            return doubleJump;
        }
        private set{
            doubleJump = value;
        }
    }

    public bool SuperSpeed
    {
        get{
            return superSpeed;
        }
        private set{
            superSpeed = value;
        }
    }

    //Tier1
    public void ActivateDoubleJump()
    {
        if (doubleJump)
        { return; }
        if(mutationCurrency > 0 && (!tripleJump || !superSpeed))
        {
            doubleJump = true;
            mutationCurrency--;
        }
    }

    //Tier2
    public void ActivateTripleJump()
    {
        if(tripleJump)
        { return; }
        if (mutationCurrency > 0 && doubleJump && !superSpeed) // && check for tier3
        {
            tripleJump = true;
            mutationCurrency--;
        }
    }

    public void ActivateSuperSpeed()
    {
        if (superSpeed)
        { return; }
        if (mutationCurrency > 0 && doubleJump && !tripleJump) // && check for tier3
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
