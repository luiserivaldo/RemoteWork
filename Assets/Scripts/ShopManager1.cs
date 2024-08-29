using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager1 : MonoBehaviour
{
    public TaskManager taskManager;
    public NPCGenerator npcGenerator;
    // Buy office upgrades buttons
    [Header("Office Upgrade Fields")]
    public Button cleanJunk; // Remove junk
    public Button upgradeSetup; // Upgrade from CardboardSetups to WorkerDesks 
    public Button buyPantry; // Pantry
    public Button buyPrinter; // PrinterSet
    public Button upgradeOfficeButton; // Upgrades office and switches scenes
    public GameObject junkGameObject; // Group of GameObjects for the first upgrade
    public GameObject cardboardDeskGameObject; 
    public GameObject deskSetupGameObject; // Group of GameObjects for the second upgrade
    public GameObject pantryGameObject; // Group of GameObjects for the second upgrade
    public GameObject printerGameObject; 

    private Dictionary<string, UpgradeSet> upgradeSets = new Dictionary<string, UpgradeSet>
    {
        // SetName, Cost, MoodBonus, WorkBonus, isPurchased
        { "CleanJunk", new UpgradeSet("CleanJunk", 20000, 1, 0) },
        { "DeskSetup", new UpgradeSet("DeskSetup", 50000, 1, 0) },
        { "Pantry", new UpgradeSet("Pantry", 100000, 1, 0) },
        { "Printer", new UpgradeSet("Printer", 125000, 0, 1f) },
        { "UpgradeOffice", new UpgradeSet("UpgradeOffice", 250000, 0, 0)}
    };
    void Start()
    {   
        npcGenerator = FindObjectOfType<NPCGenerator>();
        taskManager = FindObjectOfType<TaskManager>();

        cleanJunk.onClick.AddListener(() => TryPurchaseUpgrade("CleanJunk"));
        upgradeSetup.onClick.AddListener(() => TryPurchaseUpgrade("DeskSetup"));
        buyPantry.onClick.AddListener(() => TryPurchaseUpgrade("Pantry"));
        buyPrinter.onClick.AddListener(() => TryPurchaseUpgrade("Printer"));
        upgradeOfficeButton.onClick.AddListener(() => TryPurchaseUpgrade("UpgradeOffice"));

    }
    void Update()
    {
        UpdateButtonInteractble(cleanJunk, taskManager.currentBudget >= upgradeSets["CleanJunk"].Cost && !upgradeSets["CleanJunk"].IsPurchased);
        UpdateButtonInteractble(buyPantry, taskManager.currentBudget >= upgradeSets["Pantry"].Cost && !upgradeSets["Pantry"].IsPurchased && upgradeSets["CleanJunk"].IsPurchased);
        UpdateButtonInteractble(upgradeSetup, taskManager.currentBudget >= upgradeSets["DeskSetup"].Cost && !upgradeSets["DeskSetup"].IsPurchased && upgradeSets["CleanJunk"].IsPurchased);
        UpdateButtonInteractble(buyPrinter, taskManager.currentBudget >= upgradeSets["Printer"].Cost && !upgradeSets["Printer"].IsPurchased && upgradeSets["CleanJunk"].IsPurchased);
        UpdateButtonInteractble(upgradeOfficeButton, taskManager.currentBudget >= upgradeSets["UpgradeOffice"].Cost && !upgradeSets["UpgradeOffice"].IsPurchased && upgradeSets["CleanJunk"].IsPurchased);
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
            case "DeskSetup":
                cardboardDeskGameObject.SetActive(false);
                deskSetupGameObject.SetActive(true);
                break;
            case "CleanJunk":
                junkGameObject.SetActive(false);
                break;
            case "Printer":
                printerGameObject.SetActive(true);
                break;
            case "UpgradeOffice":
                SwitchScene();
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
    private void SwitchScene()
    {
        SceneManager.LoadScene("OfficeMap2");
    }
}

