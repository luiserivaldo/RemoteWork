using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OutputManager : MonoBehaviour
{
    public Slider workDoneSlider; // Task progress slider
    public Text npcNameOutput; // Name
    public Text npcSalaryOutput; // Salary
    public Slider npcMoodSlider; // Mood slider
    public Text selectedNPCOutput; // Detailed info
    public TextMeshProUGUI allNPCOutput;

    public Button showNPCDetailsButton;

    private NPCGenerator npcGenerator;
    private NPC selectedNPC;

    void Start()
    {
        npcGenerator = FindObjectOfType<NPCGenerator>();
        showNPCDetailsButton.onClick.AddListener(DisplayNPCOnButton);
        DisplayAllNPCInfo();
        StartCoroutine(UpdateOutput());
    }

    void Update()
    {
        DisplayAllNPCInfo();
    }
    private IEnumerator UpdateOutput()
    {
        while (true)
        {
            UpdateNPCSliders();
            yield return new WaitForSeconds(0);
        }
    }

    public void DisplaySpecificNPC(NPC npc)
    {
        selectedNPC = npc;
        if (npc != null)
        {
            npcNameOutput.text = npc.Name;
            npcSalaryOutput.text = $"$ {npc.Salary}";
            npcMoodSlider.value = npc.Mood;
            workDoneSlider.value = npc.TotalWorkDone;
            selectedNPCOutput.text = npc.ToString();
            UpdateNPCSliders();
        }
        else
        {
            selectedNPCOutput.text = "No NPC is currently selected.";
        }
    }
    public void DisplayAllNPCInfo()
    {
        string allNPCsText = "";
        foreach (var npc in npcGenerator.npcList.Values)
        {
            allNPCsText += npc.ToString() + "\n\n"; // Append each NPC's info and add a newline for spacing
        }
        allNPCOutput.text = allNPCsText; // Display all NPCs' information in the text component
    }
    private void DisplayNPCOnButton()
    {
        
    }

    public void SetSelectedNPC(NPC npc)
    {
        selectedNPC = npc;
        DisplaySpecificNPC(npc);
    }
    private void UpdateNPCSliders()
    {
        if (selectedNPC != null)
        {
            workDoneSlider.maxValue = selectedNPC.MaxTaskCapacity;
            npcMoodSlider.value = selectedNPC.Mood;
            workDoneSlider.value = selectedNPC.TotalWorkDone;
        }
    }
}
