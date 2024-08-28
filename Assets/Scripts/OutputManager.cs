using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OutputManager : MonoBehaviour
{  
    // Reference to Game Managers
    [Header("Reference to Game Managers")]
    private NPCGenerator npcGenerator;
    private TaskManager taskManager;
    private ActionManager actionManager;

    // Game Info Bar
    [Header("Game Info Bar Fields")]
    public Text currentDate;
    public Text NumOfTasksCompleted;
    public Text currentBudget;

    // NPC Info Bar
    [Header("NPC Info Bar Fields")]
    public Text npcNameOutput; // Name
    public Text npcSalaryOutput; // Salary
    //public Text npcCurrentActivity;
    public Text npcWorkPerIncrement;
    public Text npcCurrentWorkArrangement;
    public Slider npcMoodSlider; // Mood slider
    public int minMoodvalue = -10;
    public int maxMoodvalue = 10;
    public Slider npcWorkDoneSlider; // Task progress slider
    public Text npcNumOfTasksCompletedOutput;
    
    // Selected NPC to display to NPC info bar
    private NPC selectedNPC;
    // Select NPC Screen
    [Header("Select NPC Screen")]
    // Bio
    public Text selectedNpcNameOutput; // Name
    public Text selectednpcCurrentWorkArrangement;
    public Text selectedageOutput;
    public Text selectednpcSalaryOutput; // Salary

    // Work
    public Text selectedworkEfficiencyOutput;
    public Text selectedworkBonusOutput;
    public Text selectednpcWorkPerIncrement;
    public Slider selectednpcWorkDoneSlider; // Task progress slider
    public Text selectedcompletedTasksOutput;

    // Mood and Traits
    public Slider selectednpcMoodSlider; // Mood slider
    public Text selectedmoodValueOutput;
    public Text selectedmoodBonusOutput;
    public Text selectedtraitsOutput;

    // Purchase screen
    [Header("Purchase screen")]
    public Text currentBudgetPurchaseScreen;

    // Debug output
    [Header("Debug output")]
    public Text selectedNPCOutput; // Full info of selected NPC
    public TextMeshProUGUI displayAllNPCsOutput; // Display full list of NPCs information (debug)
    public GameObject SelectedUI; // GameObject for selected UI elements
    public GameObject NoSelectedUI; // GameObject for no selected UI elements

    void Start()
    {
        // Set min-max range of sliders
        npcMoodSlider.minValue = minMoodvalue;
        npcMoodSlider.maxValue = maxMoodvalue;

        // Find related Game Managers
        npcGenerator = FindObjectOfType<NPCGenerator>();
        taskManager = FindObjectOfType<TaskManager>(); 
        actionManager = FindObjectOfType<ActionManager>();

        // Start live updating components
        StartCoroutine(UpdateOutputCoroutine());
        npcGenerator.OnNPCsGenerated += OnNPCsGenerated; // Subscribe to the event
    }

    void Update() // Updates per frame
    {
        UpdateAllNPCInfo(); // Debug output to display all NPCs in the dictionary
        UpdateSelectedNPCInfo(); // Update selected NPC info
        UpdateSelectedUIState(); // Switch between NoSelectedNPCs and SelectedNPCs UI
        UpdateGameInfoBar();
        currentBudgetPurchaseScreen.text = $"{(taskManager.currentBudget/1000).ToString("N0")}K";
        
    }
    void OnDestroy()
    {
        npcGenerator.OnNPCsGenerated -= OnNPCsGenerated; // Unsubscribe from the event; reset isSelected state to switch to NoSelectedUI
    }

    private void UpdateGameInfoBar() // Change slider values by taking from NPC data
    {
        currentDate.text = $"M0 / W{taskManager.weeksPassed} / D0";
        NumOfTasksCompleted.text = $"{taskManager.numOfTotalTasksCompleted} / 15";
        currentBudget.text = $"{(taskManager.currentBudget/1000).ToString("N0")}K";
    }
    private IEnumerator UpdateOutputCoroutine()
    {
        while (true)
        {
            UpdateSelectedNPCSliders();
            yield return new WaitForSeconds(0);
        }
    }

    private void UpdateSelectedNPCSliders() // Change slider values by taking from NPC data
    {
        if (selectedNPC != null)
        {
            npcMoodSlider.value = selectedNPC.Mood;
            npcWorkDoneSlider.maxValue = selectedNPC.MaxTaskCapacity;
            npcWorkDoneSlider.value = selectedNPC.TotalWorkDone;

            selectednpcWorkDoneSlider.maxValue = selectedNPC.MaxTaskCapacity;
            selectednpcWorkDoneSlider.value = selectedNPC.TotalWorkDone;
        }
    }

    private void UpdateSelectedNPCInfo()
    {
        if (selectedNPC != null)
        {
            // Active info bar
            npcNameOutput.text = selectedNPC.Name;
            npcSalaryOutput.text = $"$ {selectedNPC.Salary.ToString("N0")} / month";
            //npcCurrentActivity.text = selectedNPC.CurrentActivity;
            WorkIncrementText(); // Both info bar and selected NPC Screen 
            npcCurrentWorkArrangement.text = selectedNPC.CurrentWorkArrangement;
            npcMoodSlider.value = selectedNPC.Mood;
            npcWorkDoneSlider.value = selectedNPC.TotalWorkDone;

            // Selected NPC Screen
            selectedNpcNameOutput.text = selectedNPC.Name;
            selectednpcSalaryOutput.text = $"$ {selectedNPC.Salary.ToString("N0")} / month";
            SeniorityLevelText(); // Age/Seniority level
            //WorkIncrementText();
            selectednpcCurrentWorkArrangement.text = selectedNPC.CurrentWorkArrangement;
            selectednpcMoodSlider.value = selectedNPC.Mood;
            selectedmoodBonusOutput.text = selectedNPC.MoodBonus.ToString();
            selectedmoodValueOutput.text = selectedNPC.Mood.ToString();
            selectedworkEfficiencyOutput.text = selectedNPC.WorkEfficiency.ToString();
            selectednpcWorkDoneSlider.value = selectedNPC.TotalWorkDone;
            selectedworkBonusOutput.text = selectedNPC.WorkEfficiencyBonus.ToString();
            selectedcompletedTasksOutput.text = selectedNPC.numOfTasksCompleted.ToString();
            selectedNPCOutput.text = NPCToString(selectedNPC);
            selectedtraitsOutput.text = $"{selectedNPC.IsDisabled}, {selectedNPC.SocialPref}";

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
            actionManager.SetSelectedNPC(npc); // Ensure TaskManager knows about the selected NPC
        }
        UpdateSelectedNPCInfo(); // Update UI when NPC is selected
    }

    public void UpdateAllNPCInfo()
    {
        string allNPCsText = "";
        foreach (var npc in npcGenerator.npcList.Values)
        {
            allNPCsText += NPCToString(npc) + "\n\n"; // Append each NPC's info and add a newline for spacing
        }
        displayAllNPCsOutput.text = allNPCsText; // Display all NPCs' information in the text component
    }
     private void OnNPCsGenerated()
    {
        selectedNPC = null; // Clear the selected NPC
        UpdateSelectedNPCInfo(); // Update UI to reflect no NPC is selected
    }
    public void SetSelectedNPC(NPC npc)
    {
        selectedNPC = npc; // Set the selected NPC
        DisplaySpecificNPC(npc); // Display details of the selected NPC
    }
    private void UpdateSelectedUIState()
    {
        bool isAnyNPCSelected = false;
        foreach (var npc in npcGenerator.npcList.Values)
        {
            if (npc.IsSelected)
            {
                isAnyNPCSelected = true;
                break;
            }
        }

        if (isAnyNPCSelected)
        {
            SelectedUI.SetActive(true);
            NoSelectedUI.SetActive(false);
        }
        else
        {
            SelectedUI.SetActive(false);
            NoSelectedUI.SetActive(true);
        }
    }
    public void WorkIncrementText()
    {
        switch (selectedNPC.WorkDonePerIncrement)
        {
            case <= 2.5f:
                npcWorkPerIncrement.color = Color.red;
                break;
            case <= 4f:
                npcWorkPerIncrement.color = new Color(1.0f, 0.64f, 0.0f); // Orange 
                break;
            case <= 7.5f:
                npcWorkPerIncrement.color = new Color(0.082f, 0.812f, 0.216f); // Dark Green
                break;
            case >7.5f: // Exceeds value expected
                npcWorkPerIncrement.color = Color.gray;
                break;
        }
        npcWorkPerIncrement.text = selectedNPC.WorkDonePerIncrement.ToString("N1");
        selectednpcWorkPerIncrement.text = selectedNPC.WorkDonePerIncrement.ToString("N1");
    }
    public void SeniorityLevelText()
    {
        switch (selectedNPC.Age)
        {
            case <= 25:
                selectedageOutput.text = "Junior";
                break;
            case <= 40:
                selectedageOutput.text = "Mid-level";
                break;
            case <= 60:
                selectedageOutput.text = "Senior";
                break;
            case >60: // Exceeds value expected
                selectedageOutput.text = "Senior";
                break;
        }
        //selectednpcWorkPerIncrement.text = selectedNPC.Age.ToString();
    }
    
    private string NPCToString(NPC npc)
    {
        return $"isSelected: {npc.IsSelected}\nID: {npc.NPCId}\nName: {npc.Name}\nAge: {npc.Age}\nWork Efficiency: {npc.WorkEfficiency}\nSalary: {npc.Salary}\nMood: {npc.Mood}\nCurrent activity: {npc.CurrentActivity}\nWork per Increment: {npc.WorkDonePerIncrement}\nTotal Work Done: {npc.TotalWorkDone}\nWork Location: {npc.CurrentWorkArrangement} \nTotal Salaries: {taskManager.totalSalary}";
    }
}
