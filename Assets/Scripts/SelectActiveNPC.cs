using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectActiveNPC : MonoBehaviour
{
    //public NPCGenerator npcGenerator;
    private NPC assignedNPC;

    void Start()
    {
        EnsureColliderExists();
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
        Debug.Log("Model clicked");
        if (assignedNPC == null) // Error check when NPC has no data or reference
        {
            Debug.LogError("Assigned NPC data is null.");
            return;
        }
        
        else // Output to the Unity Console when the model is clicked
        {
            Debug.Log($"NPC Clicked: ID={assignedNPC.NPCId}, Name={assignedNPC.Name}");
        }
    }
}
