using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public TaskManager taskManager;
    // Buy office upgrades buttons
    [Header("Office Upgrade Fields")]
    public Button buyPantry; // Pantry
    public Button buyLounge; // Lounge
    public Button buyLibrary; // LibraryCorner
    public GameObject pantryGameObject; // Group of GameObjects for the first upgrade
    public GameObject loungeGameObject; // Group of GameObjects for the second upgrade
    public GameObject libraryGameObject; // Group of GameObjects for the second upgrade

    private Dictionary<string, UpgradeSet> upgradeSets = new Dictionary<string, UpgradeSet>
    {
        // SetName, Cost, MoodBonus, WorkBonus, isPurchased
        { "Pantry", new UpgradeSet("Pantry", 100000, 1, 0) },
        { "Lounge", new UpgradeSet("Lounge", 150000, 1, 0) },
        { "Library", new UpgradeSet("Library", 125000, 0, 2) }
    };
    void Start()
    {
        taskManager = FindObjectOfType<TaskManager>();

        buyPantry.onClick.AddListener(() => TryPurchaseUpgrade("Pantry"));
        buyLounge.onClick.AddListener(() => TryPurchaseUpgrade("Lounge"));
        buyLibrary.onClick.AddListener(() => TryPurchaseUpgrade("Library"));

    }
    void Update()
    {
        UpdateButtonInteractble(buyPantry, taskManager.currentBudget >= upgradeSets["Pantry"].Cost && !upgradeSets["Pantry"].IsPurchased);
        UpdateButtonInteractble(buyLounge, taskManager.currentBudget >= upgradeSets["Lounge"].Cost && !upgradeSets["Lounge"].IsPurchased);
        UpdateButtonInteractble(buyLibrary, taskManager.currentBudget >= upgradeSets["Library"].Cost && !upgradeSets["Library"].IsPurchased);
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
        }
    }
}
