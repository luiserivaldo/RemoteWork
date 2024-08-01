using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{

    // Reference to Game Managers
    [Header("Reference to Game Managers")]
    private TaskManager taskManager;
    
    // NPC Action Bar Buttons
    [Header("NPC Action Bar Buttons")]
    public Button enquireButton;
    public Button praiseButton;

    
    // To identify currently selected NPC
    private NPC selectedNPC;
    
    void Start() 
    {
        enquireButton.onClick.AddListener(OnEnquireButtonClick);
        praiseButton.onClick.AddListener(OnPraiseButtonClick);
    }
    public void SetSelectedNPC(NPC npc) // Set clicked NPC as the selected NPC for logic functions
    {
        selectedNPC = npc;
    }
    public void AdjustMoodBonus()
    {
        /* // Adjust mood based on working arrangement
        if (selectedNPC.CurrentWorkArrangement == "On-site")
        {
            selectedNPC.MoodBonus -= 2;
        }
        else if (selectedNPC.CurrentWorkArrangement == "Remote Working")
        {
            selectedNPC.MoodBonus += 10;
        }

        selectedNPC.Mood += selectedNPC.MoodBonus;  */
    }
    public void OnEnquireButtonClick()
    {
        if (selectedNPC != null)
        {
            selectedNPC.Mood -= 1; // Decrease mood by 1
            if (selectedNPC.Mood < -10)
            {
                selectedNPC.Mood = -10; // Ensure mood doesn't go below -5
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
            if (selectedNPC.Mood > 10)
            {
                selectedNPC.Mood = 10; // Increase mood doesn't go below -5
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
