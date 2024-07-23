using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public NPCGenerator npcGenerator; // Reference to the NPC generator
    public Slider weeklyQuotaSlider;
    private float workerCollectedProgress;
    public int weeklyQuotaGoal = 1000; // Max value required to reach weekly quota
    public Button enquireButton;
    private NPC selectedNPC;
    void Start()
    {
        // Initialize the slider
        weeklyQuotaSlider.maxValue = weeklyQuotaGoal;
        weeklyQuotaSlider.value = workerCollectedProgress;

        // Start the work process
        StartCoroutine(UpdateWorkDone());
        StartCoroutine(UpdateWeeklyWorkProgress());
        enquireButton.onClick.AddListener(OnEnquireButtonClick);
    }
    private IEnumerator UpdateWorkDone()
    {
        while (true)
        {
            foreach (var npc in npcGenerator.npcList.Values)
            {
                float workDoneValue = npc.WorkEfficiency * (1 + (npc.Mood / 20));
                npc.WorkDone += workDoneValue;
                //Debug.Log($"Updated WorkDone for {npc.WorkDone} value.");
            }
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator UpdateWeeklyWorkProgress()
    {
        while (workerCollectedProgress < weeklyQuotaSlider.maxValue)
        {
             float totalWorkDone = 0;

            foreach (var npc in npcGenerator.npcList.Values)
            {
                totalWorkDone += npc.WorkDone;
            }

            workerCollectedProgress += totalWorkDone;
            weeklyQuotaSlider.value = workerCollectedProgress;

            yield return new WaitForSeconds(1);
        }   
    }

    private void OnEnquireButtonClick()
    {
        //if (selectedNPC != null)
        {
            selectedNPC.Mood -= 2; // Decrease mood by 1
            /* if (selectedNPC.Mood < 0)
            {
                selectedNPC.Mood = 0; // Ensure mood doesn't go below 0
            } */
        }
        Debug.Log("Enquire clicked");
        //Debug.Log($"Clicked NPC ID {selectedNPC.NPCId}. Mood: {selectedNPC.Mood}");
        //Debug.Log($"Clicked NPC. Mood: {selectedNPC.Mood}");
    }

    // This method should be called to set the selected NPC, for example, when an NPC is clicked in the UI
    public void SetSelectedNPC(NPC npc)
    {
        selectedNPC = npc;
    }
}
