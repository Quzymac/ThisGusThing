using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    // :THISISFINE:


    [Header("Tier 1 buttons")]
    [SerializeField]
    Button tier1DoubleJump;
    [Header("Tier 2 buttons")]
    [SerializeField]
    Button tier2SuperSpeed;
    [SerializeField] Button tier2TripleJump;

    private List<Button> tier1 = new List<Button>();
    private List<Button> tier2 = new List<Button>();

    MutationManager myMutationManager;

    private void Start() 
    { 
        tier1.Add(tier1DoubleJump);

        tier2.Add(tier2TripleJump);
        tier2.Add(tier2SuperSpeed);

        myMutationManager = this.GetComponent<MutationManager>();
    }

    public void SetButtonActive()  // Activates when player gets an orb! BEFORE clicking a button.
    {
        if(myMutationManager.DoubleJump == false) // if we don't have doublejump yet...
        {
            print("We don't have double jump yet!");
            foreach (Button buttons in tier1)
            {
                buttons.interactable = true; // set it to active.
            }

            foreach (Button buttons in tier2)
            {
                buttons.interactable = false; // the others, no.
            }
        }

        else if(myMutationManager.DoubleJump && myMutationManager.GetCurrentMutationCurrency() != 0) // if we DO have doublejump and we have currency...
        {
            print("We have double jump!");

            if (myMutationManager.SuperSpeed) // and we have Super Speed..
            {
                tier1DoubleJump.interactable = false; // TEST try to delete this, may work anyways.
                tier2SuperSpeed.interactable = false; // set it to inactive.
                tier2TripleJump.interactable = true;
            }

            if (myMutationManager.TripleJump) // and if we have Triple Jump...
            {
                tier1DoubleJump.interactable = false; // TEST try to delete this, may work anyways.
                tier2SuperSpeed.interactable = true;
                tier2TripleJump.interactable = false; 
            }
            else if (myMutationManager.TripleJump == false && myMutationManager.SuperSpeed == false)
            {
                tier2SuperSpeed.interactable = true;
                tier2TripleJump.interactable = true;
            }
        }

        else if (myMutationManager.GetCurrentMutationCurrency() == 0) // if we DON'T have currency (when we enter a checkpoint)
        {
            foreach (Button buttons in tier1)
            {
                buttons.interactable = false; 
            }

            foreach (Button buttons in tier2)
            {
                buttons.interactable = false; 
            }
        }          
    }

    public void ButtonIsClicked(Button clickedButton) // Activates when player chooses power. 
    {
        if (clickedButton.name.Contains("Tier1"))
        {
            clickedButton.interactable = false;
            //clickedButton.GetComponent<Image>().color = Color.green;
        }

        if (clickedButton.name.Contains("Tier2"))
        {
            clickedButton.interactable = false;
            //clickedButton.GetComponent<Image>().color = Color.green;
        }
    }
}
