using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NPCGenerator : MonoBehaviour
{
    // 3D Model reference
    [Header("Instantiation references")]
    public int maxNPCsGenerated = 4;
    public GameObject[] npcPrefabs; // Array of different 3D model prefabs
    public Transform[] spawnPoints; // Array of spawn points for NPCs

    private string[] firstNames = { "Alex", "Jordan", "Taylor", "Morgan", "Casey", "Riley", "Dakota", "Reese", "Skyler", "Quinn" };
    private string[] lastNames = { "Smith", "Johnson", "Brown", "Williams", "Jones", "Garcia", "Miller"};
    private static int npcGenCounter = 1; // Number and order of NPCs generated, starting ID = 1
    public Dictionary<int, NPC> npcList = new Dictionary<int, NPC>(); // Dictionary containing all generated NPCs
    private List<GameObject> instantiatedNPCs = new List<GameObject>(); // List to store references to instantiated NPCs
    public Action OnNPCsGenerated; // Event to notify when NPCs are generated
    public event Action OnNPCListUpdated; // Event to notify when NPCs details are updated
 

    private void Start()
    {
        npcGenCounter = 1;
        // Clear all past NPCs
        foreach (var npc in npcList.Values)
        {
            npcList.Clear();
        }
        
        for (int i = 0; i < maxNPCsGenerated; i++) // Set max number of NPCs depending on scene/office size
        {
            NPC newNPC = GenerateRandomNPC();

            if (newNPC == null)
            {
                Debug.LogError("GenerateRandomNPC returned null. Skipping this NPC.");
                continue; // Skip this iteration if NPC creation failed
            }
            npcList.Add(newNPC.NPCId, newNPC); // Add NPCs to dictionary
            if (newNPC.CurrentWorkArrangement == "On-site")
            {
                SpawnNPCModel(newNPC, i);
            }
        }

        //generateButton.onClick.AddListener(OnGenerateButtonClick); // Debug Generate

        //* Ensure all NPC prefabs have a collider
        foreach (var prefab in npcPrefabs)
        {
            if (prefab.GetComponent<Collider>() == null)
            {
                prefab.AddComponent<MeshCollider>();
            }
        }
        NotifyNPCListUpdated();
    }
    void Update()
    {

    }
    
    public NPC GenerateRandomNPC()
    {
        if (spawnPoints.Length == 0 || npcPrefabs.Length == 0)
        {
            Debug.LogError("No spawn points or NPC prefabs available.");
            return null;  // No spawn points or prefabs to process
        }
        foreach (var spawnPoint in spawnPoints)
        {
            if (npcGenCounter <= spawnPoints.Length)
            {
                NPC newNPC = new NPC
                {
                    NPCId = npcGenCounter++, // Sequential ID based on the order of creation, default 1
                    Name = firstNames[Random.Range(0, firstNames.Length)] + " " + lastNames[Random.Range(0, lastNames.Length)],
                    Age = Random.Range(20, 61),
                    WorkEfficiency = Mathf.Round(Random.Range(1f, 5f) * 100f) / 100f,
                    Salary = 0,
                    CurrentActivity = "Working",
                    CurrentWorkArrangement = Random.value > 0.2f ? "On-site" : "Remote",
                    Mood = 0,
                    WorkDonePerIncrement = 0f,
                    TotalWorkDone = 0f,
                    MaxTaskCapacity = 100f,
                    numOfTasksCompleted = 0,
                    SocialPref = Random.value > 0.5f ? "Introvert" : "Extrovert",
                    IsDisabled = Random.value > 0.9f ? "Disabled" : "Able-bodied",
                    TechSkill = Random.Range(1, 11)
                };
                if (newNPC.CurrentWorkArrangement == "On-site")
                    {
                        newNPC.Mood -= 2;
                    }
                else if (newNPC.CurrentWorkArrangement == "Remote")
                    {
                        newNPC.Mood += 2;
                    }
                newNPC.Salary = CalculateSalary(newNPC.Age, newNPC.WorkEfficiency);
                return newNPC;
            }
        }
        // if GenCounter exceeds spawnpoints
        return null;
    }
    private void SpawnNPCModel(NPC npc, int index)
    {
        if (npcPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogError("NPC Prefabs or Spawn Points are not assigned.");
            return;
        }

        GameObject npcPrefab = npcPrefabs[Random.Range(0, npcPrefabs.Length)];
        Transform spawnPoint = spawnPoints[index % spawnPoints.Length];
        //Debug.Log($"Selected Prefab: {npcPrefab.name} for NPC {index + 1}");

        GameObject npcModel = Instantiate(npcPrefab, spawnPoint.position, spawnPoint.rotation);
        instantiatedNPCs.Add(npcModel); // Add to the list of instantiated NPCs
        npcModel.GetComponent<SelectActiveNPC>().InitializeNPC(npc);
    } 
    private int CalculateSalary(int age, float workEfficiency)
    {
        //return Mathf.RoundToInt(Random.Range(6000f, 6501f) + (age * (workEfficiency * 10)));
        float baseSalary = Random.Range(6000f, 6501f);
        float additionalPay = age * (workEfficiency * 10);
        float totalSalary = baseSalary + additionalPay;

        int roundedSalary = (int)Math.Ceiling(totalSalary / 10.0) * 10; // Round up to the nearest decimal point

        return roundedSalary;
    }
    public void GenerateNewNPCs()
    {
        // Clear current NPCs
        foreach (var npcModel in instantiatedNPCs)
        {
            Destroy(npcModel); // Destroy NPC model
        }
        npcList.Clear(); // Clear the dictionary

        // Reset NPC counter
        npcGenCounter = 1;

        // Generate new NPCs
        for (int i = 0; i < maxNPCsGenerated; i++)
        {
            NPC newNPC = GenerateRandomNPC();
            npcList.Add(newNPC.NPCId, newNPC);
            if (newNPC.CurrentWorkArrangement == "On-site")
            {
                SpawnNPCModel(newNPC, i);
            }
        }
        // Notify that NPCs have been generated
        OnNPCsGenerated?.Invoke();
        NotifyNPCListUpdated();
    }

    private void NotifyNPCListUpdated()
    {
        if (OnNPCListUpdated != null)
        {
            OnNPCListUpdated();
        }
    }

    private void AddNPC(NPC newNPC)
    {
        npcList.Add(newNPC.NPCId, newNPC);
        NotifyNPCListUpdated();
    }

    private void RemoveNPC(int npcId)
    {
        npcList.Remove(npcId);
        NotifyNPCListUpdated();
    }
}

public class NPC
{
    public int NPCId {get; set;}
    public bool IsSelected {get; set;} = false;
    public string Name { get; set; }
    public int Age { get; set; }
    public float WorkEfficiency { get; set; }
    public float WorkEfficiencyBonus {get; set;}
    public int Salary { get; set; }
    public string CurrentActivity { get; set;}
    public string CurrentWorkArrangement { get; set;}
    public float Mood { get; set; }
    public float MoodBonus {get; set;}
    public float WorkDonePerIncrement { get; set; } = 0f;
    public float TotalWorkDone { get; set;} = 0f;
    public float MaxTaskCapacity { get; set;} = 100f;
    public int numOfTasksCompleted {get; set;}
    public string SocialPref { get; set; }
    public string IsDisabled { get; set; }
    public float TechSkill { get; set; }


    // Expanded list
    /* public string LeadershipStyle { get; set; }
    
    public float SocialSkill { get; set; }
    public bool IsNeurodivergent { get; set; }
    public bool InRelationship { get; set; } */

    public override string ToString() // Output the class elements as a string
    {
        return $"isSelected: {IsSelected}\nID: {NPCId}\nName: {Name}\nAge: {Age}\nWork Efficiency: {WorkEfficiency}\nSalary: {Salary}\nMood: {Mood}\n";
    }
}