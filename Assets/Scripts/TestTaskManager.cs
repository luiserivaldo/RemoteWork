using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public Slider taskSlider;
    public float workCompletion;
    public float taskGoal = 1000.0f; // Example goal, adjust as necessary
    private List<NPC> workers = new List<NPC>();
    public SimpleNPCGenerator simpleNpcGenerator; // Reference to the NPC generator

    void Start()
    {
        // Generate 6 random workers
        for (int i = 0; i < 6; i++)
        {
            workers.Add(simpleNpcGenerator.GenerateRandomNPC());
        }

        // Initialize the slider
        taskSlider.maxValue = taskGoal;
        taskSlider.value = workCompletion;

        // Start the work process
        StartCoroutine(UpdateWorkProgress());
    }

    IEnumerator UpdateWorkProgress()
    {
        while (workCompletion < taskGoal)
        {
            float totalWorkDone = 0;

            foreach (var worker in workers)
            {
                totalWorkDone += CalculateWorkDone(worker);
            }

            workCompletion += totalWorkDone;
            taskSlider.value = workCompletion;

            yield return new WaitForSeconds(1);
        }
    }

    float CalculateWorkDone(NPC worker)
    {
        return worker.WorkEfficiency * (1 + (worker.Mood / 20.0f));
    }
}
