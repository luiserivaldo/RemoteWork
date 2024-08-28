using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    // Reference to Game Managers
    [Header("Reference to Game Managers")]
    public NPCGenerator npcGenerator; // Reference to the NPC generator
    public ActionManager actionManager; // Reference to the Action Manager

    // Weekly Quota components
    [Header("Weekly Quota Components")]
    public Slider weeklyQuotaSlider;
    private float workerCollectedProgress;
    public int weeklyQuotaGoal = 10000; // Max value required to reach weekly quota
    [HideInInspector] public int numOfTotalTasksCompleted;
    [HideInInspector] public int weeksPassed;

    // Budget components
    [Header("Budget Components")]
    public int startingBudget = 300000;
    [HideInInspector] public int currentBudget = 0;
    private int totalSalary = 0;

    void Start()
    {
        // Initialize sliders
        weeklyQuotaSlider.maxValue = weeklyQuotaGoal;
        weeklyQuotaSlider.value = workerCollectedProgress;
        
        CalculateBudget();
        // Start the work process
        StartCoroutine(UpdateWorkProgress());
        
    }
    void Update()
    {

    }

    IEnumerator UpdateWorkProgress() // Updates slider values
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
                totalSalary += npc.Salary;
            }

            workerCollectedProgress += totalWeeklyIncrementalWorkDone;
            weeklyQuotaSlider.value = workerCollectedProgress;

            if (workerCollectedProgress >= weeklyQuotaGoal)
            {
                weeksPassed += 1;
                workerCollectedProgress = 0;
                currentBudget -= totalSalary;
            }

            yield return new WaitForSeconds(1);
        }
    }

    private void UpdateWorkDone(NPC npc) // Calculate work done per individual NPC
    {
        npc.WorkDonePerIncrement = npc.WorkEfficiency * (1 + (npc.Mood / 20f));
        npc.TotalWorkDone += npc.WorkDonePerIncrement;
        if (npc.TotalWorkDone >= npc.MaxTaskCapacity)
        {
            npc.TotalWorkDone = 0;
            npc.numOfTasksCompleted += 1;
            numOfTotalTasksCompleted += 1;
            currentBudget += 10000; // Increase budget when NPC has completed a task
        }
        //Debug.Log($"NPC ID: {npc.NPCId} total work: {npc.TotalWorkDone}");
    }
    private void CalculateBudget()
    {
        currentBudget += startingBudget;
    }
}
