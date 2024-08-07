using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectActiveNPC : MonoBehaviour
{
    private NPCGenerator npcGenerator;
    public UIManager uiManager; // Reference to the UIManager
    private NPC assignedNPC;

    void Start()
    {
        EnsureColliderExists();
        npcGenerator = FindObjectOfType<NPCGenerator>();
        uiManager = FindObjectOfType<UIManager>();
        if (npcGenerator == null)
        {
            Debug.LogError("NPCGenerator not found.");
            return;
        }
    }
    public void InitializeNPC(NPC npc)
    {
        assignedNPC = npc;  // Store the NPC data in the prefab
    }

    private void EnsureColliderExists()
    {
        Collider collider = GetComponent<Collider>();
        if (collider == null)
        {
            // Add a BoxCollider if no collider is found
            gameObject.AddComponent<BoxCollider>();
            Debug.Log("No collider found. BoxCollider has been added automatically.");
        }
    }
    void OnMouseDown()
    {   
        //Debug.Log("Model clicked");
        // Deselect any previously selected NPC
        foreach (var npcEntry in npcGenerator.npcList)
        {
            if (npcEntry.Value.IsSelected && npcEntry.Value != assignedNPC)
            {
                npcEntry.Value.IsSelected = false;
            }
        }

        // Select this NPC
        assignedNPC.IsSelected = true;
        //Debug.Log($"NPC Clicked: ID={assignedNPC.NPCId}, Name={assignedNPC.Name}, Selected={assignedNPC.IsSelected}");

        // Reset RemoteNPC Dropdown menu
        uiManager.ResetDropdownToPlaceholder();

        // Notify OutputManager to update the display
        FindObjectOfType<OutputManager>().DisplaySpecificNPC(assignedNPC);
    }
}
