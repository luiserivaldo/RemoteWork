using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public TaskManager taskManager;
    public NPCGenerator npcGenerator;
    // Buy office upgrades buttons
    [Header("Office Upgrade Fields")]
    public Button buyPantryButton; // Pantry button
    public Button buyLoungeButton; // Lounge button
    public Button buyLibraryButton; // LibraryCorner button
    public Button upgradeSetupButton; // Change DeskSetupPoor to Cubicle
    public GameObject pantryGameObject; // Pantry group of gameobjects
    public GameObject loungeGameObject; // Lounge gameobjects
    public GameObject libraryGameObject; // Group of GameObjects for the second upgrade
    public GameObject deskSetupPoorGameObject; // Initial desk to be replaced
    public GameObject cubicleGameObject; // New desks to replace poor desks

    private Dictionary<string, UpgradeSet> upgradeSets = new Dictionary<string, UpgradeSet>
    {
        // SetName, Cost, MoodBonus, WorkBonus, isPurchased
        { "Pantry", new UpgradeSet("Pantry", 100000, 1, 0) },
        { "Lounge", new UpgradeSet("Lounge", 150000, 1, 0) },
        { "Library", new UpgradeSet("Library", 125000, 1, 0.5f) },
        { "UpgradeSetup", new UpgradeSet("Library", 250000, 0, 1f) }
    };
    void Start()
    {   
        npcGenerator = FindObjectOfType<NPCGenerator>();
        taskManager = FindObjectOfType<TaskManager>();

        buyPantryButton.onClick.AddListener(() => TryPurchaseUpgrade("Pantry"));
        buyLoungeButton.onClick.AddListener(() => TryPurchaseUpgrade("Lounge"));
        buyLibraryButton.onClick.AddListener(() => TryPurchaseUpgrade("Library"));
        upgradeSetupButton.onClick.AddListener(() => TryPurchaseUpgrade("UpgradeSetup"));

    }
    void Update()
    {
        UpdateButtonInteractble(buyPantryButton, taskManager.currentBudget >= upgradeSets["Pantry"].Cost && !upgradeSets["Pantry"].IsPurchased);
        UpdateButtonInteractble(buyLoungeButton, taskManager.currentBudget >= upgradeSets["Lounge"].Cost && !upgradeSets["Lounge"].IsPurchased);
        UpdateButtonInteractble(buyLibraryButton, taskManager.currentBudget >= upgradeSets["Library"].Cost && !upgradeSets["Library"].IsPurchased);
        UpdateButtonInteractble(upgradeSetupButton, taskManager.currentBudget >= upgradeSets["UpgradeSetup"].Cost && !upgradeSets["UpgradeSetup"].IsPurchased);
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
    
    private void TryPurchaseUpgrade(string upgradeName)
    {
        if (taskManager.currentBudget >= upgradeSets[upgradeName].Cost && !upgradeSets[upgradeName].IsPurchased)
        {
            taskManager.currentBudget -= upgradeSets[upgradeName].Cost;
            upgradeSets[upgradeName].IsPurchased = true;
            ApplyUpgradeEffects(upgradeName);
        }
    }

    private void ApplyUpgradeEffects(string upgradeName)
    {
        switch (upgradeName)
        {
            case "Pantry":
                pantryGameObject.SetActive(true);
                break;
            case "Lounge":
                loungeGameObject.SetActive(true);
                break;
            case "Library":
                libraryGameObject.SetActive(true);
                break;
            case "UpgradeSetup":
                deskSetupPoorGameObject.SetActive(false);
                cubicleGameObject.SetActive(true);
                break;
        }

        if (upgradeSets.ContainsKey(upgradeName))
        {
            var upgradeSet = upgradeSets[upgradeName];
            ApplyBonuses(upgradeSet.MoodBonus, upgradeSet.WorkBonus);
        }
    }
    private void ApplyBonuses(int moodBonus, float workBonus)
    {
        foreach (var npc in npcGenerator.npcList.Values)
        {
            if (npc.CurrentWorkArrangement == "On-site"){
                npc.Mood += moodBonus;
                npc.WorkEfficiency += workBonus;
            }
        }
    }
}

