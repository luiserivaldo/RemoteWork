using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public Slider weeklyQuotaSlider;
    public Slider[] individualTaskSliders = new Slider[6]; // Individual task sliders for each worker
    public float workCompletion;
    public float taskGoal = 1000.0f; // Example goal, adjust as necessary
    private List<NPC> workers = new List<NPC>();
    public SimpleNPCGenerator simpleNpcGenerator; // Reference to the NPC generator

    void Start()
    {
        // Generate 6 random workers
        for (int i = 0; i < 6; i++)
        {
            NPC worker = simpleNpcGenerator.GenerateRandomNPC();
            workers.Add(worker);
            individualTaskSliders[i].maxValue = worker.TaskValue;
            individualTaskSliders[i].value = worker.WorkDone;
        }

        // Initialize the slider
        weeklyQuotaSlider.maxValue = CalculateWeeklyQuota();
        weeklyQuotaSlider.value = workCompletion;

        // Start the work process
        StartCoroutine(UpdateWorkProgress());
    }

    IEnumerator UpdateWorkProgress()
    {
        while (workCompletion < weeklyQuotaSlider.maxValue)
        {
            float totalWorkDone = 0;

            for (int i = 0; i < workers.Count; i++)
            {
                var worker = workers[i];
                float workDone = CalculateWorkDone(worker);
                worker.WorkDone += workDone;
                totalWorkDone += workDone;
                individualTaskSliders[i].value = worker.WorkDone;
            }

            workCompletion += totalWorkDone;
            weeklyQuotaSlider.value = workCompletion;

            yield return new WaitForSeconds(1);
        }
    }

    float CalculateWorkDone(NPC worker)
    {
        return worker.WorkEfficiency * (1 + (worker.Mood / 20.0f));
    }

    float CalculateWeeklyQuota()
    {
        float weeklyQuota = 0;
        foreach (var worker in workers)
        {
            weeklyQuota += worker.TaskValue;
        }
        return weeklyQuota;
    }
}
