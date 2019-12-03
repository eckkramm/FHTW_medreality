using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class obj_info : MonoBehaviour
{
    public GameObject assignedObject;
    public string infoText;
    public SteamVR_Action_Boolean myAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");


    private GameObject attachedObject;
    private Interactable hoveringObject; 
    private Hand hand;
    private Player player = null;
    //private Coroutine hintCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;

        if (player == null)
        {
            Debug.LogError("<b>[SteamVR Interaction]</b> Teleport: No Player instance found in map.");
            Destroy(this.gameObject);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {       
        foreach (Hand hand in player.hands)
        {
            bool showHint = IsEligibleForHint(hand);
            bool isShowingHint = !string.IsNullOrEmpty(ControllerButtonHints.GetActiveHintText(hand, myAction));
                
            if (showHint)
            {
                if (!isShowingHint)
                {
                    if (hand.hoveringInteractable != null && hand.hoveringInteractable.name == assignedObject.name)
                    {
                        if (hand == player.leftHand)
                        {
                            ControllerButtonHints.ShowTextHint(hand, myAction, this.infoText);
                            //Debug.Log("linke hand");
                        }
                        else if (hand == player.rightHand)
                        {
                            ControllerButtonHints.ShowTextHint(hand, myAction, hoveringObject.name);
                            //Debug.Log("rechte hand");
                        }
                    }
                }
            }
            else
            {
                if(isShowingHint)
                {
                    if (hand == player.leftHand)
                    {
                        ControllerButtonHints.HideTextHint(player.leftHand, myAction);
                        //ControllerButtonHints.HideTextOnlyHint(player.leftHand, myAction);
                        //Debug.Log("linke hand verstecken");
                    }
                    else if (hand == player.rightHand)
                    {
                        ControllerButtonHints.HideTextHint(player.rightHand, myAction);
                        //ControllerButtonHints.HideTextOnlyHint(player.rightHand, myAction);
                        //Debug.Log("rechte hand verstecken");
                    }
                }
            }                
            
            //Debug.Log(isShowingHint);
        }
    }

    public bool IsEligibleForHint(Hand hand)
    {
        if (hand == null)
        {
            return false;
        }

        //Hand hovers over object
        if (hand.hoveringInteractable != null)
        {
            hoveringObject = hand.hoveringInteractable;
            //Debug.Log(hoveringObject);
            return true;
        }

        if (hand.noSteamVRFallbackCamera == null)
        {
            if (hand.isActive == false)
            {
                return false;
            }

            //Something is attached to the hand
            if (hand.currentAttachedObject != null)
            {
                attachedObject = hand.currentAttachedObject;
                return false;
            }
        }
        return false;
    }
}
