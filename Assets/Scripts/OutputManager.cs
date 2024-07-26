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
    public Slider npcWorkDoneSlider; // Task progress slider
    public Text npcNumOfTasksCompletedOutput;
    
    // Selected NPC to display to NPC info bar
    private NPC selectedNPC;
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
        }
    }

    private void UpdateSelectedNPCInfo()
    {
        if (selectedNPC != null)
        {
            npcNameOutput.text = selectedNPC.Name;
            npcSalaryOutput.text = $"$ {selectedNPC.Salary.ToString("N0")} / month";
            //npcCurrentActivity.text = selectedNPC.CurrentActivity;
            npcWorkPerIncrement.text = selectedNPC.WorkDonePerIncrement.ToString("N2");
            npcCurrentWorkArrangement.text = selectedNPC.CurrentWorkArrangement;
            npcMoodSlider.value = selectedNPC.Mood;
            npcWorkDoneSlider.value = selectedNPC.TotalWorkDone;
            selectedNPCOutput.text = NPCToString(selectedNPC);
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
        selectedNPC = npc;
        DisplaySpecificNPC(npc);
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
    private string NPCToString(NPC npc)
    {
        return $"isSelected: {npc.IsSelected}\nID: {npc.NPCId}\nName: {npc.Name}\nAge: {npc.Age}\nWork Efficiency: {npc.WorkEfficiency}\nSalary: {npc.Salary}\nMood: {npc.Mood}\nCurrent activity: {npc.CurrentActivity}\nWork per Increment: {npc.WorkDonePerIncrement}\nTotal Work Done: {npc.TotalWorkDone}\nWork Location: {npc.CurrentWorkArrangement}";
    }
}
