using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    private TaskManager taskManager;
    private NPC selectedNPC;
    public Button enquireButton;
    public Button praiseButton;

    // Buy office upgrades buttons
    public Button buyUpgrade1;
    public Button buyUpgrade2;
    public GameObject upgradeSet1; // Group of GameObjects for the first upgrade
    public GameObject upgradeSet2; // Group of GameObjects for the second upgrade
    public int upgradeCost1 = 50000; // Cost for the first upgrade
    public int upgradeCost2 = 100000; // Cost for the second upgrade
    
    void Start()
    {
        taskManager = FindObjectOfType<TaskManager>();

        enquireButton.onClick.AddListener(OnEnquireButtonClick);
        praiseButton.onClick.AddListener(OnPraiseButtonClick);
        buyUpgrade1.onClick.AddListener(() => TryPurchaseUpgrade(upgradeSet1, upgradeCost1));
        buyUpgrade2.onClick.AddListener(() => TryPurchaseUpgrade(upgradeSet2, upgradeCost2));

    }
    public void SetSelectedNPC(NPC npc) // Set clicked NPC as the selected NPC for logic functions
    {
        selectedNPC = npc;
    }
    
    public void OnEnquireButtonClick()
    {
        if (selectedNPC != null)
        {
            selectedNPC.Mood -= 1; // Decrease mood by 1
            if (selectedNPC.Mood < -5)
            {
                selectedNPC.Mood = -5; // Ensure mood doesn't go below -5
            } 
        }
        else
        {
            Debug.LogError("No NPC selected. Cannot enquire.");
        }
    }
    public void OnPraiseButtonClick()
    {
        if (selectedNPC != null)
        {
            selectedNPC.Mood += 1; // Increase mood by 1
            if (selectedNPC.Mood > 5)
            {
                selectedNPC.Mood = 5; // Increase mood doesn't go below -5
            } 
            /* Debug.Log("Praise clicked");
            Debug.Log($"Praise NPC ID {selectedNPC.NPCId}. Mood: {selectedNPC.Mood}"); */
        }
        else
        {
            Debug.LogError("No NPC selected. Cannot praise.");
        }
    }
    public bool PurchaseUpgrade(int cost)
    {
        if (taskManager.currentBudget >= cost)
        {
            taskManager.currentBudget -= cost;
            return true;
        }
        return false;
    }
    private void TryPurchaseUpgrade(GameObject upgradeGroup, int cost)
    {
        if (PurchaseUpgrade(cost))
        {
            ToggleUpgrade(upgradeGroup);
        }
    }
    
    private void ToggleUpgrade(GameObject upgradeGroup)
    {
        upgradeGroup.SetActive(upgradeGroup.activeSelf);
    }
}
