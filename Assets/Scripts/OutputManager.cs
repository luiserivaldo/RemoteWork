using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OutputManager : MonoBehaviour
{
    
    public NPCGenerator npcGenerator;
    public int npcIdSelect; // Specify the ID of the NPC you want to display [REMOVE WITH PROPER NPC SELECT LOGIC]

    // Text Output Fields: Reference to game objects in MainUI or WorkerDetails
    public TMP_Text allNPCOutput; // Output all NPCs in dictionary
    public Text npcNameOutput; // Name
    
    public Text npcSalaryOutput; // Salary
    public Slider npcMoodSlider; //Mood slider game object reference
    public int taskMaxValue = 100;
    public Slider npcWorkDoneSlider;
    //public TMP_Text TaskCapacityOutput; // Current assigned maximum task value
    //public Slider npcWorkDoneOutput; // Work currently completed

    // *USE AS NECESSARY*
    //public Text npcAgeOutput; // Age
    //public Text npcWorkEfficiencyOutput; // Work Efficiency
    //public Text npcMoodOutput; // Mood

    public Button showNPCDetailsButton;
    public Text selectedNPCOutput;

    void Start()
    {
        npcWorkDoneSlider.maxValue = taskMaxValue;
        DisplayNPCInfo();
        //DisplaySpecificNPC();
        showNPCDetailsButton.onClick.AddListener(DisplayNPCOnButton); // Debug Generate
        StartCoroutine(UpdateOutput());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public TMP_Text DisplayNPCInfo()
    {
        /* npcNameOutput[index].text = $"{npc.Name}";
        //npcAgeOutput.text = $"{npc.Age}";
        //npcWorkEfficiencyOutput.text = $"{npc.WorkEfficiency}";
        npcSalaryOutput.text = $"<color=green>{npc.Salary}</color> / month";
        //npcMoodOutput.text = $"{npc.Mood}";
        npcMoodSlider.value = npc.Mood;
        npcInfoOutput.text = $"Name: {npc.Name}\nAge: {npc.Age}\nWork Efficiency: {npc.WorkEfficiency}\nSalary: {npc.Salary}\nMood: {npc.Mood}";
        //Debug.Log("Name: " + npc.Name + "\nAge: " + npc.Age); */

        string allNPCsText = "";
        foreach (var i in npcGenerator.npcList.Values)
        {
            allNPCsText += i.ToString() + "\n\n"; // Append each NPC's info and add a newline for spacing
        }
        allNPCOutput.text = allNPCsText; // Display all NPCs' information in the text component
        return allNPCOutput;
    }
    public void DisplaySpecificNPC(NPC selectedNPC)
    {
        if (selectedNPC != null && selectedNPC.IsSelected)
        {
            string npcInfo = selectedNPC.ToString();
            selectedNPCOutput.text = npcInfo;
            npcNameOutput.text = selectedNPC.Name;
            npcSalaryOutput.text = $"$ {selectedNPC.Salary}";
            npcMoodSlider.value = selectedNPC.Mood;
            npcWorkDoneSlider.value = selectedNPC.WorkDone;
        }
        else
        {
            selectedNPCOutput.text = "No NPC is currently selected.";
        }
    }
    private void DisplayNPCOnButton()
    {
        
    }
    private IEnumerator UpdateOutput()
    {
        while (true)
        {
            foreach (var npc in npcGenerator.npcList.Values)
            {
                // Update the work done slider for each NPC
                UpdateWorkDoneSlider(npc);
            }
            DisplayNPCInfo();
            yield return new WaitForSeconds(1);
        }
    }
    private void UpdateWorkDoneSlider(NPC npc)
    {
        if (npc.IsSelected)
        {
            npcWorkDoneSlider.value = npc.WorkDone;
        }
    }
}
