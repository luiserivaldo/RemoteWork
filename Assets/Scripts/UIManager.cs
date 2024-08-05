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

    private void Start()
    {
        PopulateNPCInfoRows();
        Debug.Log("UI Manager started.");
    }
    void Update()
    {
        //PopulateNPCInfoRows();
    }
    public void PopulateNPCInfoRows()
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
            PopulateRow(row, npc);
        }
    }

    private void PopulateRow(GameObject row, NPC npc)
    {
        // Find text components in the row and set their values based on the NPC data
        TextMeshProUGUI[] textComponents = row.GetComponentsInChildren<TextMeshProUGUI>();

        foreach (TextMeshProUGUI textComponent in textComponents)
        {
            switch (textComponent.name)
            {
                case "NameText":
                    textComponent.text = npc.Name;
                    break;
                case "AgeText":
                    textComponent.text = npc.Age.ToString();
                    break;
                case "ProdText":
                    textComponent.text = npc.WorkDonePerIncrement.ToString();
                    break;
                case "SalaryText":
                    textComponent.text = npc.Salary.ToString();
                    break;
                case "WorkArrangementText":
                    textComponent.text = npc.CurrentWorkArrangement;
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
            Debug.Log("Spawned a row of NPC details.");
        }
    }
}
