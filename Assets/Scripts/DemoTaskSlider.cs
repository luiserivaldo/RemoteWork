using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DemoTaskSlider : MonoBehaviour
{
    public Slider taskProgressSlider; // Reference to the UI slider
    public float totalDuration  = 60f; // Duration in seconds to fill the slider
    public float incrementInterval = 2f;  // Interval in seconds for each increment

    void Start()
    {
        if (taskProgressSlider == null)
        {
            Debug.LogError("Slider reference not set.");
            return;
        }

        // Initialize slider values
        taskProgressSlider.minValue = 0;
        taskProgressSlider.maxValue = 1000; // Set the maximum value of the slider
        taskProgressSlider.value = 0;

        // Start the coroutine to fill the slider
        StartCoroutine(FillSliderInIncrements());
    }

    IEnumerator FillSliderInIncrements()
    {
        int numIncrements = (int)(totalDuration / incrementInterval);
        float incrementValue = taskProgressSlider.maxValue / numIncrements;

        for (int i = 0; i < numIncrements; i++)
        {
            taskProgressSlider.value += incrementValue;
            yield return new WaitForSeconds(incrementInterval);  // Wait for 2 seconds before the next increment
        }

        // Ensure the slider value is maxed out at the end
        taskProgressSlider.value = taskProgressSlider.maxValue;
    }
}
