using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutationManager : MonoBehaviour {

    //Borde denna vara här...? Oklart.
    [SerializeField] Text currencyTextBox; // Ta bort denna när vi kan. 

    [SerializeField] int mutationCurrency = 0;
    [SerializeField] GameObject gus;

    [Header("Tier1Ability")]
    [SerializeField] bool doubleJump = false;

    [Header("Tier2Ability")]
    [SerializeField] bool tripleJump = false;
    [SerializeField] bool superSpeed = false;

    [Header("TierUpgraded")]
    [SerializeField] bool tier1Upgraded = false;
    [SerializeField] bool tier2Upgraded = false;

    [Header("Mutation Manager Panel")]
    [SerializeField]
    GameObject MMPanel;
    [SerializeField]
    GameObject MMExitButton;
    [SerializeField]
    GameObject MMOpenPanel;

    UIManager myUIManager;

    private int currentTier;

    private void Start()
    {
        myUIManager = this.GetComponent<UIManager>();

        currencyTextBox.text = mutationCurrency.ToString();
        currentTier = 0;
        MMPanel.SetActive(false);
        MMOpenPanel.SetActive(false);
    }

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

    public bool TripleJump
    {
        get
        {
            return tripleJump;
        }
        private set
        {
            tripleJump = value;
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
                LoseMutationCurrency();
                currencyTextBox.text = mutationCurrency.ToString();
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
                LoseMutationCurrency();
                currencyTextBox.text = mutationCurrency.ToString();
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
                LoseMutationCurrency();
                currencyTextBox.text = mutationCurrency.ToString();
                tier2Upgraded = true;
            }
        }
    }

    public void PickUpMutationCurrency()
    {
        mutationCurrency++;
        currencyTextBox.text = mutationCurrency.ToString();

        if (currentTier == 0)
        {
            currentTier++;
        }

        print("Current tier is tier " + currentTier);
        TimeToMutate();
    }

    public void LoseMutationCurrency()
    {
        mutationCurrency--;
        if (mutationCurrency == 0 && MMPanel.activeInHierarchy)
        {
            MMExitButton.SetActive(true);
        }
    }

    public int GetCurrentMutationCurrency()
    {
        return mutationCurrency;
    }

    public void MMPanelOpen()
    {
        MMOpenPanel.SetActive(true);
    }

    public void MMPanelClose()
    {
        MMOpenPanel.SetActive(false);
    }

    public void TimeToMutate()
    {
        MMPanel.SetActive(true);
        myUIManager.SetButtonActive();
        Time.timeScale = 0;
        if (mutationCurrency == 0)
        {
            MMExitButton.SetActive(true);
        }
        else
        {
            MMExitButton.SetActive(false);
        }
    }

    public void MutationDone()
    {
        MMPanel.SetActive(false);
        Time.timeScale = 1;
    }

}
