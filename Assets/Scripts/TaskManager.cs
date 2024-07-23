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
    private NPC selectedNPC;
    void Start()
    {
        // Initialize the slider
        weeklyQuotaSlider.maxValue = weeklyQuotaGoal;
        weeklyQuotaSlider.value = workerCollectedProgress;

        // Start the work process
        StartCoroutine(UpdateWorkProgress());
        enquireButton.onClick.AddListener(OnEnquireButtonClick);
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
        npc.WorkDonePerIncrement = npc.WorkEfficiency * (1 + (npc.Mood / 20f));
        npc.TotalWorkDone += npc.WorkDonePerIncrement;
        if (npc.TotalWorkDone >= npc.MaxTaskCapacity)
        {
            npc.TotalWorkDone = 0;
        }
        //Debug.Log($"NPC ID: {npc.NPCId} total work: {npc.TotalWorkDone}");
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
