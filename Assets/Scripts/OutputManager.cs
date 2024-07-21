using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OutputManager : MonoBehaviour
{
    
    public NPCGenerator npcGenerator;
    public int npcIdSelect; // Specify the ID of the NPC you want to display [REMOVE WITH PROPER NPC SELECT LOGIC]
    /* PSEUDOCODE
    refer gameobjects for output
    [
        //WorkerDetails
        public Text IdOutput;
        public Text NameOutput;
        public Text AgeOutput;

        //MainInfobar 
        public Text MainIdOutput;
        public Text MainNameOutput;
        public Text MainAgeOutput;

    ]
    retrieve variables from NPCGenerator
    output each variable individually
    assign gameobject/ui objects to each variable

    mainBarOutput {
        check for isSelected = true
        output those values to main 
    }
    */

    // Text Output Fields: Reference to game objects in MainUI or WorkerDetails
    public TMP_Text allNPCOutput; // Output all NPCs in dictionary
    public Text npcNameOutput; // Name
    
    public Text npcSalaryOutput; // Salary
    public Slider npcMoodSlider; //Mood slider game object reference
    //public TMP_Text TaskCapacityOutput; // Current assigned maximum task value
    //public Slider npcWorkDoneOutput; // Work currently completed

    // *USE AS NECESSARY*
    //public Text npcAgeOutput; // Age
    //public Text npcWorkEfficiencyOutput; // Work Efficiency
    //public Text npcMoodOutput; // Mood

    void Start()
    {
        DisplayNPCInfo();
        //DisplaySpecificNPC();
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

    public void DisplaySpecificNPC()
    {
        if (npcGenerator.npcList.TryGetValue(npcIdSelect, out NPC npc))
        {
            // ID found, display its information
            string npcInfo = npc.ToString();  // check NPCGenerator for ToString output
            allNPCOutput.text = npcInfo;
            npcNameOutput.text = npc.Name;
            npcSalaryOutput.text = $"$ {npc.Salary}";
            npcMoodSlider.value = npc.Mood;
            //Debug.Log("Displaying info for NPC ID 3: " + npcInfo);
        }
        else
        {
            // ID not found, display error message
            allNPCOutput.text = "NPC with ID 3 not found.";
            //Debug.Log($"NPC with ID (" + $"{npcIdSelect}" + ") not found.");
        }
    }
}
