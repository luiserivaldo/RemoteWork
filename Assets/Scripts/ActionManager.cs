using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    private NPC selectedNPC;
    public Button enquireButton;
    public Button praiseButton;
    
    void Start()
    {
    enquireButton.onClick.AddListener(OnEnquireButtonClick);
    praiseButton.onClick.AddListener(OnPraiseButtonClick);

    }
    public void SetSelectedNPC(NPC npc)
    {
        selectedNPC = npc;
    }
    
    public void OnEnquireButtonClick()
    {
        if (selectedNPC != null)
        {
            selectedNPC.Mood -= 1; // Decrease mood by 1
            if (selectedNPC.Mood < -5)
            {
                selectedNPC.Mood = -5; // Ensure mood doesn't go below -5
            } 
        }
        else
        {
            Debug.LogError("No NPC selected. Cannot enquire.");
        }
    }
    public void OnPraiseButtonClick()
    {
        if (selectedNPC != null)
        {
            selectedNPC.Mood += 1; // Increase mood by 1
            if (selectedNPC.Mood > 5)
            {
                selectedNPC.Mood = 5; // Increase mood doesn't go below -5
            } 
            /* Debug.Log("Praise clicked");
            Debug.Log($"Praise NPC ID {selectedNPC.NPCId}. Mood: {selectedNPC.Mood}"); */
        }
        else
        {
            Debug.LogError("No NPC selected. Cannot praise.");
        }
    }

}
