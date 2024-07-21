using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private static int npcGenCounter = 0; // Number and order of NPCs generated
    public Dictionary<int, NPC> npcList = new Dictionary<int, NPC>(); // Dictionary containing all generated NPCs

    private void Start()
    {
         //Debug NPC Spawn
        /* if (spawnPoint == null)
        {
            Debug.LogWarning("No spawn point detected.");
            //GameObject defaultSpawnPoint = new GameObject("DefaultSpawnPoint");
            //spawnPoint = defaultSpawnPoint.transform;
            //spawnPoint.position = new Vector3(0, 0, 0); // Adjust this position as needed
        } */

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
        NPC newNPC = new NPC
        {
            NPCId = npcGenCounter++, // Sequential ID based on the order of creation, default 0
            Name = firstNames[Random.Range(0, firstNames.Length)] + " " + lastNames[Random.Range(0, lastNames.Length)],
            Age = Random.Range(20, 61),
            WorkEfficiency = Mathf.Round(Random.Range(1f, 10f) * 100f) / 100f,
            Mood = Random.Range(-5, 6),
            TaskCapacity = Random.Range(1000, 2000)
        };
        newNPC.Salary = CalculateSalary(newNPC.Age, newNPC.WorkEfficiency);

        return newNPC;
        //return new NPC(name, age, leadershipStyle, techSkill, socialSkill, workEfficiency, socialPref, isNeurodivergent, isDisabled, inRelationship, mood);
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
    public int Mood { get; set; }
    public int TaskCapacity { get; set; }
    public int WorkDone { get; set; }

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
        return $"ID: {NPCId}\nName: {Name}\nAge: {Age}\nWork Efficiency: {WorkEfficiency}\nSalary: {Salary}\nMood: {Mood}\nTask Capacity: {TaskCapacity}";
    }

    
}
/* public class NPCModel : MonoBehaviour
{
    private NPC npc;
    private NPCGenerator npcGenerator;

    public void Initialize(NPC npc, NPCGenerator generator)
    {
        this.npc = npc;
        this.npcGenerator = generator;
    }
} */