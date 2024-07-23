using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public NPCGenerator npcGenerator; // Reference to the NPC generator
    public Slider weeklyQuotaSlider;
    private float workerCollectedProgress;
    public int weeklyQuotaGoal = 10000; // Max value required to reach weekly quota
    public Button enquireButton;
    public Button praiseButton;
    private NPC selectedNPC;

    void Start()
    {
        // Initialize the slider
        weeklyQuotaSlider.maxValue = weeklyQuotaGoal;
        weeklyQuotaSlider.value = workerCollectedProgress;

        // Start the work process
        StartCoroutine(UpdateWorkProgress());
        enquireButton.onClick.AddListener(OnEnquireButtonClick);
        praiseButton.onClick.AddListener(OnPraiseButtonClick);
    }

    IEnumerator UpdateWorkProgress()
    {
        while (workerCollectedProgress < weeklyQuotaSlider.maxValue)
        {
            float totalWeeklyIncrementalWorkDone = 0;

            foreach (var npc in npcGenerator.npcList.Values)
            {
                UpdateWorkDone(npc);
                float incrementalWorkDone = npc.WorkDonePerIncrement;
                totalWeeklyIncrementalWorkDone += incrementalWorkDone;
                //npc.WorkDonePerIncrement = npc.TotalWorkDone; // Update the last recorded work done
            }

            workerCollectedProgress += totalWeeklyIncrementalWorkDone;
            weeklyQuotaSlider.value = workerCollectedProgress;

            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateWorkDone(NPC npc)
    {
        npc.WorkDonePerIncrement = npc.WorkEfficiency * (1 + (npc.Mood / 10f));
        npc.TotalWorkDone += npc.WorkDonePerIncrement;
        if (npc.TotalWorkDone >= npc.MaxTaskCapacity)
        {
            npc.TotalWorkDone = 0;
        }
        //Debug.Log($"NPC ID: {npc.NPCId} total work: {npc.TotalWorkDone}");
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
            /* Debug.Log("Enquire clicked");
            Debug.Log($"Clicked NPC ID {selectedNPC.NPCId}. Mood: {selectedNPC.Mood}"); */
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

    // This method should be called to set the selected NPC, for example, when an NPC is clicked in the UI
    public void SetSelectedNPC(NPC npc)
    {
        selectedNPC = npc;
    }
}
