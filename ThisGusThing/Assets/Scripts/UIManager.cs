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
    [SerializeField]
    Button tier2TripleJump;

    [Header("Sprites!")]
    [SerializeField]
    Sprite questionMarkSprite;
    [SerializeField]
    Sprite doubleJumpSprite;
    [SerializeField]
    Sprite tripleJumpSprite;
    [SerializeField]
    Sprite superSpeedSprite;

    private List<Button> tier1 = new List<Button>();
    private List<Button> tier2 = new List<Button>();

    MutationManager myMutationManager;

    Sprite mySprite;

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
            if (myMutationManager.GetCurrentMutationCurrency() != 0)
            {
                foreach (Button buttons in tier1)
                {
                    buttons.interactable = true;
                    buttons.GetComponent<Image>().sprite = doubleJumpSprite;
                }

                foreach (Button buttons in tier2)
                {
                    buttons.interactable = false;
                    buttons.GetComponent<Image>().sprite = questionMarkSprite;
                }
            }

            else
                return;
        }

        else if(myMutationManager.DoubleJump && myMutationManager.GetCurrentMutationCurrency() != 0) // if we DO have doublejump and we have currency...
        {
            print("We have double jump!");
            tier2SuperSpeed.GetComponentInChildren<Image>().sprite = superSpeedSprite;
            tier2TripleJump.GetComponentInChildren<Image>().sprite = tripleJumpSprite;


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
    }

    public void ButtonIsClicked(Button clickedButton) // Activates when player chooses power. 
    {
        if (clickedButton.name.Contains("Tier1"))
        {
            clickedButton.interactable = false;
        }

        if (clickedButton.name.Contains("Tier2"))
        {

            clickedButton.interactable = false;

            if (clickedButton.name == ("Tier2SuperSpeed") && tier2TripleJump.interactable == false)
            {
                tier2TripleJump.interactable = true;
            }
            if (clickedButton.name == ("Tier2TripleJump") && tier2SuperSpeed.interactable == false)
            {
                tier2SuperSpeed.interactable = true;      
            }
        }
    }
}
