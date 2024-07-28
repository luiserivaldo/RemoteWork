using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{

    // Reference to Game Managers
    [Header("Reference to Game Managers")]
    private TaskManager taskManager;
    
    // NPC Action Bar Buttons
    [Header("NPC Action Bar Buttons")]
    public Button enquireButton;
    public Button praiseButton;

    // Buy office upgrades buttons
    [Header("Office Upgrade Fields")]
    public Button buyUpgrade1; // Pantry
    public Button buyUpgrade2; // Lounge
    public Button buyUpgrade3; // LibraryCorner
    public Button buyUpgrade4; // LibraryBooks
    public GameObject upgradeSet1; // Group of GameObjects for the first upgrade
    public GameObject upgradeSet2; // Group of GameObjects for the second upgrade
    public GameObject upgradeSet3; // Group of GameObjects for the second upgrade
    public GameObject upgradeSet4; // Group of GameObjects for the second upgrade
    public int upgradeCost1 = 100000; // Cost for the first upgrade
    public int upgradeCost2 = 150000; // Cost for the second upgrade
    public int upgradeCost3= 75000;
    public int upgradeCost4= 50000; 
    private bool upgrade1Purchased = false;
    private bool upgrade2Purchased = false;
    private bool upgrade3Purchased = false;
    private bool upgrade4Purchased = false;

    // To identify currently selected NPC
    private NPC selectedNPC;
    
    void Start()
    {
        taskManager = FindObjectOfType<TaskManager>();

        enquireButton.onClick.AddListener(OnEnquireButtonClick);
        praiseButton.onClick.AddListener(OnPraiseButtonClick);
        buyUpgrade1.onClick.AddListener(() => TryPurchaseUpgrade(ref upgrade1Purchased, upgradeSet1, upgradeCost1));
        buyUpgrade2.onClick.AddListener(() => TryPurchaseUpgrade(ref upgrade2Purchased, upgradeSet2, upgradeCost2));
        buyUpgrade3.onClick.AddListener(() => TryPurchaseUpgrade(ref upgrade3Purchased, upgradeSet3, upgradeCost3, true));
        buyUpgrade4.onClick.AddListener(() => TryPurchaseUpgrade(ref upgrade4Purchased, upgradeSet4, upgradeCost4));

    }
    void Update()
    {
        UpdateButtonInteractble(buyUpgrade1, taskManager.currentBudget >= upgradeCost1);
        UpdateButtonInteractble(buyUpgrade2, taskManager.currentBudget >= upgradeCost2);
        UpdateButtonInteractble(buyUpgrade3, taskManager.currentBudget >= upgradeCost3);
        UpdateButtonInteractble(buyUpgrade4, upgrade3Purchased && taskManager.currentBudget >= upgradeCost4);
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
    private void UpdateButtonInteractble(Button button, bool isInteractable)
    {
        button.interactable = isInteractable;
        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.color = isInteractable ? Color.black : Color.red;
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
    private void TryPurchaseUpgrade(ref bool upgradePurchased, GameObject upgradeGroup, int cost, bool isUpgrade3 = false)
    {
        if (!upgradePurchased && PurchaseUpgrade(cost))
        {
            ToggleUpgrade(upgradeGroup);
            upgradePurchased = true;
            if (isUpgrade3)
            {
                upgrade3Purchased = true;
            }
        }
    }
    
    private void ToggleUpgrade(GameObject upgradeGroup)
    {
        upgradeGroup.SetActive(true);
    }
}
