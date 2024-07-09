using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCGenerator : MonoBehaviour
{
    // Text Output Fields: Reference to game objects in MainUI or WorkerDetails
    public Text[] npcNameOutput = new Text[6]; // Name
    public Text npcSalaryOutput; // Salary
    public Text npcInfoOutput; // Whole info
    public Slider npcMoodSlider; //Mood slider game object reference
    public Text TaskCapacityOutput; // Current assigned maximum task value
    public Text WorkDoneOutput; // Current work completed
    public Text npcWorkDoneOutput; // Work currently completed

    // *USE AS NECESSARY*
    //public Text npcAgeOutput; // Age
    //public Text npcWorkEfficiencyOutput; // Work Efficiency
    //public Text npcMoodOutput; // Mood

    // Debug options
    public Button generateButton; // Debug Generate NPC Button


    // 3D Model reference
    public GameObject[] npcPrefabs; // Array of different 3D model prefabs
    public Transform[] spawnPoints; // Array of spawn points for NPCs


    private string[] firstNames = { "Alex", "Jordan", "Taylor", "Morgan", "Casey", "Riley", "Dakota", "Reese", "Skyler", "Quinn" };
    private string[] lastNames = { "Smith", "Johnson", "Brown", "Williams", "Jones", "Garcia", "Miller"};

    private void Start()
    {
        /* //Debug NPC Spawn
        if (spawnPoint == null)
        {
            Debug.LogWarning("Spawn point is not set. Creating a default spawn point.");
            //GameObject defaultSpawnPoint = new GameObject("DefaultSpawnPoint");
            //spawnPoint = defaultSpawnPoint.transform;
            //spawnPoint.position = new Vector3(0, 0, 0); // Adjust this position as needed
        }
        else {
            Debug.LogWarning("Spawn point has been set.");
        } */
        for (int i = 0; i < 6; i++)
        {
            NPC newNPC = GenerateRandomNPC();
            DisplayNPCInfo(newNPC, i); // Optionally display the first NPC's info
            SpawnNPCModel(newNPC, i);
        }
        generateButton.onClick.AddListener(OnGenerateButtonClick);

        // Ensure all NPC prefabs have a collider
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

    void OnNPCPrefabClicked(int index)
    {
        Debug.Log($"Model {index} clicked");
    }
    private void OnGenerateButtonClick()
    {
        for (int i = 0; i < 6; i++)
        {
            NPC newNPC = GenerateRandomNPC();
            DisplayNPCInfo(newNPC, i); // Optionally display the first NPC's info
            SpawnNPCModel(newNPC, i);
        }
    }
    

    public NPC GenerateRandomNPC()
    {
        NPC newNPC = new NPC
        {
            Name = firstNames[Random.Range(0, firstNames.Length)] + " " + lastNames[Random.Range(0, lastNames.Length)],
            Age = Random.Range(20, 61),
            WorkEfficiency = Mathf.Round(Random.Range(1f, 10f) * 100f) / 100f,
            Mood = Random.Range(-5, 6),
            TaskCapacity = Random.Range(1000, 2000),
        };

        newNPC.Salary = CalculateSalary(newNPC.Age, newNPC.WorkEfficiency);

        return newNPC;
        //return new NPC(name, age, leadershipStyle, techSkill, socialSkill, workEfficiency, socialPref, isNeurodivergent, isDisabled, inRelationship, mood);
    }

    private int CalculateSalary(int age, float workEfficiency)
    {
        //return Mathf.RoundToInt(Random.Range(6000f, 8001f) + (age * (workEfficiency * 10)));
        return Mathf.RoundToInt(Random.Range(6000f, 6001f) + (age * (workEfficiency * 10)));
    }

    public void DisplayNPCInfo(NPC npc, int index)
    {
        npcNameOutput[index].text = $"{npc.Name}";
        //npcAgeOutput.text = $"{npc.Age}";
        //npcWorkEfficiencyOutput.text = $"{npc.WorkEfficiency}";
        npcSalaryOutput.text = $"<color=green>{npc.Salary}</color> / month";
        //npcMoodOutput.text = $"{npc.Mood}";
        npcMoodSlider.value = npc.Mood;
        npcInfoOutput.text = $"Name: {npc.Name}\nAge: {npc.Age}\nWork Efficiency: {npc.WorkEfficiency}\nSalary: {npc.Salary}\nMood: {npc.Mood}\nCurrent task progress: {npc.WorkDone}\nCurrent workload: {npc.TaskCapacity}\n";
        //Debug.Log("Name: " + npc.Name + "\nAge: " + npc.Age);
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
        /* if (npcModel != null)
        {
            Debug.Log($"Spawned NPC Model at {spawnPoint.position}");
        }
        else
        {
            Debug.LogError("Failed to instantiate NPC Model.");
        } */
    }
}

public class NPC
{
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

    public override string ToString()
    {
        return $"Name: {Name}\nAge: {Age}\nWork Efficiency: {WorkEfficiency}\nSalary: {Salary}\nMood: {Mood}";
    }

    
}
public class NPCModel : MonoBehaviour
{
    private NPC npc;
    private NPCGenerator npcGenerator;

    public void Initialize(NPC npc, NPCGenerator generator)
    {
        this.npc = npc;
        this.npcGenerator = generator;
    }

    private void OnMouseDown()
    {
        // Find the index of this NPC model and display its info
        int index = System.Array.IndexOf(npcGenerator.npcPrefabs, this.gameObject);
        npcGenerator.DisplayNPCInfo(npc, index);
        Debug.Log($"{index} Model clicked.");
    }
}