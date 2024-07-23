using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OutputManager : MonoBehaviour
{
    public Slider workDoneSlider;
    public Text npcNameOutput; // Name
    public Text npcSalaryOutput; // Salary
    public Slider npcMoodSlider; // Mood slider
    public Text selectedNPCOutput; // Detailed info
    public TextMeshProUGUI displayAllNPCsOutput; // Debug: Display all NPCs' information

    public Button showNPCDetailsButton;

    private NPCGenerator npcGenerator;
    private TaskManager taskManager;
    private NPC selectedNPC;

    void Start()
    {
        npcGenerator = FindObjectOfType<NPCGenerator>();
        taskManager = FindObjectOfType<TaskManager>(); // Add this line
        showNPCDetailsButton.onClick.AddListener(DisplayNPCOnButton);

        StartCoroutine(UpdateOutputCoroutine());
    }

    void Update()
    {
        UpdateAllNPCInfo();
        UpdateSelectedNPCInfo(); // Call this method every frame to update selected NPC info without delay
    }

    private IEnumerator UpdateOutputCoroutine()
    {
        while (true)
        {
            UpdateSelectedNPCSliders();
            yield return new WaitForSeconds(0);
        }
    }

    private void UpdateSelectedNPCSliders()
    {
        if (selectedNPC != null)
        {
            npcMoodSlider.value = selectedNPC.Mood;
            workDoneSlider.maxValue = selectedNPC.MaxTaskCapacity;
            workDoneSlider.value = selectedNPC.TotalWorkDone;
        }
    }

    private void UpdateSelectedNPCInfo()
    {
        if (selectedNPC != null)
        {
            npcNameOutput.text = selectedNPC.Name;
            npcSalaryOutput.text = $"$ {selectedNPC.Salary}";
            npcMoodSlider.value = selectedNPC.Mood;
            workDoneSlider.value = selectedNPC.TotalWorkDone;
            selectedNPCOutput.text = selectedNPC.ToString();
        }
        else
        {
            selectedNPCOutput.text = "No NPC is currently selected.";
        }
    }
    public void DisplaySpecificNPC(NPC npc)
    {
        selectedNPC = npc;
        if (taskManager != null)
        {
            taskManager.SetSelectedNPC(npc); // Ensure TaskManager knows about the selected NPC
        }
        UpdateSelectedNPCInfo(); // Immediately update UI when NPC is selected
    }

    public void UpdateAllNPCInfo()
    {
        string allNPCsText = "";
        foreach (var npc in npcGenerator.npcList.Values)
        {
            allNPCsText += npc.ToString() + "\n\n"; // Append each NPC's info and add a newline for spacing
        }
        displayAllNPCsOutput.text = allNPCsText; // Display all NPCs' information in the text component
    }

    private void DisplayNPCOnButton()
    {
        // Implement the logic to display NPC details on button click
    }

    public void SetSelectedNPC(NPC npc)
    {
        selectedNPC = npc;
        DisplaySpecificNPC(npc);
    }
}
