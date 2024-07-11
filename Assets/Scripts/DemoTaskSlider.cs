using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DemoTaskSlider : MonoBehaviour
{
    public Slider taskProgressSlider; // Weekly Slider UI GameObject
    public float totalDuration  = 60f; // Duration in seconds to fill the slider
    public float incrementInterval = 2f;  // Interval in seconds for each increment
    public Slider npcSliders; 
    public float totalDurationNPC = 15f;
    public float incrementIntervalNPC = 0.5f;

    void Start()
    {
        // Initialize slider values
        taskProgressSlider.minValue = 0;
        taskProgressSlider.maxValue = 1000; // Set the maximum value of the slider
        taskProgressSlider.value = 0;

        
        // Start the coroutine to fill the slider
        StartCoroutine(FillWeeklySliderInIncrements());
        StartCoroutine(FillNPCSliderInIncrements());
    }

    IEnumerator FillWeeklySliderInIncrements()
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

    IEnumerator FillNPCSliderInIncrements()
    {
        int numIncrements = (int)(totalDurationNPC / incrementIntervalNPC);
        float incrementValue = npcSliders.maxValue / numIncrements;

        for (int i = 0; i < numIncrements; i++)
        {
            npcSliders.value += incrementValue;
            yield return new WaitForSeconds(incrementIntervalNPC);  // Wait for 2 seconds before the next increment
        }

        // Ensure the slider value is maxed out at the end
        npcSliders.value = npcSliders.maxValue;
    }
}
