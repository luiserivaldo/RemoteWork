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
    // Debug options
    public Button generateButton; // Debug Generate NPC Button

    // 3D Model reference
    public GameObject[] npcPrefabs; // Array of different 3D model prefabs
    public Transform[] spawnPoints; // Array of spawn points for NPCs

    private string[] firstNames = { "Alex", "Jordan", "Taylor", "Morgan", "Casey", "Riley", "Dakota", "Reese", "Skyler", "Quinn" };
    private string[] lastNames = { "Smith", "Johnson", "Brown", "Williams", "Jones", "Garcia", "Miller"};
    private static int npcGenCounter = 1; // Number and order of NPCs generated, starting ID = 1
    public Dictionary<int, NPC> npcList = new Dictionary<int, NPC>(); // Dictionary containing all generated NPCs

    private void Start()
    {
        for (int i = 0; i < 6; i++) // Initial starting no. of NPCs = 6
        {
            NPC newNPC = GenerateRandomNPC();
            npcList.Add(newNPC.NPCId, newNPC); // Add NPCs to dictionary
            SpawnNPCModel(newNPC, i);
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
        
    }
    void Update()
    {
         //UpdateWorkProgress();
    }

    /*// Debug Generate
    private void OnGenerateButtonClick()
    {
        NPC newNPC = GenerateRandomNPC();
        DisplayNPCInfo(newNPC, i); // Optionally display the first NPC's info
        //SpawnNPCModel(newNPC, i);
    } */
    
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
                    CurrentWorkArrangement = Random.value > 0.5f ? "On-site" : "Remote Working",
                    Mood = Random.Range(-5, 6),
                    WorkDonePerIncrement = 0f,
                    TotalWorkDone = 0f,
                    MaxTaskCapacity = 100f,
                };
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

}

public class NPC
{
    public int NPCId {get; set;}
    public bool IsSelected {get; set;} = false;
    public string Name { get; set; }
    public int Age { get; set; }
    public float WorkEfficiency { get; set; }
    public int Salary { get; set; }
    public string CurrentActivity { get; set;}
    public string CurrentWorkArrangement { get; set;}
    public float Mood { get; set; }
    public float WorkDonePerIncrement { get; set; } = 0f;
    public float TotalWorkDone { get; set;} = 0f;
    public float MaxTaskCapacity { get; set;} = 100f;


    // Expanded list
    /* public string LeadershipStyle { get; set; }
    public float TechSkill { get; set; }
    public float SocialSkill { get; set; }
    public string SocialPref { get; set; }
    public bool IsNeurodivergent { get; set; }
    public bool IsDisabled { get; set; }
    public bool InRelationship { get; set; } */

    public override string ToString() // Output the class elements as a string
    {
        return $"isSelected: {IsSelected}\nID: {NPCId}\nName: {Name}\nAge: {Age}\nWork Efficiency: {WorkEfficiency}\nSalary: {Salary}\nMood: {Mood}\n";
    }
}