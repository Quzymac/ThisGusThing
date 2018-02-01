using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutationManager : MonoBehaviour {

    [SerializeField] int mutationCurrency = 3;
    [SerializeField] GameObject gus;

    [Header("Tier1Ability")]
    [SerializeField] bool doubleJump = false;

    [Header("Tier2Ability")]
    [SerializeField] bool tripleJump = false;
    [SerializeField] bool superSpeed = false;

    [Header("TierUpgraded")]
    [SerializeField] bool tier1Upgraded = false;
    [SerializeField] bool tier2Upgraded = false;
    


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
        if(mutationCurrency > 0 && !tier1Upgraded && !tier2Upgraded)
        {
            gus.GetComponent<GusMovement2>().SetAirJumps(1);
            if (!doubleJump)
            {
                mutationCurrency--;
                doubleJump = true;
                tier1Upgraded = true;
            }
            

        }
    }

    //Tier2
    public void ActivateTripleJump()
    {
        if(tripleJump)
        { return; }
        if ((mutationCurrency > 0 && tier1Upgraded) || tier2Upgraded) // && check for tier3
        {
            gus.GetComponent<GusMovement2>().SetAirJumps(2);
            tripleJump = true;
            superSpeed = false;

            if (!tier2Upgraded)
            {
                mutationCurrency--;
                tier2Upgraded = true;
            }
        }
    }

    public void ActivateSuperSpeed()
    {
        if (superSpeed)
        { return; }
        if ((mutationCurrency > 0 && tier1Upgraded) || tier2Upgraded) // && check for tier3
        {
            superSpeed = true;
            tripleJump = false;
            gus.GetComponent<GusMovement2>().SetAirJumps(1);

            if (!tier2Upgraded)
            {
                mutationCurrency--;
                tier2Upgraded = true;
            }
        }
    }

    public void PickUpMutationCurrency()
    {
        mutationCurrency++;
        print(mutationCurrency);
    }

}
