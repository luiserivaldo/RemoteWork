using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public Slider weeklyQuotaSlider;
    private float workerCollectedProgress;
    public int weeklyQuotaGoal = 1000; // Max value required to reach weekly quota
    public NPCGenerator npcGenerator; // Reference to the NPC generator

    void Start()
    {
        // Initialize the slider
        weeklyQuotaSlider.maxValue = weeklyQuotaGoal;
        weeklyQuotaSlider.value = workerCollectedProgress;

        // Start the work process
        StartCoroutine(UpdateWorkProgress());
    }

    IEnumerator UpdateWorkProgress()
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
}
