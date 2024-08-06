using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public GameObject npcInfoRowPrefab; // Reference to the NPCInfoRow prefab
    public Transform npcInfoGrid; // Reference to the grid layout to house the rows
    public NPCGenerator npcGenerator;
    private Dictionary<int, GameObject> npcRowDict = new Dictionary<int, GameObject>(); // Dictionary to keep track of NPC rows

    private void Start()
    {
        npcGenerator.OnNPCsGenerated += UpdateUI; // Subscribe to the event

        // Delete temp rows
        foreach (Transform child in npcInfoGrid)
        {
            Destroy(child.gameObject);
            Debug.Log("Deleted temp rows");
        }

        // Initial UI update
        UpdateUI();
    }
    private void OnDestroy()
    {
        npcGenerator.OnNPCsGenerated -= UpdateUI; // Unsubscribe from the event
    }
    void Update()
    {
        UpdateUI();
    }
/*     public void CreateNPCInfoRows()
    {
        // Clear existing rows
        foreach (Transform child in npcInfoGrid)
        {
            Destroy(child.gameObject);
            Debug.Log("Deleted temp rows");
        }

        // Create a new row for each NPC
        foreach (var npc in npcGenerator.npcList.Values)
        {
            Debug.Log($"Instantiating row for NPC: {npc.Name}");
            GameObject row = Instantiate(npcInfoRowPrefab, npcInfoGrid);
            FillRowInfo(row, npc);
        }
    } */

    private void FillRowInfo(GameObject row, NPC npc)
    {
        // Find text components in the row and set their values based on the NPC data
        TextMeshProUGUI[] textComponents = row.GetComponentsInChildren<TextMeshProUGUI>();
        Slider taskSlider =  row.transform.Find("TaskSlider").GetComponent<Slider>();
        foreach (TextMeshProUGUI textComponent in textComponents)
        {
            switch (textComponent.name)
            {
                case "NPCIdText":
                    textComponent.text = npc.NPCId.ToString();
                    break;
                case "NameText":
                    textComponent.text = npc.Name;
                    break;
                case "AgeText":
                    textComponent.text = npc.Age.ToString();
                    break;
                case "ProdText":
                    textComponent.text = npc.WorkDonePerIncrement.ToString();
                    switch (npc.WorkDonePerIncrement)
                        {
                            case <= 2.5f:
                                textComponent.color = Color.red;
                                break;
                            case <= 4f:
                                textComponent.color = new Color(1.0f, 0.64f, 0.0f); // Orange 
                                break;
                            case <= 7.5f:
                                textComponent.color = new Color(0.082f, 0.812f, 0.216f); // Dark Green
                                break;
                            case >7.5f: // Exceeds value expected
                                textComponent.color = Color.gray;
                                break;
                        }
                    break;
                case "SalaryText":
                    textComponent.text = $"$ {npc.Salary.ToString("N0")}";
                    break;
                case "WorkArrangementText":
                    textComponent.text = npc.CurrentWorkArrangement;
                    switch (npc.CurrentWorkArrangement)
                    {
                        case "Remote":
                            textComponent.color = Color.red;
                            break;
                        case "On-site":
                            textComponent.color = new Color(0.082f, 0.812f, 0.216f);
                            break;
                    }
                    break;
                case "MoodText":
                    textComponent.text = npc.Mood.ToString();
                    break;
                case "TaskProgressText":
                    textComponent.text = npc.TotalWorkDone.ToString();
                    break;
                default:
                    Debug.LogWarning("Unhandled text component: " + textComponent.name);
                    break;
            }
        }
        if (taskSlider != null)
            {
                taskSlider.value = npc.TotalWorkDone;
            }
    }
    private void UpdateUI()
    {
        // Loop through NPC list and update/add rows
        foreach (var npc in npcGenerator.npcList.Values)
        {
            if (npcRowDict.ContainsKey(npc.NPCId))
            {
                // Update existing row
                FillRowInfo(npcRowDict[npc.NPCId], npc);
            }
            else
            {
                // Create new row
                GameObject row = Instantiate(npcInfoRowPrefab, npcInfoGrid);
                npcRowDict[npc.NPCId] = row;
                FillRowInfo(row, npc);
            }
        }

        // Remove rows for NPCs that no longer exist
        var npcIds = new HashSet<int>(npcGenerator.npcList.Keys);
        var keysToRemove = new List<int>();

        foreach (var npcId in npcRowDict.Keys)
        {
            if (!npcIds.Contains(npcId))
            {
                Destroy(npcRowDict[npcId]);
                keysToRemove.Add(npcId);
            }
        }

        foreach (var key in keysToRemove)
        {
            npcRowDict.Remove(key);
        }
    }
}
