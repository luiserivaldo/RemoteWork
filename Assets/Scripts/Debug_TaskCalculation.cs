using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskCompletionCalculator : MonoBehaviour
{
    // Input field references
    // public TMP_InputField workEfficiencyInput;
    // public TMP_InputField moodInput;
    // public TMP_InputField taskCapacityInput;

    // Slider references
    public Slider workEfficiencySlider;
    public Slider moodSlider;
    public Slider taskCapacitySlider;

    // Button reference
    public Button calculateButton;
    
    // Output text reference
    public Text resultText;

    // Constants
    private const int minutesPerDay = 480; // 8 hours workday converted to minutes
    private const int minutesPerInterval = 5; // Game calculation time scaling to real-time

    private void Start()
    {
        // Set up the OnClick listener for the calculate button
        calculateButton.onClick.AddListener(OnCalculateButtonClicked);
    }

    // Method to be called when the Calculate button is clicked
    public void OnCalculateButtonClicked()
    {
        // // Parse input values from InputFields
        // float workEfficiency = float.Parse(workEfficiencyInput.text);
        // float mood = float.Parse(moodInput.text);
        // int taskCapacity = int.Parse(taskCapacityInput.text);

         // Get values from sliders
        float workEfficiency = workEfficiencySlider.value;
        float mood = moodSlider.value;
        int taskCapacity = (int)taskCapacitySlider.value; // Whole numbers only (int)

        // Calculate time to complete the task
        float timeToCompleteTask = CalculateTimeToCompleteTask(workEfficiency, mood, taskCapacity);

        // Display the result
        resultText.text = timeToCompleteTask.ToString("N2") + " days";

        // Debug.Log("Time to complete task: " + timeToCompleteTask + " days");
        Debug.Log("TaskCompletion Debug Button Clicked");
    }

    float CalculateTimeToCompleteTask(float workEfficiency, float mood, int taskCapacity)
    {
        // Calculate mood multiplier
        float moodMultiplier = 1 + (mood / 20.0f);

        // Calculate effective work points per interval
        float workDone = workEfficiency * moodMultiplier;

        // Calculate total work done per day
        float workDonePerDay = (minutesPerDay / minutesPerInterval) * workDone;

        // Calculate the number of days to complete the task
        float daysToCompleteTask = taskCapacity / workDonePerDay;

        return daysToCompleteTask;
    }
}